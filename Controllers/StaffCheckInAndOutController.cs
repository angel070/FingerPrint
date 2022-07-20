using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FingerPrint.Controllers
{
    public class StaffCheckInAndOutController : Controller
    {
        private FingerContext _context;

        public StaffCheckInAndOutController()
        {
            _context = new FingerContext();
        }
        // GET: StaffCheckInAndOut
        public ActionResult Index()
        {
			var branch="";
			if (Session["UserRoles"] != null)
			{
				if (Session["UserRoles"].ToString() == "Admin")
				{
					var staff = _context.StaffCheckInAndOutReports.ToList();
					return View(staff);
				}
				else
					 branch=Session["UserBranch"].ToString();
				     var staffBranch = _context.StaffCheckInAndOutReports.Where(c=>c.Branch.ToString() == branch).ToList();
				     return View(staffBranch);
			}
			else
				return RedirectToAction("Login", "User");
		}

		public ActionResult Present()
		{
			if (Session["UserRoles"] != null)
			{
				DateTime dt = DateTime.Now.Date;
				var PresentEmployees = _context.StaffCheckInAndOutReports.Where(c => c.Date == dt && c.status != "Absent").ToList();
				return View(PresentEmployees);
			}
			else
				return RedirectToAction("Login", "User");
		}
		public ActionResult LateEmployees()
		{
			if (Session["UserRoles"] != null)
			{
				DateTime dt = DateTime.Now.Date;
			var PresentEmployees = _context.StaffCheckInAndOutReports.Where(c => c.Date == dt && c.status == "Arrived Late").ToList();
			return View(PresentEmployees);
		    }
		    else
				return RedirectToAction("Login", "User");
	    }

		public ActionResult AbsentEmployees()
		{
			if (Session["UserRoles"] != null)
			{
				var staffs = _context.Staffs.ToList();
				DateTime dt = DateTime.Now.Date;

				foreach(var item in staffs)
				{
					ViewBag.AbsentEmployees = _context.StaffCheckInAndOutReports.Where(c =>  c.firstName != item.FirstName ).ToList();
					return View(ViewBag.AbsentEmployees);
				}
				return View(ViewBag.AbsentEmployees);
			}
			else
				return RedirectToAction("Login", "User");
		}

        public ActionResult Delete(int? id)
        {
			if (Session["UserRoles"] != null)
			{
				if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            var user = _context.StaffCheckInAndOutReports.SingleOrDefault(c => c.Id == id);

            if (user == null)
                return HttpNotFound();
            _context.StaffCheckInAndOutReports.Remove(user);
            _context.SaveChanges();

            return RedirectToAction("Index");
		    }
		      else
				return RedirectToAction("Login", "User");

	    }
    }
}
