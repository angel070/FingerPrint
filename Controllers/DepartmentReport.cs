using FingerPrint.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FingerPrint.Controllers
{
	public class DepartmentReportController : Controller
    {
        private FingerContext _context;

        public DepartmentReportController()
        {
            _context = new FingerContext();
        }

        public ActionResult Index()
        {
			if (Session["UserRoles"] != null)
			{
				return View();
			}
			else
				return RedirectToAction("Login", "User");
		}

        public ActionResult Create()
        {
			if (Session["UserRoles"] != null)
			{
			    var	branch = Session["UserBranch"].ToString();
				ViewBag.branches = new SelectList(_context.Branches, "id", "name");
				if(Session["UserRoles"].ToString() != "Admin"){
				 ViewBag.Departments = new SelectList(_context.Departments.Where(c => c.BranchId.ToString() == branch), "Id", "Name");
				}else
				ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
				return View();
			}
			else
				return RedirectToAction("Login", "User");
		}

		[HttpPost]
		public JsonResult DepartmentReport()
		{
			var branch = "";
			var id = 1;

			if (Session["UserRoles"] != null)
			{
				if (Session["UserRoles"].ToString() == "Admin")
				{
					var DepartmentDetails = _context.StaffCheckInAndOutReports.ToList();
					var FormatedDepartmentDetails = DepartmentDetails.Select(c => new
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

					return Json(new { data = FormatedDepartmentDetails });
				}
				else
					branch = Session["UserBranch"].ToString();
				var DepartmentData = _context.StaffCheckInAndOutReports.Where(c=>c.Branch.ToString() == branch).ToList();
				var FormatedDepartmentData = DepartmentData.Select(c => new
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

				return Json(new { data = FormatedDepartmentData });

			}
			else
				return Json("Login", "User");


		}
	}
}
