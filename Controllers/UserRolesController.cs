using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace FingerPrint.Controllers
{
    public class UserRolesController : Controller
    {
        private FingerContext _context;

        public UserRolesController()
        {
            _context = new FingerContext();
        }
        // GET: UserRoles
        public ActionResult Index()
        {
			if (Session["UserRoles"] != null)
			{
				var roles = _context.Roles.ToList();
            return View(roles);
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
        public ActionResult Create(UserRoles _roles)
        {
			if (Session["UserRoles"] != null)
			{
				if (!ModelState.IsValid)
				return View("Invalid");

				if (_roles.Id > 0)
					_context.Entry(_roles).State = System.Data.Entity.EntityState.Modified;


				var repeat = _context.Roles.Where(c => c.Name == _roles.Name).SingleOrDefault();
				if (repeat != null)
				{
					TempData["message"] = "Role is already exists!";
					return RedirectToAction("Create");
				}
				
				else
					_context.Roles.Add(_roles);
					_context.SaveChanges();

				return RedirectToAction("Index");
			}
			else
				return RedirectToAction("Login", "User");
		}

        public ActionResult Edit(int ? id)
        {
			if (Session["UserRoles"] != null)
			{
				if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				var roles = _context.Roles.SingleOrDefault(c => c.Id == id);

				if (roles == null)
					return HttpNotFound();

				return View("Edit", roles);
			}
			else
				return RedirectToAction("Login", "User");

	    }

		[HttpPost]
		public ActionResult Edit(UserRoles roles)
        {
			if (Session["UserRoles"] != null)
			{
				if (roles == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

			if (!ModelState.IsValid)
			{
				return View("Edit", roles);
			}

            var roles_data = _context.Roles.Find(roles.Id);

               if (roles_data == null)
                   return HttpNotFound();

			roles_data.Name = roles.Name;
			_context.Entry(roles_data).State = System.Data.Entity.EntityState.Modified;
			_context.SaveChanges();

			return RedirectToAction("Index");
			}
			else
				return RedirectToAction("Login", "User");
		}

        public JsonResult Delete(int? id)
        {
			try
			{
				bool result = false;

				var roles = _context.Roles.SingleOrDefault(c => c.Id == id);
				if (roles != null)
				{
					_context.Roles.Remove(roles);
					_context.SaveChanges();

					result = true;
				}
				return Json(new { status = result, message = "successfully deleted" });
			}
			catch (Exception e)
			{
				return Json(new { status = false, message = "This Role is already used" });
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
