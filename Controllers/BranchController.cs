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
    public class BranchController : Controller
    {
        private FingerContext _context;

        public BranchController()
        {
            _context = new FingerContext();
        }
        // GET: Branch
		
        public ActionResult Index()
        {
			if (Session["userRoles"] != null)
			{
				var branch = _context.Branches.Include(c => c.Country).ToList();
				ViewBag.Branches = _context.Branches.Count();
				return View(branch);

			}
			else
				return RedirectToAction("login", "user");

		}

		public ActionResult Create()
        {
			if (Session["UserRoles"] != null)
			{
				ViewBag.Branches = new SelectList(_context.Countries, "Id", "Name");
				return View();

			}
			else
				return RedirectToAction("Login", "User");

		}

        [HttpPost]
        public ActionResult Create(Branch _branch)
        {

			if (Session["UserRoles"] != null)
			{
				var repeat = _context.Branches.Where(c => c.Name ==_branch.Name && c.CountryId == _branch.CountryId).SingleOrDefault();
				if (repeat != null)
				{
					TempData["message"] = "Branch  already exists!";
					return RedirectToAction("Create");
				}
				else
				_context.Branches.Add(_branch);
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

				var branch = _context.Branches.SingleOrDefault(c => c.Id == id);

				if (branch != null)
				{
					_context.Branches.Remove(branch);
					_context.SaveChanges();

					result = true;
				}
				return Json(new { status = result, message = "successfully deleted" });
			}
			catch(Exception e)
			{
				return Json(new { status = false, message = "This Branch is already used" });
			}		

		}

        public ActionResult Edit(int ? id)
        {
			if (Session["UserRoles"] != null)
			{
				ViewBag.Branches = new SelectList(_context.Countries, "Id", "Name");

				if (id == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				var branch = _context.Branches.SingleOrDefault(c => c.Id == id);

				if (branch == null)
					return HttpNotFound();

				return View("Edit", branch);

			}
			else
				return RedirectToAction("Login", "User");


		}

		[HttpPost]
		public ActionResult Edit(Branch branches)
		{

			if (Session["UserRoles"] != null)
			{
				if (branches == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				if (!ModelState.IsValid)
				{
					return View("Edit", branches);
				}

				var branch_data = _context.Branches.Find(branches.Id);
				if (branch_data == null)
					return HttpNotFound();

				
				branch_data.Name = branches.Name;
				branch_data.CountryId = branches.CountryId;
				_context.Entry(branch_data).State = System.Data.Entity.EntityState.Modified;
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
