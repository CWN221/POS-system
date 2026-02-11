using Pos_system.Models;
using Pos_system.Database;
using MySql.Data.MySqlClient;
using Pos_system.logs;


namespace Pos_system.Services
{
    public class UserRepository
    {


        /*
         * 
         * Method to update user information (Admin only)
         * 
        */
        public async Task<string> UpdateUser(UserModel userInfo)
        {
            using var conn = DBConnection.GetConnection();

            if (conn == null) return "Database could not be reached!";

            string query = "UPDATE users SET Username = @Username, Role = @Role, IsActive = @IsActive WHERE id = @Id";

            try
            {
                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Username", userInfo.Username);
                cmd.Parameters.AddWithValue("@Role", userInfo.Role);
                cmd.Parameters.AddWithValue("@IsActive", userInfo.IsActive);

                return await cmd.ExecuteNonQueryAsync() > 0 ? "User information updated." : "Error updating user information.";
            }
            catch (MySqlException ex)
            {
                Logger.Log("Error updating user information: " + ex.Message);
                return "Error updating user information";
            }
        }


        // method to delete a user (Admin only)
        public async Task<string> DeleteUser(int userId)
        {
            using var conn = DBConnection.GetConnection();

            if (conn == null) return "Database could not be reached!";
            string query = "DELETE FROM users WHERE Id=@Id";

            try
            {
                using var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("Id", userId);
                await cmd.ExecuteNonQueryAsync();

                return "User deleted successfully.";
            }
            catch (MySqlException ex)
            {
                Logger.Log("Error deleting user: " + ex.Message);
                return "Error deleting user.";
            }
        }

    }
}
