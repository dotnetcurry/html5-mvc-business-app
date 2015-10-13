using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace A2_HTML5_Biz_App_New.Controllers
{
    [Authorize(Roles = "Customer")]
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index()
        {
            var user = this.User.Identity.Name;
            ViewBag.CurrentUserEmail = user;
            return View();
        }

        public ActionResult SearchProperties()
        {
            return View();
        }
    }
}