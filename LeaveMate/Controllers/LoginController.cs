using LeaveMate.Models;
using LeaveMate.Others;
using LeaveMate.SP;
using Microsoft.AspNetCore.Mvc;

namespace LeaveMate.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authenticate(string username, string password)
        {
            bool IsVerified = false, IsUserExist = false;
            UserSP spUser = new UserSP();
            EmployeeSP spEmployee = new EmployeeSP();
            
            IsUserExist = spUser.IsUserExist(username);

            if(IsUserExist)
            {
                string HashedPassword = spUser.GetUserPasswordByUsername(username);
                IsVerified = PublicFunctions.VerifyPassword(password, HashedPassword);
                if(IsVerified)
                {
                    UserInfo infoUser = spUser.GetUserByUsername(username);
                    EmployeeInfo infoEmployee = spEmployee.GetEmployeeByUsername(username);
                    Session.UserID = infoUser.UserID;
                    Session.DesignationID = infoEmployee.DesignationID;
                }
            }
            return Json(new { isAuthenticated = IsVerified, isUserExists = IsUserExist });
        }

        public IActionResult Register()
        {
            ViewBag.Designations = FixedValues.Designations;
            ViewBag.ManagerID = FixedValues.Designations.Keys.First();
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser(string employeeName, int designationID, DateTime dateOfBirth, string address, string contact, string username, string password)
        {
            try
            {
                EmployeeSP spEmployee = new EmployeeSP();
                EmployeeInfo infoEmployee = new EmployeeInfo()
                {
                    EmployeeName = employeeName,
                    DesignationID = designationID,
                    DateOfBirth = dateOfBirth,
                    Address = address,
                    Contact = contact
                };
                int EmployeeID = spEmployee.Save(infoEmployee);

                UserSP spUser = new UserSP();
                UserInfo infoUser = new UserInfo()
                {
                    EmployeeID = EmployeeID,
                    Username = username,
                    Password = PublicFunctions.HashPassword(password)
                };
                int UserID = spUser.Save(infoUser);
                return Json(new { success = true, message = "Registration successful." });
            }
            catch (Exception ex)
            {
                ErrorLog.Print(ex.ToString());
                return Json(new { success = false, message = "An error occurred during registration." });
            }
        }
    }
}