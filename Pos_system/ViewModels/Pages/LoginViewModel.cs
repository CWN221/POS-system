using Pos_system.Core;
using Pos_system.logs;
using Pos_system.Models;
using Pos_system.Services;
using AppContext = Pos_system.Core.AppContext;

namespace Pos_system.ViewModels
{
    public class LoginViewModel
    {
        private readonly Authorization _authorization = AppServices.GetRequiredService<Authorization>();

        public required string Username { get; set; }
        public required string Password { get; set; }
        public UserRole Role { get; set; }
        public int BranchId { get; set; }

        public async Task UserSignup()
        {
            try
            {
                if (!Enum.IsDefined(typeof(UserRole), Role))
                {
                    MessageBox.Show("Invalid role selected.");
                    return;
                }

                if (BranchId <= 0)
                {
                    MessageBox.Show("Please select a valid branch.");
                    return;
                }

                var passwordCheck = PasswordValidator.Validate(Password);
                if (!passwordCheck.Success)
                {
                    MessageBox.Show(passwordCheck.Message, "Password Policy", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = await _authorization.Signup(Username, Password, Role, BranchId);
                MessageBox.Show(result, "User Registration",
                    MessageBoxButtons.OK,
                    result.Contains("Successful") ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error during signup: " + ex.Message);
            }
        }

        public async Task<bool> UserLogin()
        {
            try
            {
                var user = await _authorization.Login(Username, Password);

                if (user == null)
                    return false;

                SessionManager.Login(user);
                var branchService = AppServices.GetRequiredService<StoreSettingsService>();

                if (user.Role == UserRole.Admin)
                {
                    var branches = await branchService.GetAllBranches();
                    AppContext.AllowedBranches = branches.Select(b => b.Id).ToList();
                    if (branches.Any())
                        AppContext.SetBranch(branches.First());
                }
                else
                {
                    var branch = (await branchService.GetAllBranches()).FirstOrDefault(b => b.Id == user.BranchId);
                    if (branch == null)
                    {
                        MessageBox.Show("Assigned branch not found.");
                        SessionManager.Logout();
                        return false;
                    }

                    AppContext.AllowedBranches = [user.BranchId];
                    AppContext.SetBranch(branch);
                }

                await AppServices.GetRequiredService<AuditService>()
                    .LogAsync("USER_LOGIN", "user", user.Id.ToString());

                return true;
            }
            catch (Exception ex)
            {
                Logger.Log("Error during login: " + ex.Message);
                MessageBox.Show("Error during login: " + ex.Message);
                return false;
            }
        }
    }
}
