using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FingerPrint.Controllers
{
    public class LateReportController : Controller
    {
        private FingerContext _context;

        public LateReportController()
        {
            _context = new FingerContext();
        }
        // GET: LateReport
   
		public ActionResult Latestaffs()
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

			}else
				return RedirectToAction("Login", "User");
		}
		[HttpPost]
        public JsonResult LateStaff()
        {
			var branch = "";
			var id = 1;
			if (Session["UserRoles"] != null)
			{
				if (Session["UserRoles"].ToString() == "Admin")
				{
					var LateStaffs = _context.StaffCheckInAndOutReports.Where(c => c.status == "Arrived Late").ToList();
					var formatedLatestaffDetails = LateStaffs.Select(c => new
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
					return Json(new { data = formatedLatestaffDetails });
				}
				else
				branch= Session["UserBranch"].ToString();
				var LateStaff = _context.StaffCheckInAndOutReports.Where(c => c.status == "Arrived Late" && c.Branch.ToString() == branch ).ToList();
				var formatedLatestaffData = LateStaff.Select(c => new
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
				return Json(new { data = formatedLatestaffData });

			}
			else
				return Json("Login", "User");
		}
    }
}
