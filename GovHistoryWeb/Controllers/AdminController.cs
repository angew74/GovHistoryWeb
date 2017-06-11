using GovHistoryRepository.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;

namespace GovHistoryWeb.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        // GET: Admin
        public ActionResult UserCreate()
        {
            return View();
        }

        // GET: Admin
        public ActionResult UserDetails()
        {
            return View();
        }

        public ActionResult UserEdit()
        {
            var id = HttpContext.Request.QueryString["id"].First();
            ViewBag.UserId = id;
            return View();
        }

        public ActionResult RoleDetails()
        {
            var id = HttpContext.Request.QueryString["id"].First();
            ViewBag.GroupId = id;
            ApplicationRole model = ApplicationRoleManager.GetRole(id);
            return View(model);
        }
    }
}