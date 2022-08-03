using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace FingerPrint.Controllers
{
    public class LeaveController : Controller
    {
		private FingerContext _context;

		public LeaveController()
		{
			_context = new FingerContext();
		}
		// GET: Leave
		public ActionResult Index()
        {
			if (Session["UserRoles"] != null)
			{
				var leave = _context.Leaves.Include(c => c.LeaveType).ToList();
			    return View(leave);
			}
			else
				return RedirectToAction("Login", "User");
		}

		public ActionResult Create()
		{
			if (Session["UserRoles"] != null)
			{
				ViewBag.Leaves = new SelectList(_context.LeaveTypes, "Id", "Type");
				return View();

			}
			else
				return RedirectToAction("Login", "User");

		}

		[HttpPost]
		public ActionResult Create(Leave _leave)
		{
			if (Session["userRoles"] != null)
			{
				var staffNames = _context.Staffs.Where(c => c.Staff_id == _leave.StaffId).SingleOrDefault();
				
				if (staffNames == null)
				{
					ViewBag.Leaves = new SelectList(_context.LeaveTypes, "Id", "Type");
					TempData["ExistId"] = "Id does not exist";
					return View();
				}
				ViewBag.Leaves = new SelectList(_context.LeaveTypes, "Id", "Type");

				var timeIn = DateTime.Parse(_leave.DateFrom.ToString());
				var timeOut = DateTime.Parse(_leave.DateTo.ToString());
				var diff = timeOut - timeIn;
				
				_leave.TotalDays = (double)diff.TotalDays;
				_leave.Name = staffNames.FirstName + " " + staffNames.LastName;
				_context.Leaves.Add(_leave);
				_context.SaveChanges();
				TempData["test"] = diff;
				return RedirectToAction("Index");
			}
			else
				return RedirectToAction("login", "user");
			
		}

		[HttpPost]
		public ActionResult CheckStaffIdExists(string Staff_id, string StaffId_clone)
		{

			try
			{
				//if (Staff_id == StaffId_clone)
				//{
				//	return Json(true);
				//}
				
				var nameexits = _context.Staffs.Where(c => c.Staff_id == Staff_id).SingleOrDefault();
				
				bool UserExists = (nameexits == null) ? true: false;
				//if(nameexits != null)
				//{
				//	TempData["ExistStaff"] = nameexits.FirstName + " " + nameexits.LastName;
				//	return Json (UserExists);
				//}

				return Json(UserExists);

			}

			catch (Exception)

			{
				return Json(false);
			}
		}

		public ActionResult Edit(int? id)
		{
			if (Session["UserRoles"] != null)
			{
				ViewBag.Leaves = new SelectList(_context.LeaveTypes, "Id", "Type");

				if (id == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				var leave = _context.Leaves.SingleOrDefault(c => c.Id == id);

				if (leave == null)
					return HttpNotFound();

				return View("Edit", leave);

			}
			else
				return RedirectToAction("Login", "User");


		}

		[HttpPost]
		public ActionResult Edit(Leave _leave)
		{

			if (Session["UserRoles"] != null)
			{
			
				 if (_leave == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				if (!ModelState.IsValid)
				{
					return View("Edit", _leave);
				}
				var timeIn = DateTime.Parse(_leave.DateFrom.ToString());
				var timeOut = DateTime.Parse(_leave.DateTo.ToString());
				var diff = timeOut - timeIn;
				

				var Leave_data = _context.Leaves.Find(_leave.Id);
				if (Leave_data == null)
					return HttpNotFound();
				var staffNames = _context.Staffs.Where(c => c.Staff_id == _leave.StaffId).SingleOrDefault();
				if (staffNames == null)
				{
					
					ViewBag.Leaves = new SelectList(_context.LeaveTypes, "Id", "Type");
					TempData["ExistId"] = "Id does not exist";
					return View();
				}
				
				Leave_data.StaffId= _leave.StaffId;
				Leave_data.DateFrom= _leave.DateFrom;
				Leave_data.DateTo= _leave.DateTo;
				Leave_data.Name = staffNames.FirstName + " " + staffNames.LastName;
				Leave_data.TotalDays = (double)diff.TotalDays;

				_context.Entry(Leave_data).State = System.Data.Entity.EntityState.Modified;
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			else
				return RedirectToAction("Login", "User");

		}

		public ActionResult Delete(int? id) 
		{
			try
		    {
				bool result = false;
				var leave = _context.Leaves.Where(c => c.Id == id).SingleOrDefault();

				if (leave != null)
				{
					_context.Leaves.Remove(leave);
					_context.SaveChanges();

				}
				//return RedirectToAction("Index");
				return Json(new { status = result, message = "successfully deleted" });
			}
			catch (Exception e)
			{
				return Json(new { status = false, message = "This Leave is already used" });
			}

		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				_context.Dispose();

			base.Dispose(disposing);

		}
	}
}
