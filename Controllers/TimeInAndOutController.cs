using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Security;
using System.Drawing;
using System.Globalization;

namespace FingerPrint.Controllers
{
    public class TimeInAndOutController : Controller
    {
        private FingerContext _context;

        public TimeInAndOutController()
        {
            _context = new FingerContext();
        }
		// GET: TimeInAndOut
		public ActionResult Index()
		{
			if (Session["UserRoles"] != null)
			{
				var Time = _context.TimeInAndOuts.Include(c => c.Branch).Include(c=>c.Department).ToList();
			    return View(Time);
		    }
			else
				return RedirectToAction("Login", "User");
		}

        public ActionResult Create()
        {
			if (Session["UserRoles"] != null)
			{
				ViewBag.Branches = new SelectList(_context.Branches, "Id", "Name");
				ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
				return View();
			}
			else
				return RedirectToAction("Login", "User");
		}

        [HttpPost]
        public ActionResult Create(TimeInAndOut _time)
        {
			var timeIn = DateTime.Parse(_time.TimeIn.ToString());
			var timeOut = DateTime.Parse(_time.TimeOut.ToString());
			var diff = timeOut - timeIn;
			var hours = diff.TotalMinutes / 60;
			if (Session["UserRoles"] != null)
			{
				if (_time.Id > 0)
				{
					_context.Entry(_time).State = System.Data.Entity.EntityState.Modified;
				}
				var AddedTime = _context.TimeInAndOuts.Where(c=>c.Days == _time.Days && c.BranchId == _time.BranchId && c.DepartmentId == _time.DepartmentId).SingleOrDefault();
				if( AddedTime != null)
				{
					TempData["AddedTime"] = "This Is already Exists";
					return RedirectToAction("Create");
				}
				_time.WorkingHours =(decimal) hours;
				_time.CreatedDate = DateTime.Now.Date;
				_context.TimeInAndOuts.Add(_time);
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			else
				return RedirectToAction("Login", "User");
		}

		public ActionResult Delete(int ? id)
        {

			if (Session["UserRoles"] != null)
			{
				if (id == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				var time = _context.TimeInAndOuts.SingleOrDefault(c => c.Id == id);

				if (time == null)
					return HttpNotFound();
				_context.TimeInAndOuts.Remove(time);
				_context.SaveChanges();
				TempData["Time"] = "Successfully deleted";
				return RedirectToAction("Index");
			}
			else
				return RedirectToAction("Login", "User");
		}

		public ActionResult Edit(int? id)
		{
			ViewBag.Branches = new SelectList(_context.Branches, "Id", "Name");
			ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
			if (Session["UserRoles"] != null)
			{
				if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				var timeinandout = _context.TimeInAndOuts.Where(c => c.Id == id).SingleOrDefault();
				if (timeinandout == null)
					return HttpNotFound();


				return View("Edit", timeinandout);
			}
			else
				return RedirectToAction("Login", "User");

		}

		[HttpPost]
		public ActionResult Edit(TimeInAndOut time)
		{
			
			DateTime dat = DateTime.Now.Date;
			if (Session["UserRoles"] != null)
			{
				if (time == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				if (!ModelState.IsValid)
				{
					return View("Edit", time);
				}

				var time_data = _context.TimeInAndOuts.Find(time.Id);
				var validateday = _context.TimeInAndOuts.Where(c => c.Days == time.Days);
				if (validateday == null)
				{
					return View("edit", time);
				}
				ViewBag.Branches = new SelectList(_context.Branches, "Id", "Name");
				ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");

				if (time_data == null)
					return HttpNotFound();

				var timeIn = DateTime.Parse(time.TimeIn.ToString());
				var timeOut = DateTime.Parse(time.TimeOut.ToString());
				var diff = timeOut - timeIn;
				var hours = diff.TotalMinutes / 60;

				time_data.TimeIn = time.TimeIn;
				time_data.TimeOut = time.TimeOut;
				time_data.Days = time.Days;
				time_data.WorkingHours =(decimal) hours;
				time_data.DepartmentId = time.DepartmentId;
				time_data.BranchId = time.BranchId;
				_context.Entry(time_data).State = System.Data.Entity.EntityState.Modified;
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			else
				return RedirectToAction("Login", "User");
		}
	}
}
