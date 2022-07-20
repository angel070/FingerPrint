using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FingerPrint.Controllers
{
    public class HolidayController : Controller
    {
        private FingerContext _context;

        public HolidayController()
        {
            _context = new FingerContext();
        }
        // GET: Holiday
        public ActionResult Index()
        {
			if (Session["UserRoles"] != null)
			{
				var holiday = _context.Holdays.ToList();
				return View(holiday);
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
        public ActionResult Create(Holiday holiday)
        {
			if (Session["UserRoles"] != null)
			{
				var holidayExist = _context.Holdays.Where(c => c.Name == holiday.Name && c.DateFrom == holiday.DateFrom).SingleOrDefault();

				if(holidayExist != null)
				{
					TempData["Holiday"] = "Holiday already Exists";
					return RedirectToAction("Create");
				}
				_context.Holdays.Add(holiday);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			else
				return RedirectToAction("Login", "User");
		}

		public ActionResult Delete(int? id)
		{

			if (id == null)
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			var holiday = _context.Holdays.SingleOrDefault(c => c.Id == id);

			if (holiday == null)
				return HttpNotFound();
			_context.Holdays.Remove(holiday);
			_context.SaveChanges();

			return RedirectToAction("Index");

		}

		public ActionResult Edit(int? id)
		{
			if (Session["UserRoles"] != null)
			{
				if (id == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				var holiday = _context.Holdays.SingleOrDefault(c => c.Id == id);

				if (holiday == null)
					return HttpNotFound();

				return View("Edit", holiday);
			}
			else
				return RedirectToAction("Login", "User");
		}

		[HttpPost]
		public ActionResult Edit(Holiday holiday)
		{
			if (Session["UserRoles"] != null)
			{
				if (holiday == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				if (!ModelState.IsValid)
				{
					return View("Edit", holiday);
				}

				var holiday_data = _context.Holdays.Find(holiday.Id);

				if (holiday_data == null)
					return HttpNotFound();

				holiday_data.Name = holiday.Name;
				holiday_data.DateFrom = holiday.DateFrom;
				holiday_data.DateTo = holiday.DateTo;
				_context.Entry(holiday_data).State = System.Data.Entity.EntityState.Modified;
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			else
				return RedirectToAction("Login", "User");
		}

	}
}
