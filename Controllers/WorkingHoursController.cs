using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FingerPrint.Controllers
{
    public class WorkingHoursController : Controller
    {
		private FingerContext _context;

		public WorkingHoursController()
		{
			_context = new FingerContext();
		}
		// GET: WorkingHours
		public ActionResult Index()
        {
			if (Session["UserRoles"] != null)
			{
				var working = _context.WorkingHourse.ToList();
            return View(working);
		    }
			else
				return RedirectToAction("Login", "User");
	    }

		public ActionResult Create()
		{
			if (Session["UserRoles"] != null)
			{
				return View();
			}
			else
				return RedirectToAction("Login", "User");
		}

		[HttpPost]
		public ActionResult Create(WorkingHours working)
		{
			if (Session["UserRoles"] != null)
			{
				_context.WorkingHourse.Add(working);
			    _context.SaveChanges();
			    return RedirectToAction("Index");
			}
			else
				return RedirectToAction("Login", "User");
		}

		public ActionResult Edit(int? id)
		{
			if (Session["UserRoles"] != null)
			{
				if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			var working = _context.WorkingHourse.SingleOrDefault(c => c.Id == id);

			if (working == null)
				return HttpNotFound();

			return View("Edit", working);
		    }
			else
				return RedirectToAction("Login", "User");

	    }

		[HttpPost]
		public ActionResult Edit(WorkingHours working)
		{
			if (Session["UserRoles"] != null)
			{
				if (working == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				if (!ModelState.IsValid)
				{
					return View("Edit", working);
				}

				var working_data = _context.WorkingHourse.Find(working.Id);

				if (working_data == null)
					return HttpNotFound();

				working_data.workingHours = working.workingHours;
				_context.Entry(working_data).State = System.Data.Entity.EntityState.Modified;
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			else
				return RedirectToAction("Login", "User");
		}

		public JsonResult Delete(int? id)
		{

			bool result = false;

			var working = _context.WorkingHourse.SingleOrDefault(c => c.Id == id);

			if (working != null)
			{
				_context.WorkingHourse.Remove(working);
				_context.SaveChanges();

				result = true;
			}
			return Json(result, JsonRequestBehavior.AllowGet);
		}

	}
}
