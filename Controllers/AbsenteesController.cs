using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FingerPrint.Controllers
{
    public class AbsenteesController : Controller
    {
		private FingerContext _context;

		public AbsenteesController()
		{
			_context = new FingerContext();
		}
		// GET: Absentees
		public ActionResult Index()
        {
            return View();
        }

		public ActionResult Create()
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
		public JsonResult Absentees()
		{
			var branch = "";
			var id = 1;
			if (Session["UserRoles"] != null)
			{
				if (Session["UserRoles"].ToString() == "Admin")
				{
					StaffCheckInAndOutReport staffReport = new StaffCheckInAndOutReport();
					var staff = _context.Staffs.ToList();
					var AbsentStaff = _context.StaffCheckInAndOutReports.Where(c => c.status == "Absent").ToList();
					var formatedOverTimestaffDetails = AbsentStaff.Select(c => new
					{
						sno = id++,
						Date = (c.Date == null) ? null : c.Date.ToString("dd/MM/yyyy"),
						c.firstName,
						c.LastName,
						c.StaffId,
						c.status,
						c.DepartmentStr,
						c.BranchStr,
					}).ToList();
					return Json(new { data = formatedOverTimestaffDetails });
				}
				else
					branch = Session["UserBranch"].ToString();
			    	var AbsentStaffs = _context.StaffCheckInAndOutReports.Where(c => c.status == "Absent" && c.Branch.ToString() == branch).ToList();
				var formatedOverTimestaffData = AbsentStaffs.Select(c => new
				{
					sno = id++,
					Date = (c.Date == null) ? null : c.Date.ToString("dd/MM/yyyy"),
					c.firstName,
					c.LastName,
					c.StaffId,
					c.status,
					c.DepartmentStr,
					c.BranchStr,
				}).ToList();
				return Json(new { data = formatedOverTimestaffData });
			}
			else
				return Json("Login", "User");

		}
		
		public ActionResult TodayAbsent()
		{
			if (Session["UserRoles"] != null)
			{
				DateTime dat = DateTime.Now.Date;
			   ViewBag.TodayAbsent = _context.StaffCheckInAndOutReports.Where(c => c.status == "Absent" && c.Date == dat).ToList();
			  return View(ViewBag.TodayAbsent);
			}
			else
				return Json("Login", "User");
		}

    }
}
