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
	public class StaffController : Controller
	{
		private FingerContext _context;

		public StaffController()
		{
			_context = new FingerContext();
		}
		// GET: Staff
		public ActionResult Index()
		{
			if (Session["UserRoles"] != null)
			{
				ViewBag.Country = new SelectList(_context.Countries, "Id", "Name");
				var staff = _context.Staffs.Include(c => c.Branch).Include(u => u.Country).Include(b => b.Department).ToList();
				return View(staff);
			}
			else
				return RedirectToAction("Login", "User");
		}

		public ActionResult Create()
		{
			if (Session["UserRoles"] != null)
			{
				ViewBag.Department = new SelectList(_context.Departments, "Id", "Name");
				ViewBag.Branch = new SelectList(_context.Branches, "Id", "Name");
				ViewBag.Country = new SelectList(_context.Countries, "Id", "Name");
				return View();
			}
			else
				return RedirectToAction("Login", "User");
		}

		[HttpPost]
		public ActionResult Create(Staff _staff)
		{
			if (Session["UserRoles"] != null)
			{
				var CheckId = _context.Staffs.Where(c => c.Staff_id == _staff.Staff_id).SingleOrDefault();
				if (CheckId != null)
				{
					return RedirectToAction("Index");
				}

				var name = _staff.BranchId;
				/*Branch branch = new Branch();*/
				var branch = _context.Branches.Where(c => c.Id == name).FirstOrDefault();
				var country = branch.CountryId;
				_staff.CountryId = country;
				_staff.StaffId_clone = _staff.Staff_id;
				_staff.Email_clone = _staff.Email;
				if (_staff.Id > 0)
					_context.Entry(_staff).State = System.Data.Entity.EntityState.Modified;
				else
					_context.Staffs.Add(_staff);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			else
				return RedirectToAction("Login", "User");
		}

		[HttpPost]
		public ActionResult CheckStaffIdExists(string Staff_id, string StaffId_clone)
		{

			try
			{
				if (Staff_id == StaffId_clone)
				{
					return Json(true);
				}
				var nameexits = _context.Staffs.Where(c => c.Staff_id == Staff_id).SingleOrDefault();

				bool UserExists = (nameexits != null)? true: false;

				return Json(UserExists);

			}

			catch (Exception)

			{
				return Json(false);
			}
		}

		[HttpPost]
		public ActionResult CheckEmailExists(string Email, string Email_clone)
		{

			bool UserExists = false;
			try
			{
				if (Email == Email_clone) return Json(!UserExists);


				var nameexits = _context.Staffs.Where(c => c.Email == Email).SingleOrDefault();

				var userExists = (nameexits != null) ? true : false;

				return Json(!UserExists);

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
				ViewBag.Department = new SelectList(_context.Departments, "Id", "Name");
				ViewBag.Branch = new SelectList(_context.Branches, "Id", "Name");
				ViewBag.Country = new SelectList(_context.Countries, "Id", "Name");

				if (id == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				var staffDetails = _context.Staffs.SingleOrDefault(c => c.Id == id);

				if (staffDetails == null)
					return HttpNotFound();

				staffDetails.StaffId_clone = staffDetails.Staff_id;
				staffDetails.Email_clone = staffDetails.Email;

				return View("Edit", staffDetails);
			}
			else
				return RedirectToAction("Login", "User");

		}

		[HttpPost]
		public ActionResult Edit(Staff staff)
		{
			if (Session["UserRoles"] != null)
			{
				if (staff == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				if (!ModelState.IsValid)
				{
					return View("Edit", staff);
				}

				var staff_data = _context.Staffs.Find(staff.Id);

				if (staff_data == null)
					return HttpNotFound();

				staff_data.Staff_id = staff.Staff_id;
				staff_data.FirstName = staff.FirstName;
				staff_data.LastName = staff.LastName;
				staff_data.Email = staff.Email;
				staff_data.Phone_no = staff.Phone_no;
				staff_data.BranchId = staff.BranchId;
				staff_data.DepartmentId = staff.DepartmentId;
				_context.Entry(staff_data).State = System.Data.Entity.EntityState.Modified;
				_context.SaveChanges();

				return RedirectToAction("Index");
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

				var staff = _context.Staffs.SingleOrDefault(c => c.Id == id);

				if (staff == null)
					return HttpNotFound();
				_context.Staffs.Remove(staff);
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			else
				return RedirectToAction("Login", "User");
		}

		public ActionResult FingerPrintForm(int? id)
		{
			if (Session["UserRoles"] != null)
			{
				var finger = _context.Staffs.Find(id);
				var branch = _context.Branches.Where(c => c.Id == finger.BranchId).SingleOrDefault();
				var country = _context.Countries.Where(c => c.Id == finger.CountryId).SingleOrDefault();
				var department = _context.Departments.Where(c => c.Id == finger.DepartmentId).SingleOrDefault();
				var Id = finger.Id;
				var FirstName = finger.FirstName;
				var LastName = finger.LastName;
				var StaffId = finger.Staff_id;
				var Phone = finger.Phone_no;
				var Email = finger.Email;
				var Country = country.Name;
				var Branch = branch.Name;
				var Department = department.Name;

				TempData["Id"] = finger.Id;
				TempData["LastName"] = LastName;
				TempData["FirstName"] = FirstName;
				TempData["StaffId"] = StaffId;
				TempData["Phone"] = Phone;
				TempData["Email"] = Email;
				TempData["Country"] = Country;
				TempData["Branch"] = Branch;
				TempData["Department"] = Department;
				return View();
			}
			else
				return RedirectToAction("Login", "User");
		}

		[HttpPost]
		public ActionResult FingerPrintForm(int? id, string txtIsoTemplate)
		{
			if (Session["UserRoles"] != null)
			{
				if (id == null)
					return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

				var finger = _context.Staffs.Find(id);
				finger.Fingerprint = txtIsoTemplate;
				var FirstName = finger.FirstName;
				var LastName = finger.LastName;
				var StaffId = finger.Staff_id;
				var Phone = finger.Phone_no;
				var Email = finger.Email;
				var Country = finger.Country;
				var Branch = finger.Branch;
				var Department = finger.Department;
				TempData["LastName"] = LastName;
				_context.Staffs.Append(finger);
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			else
				return RedirectToAction("Login", "User");

		}
		public ActionResult Fingerprint()
		{

			return View();
		}

		public ActionResult EventAdminDashboard()
		{
			return View();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
				_context.Dispose();

			base.Dispose(disposing);

		}
	}

}
