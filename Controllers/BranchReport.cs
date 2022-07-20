using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FingerPrint.Controllers
{
    public class BranchReportController : Controller
    {
        private FingerContext _context;

        public BranchReportController()
        {
            _context = new FingerContext();
        }
        // GET: User
		
        public ActionResult Index()
        {
			if (Session["UserRoles"] != null)
			{
				var b = Session["UserId"];
				var a = _context.StaffCheckInAndOutReports.ToList();
				return View(a);
			}
			else
				return RedirectToAction("Login", "User");
		}
        public ActionResult Create()
        {
			if (Session["UserRoles"] != null)
			{
				ViewBag.branches = new SelectList(_context.Branches, "id", "name");
				return View();
			}
			else
				return RedirectToAction("Login", "User");

		}

        [HttpPost]
        public JsonResult Create(int ? branc)
        {
			var branch = "";
			var id = 1;
			if (Session["UserRoles"] != null)
			{
				if(Session["UserRoles"].ToString() == "Admin") {
				    var branchDetails = _context.StaffCheckInAndOutReports.ToList();
					
					var formatedBranchDetails = branchDetails.Select(c => new
					{
						sno = id++,
						Date = (c.Date == null) ? null : c.Date.ToString("dd/MM/yyyy"),
						TimeIn = (c.TimeIn == null) ? null : Convert.ToDateTime(c.TimeIn).ToString("HH:mm"),
						TimeOut = (c.TimeOut == null) ? null : Convert.ToDateTime(c.TimeOut).ToString("HH:mm"),
						TotalStaffHours = (c.TotalStaffHours == null) ? null : c.TotalStaffHours.ToString(),
						c.firstName,
						c.LastName,
						c.StaffId,
						c.status,
						c.OverTimeHours,
						c.PercentageHours,
						c.DepartmentStr,
						c.BranchStr,
					}).ToList();

					return Json(new { data = formatedBranchDetails });
				}
				else
				    branch = Session["UserBranch"].ToString();
					var branchData = _context.StaffCheckInAndOutReports.Where(c=>c.Branch.ToString() == branch).ToList();
					var formatedBranchData = branchData.Select(c => new
					{
						sno = id++,
						Date = (c.Date == null) ? null : c.Date.ToString("dd/MM/yyyy"),
						TimeIn = (c.TimeIn == null) ? null : Convert.ToDateTime(c.TimeIn).ToString("HH:mm"),
						TimeOut = (c.TimeOut == null) ? null : Convert.ToDateTime(c.TimeOut).ToString("HH:mm"),
						TotalStaffHours = (c.TotalStaffHours == null) ? null : c.TotalStaffHours.ToString(),
						c.firstName,
						c.LastName,
						c.StaffId,
						c.status,
						c.OverTimeHours,
						c.PercentageHours,
						c.DepartmentStr,
						c.BranchStr,
					}).ToList();

					return Json(new { data = formatedBranchData });

			}
			else
				return Json("Login", "User");

		}

		[HttpPost]
		public JsonResult Branch (string UserId)
		{
				
				var branch_details = _context.Users.Where(c => c.Id.ToString() == UserId).SingleOrDefault();
				ViewBag.BranchDetails = _context.StaffCheckInAndOutReports.Where(c => c.Branch == branch_details.BranchId).ToList();
				return Json(ViewBag.BranchDetails, JsonRequestBehavior.AllowGet);	
		}


    }
}
