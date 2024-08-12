﻿using LeaveMate.Others;

namespace LeaveMate.Models
{
    public class EmployeeInfo : DBModifiers
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int DesignationID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Contact { get; set; }
    }
}