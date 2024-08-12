namespace LeaveMate.Others
{
    public class FixedValues
    {
        public static Dictionary<int, string> LeaveTypes = new Dictionary<int, string>
        {
            { 1, "AnnualLeave" },
            { 2, "SickLeave" }
        };

        public static Dictionary<int, string> Designations = new Dictionary<int, string>
        {
            { 1, "Manager"},
            { 2, "Employee"}
        };

        public Dictionary<int, string> LeaveStatus = new Dictionary<int, string>
        {
            { 1, "Pending"},
            { 2, "Rejected"},
            { 3, "Approved" }
        };

        public static readonly string DBConnectionString = "Server=DESKTOP-140903A\\SQLEXPRESS;Database=DB_LeaveMate;User Id=LeaveMate;Password=Admin@123;";
        public static readonly string ErrorLogFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"LeaveMate","Error.log");
    }
}