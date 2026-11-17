namespace Pos_system.Views.Dialogs
{
    public partial class StockAdjustmentDialog : Form
    {
        private readonly int _currentQty;

        public int Delta { get; private set; }
        public string Reason { get; private set; } = "";

        public StockAdjustmentDialog(string productName, int currentQty)
        {
            InitializeComponent();

            _currentQty = currentQty;
            productValue.Text = productName;
            currentValue.Text = currentQty.ToString();
            UpdateNewQty();

            BuildPresets();

            ActiveControl = deltaBox;
            KeyPreview = true;
            KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter && deltaBox.Focused)
                {
                    e.SuppressKeyPress = true;
                    reasonBox.Focus();
                }
                else if (e.KeyCode == Keys.Escape)
                {
                    DialogResult = DialogResult.Cancel;
                    Close();
                }
            };
        }

        private void BuildPresets()
        {
            var presets = new[]
            {
                ("Damaged", "Damaged in storage"),
                ("Lost / Stolen", "Lost or stolen"),
                ("Stock Take", "Physical stock count correction"),
                ("Expired", "Expired - removed from stock"),
                ("Received", "New stock received"),
                ("Correction", "Entry error correction"),
            };

            foreach (var (label, reason) in presets)
            {
                var btn = new Button
                {
                    Text = label,
                    AutoSize = false,
                    Size = new Size(115, 32),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.White,
                    ForeColor = Color.FromArgb(51, 65, 85),
                    Font = new Font("Segoe UI", 9F),
                    Cursor = Cursors.Hand,
                    Margin = new Padding(0, 0, 6, 0)
                };
                btn.FlatAppearance.BorderColor = Color.FromArgb(203, 213, 225);
                btn.Click += (_, _) => reasonBox.Text = reason;

                presetFlow.Controls.Add(btn);
            }
        }

        private void DeltaBox_TextChanged(object? sender, EventArgs e)
        {
            UpdateNewQty();
        }

        private void UpdateNewQty()
        {
            if (int.TryParse(deltaBox.Text, out int delta))
            {
                int newQty = _currentQty + delta;
                newQtyValue.Text = newQty.ToString();
                newQtyValue.ForeColor = newQty < 0 ? Color.FromArgb(220, 38, 38) : Color.FromArgb(22, 163, 74);
            }
            else
            {
                newQtyValue.Text = "—";
                newQtyValue.ForeColor = Color.FromArgb(148, 163, 184);
            }
        }

        private void SaveBtn_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(deltaBox.Text, out int delta))
            {
                MessageBox.Show("Enter a valid adjustment amount.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                deltaBox.Focus();
                return;
            }

            if (delta == 0)
            {
                MessageBox.Show("Adjustment cannot be zero.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                deltaBox.Focus();
                return;
            }

            if (_currentQty + delta < 0)
            {
                MessageBox.Show($"This would result in negative stock ({_currentQty + delta}).", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                deltaBox.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(reasonBox.Text))
            {
                MessageBox.Show("A reason is required for every stock adjustment.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                reasonBox.Focus();
                return;
            }

            Delta = delta;
            Reason = reasonBox.Text.Trim();
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