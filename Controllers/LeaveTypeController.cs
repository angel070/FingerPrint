using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FingerPrint.Controllers
{
    public class LeaveTypeController : Controller
    {
		private FingerContext _context;

		public LeaveTypeController()
		{
			_context = new FingerContext();
		}
		// GET: LeaveType
		public ActionResult Index()
        {
			if (Session["UserRoles"] != null)
			{

				var Leave = _context.LeaveTypes.ToList();
              return View(Leave);
			}
			else
				return RedirectToAction("login", "user");

		}

		public ActionResult Create()
		{

			if (Session["userRoles"] != null)
			{
				return View();
			}
			else
				return RedirectToAction("login", "user");
		}

		[HttpPost]
		public ActionResult Create(LeaveType _leave)
		{

			if (Session["userRoles"] != null)
			{
				_context.LeaveTypes.Add(_leave);
				_context.SaveChanges();

				return RedirectToAction("Index");
		    }
			else
				return RedirectToAction("login", "user");
	    }

		public ActionResult Edit(int? id)
		{
			if (Session["UserRoles"] != null)
			{
				if (id == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				var leaveType = _context.LeaveTypes.SingleOrDefault(c => c.Id == id);

				if (leaveType == null)
					return HttpNotFound();

				return View("Edit", leaveType);

			}
			else
				return RedirectToAction("Login", "User");


		}

		[HttpPost]
		public ActionResult Edit(LeaveType _leaveType)
		{

			if (Session["UserRoles"] != null)
			{
				if (_leaveType == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				if (!ModelState.IsValid)
				{
					return View("Edit", _leaveType);
				}

				var leaveType_data = _context.LeaveTypes.Find(_leaveType.Id);
				if (leaveType_data == null)
					return HttpNotFound();


				leaveType_data.ShortType = _leaveType.ShortType;
				leaveType_data.Type = _leaveType.Type;
				_context.Entry(leaveType_data).State = System.Data.Entity.EntityState.Modified;
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			else
				return RedirectToAction("Login", "User");

		}

		public JsonResult Delete(int? id)
		{
			if (Session["UserRoles"] != null)
			{
				try
			    {

				bool result = false;

				var leaveType = _context.LeaveTypes.Where(c => c.Id == id).SingleOrDefault();

				if (leaveType != null)
				{
					_context.LeaveTypes.Remove(leaveType);
					_context.SaveChanges();

					result = true;
				}
				return Json(new { status = result, message = "successfully deleted" });
			}
			catch (Exception e)
			{
				return Json(new { status = false, message = "This Type is already used" });
			}

		}
			else
				return Json("login", "user");
	}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				_context.Dispose();

			base.Dispose(disposing);

		}
	}
}
