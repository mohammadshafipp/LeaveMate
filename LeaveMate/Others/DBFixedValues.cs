namespace LeaveMate.Others
{
    public class DBFixedValues
    {
        Dictionary<string, string> LeaveTypes = new Dictionary<string, string>
        {
            { "AnnualLeave", "Annual Leave" },
            { "SickLeave", "Sick Leave" }
        };

        List<string> UserTypes = new List<string>
        {
            { "Manager"},
            { "Employee"}
        };

        List<string> Status = new List<string>
        {
            { "Pending"},
            { "Rejected"},
            { "Approved" }
        };
    }
}