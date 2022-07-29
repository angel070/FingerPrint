using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FingerPrint.Controllers
{
	public class DashBoardController : Controller
	{
		private FingerContext _context;

		public DashBoardController()
		{
			_context = new FingerContext();
		}
		// GET: DashBoard			
		public ActionResult Index()
		{
			var logs = new HashSet<BranchLog>();
			if (Session["userRoles"] != null)
			{
				DateTime dat = DateTime.Now.Date;
				var staffs = _context.Staffs.ToList();
				if (Session["userRoles"].ToString() == "Admin")
				{
					ViewBag.Branches = _context.Branches.Count();
					ViewBag.Departments = _context.Departments.Count();
					ViewBag.Users = _context.Users.Count();
					ViewBag.staff = _context.Staffs.Count();
					ViewBag.Present = _context.StaffCheckInAndOutReports.Where(c => c.Date == dat && c.status != "Absent").GroupBy(c => c.StaffId).Count();
					ViewBag.LateReport = _context.StaffCheckInAndOutReports.Where(c => c.status == "Arrived Late" && c.Date == dat).GroupBy(c=>c.StaffId).Count();
					ViewBag.TodayAbsent = _context.StaffCheckInAndOutReports.Where(c => c.status == "Absent" && c.Date == dat).Count();
					ViewBag.AllBranches = _context.Branches.ToList();
				}


				var branchId = Session["UserBranch"].ToString();
				var Allstaff = _context.Staffs.Where(c=>c.BranchId.ToString() == branchId).ToList();
				if (Session["userRoles"].ToString() == "Branch")
				{
					ViewBag.Departments = _context.Departments.Where(c=>c.BranchId.ToString() == branchId).Count();
					ViewBag.Users = _context.Users.Where(c => c.BranchId.ToString() == branchId).Count();
					ViewBag.staff = _context.Staffs.Where(c=> c.BranchId.ToString() == branchId).Count();
					ViewBag.Present = _context.StaffCheckInAndOutReports.Where(c => c.Date == dat && c.status != "Absent" && c.Branch.ToString() == branchId).GroupBy(c => c.StaffId).Count();
					ViewBag.LateReport = _context.StaffCheckInAndOutReports.Where(c => c.status == "Arrived Late" && c.Date == dat && c.Branch.ToString() == branchId).GroupBy(c=>c.StaffId).Count();
					ViewBag.TodayAbsent = _context.StaffCheckInAndOutReports.Where(c => c.Date == dat && c.status == "Absent" ).Count();
					

					//foreach (var staff in Allstaff)
					//{
					//	var staffLeave = _context.Leaves.Where(c => c.StaffId == staff.Staff_id && c.DateFrom <= dat && c.DateTo >= dat).SingleOrDefault();

					//	if(staffLeave == null)
					//	{
					//		var TotalTodayAbsent = _context.StaffCheckInAndOutReports.Where(c => c.Date == dat && c.StaffId != staff.Staff_id).Count();
					//		logs.Add(new BranchLog
					//		{
					//			Id = staff.Id,
					//			StaffAbsent = TotalTodayAbsent
					//		});
					//	}
					//}

					
					ViewBag.AllBranches = _context.Branches.ToList();
					
				}

				var branches = _context.Branches.ToList();

				//create a list of branches with counts or names
				// this collection will hold all the branches names and the staff counts
				// then you will just set them to the view bag and loop through on the view part
				
				if (Session["userRoles"].ToString() == "Admin")
				{
					foreach (var branch in branches)
					{
						var totalPresent = _context.StaffCheckInAndOutReports.Where(c => c.Date == dat && c.Branch == branch.Id && c.status != "Absent").GroupBy(c => c.StaffId).Count();
						var totalAbsent = _context.StaffCheckInAndOutReports.Where(c => c.Date == dat && c.Branch == branch.Id && c.status == "Absent").GroupBy(c => c.StaffId).Count();
						logs.Add(new BranchLog
						{
							Id = branch.Id,
							Branch = branch.Name,
							StaffPresent = totalPresent,
							StaffAbsent = totalAbsent
						});
					}
				}

				var departments = _context.Departments.Where(c => c.BranchId.ToString() == branchId).ToList();
				if (Session["userRoles"].ToString() == "Branch")
				{
					foreach (var department in departments)
					{
						var totalPresent = _context.StaffCheckInAndOutReports.Where(c => c.Date == dat && c.Department == department.Id && c.status != "Absent" && c.Branch.ToString() == branchId).GroupBy(c => c.StaffId).Count();
						var totalAbsent = _context.StaffCheckInAndOutReports.Where(c => c.Date == dat && c.Department == department.Id && c.status == "Absent" && c.Branch.ToString() == branchId).GroupBy(c => c.StaffId).Count();
						logs.Add(new BranchLog
						{
							Id = department.Id,
							Branch = department.Name,
							StaffPresent = totalPresent,
							StaffAbsent = totalAbsent
						});
					}

				}

				//Kama kuna mtu alilogin zaid ya mara moja kwa siku inacount mara zote so hapa nadhani inabdi kugroup by Id
				ViewBag.TotalBranchEmployee = logs;

				return View();
			}
			  else
			 return RedirectToAction("login", "user");
	    }
    }
}
