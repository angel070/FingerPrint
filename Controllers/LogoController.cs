using FingerPrint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FingerPrint.Controllers
{
    public class LogoController : Controller
    {
		private FingerContext _context;

		public LogoController()
		{
			_context = new FingerContext();
		}
		// GET: Logo
		public ActionResult Index()
        {
			if (Session["UserRoles"] != null)
			{
				var logo = _context.Logos.ToList();
               return View(logo);
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
		public ActionResult Create(Logo logo, HttpPostedFileBase picture)
		{
			if (Session["UserRoles"] != null)
			{
				//File Name
				string imageName = (picture == null) ? null : System.IO.Path.GetFileName(picture.FileName);

				if (picture == null)
				{
					return Content("Null reference");
				}
				//image path
				string imagePath = "~/Upload/Logo/" + imageName;
				//Save image into server location
				picture.SaveAs(Server.MapPath(imagePath));

				logo.FileName = imageName;
				logo.FilePath = imagePath;

				_context.Logos.Add(logo);
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

				var logo = _context.Logos.SingleOrDefault(c => c.Id == id);

				if (logo != null)
				{
					_context.Logos.Remove(logo);
					_context.SaveChanges();

					result = true;
				}
				return Json(new { status = result, message = "successfully deleted" });
			}
			catch (Exception e)
			{
				return Json(new { status = false, message = "This Logo is already used" });
			}

		}
	}
}
