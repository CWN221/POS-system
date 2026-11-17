using Npgsql;
using Pos_system.Core;
using Pos_system.Database;
using Pos_system.Services;
using System.Globalization;

namespace Pos_system.Views.Dialogs
{
    public partial class TenderDialog : Form
    {
        private readonly decimal _totalDue;
        private readonly CultureInfo _culture = CultureInfo.GetCultureInfo("en-KE");

        public string SelectedMethod { get; private set; } = "Cash";
        public decimal AmountTendered { get; private set; }
        public decimal ChangeGiven { get; private set; }
        public string? Reference { get; private set; }
        public string? MpesaCheckoutRequestId { get; private set; }
        public string? MpesaPhone { get; private set; }
        public bool MpesaInitiated { get; private set; }

        public TenderDialog(decimal totalDue, string defaultMethod = "Cash")
        {
            InitializeComponent();

            _totalDue = totalDue;
            totalValue.Text = _totalDue.ToString("C", _culture);

            methodCombo.Items.AddRange(new object[] { "Cash", "Mpesa", "Card" });
            methodCombo.SelectedItem = defaultMethod;
            if (methodCombo.SelectedIndex < 0) methodCombo.SelectedIndex = 0;

            methodCombo.SelectedIndexChanged += (_, _) => OnMethodChanged();

            ActiveControl = tenderedBox;
            OnMethodChanged();
        }

        private void OnMethodChanged()
        {
            var method = methodCombo.SelectedItem as string ?? "Cash";
            bool isCash = method == "Cash";
            bool isMpesa = method == "Mpesa";
            bool isCard = method == "Card";

            tenderedCaption.Visible = isCash;
            tenderedBox.Visible = isCash;
            changeCaption.Visible = isCash;
            changeValue.Visible = isCash;

            phoneCaption.Visible = isMpesa;
            phoneBox.Visible = isMpesa;
            stkBtn.Visible = isMpesa;
            statusLbl.Visible = isMpesa;

            refCaption.Visible = !isCash;
            refBox.Visible = !isCash;

            if (isCash)
            {
                tenderedBox.Text = _totalDue.ToString("F2");
                UpdateChange();
            }
            else
            {
                changeValue.Text = "KSh 0.00";
            }

            if (isMpesa)
            {
                refCaption.Text = "Reference";
                refBox.PlaceholderText = "Leave blank until payment confirms";
                if (!MpesaInitiated) statusLbl.Text = "";
            }
            else if (isCard)
            {
                refCaption.Text = "Card Reference";
                refBox.PlaceholderText = "";
            }
            else
            {
                refCaption.Text = "Reference (M-Pesa / Card)";
                refBox.PlaceholderText = "Optional";
            }
        }

        private async void StkBtn_Click(object? sender, EventArgs e)
        {
            var rawPhone = phoneBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(rawPhone))
            {
                MessageBox.Show("Enter the customer's phone number.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                phoneBox.Focus();
                return;
            }

            try
            {
                stkBtn.Enabled = false;
                stkBtn.Text = "Sending...";
                statusLbl.Text = "Initiating STK Push...";

                var svc = AppServices.GetRequiredService<MpesaService>();
                var result = await svc.InitiateStkPushAsync(rawPhone, _totalDue, $"POS-{DateTime.Now:HHmmss}", "POS Sale");

                if (result.ResponseCode != "0")
                {
                    MessageBox.Show($"Safaricom rejected the request: {result.ResponseDescription}", "STK Push Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    stkBtn.Text = "Send STK Push";
                    stkBtn.Enabled = true;
                    statusLbl.Text = "";
                    return;
                }

                MpesaCheckoutRequestId = result.CheckoutRequestId;
                MpesaPhone = MpesaService.NormalizePhone(rawPhone);
                MpesaInitiated = true;

                statusLbl.Text = "Waiting for customer to enter PIN...";
                stkBtn.Text = "Waiting...";

                await WaitForPaymentResultAsync(result.CheckoutRequestId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"STK Push error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                stkBtn.Enabled = true;
                stkBtn.Text = "Send STK Push";
                statusLbl.Text = "";
            }
        }

        private async Task WaitForPaymentResultAsync(string checkoutRequestId)
        {
            var deadline = DateTime.UtcNow.AddSeconds(90);

            while (DateTime.UtcNow < deadline)
            {
                await Task.Delay(5000);

                try
                {
                    using var conn = DBConnection.GetConnection();
                    const string sql = @"SELECT status, result_code, result_desc
                                         FROM mpesa_stk_push_log
                                         WHERE checkout_request_id = @id";
                    using var cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", checkoutRequestId);
                    using var reader = await cmd.ExecuteReaderAsync();

                    if (!await reader.ReadAsync()) continue;
                    var status = reader.GetString(0);
                    var resultDesc = reader.IsDBNull(2) ? status : reader.GetString(2);

                    if (status == "Completed")
                    {
                        statusLbl.Text = "Payment received. Click Confirm to finish.";
                        statusLbl.ForeColor = Color.FromArgb(22, 163, 74);
                        stkBtn.Text = "Paid";
                        refBox.Text = MpesaCheckoutRequestId ?? "";
                        return;
                    }

                    if (status == "Failed" || status == "Cancelled" || status == "Timeout")
                    {
                        MessageBox.Show($"Payment {status}: {resultDesc}\n\nYou may retry or enter the reference manually.", "M-Pesa Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        statusLbl.Text = "";
                        stkBtn.Enabled = true;
                        stkBtn.Text = "Send STK Push";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Polling error: {ex.Message}");
                }
            }

            MessageBox.Show("No payment confirmation received within 90 seconds. You may retry or enter a reference manually.",
                "Timeout", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            statusLbl.Text = "";
            stkBtn.Enabled = true;
            stkBtn.Text = "Send STK Push";
        }

        private void TenderedBox_TextChanged(object? sender, EventArgs e)
        {
            UpdateChange();
        }

        private void UpdateChange()
        {
            if (!decimal.TryParse(tenderedBox.Text, NumberStyles.Any, _culture, out var tendered))
            {
                changeCaption.Visible = true;
                changeValue.Visible = true;
                changeValue.Text = "—";
                return;
            }

            decimal change = tendered - _totalDue;
            changeCaption.Visible = true;
            changeValue.Visible = true;
            changeValue.Text = change.ToString("C", _culture);
            changeValue.ForeColor = change < 0 ? Color.FromArgb(220, 38, 38) : Color.FromArgb(37, 99, 235);
        }

        private void ConfirmBtn_Click(object? sender, EventArgs e)
        {
            var method = (methodCombo.SelectedItem as string) ?? "Cash";

            if (method == "Cash")
            {
                if (!decimal.TryParse(tenderedBox.Text, NumberStyles.Any, _culture, out var tendered))
                {
                    MessageBox.Show("Enter a valid amount tendered.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tenderedBox.Focus();
                    return;
                }

                if (tendered < _totalDue)
                {
                    MessageBox.Show($"Amount tendered ({tendered:N2}) is less than total due ({_totalDue:N2}).", "Insufficient Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    tenderedBox.Focus();
                    return;
                }

                AmountTendered = tendered;
                ChangeGiven = tendered - _totalDue;
                Reference = null;
            }
            else
            {
                var reference = refBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(reference) && !MpesaInitiated)
                {
                    var confirm = MessageBox.Show(
                        $"You have not entered a {method} reference and no STK Push was initiated. Continue anyway?",
                        "Missing Reference", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirm != DialogResult.Yes)
                    {
                        refBox.Focus();
                        return;
                    }
                }

                AmountTendered = _totalDue;
                ChangeGiven = 0;
                Reference = string.IsNullOrWhiteSpace(reference) ? null : reference;
            }

            SelectedMethod = method;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void CancelBtn_Click(object? sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}