using FingerPrint.Models;
using FingerPrint.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FingerPrint.Controllers
{
    public class DateRangeController : Controller
    {
        private FingerContext _context;

        public DateRangeController()
        {
            _context = new FingerContext();
        }
        // GET: DateRange
        public ActionResult Index()
        {
			if (Session["UserRoles"] != null)
			{
				var StaffDetails = _context.StaffCheckInAndOutReports.ToList();
				return View(StaffDetails);
			}
			else
				return RedirectToAction("Login", "User");
		}
        public ActionResult DateRange()
        {
			if (Session["UserRoles"] != null)
			{
				ViewBag.branches = new SelectList(_context.Branches, "id", "name");
				ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
				return View();
			}
			else
				return RedirectToAction("Login", "User");
		}

        [HttpPost]
        public JsonResult IndividualReport()
        {
			var branch = "";
			var id = 1;

		if (Session["UserRoles"] != null)
		{
				if (Session["UserRoles"].ToString() == "Admin")
				{
					var IndividualStaffDetails = _context.StaffCheckInAndOutReports.ToList();
					var formatedIndividualStaffDetails = IndividualStaffDetails.Select(c => new
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
					return Json(new { data = formatedIndividualStaffDetails });
				}
				else
				branch = Session["UserBranch"].ToString();
				var IndividualStaffData = _context.StaffCheckInAndOutReports.Where(c => c.Branch.ToString() == branch).ToList();
				var formatedIndividualStaffData = IndividualStaffData.Select(c => new
				{
					sno =id++,
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
				return Json(new { data = formatedIndividualStaffData });
			}
			else
				return Json("Login", "User");
		}
    }
}


