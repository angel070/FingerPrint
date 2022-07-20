using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FingerPrint.Controllers
{
    public class CountryController : Controller
    {
        private FingerContext _context;

        public CountryController()
        {
            _context = new FingerContext();
        } 
        // GET: Country
        
        public ActionResult Index()
        {
			if (Session["UserRoles"] != null)
			{
				var country = _context.Countries.ToList();
				return View(country);
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
        public ActionResult Create(Country _country)
        {
			/* if (!ModelState.IsValid)
                 return View("Create", _country);*/

			if (Session["UserRoles"] != null)
			{
				if (_country.Id > 0)
					_context.Entry(_country).State = System.Data.Entity.EntityState.Modified;

				var repeat = _context.Countries.Where(c => c.Name == _country.Name).SingleOrDefault();
				if( repeat != null)
				{
					TempData["message"] = "Country  already exists!";
					return RedirectToAction("Create");
				}else
				_context.Countries.Add(_country);
				_context.SaveChanges();

				//return Content(Firstrow.ToString());
				return RedirectToAction("Index");
			}
			else
				return RedirectToAction("Login", "User");
		}

        public JsonResult Delete(int ? id)
        {
			try
			{
				bool result = false;

				var country = _context.Countries.Where(c => c.Id == id).SingleOrDefault();
				var branch = _context.Branches.Where(c => c.CountryId == id);

				if (country != null)
				{
					_context.Countries.Remove(country);
					_context.SaveChanges();

					result = true;
				}
				return Json(new { status = true, message = "successfully deleted" });
			}
			catch (Exception e)
			{
				return Json(new {status = false, message = "This country is already used" });
			} 
        }

        public ActionResult Edit(int ? id)
        {
			if (Session["UserRoles"] != null)
			{
				if (id == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				var country = _context.Countries.SingleOrDefault(c => c.Id == id);

				if (country == null)
					return HttpNotFound();

				return View("Edit", country);

			}
			else
				return RedirectToAction("Login", "User");

		}

		[HttpPost]
		public ActionResult Edit(Country countries)
		{

			if (Session["UserRoles"] != null)
			{
				if (countries == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				if (!ModelState.IsValid)
				{
					return View("Edit", countries);
				}

				var country_data = _context.Countries.Find(countries.Id);
				if (country_data == null)
					return HttpNotFound();


				country_data.Name = countries.Name;
				_context.Entry(country_data).State = System.Data.Entity.EntityState.Modified;
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			else
				return RedirectToAction("Login", "User");
		}

		protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();

            base.Dispose(disposing);

        }
    }

}

