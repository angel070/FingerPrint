using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FingerPrint.Controllers
{
   
    public class OverTimeHoursController : Controller
    {
        private FingerContext _context;

        public OverTimeHoursController()
        {
            _context = new FingerContext()
               ;
        }
		// GET: OverTimeHours
		public ActionResult Index()
		{
			if (Session["UserRoles"] != null)
			{
				var branch = Session["UserBranch"].ToString();
				ViewBag.branches = new SelectList(_context.Branches, "Id", "Name");
				if (Session["UserRoles"].ToString() != "Admin")
				{
					ViewBag.Departments = new SelectList(_context.Departments.Where(c => c.BranchId.ToString() == branch), "Id", "Name");
				}
				else
					ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
				return View();

			}
			else
				return RedirectToAction("Login", "User");
		}

		[HttpPost]
		public JsonResult OverTimeHours()
		{
			var branch = "";
			var id = 1;
			if (Session["UserRoles"] != null)
			{
				if (Session["UserRoles"].ToString() == "Admin")
				{
					var OverTimeStaffs = _context.StaffCheckInAndOutReports.Where(c => c.OverTimeHours > 0).ToList();

					var formatedOverTimestaffDetails = OverTimeStaffs.Select(c => new
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
					return Json(new { data = formatedOverTimestaffDetails });
				}
				else
				branch = Session["UserBranch"].ToString();
				var OverTimeStaff = _context.StaffCheckInAndOutReports.Where(c => c.OverTimeHours > 0 && c.Branch.ToString() == branch).ToList();

				var formatedOverTimestaffDetail = OverTimeStaff.Select(c => new
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
				return Json(new { data = formatedOverTimestaffDetail });
			}
		
			else
				return Json("Login", "User");
	    }
    }
}
