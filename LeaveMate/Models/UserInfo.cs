﻿using LeaveMate.Others;

namespace LeaveMate.Models
{
    public class UserInfo : DBModifiers
    {
        public int UserID { get; set; }
        public int EmployeeID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}