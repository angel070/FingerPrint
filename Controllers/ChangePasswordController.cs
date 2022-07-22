using FingerPrint.Models;
using FingerPrint.View_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FingerPrint.Controllers
{
	public class ChangePasswordController : Controller
	{
		private FingerContext _context;

		public ChangePasswordController()
		{
			_context = new FingerContext();
		}
		//
		// GET: ChangePassword
		public ActionResult Index()
		{
			if (Session["userRoles"] != null)
			{
				return View();
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
		public ActionResult Create(ChangePasswordViewModel change)
		{
			if (Session["userRoles"] != null)
			{
				User user = new User();
				var username = Session["UserName"].ToString();
				var validateOldPassword = _context.Users.Where(c => c.Password == change.OldPassword && c.Username == username).SingleOrDefault();

				if (validateOldPassword != null )
				{
					if (change.NewPassword == change.reEnterPassword)
					{
						validateOldPassword.Password = change.NewPassword;
						validateOldPassword.Username = validateOldPassword.Username;
						validateOldPassword.IsActive = validateOldPassword.IsActive;
						validateOldPassword.LastName = validateOldPassword.LastName;
						validateOldPassword.FirstName = validateOldPassword.FirstName;
						validateOldPassword.Email = validateOldPassword.Email;
						validateOldPassword.BranchId = validateOldPassword.BranchId;
						validateOldPassword.UserRolesId = validateOldPassword.UserRolesId;
						validateOldPassword.PhoneNumber = validateOldPassword.PhoneNumber;
						_context.Entry(validateOldPassword).State = System.Data.Entity.EntityState.Modified;
						_context.SaveChanges();
					}
					else
					{
						TempData["UnMatchPassword"] = "Password Does not match";
						return View();
					}
				}else
				TempData["Invalid Password"] = "Old Password is Incorrect";
				return View();
			}

			else
				return RedirectToAction("login", "user");
		}
	}
}
