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
        {    DateTime dat = DateTime.Now.Date;
			var staffs = _context.Staffs.ToList();			
			ViewBag.Branches = _context.Branches.Count();
			ViewBag.Departments = _context.Departments.Count();
			ViewBag.Users = _context.Users.Count();
			ViewBag.staff = _context.Staffs.Count();
			ViewBag.Present = _context.StaffCheckInAndOutReports.Where(c => c.Date == dat && c.status !="Absent" ).GroupBy(c=>c.StaffId).Count();
			ViewBag.LateReport = _context.StaffCheckInAndOutReports.Where(c => c.status == "Arrived Late" && c.Date == dat).Count();
			ViewBag.TodayAbsent = _context.StaffCheckInAndOutReports.Where(c => c.status == "Absent" && c.Date == dat).Count();

			ViewBag.AllBranches = _context.Branches.ToList();
			var branches = _context.Branches.ToList();


			foreach (var branch in branches)
			{
				ViewBag.TotalBranchEmployee = _context.StaffCheckInAndOutReports.Where(c => c.Date == dat && c.Branch == branch.Id).GroupBy(c=>c.Branch).Count();
			
				//Ila nadhan c ya kwel ngoja nijaribu kitu.. Wait... So far data zako inabidi ziweje?Pale ilivyoleta ni sawa ila je data ni za kweli? Hapa tugroup by Branch ila sasa... Utest kwa data za kweli tuone kama iko sawa. Then utanipa feedback. tuendelee na git now...wait kutest ni second data zipo

			}

			return View();
        }

    }
}
