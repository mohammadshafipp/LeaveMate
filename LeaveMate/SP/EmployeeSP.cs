using LeaveMate.Others;
using System.Data.SqlClient;
using System.Data;
using LeaveMate.Models;

namespace LeaveMate.SP
{
    public class EmployeeSP
    {
        public int Save(EmployeeInfo infoEmployee)
        {
            int EmployeeID = 0;
            try
            {
                using (SqlConnection connection = new SqlConnection(FixedValues.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_SaveEmployee", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@EmployeeName", infoEmployee.EmployeeName);
                        command.Parameters.AddWithValue("@DesignationID", infoEmployee.DesignationID);
                        command.Parameters.AddWithValue("@DateOfBirth", infoEmployee.DateOfBirth);
                        command.Parameters.AddWithValue("@Address", infoEmployee.Address);
                        command.Parameters.AddWithValue("@Contact", infoEmployee.Contact);

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
                        EmployeeID = int.Parse(resultTable.Rows[0][0].ToString());
                    }
                }
            }
            catch (Exception Ex)
            {
                ErrorLog.Print(Ex.ToString());
            }
            return EmployeeID;
        }

        public EmployeeInfo GetEmployeeByUsername(string Username)
        {
            EmployeeInfo infoEmployee = new EmployeeInfo();
            try
            {
                using (SqlConnection connection = new SqlConnection(FixedValues.DBConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("sp_GetEmployeeByUsername", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Username", Username);

                        DataTable resultTable = new DataTable();
                        using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                        {
                            connection.Open();
                            adapter.Fill(resultTable);
                        }

                        infoEmployee = new EmployeeInfo()
                        {
                            EmployeeID = int.Parse(resultTable.Rows[0][0].ToString()),
                            EmployeeName = resultTable.Rows[0][1].ToString(),
                            DesignationID = int.Parse(resultTable.Rows[0][2].ToString()),
                            DateOfBirth = DateTime.Parse(resultTable.Rows[0][3].ToString()),
                            Address = resultTable.Rows[0][4].ToString(),
                            Contact = resultTable.Rows[0][5].ToString()
                        };
                    }
                }
            }
            catch (Exception Ex)
            {
                ErrorLog.Print(Ex.ToString());
            }
            return infoEmployee;
        }
    }
}