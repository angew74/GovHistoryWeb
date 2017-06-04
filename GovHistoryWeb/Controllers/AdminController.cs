using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}