using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FingerPrint.Controllers
{
    public class DailyReportController : Controller
    {
		private FingerContext _context;

		public DailyReportController()
		{
			_context = new FingerContext();
		}
		// GET: TodaysReport
		public ActionResult Index()
		{
			if (Session["UserRoles"] != null)
			{
				var dt = DateTime.Now.Date;
			   var today = _context.StaffCheckInAndOutReports.Where(c => c.Date == dt).ToList();
			   return View(today);
			}
			else
				return RedirectToAction("Login", "User");
		}
		public ActionResult Create()
		{
			if (Session["UserRoles"] != null)
			{
				ViewBag.branches = new SelectList(_context.Branches, "id", "name");
			ViewBag.departments = new SelectList(_context.Departments, "id", "name");
				
				return View();
			}
			else
				return RedirectToAction("Login", "User");
		}

		[HttpPost]
		public ActionResult DailyReport()
		{
			if (Session["UserRoles"] != null)
			{
				var id = 1;
				var branch = "";
				if (Session["UserRoles"].ToString() == "Admin")
				{
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
						//c.TotalStaffHours,
						c.DepartmentStr,
						c.BranchStr,
					}).ToList();

					return Json(new { data = formatedBranchDetails });
				}
				else
				branch = Session["UserBranch"].ToString();
				var branchData = _context.StaffCheckInAndOutReports.Where(c => c.Branch.ToString() == branch ).ToList();

				var formatedBranchDetail = branchData.Select(c => new
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
						//c.TotalStaffHours,
						c.DepartmentStr,
						c.BranchStr,
					}).ToList();

					return Json(new { data = formatedBranchDetail });


			}
			else
				return RedirectToAction("Login", "User");
		}

	}
}
