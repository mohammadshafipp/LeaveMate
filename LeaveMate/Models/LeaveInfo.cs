using LeaveMate.Others;

namespace LeaveMate.Models
{
    public class LeaveInfo : DBModifiers
    {
        public int LeaveID { get; set; }
        public int EmployeeID { get; set; }
        public int LeaveTypeID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestedDate { get; set; }
        public string Reason { get; set; }
        public bool IsActionPerformed { get; set; }
        public DateTime ActionPerformedDate { get; set; }
        public string Status { get; set; }
        public string ActionDescription { get; set; }
    }
}