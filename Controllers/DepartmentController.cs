using FingerPrint.Models;
using FingerPrint.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace FingerPrint.Controllers
{
    public class DepartmentController : Controller
    {
        private FingerContext _context;

        public  DepartmentController()
        {
            _context = new FingerContext();
        }
        // GET: Department
        public ActionResult Index()
        {
			if (Session["UserRoles"] != null)
			{
				var department = _context.Departments.Include(c => c.Branch).ToList();
				return View(department);
			}
			else
				return RedirectToAction("Login", "User");
		}

        public ActionResult Create()
        {
			if (Session["UserRoles"] != null)
			{
				ViewBag.department = new SelectList(_context.Branches, "Id", "Name");
				return View();
			}
			else
				return RedirectToAction("Login", "User");
		}

        [HttpPost]
        public ActionResult Create(Department _department)
        {
			if (Session["UserRoles"] != null)
			{

				var repeat = _context.Departments.Where(c => c.Name == _department.Name &&c.BranchId == _department.BranchId).SingleOrDefault();
				if (repeat != null)
				{
					TempData["message"] = "Department already exists!";
					return RedirectToAction("Create");
				}
				else
				_context.Departments.Add(_department);
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

				var department = _context.Departments.SingleOrDefault(c => c.Id == id);
				if (department != null)
				{
					_context.Departments.Remove(department);
					_context.SaveChanges();

					result = true;
				}
				return Json(new { status = true, message = "successfully deleted" });
			}
			catch (Exception e)
			{
				return Json(new { status = false, message = "This Department is already used" });
			}
		}

		public ActionResult Edit(int? id)
		{
			if (Session["UserRoles"] != null)
			{
				ViewBag.department = new SelectList(_context.Branches, "Id", "Name");

				if (id == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				var department = _context.Departments.SingleOrDefault(c => c.Id == id);

				if (department == null)
					return HttpNotFound();

				return View("Edit", department);
			}
			else
				return RedirectToAction("Login", "User");

		}

		[HttpPost]
		public ActionResult Edit(Department departments)
		{
			if (Session["UserRoles"] != null)
			{
				if (departments == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				if (!ModelState.IsValid)
				{
					return View("Edit", departments);
				}

				var department_data = _context.Departments.Find(departments.Id);

				if (department_data == null)
					return HttpNotFound();

				department_data.Name = departments.Name;
				department_data.BranchId = departments.BranchId;
				_context.Entry(department_data).State = System.Data.Entity.EntityState.Modified;
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
