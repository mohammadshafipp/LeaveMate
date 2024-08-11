namespace LeaveMate.Others
{
    public class DBModifiers
    {
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsModified { get; set; }
        public int ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public bool IsCancelled { get; set; }
    }
}