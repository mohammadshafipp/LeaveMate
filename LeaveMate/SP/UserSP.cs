using LeaveMate.Models;
using LeaveMate.Others;
using System.Data;
using System.Data.SqlClient;

namespace LeaveMate.SP
{
    public class UserSP
    {
        public bool IsUserExist(string username)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(FixedValues.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_CheckUserExists", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", username);

                        connection.Open();
                        int userExists = (int)command.ExecuteScalar();
                        return userExists == 1;
                    }
                }
            }
            catch(Exception Ex)
            {
                ErrorLog.Print(Ex.ToString());
            }
            return false;
        }

        public string GetUserPasswordByUsername(string username)
        {
            string HashedPassword = "";
            try
            {
                using (SqlConnection connection = new SqlConnection(FixedValues.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetUserPasswordByUsername", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", username);

                        connection.Open();
                        HashedPassword = (string)command.ExecuteScalar();
                    }
                }
            }
            catch (Exception Ex)
            {
                ErrorLog.Print(Ex.ToString());
            }
            return HashedPassword;
        }

        public int Save(UserInfo infoUser)
        {
            int UserID = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(FixedValues.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_SaveUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmployeeID", infoUser.EmployeeID);
                        command.Parameters.AddWithValue("@Username", infoUser.Username);
                        command.Parameters.AddWithValue("@Password", infoUser.Password);

                        command.Parameters.AddWithValue("@CreatedBy", Session.UserID);
                        command.Parameters.AddWithValue("@CreatedOn", DateTime.Now);
                        command.Parameters.AddWithValue("@IsModified", false);
                        command.Parameters.AddWithValue("@ModifiedBy", Session.UserID);
                        command.Parameters.AddWithValue("@ModifiedOn", DateTime.Now);
                        command.Parameters.AddWithValue("@IsCancelled", false);

                        DataTable resultTable = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            connection.Open();
                            adapter.Fill(resultTable);
                        }
                        UserID = int.Parse(resultTable.Rows[0][0].ToString());
                    }
                }
            }
            catch (Exception Ex)
            {
                ErrorLog.Print(Ex.ToString());
            }
            return UserID;
        }

        public UserInfo GetUserByUsername(string Username)
        {
            UserInfo infoUser= new UserInfo();
            try
            {
                using (SqlConnection connection = new SqlConnection(FixedValues.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetUserByUsername", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", Username);

                        DataTable resultTable = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            connection.Open();
                            adapter.Fill(resultTable);
                        }

                        infoUser = new UserInfo()
                        {
                            UserID = int.Parse(resultTable.Rows[0][0].ToString()),
                            EmployeeID = int.Parse(resultTable.Rows[0][1].ToString()),
                            Username = resultTable.Rows[0][2].ToString(),
                            Password = resultTable.Rows[0][3].ToString()
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                ErrorLog.Print(Ex.ToString());
            }
            return infoUser;
        }
    }
}