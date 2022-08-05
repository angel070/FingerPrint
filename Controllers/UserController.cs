using FingerPrint.Models;
using FingerPrint.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Security;
using System.Drawing;
using System.Globalization;

namespace FingerPrint.Controllers
{
    public class UserController : Controller
    {
        private FingerContext _context;

        public UserController()
        {
            _context = new FingerContext();
        }
		// GET: User
		public ActionResult Index()
		{
			if (Session["userRoles"] != null)
			{
				var user = _context.Users.Include(c => c.UserRoles).Include(u => u.Branch).ToList();
				return View(user);

			}
			else
				return RedirectToAction("login", "user");
		

	    }

		public ActionResult Create()
		{
			if (Session["userRoles"] != null)
			{
				ViewBag.Users = new SelectList(_context.Roles, "Id", "Name");
				ViewBag.Branches = new SelectList(_context.Branches, "Id", "Name");
				return View();

			}
			else
				return RedirectToAction("login", "user");
		}

	[HttpPost]
        public ActionResult Create(User _user)
        {
			if (Session["userRoles"] != null)
			{
				if (!ModelState.IsValid)
					return View("Create", _user);

			     _user.Username = _user.Email;
			     _user.Password = _user.Email;
			      _user.IsActive = true;
				_context.Users.Add(_user);
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			else
				return RedirectToAction("login", "user");

		}

		public ActionResult Edit(int? id)
		{
			if (Session["UserRoles"] != null)
			{
				ViewBag.Users = new SelectList(_context.Roles, "Id", "Name");
				ViewBag.Branches = new SelectList(_context.Branches, "Id", "Name");
				if (id == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				var user = _context.Users.SingleOrDefault(c => c.Id == id);

				if (user == null)
					return HttpNotFound();

				//_context.Entry(roles).State = System.Data.Entity.EntityState.Modified;
				//_context.SaveChanges();

				return View("Edit", user);
			}
			else
				return RedirectToAction("Login", "User");

		}

		[HttpPost]
		public ActionResult Edit(User user)
		{
			if (Session["UserRoles"] != null)
			{
				if (user == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				if (!ModelState.IsValid)
				{
					return View("Edit", user);
				}

				var user_data = _context.Users.Find(user.Id);

				if (user_data == null)
					return HttpNotFound();

				user_data.FirstName = user.FirstName;
				user_data.LastName = user.LastName;
				user_data.Username = user.Email;
				user_data.UserRolesId = user.UserRolesId;
				user_data.Password = user_data.Password;
				user_data.Email = user.Email;
				user_data.BranchId = user.BranchId; 
				user_data.PhoneNumber = user.PhoneNumber;
				user_data.IsActive = true;
				_context.Entry(user_data).State = System.Data.Entity.EntityState.Modified;
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			else
				return RedirectToAction("Login", "User");
		}


		[HttpPost]
		public JsonResult checkStaffFingerPrint(string StaffId)
		{
			var staffDetails = _context.Staffs.Where(c => c.Staff_id == StaffId).SingleOrDefault();
			TempData["Id"] = StaffId;
			return Json(staffDetails);
		}

		public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel _login)
        {
            try
            {
                var login = _context.Users.Where(n => n.Username == _login.Username && n.Password == _login.Password ).FirstOrDefault();
                if (login == null)
                {
                    ModelState.AddModelError("Username", "User name or Password is incorrect");
                    return View("Login", _login);
                }
                else
                {
                    Session["UserId"] = login.Id;

                    /* Session["UserName"] = login.Username;*/
                    var roles = login.UserRolesId;
                    var FoundRole = _context.Roles.Find(roles);
                    Session["UserName"] = login.Username;
                    Session["UserRoles"] = FoundRole.Name;
                    Session["UserBranch"] = login.BranchId;

					var branchName = _context.Branches.Where(c => c.Id == login.BranchId).SingleOrDefault();
					Session["UserBranchName"] = branchName.Name;
					Session.Timeout = 10;

                    return RedirectToAction("Index", "DashBoard");
                }
                /*if (login == null)
                  {

                      return View("Login", _login);
                }
                  else
                      Session["Username"] = login.Username;
                      return RedirectToAction("Index","Country");*/
            }
            catch (Exception ex)
            {              
                return Content(ex.StackTrace);
            }
        }
      
        public ActionResult Logout()
        {
            /* int UserId = (int)Session["UserId"];*/
            Session.RemoveAll();
            Session.Remove("UserRoles");
            Session.Abandon();
            return RedirectToAction("CheckId");
        }

        public ActionResult CheckId()
        {

            return View();

        }
        [HttpPost]
        public ActionResult CheckId(CheckIdViewModel check)
        {
            
            var FoundStaff = _context.Staffs.Where(c => c.Staff_id == check.Userid).FirstOrDefault();

            if (FoundStaff == null)
            {
				TempData["WrongId"] = "Wrong Id";
				return View();
            }
            if(FoundStaff.Fingerprint == null)
			{
				TempData["EmptyFinger"] = "Finger Print is not registered";

				return View();
			}
            ViewBag.Id = FoundStaff.Id.ToString();
            ViewBag.FirstName = FoundStaff.FirstName.ToString();
            ViewBag.LastName = FoundStaff.LastName.ToString();
            ViewBag.FingerPrint = FoundStaff.Fingerprint.ToString();
            TempData["Id"] = ViewBag.Id;
            TempData["FirstName"] = ViewBag.FirstName;
            TempData["LastName"] = ViewBag.LastName;
            TempData["FingerPrint"] = ViewBag.FingerPrint;
            return RedirectToAction("MatchFinger" );
           
        }


        public ActionResult MatchFinger()
        {
            var staffDetails = _context.Staffs.ToList();
          
            return View(staffDetails);
        }

        [HttpPost]
        public ActionResult MatchFinger(string id)
        {
			   
               StaffCheckInAndOutReport staff = new StaffCheckInAndOutReport();
			   WorkingHours hours = new WorkingHours();
			   int id2 = 4;
			//Check todays day
				DateTime time = DateTime.Now;
			    var todayDate = time.Date; 
				var todaysDay = time.DayOfWeek;
				var workingHours = _context.WorkingHourse.Find(id2);
                var StaffFound = _context.Staffs.Where(c=>c.Staff_id ==id ).SingleOrDefault();
                var FullName = $"{StaffFound.FirstName} {StaffFound.LastName}";
			
			   var holiday = _context.Holdays.Where(c => c.DateFrom == todayDate).SingleOrDefault();
                var staffLogout = _context.StaffCheckInAndOutReports.Where(c=>c.StaffId == StaffFound.Staff_id && c.TimeOut == null && c.Date == todayDate).SingleOrDefault();

			    var ActualTime = _context.TimeInAndOuts.Where(c => c.Days == todaysDay && c.DepartmentId == StaffFound.DepartmentId && c.BranchId == StaffFound.BranchId).SingleOrDefault();

			if (staffLogout == null)
            {
			    var dept = _context.Departments.Where(c => c.Id ==StaffFound.DepartmentId).SingleOrDefault();
				var branch = _context.Branches.Where(c => c.Id == StaffFound.BranchId).SingleOrDefault();
                staff.StaffId = StaffFound.Staff_id;
                staff.firstName = StaffFound.FirstName;
                staff.LastName = StaffFound.LastName;
                staff.TimeIn = time;
                staff.Date = todayDate;
                staff.Branch = StaffFound.BranchId;
                staff.Department = StaffFound.DepartmentId;
				staff.DepartmentStr = dept.Name;
				staff.BranchStr = branch.Name;

				//Go fetch todaysDay into db for timeIn 
			
				if(ActualTime != null)
				{
					var HourIn = ActualTime.TimeIn.Hour;
					var MinuteIn = ActualTime.TimeIn.Minute;
					var TodaysDate = time.ToShortDateString();

					
					var TodaysDateTime = DateTime.Parse($"{TodaysDate} {HourIn}:{MinuteIn}:00");

					if (time <= TodaysDateTime && holiday == null)
					{
						staff.status = "Arrived on time";
						_context.StaffCheckInAndOutReports.Add(staff);
						_context.SaveChanges();
						return Json("You have successfull Check In");

					}

					if(time >= TodaysDateTime && holiday == null)
					{
						staff.status = "Arrived Late";
						_context.StaffCheckInAndOutReports.Add(staff);
						_context.SaveChanges();
						return Json("You have successfull Check In");
					}

					if(holiday != null)
					{
						staff.status = holiday.Name + "Holiday";
						_context.StaffCheckInAndOutReports.Add(staff);
						_context.SaveChanges();
						return Json("You have successfull Check In");
					}
				}
				else
					staff.status = "Department Time is not Defined";
				    _context.StaffCheckInAndOutReports.Add(staff);
				     _context.SaveChanges();
				    return Json("You have successfull Check In");
			}

            else
            {

                staffLogout.TimeOut = DateTime.Now;
				if(ActualTime != null)
				{
					DateTime startTime = DateTime.Parse(staffLogout.TimeIn.ToString());
					DateTime endTime = DateTime.Parse(staffLogout.TimeOut.ToString());
					TimeSpan ts = endTime.Subtract(startTime);
					staffLogout.TotalStaffHours = ts;
					var PercentageHours = ((decimal)ts.TotalSeconds / (9 * 3600)) * 100;
					staffLogout.PercentageHours = PercentageHours;
					var workingTime = _context.TimeInAndOuts.Where(c => c.Days == todaysDay && c.DepartmentId == StaffFound.DepartmentId && c.BranchId == StaffFound.BranchId).SingleOrDefault();
					decimal OverTime;
					
						 OverTime = (decimal)ts.TotalHours - workingTime.WorkingHours;
					
						


					if(holiday == null)
					{
						if (OverTime > 0)
						{
							staffLogout.OverTimeHours = OverTime;
						}
						else
						{
							staffLogout.OverTimeHours = 0;
						}
					}else
						staffLogout.OverTimeHours = PercentageHours;

				}
             
                _context.Entry(staffLogout).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                return Json("you have successfull Check out");
                //TempData["Message"] = "You have successfull check Out";
                
            }
           // return RedirectToAction("Index","User");
        }

        public JsonResult Delete(int? id)
        {
			try
		    {

			
            bool result = false;

            var users = _context.Users.SingleOrDefault(c => c.Id == id);

            if (users != null)
            {
                _context.Users.Remove(users);
                _context.SaveChanges();

                result = true;
            }
			return Json(new { status = result, message = "successfully deleted" });
		    }
			catch(Exception e)
			{
				return Json(new { status = false, message = "This User is already used" });
			}	
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();

            base.Dispose(disposing);

        }


    }
}
