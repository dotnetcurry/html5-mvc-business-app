using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace A2_HTML5_Biz_App_New.Controllers
{ 
    [Authorize(Roles="Owner")]
    public class OwnerPropertyDescriptionController : Controller
    {
        // GET: OwnerPropertyDescription
        public ActionResult Index()
        {
            var data = this.User.Identity.Name;

            ViewBag.CurrentUserEmail = data; 

            return View();
        }
    }
}