using A2_HTML5_Biz_App_New.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace A2_HTML5_Biz_App_New.Controllers
{
    [Authorize(Roles="Administrator")]
    public class RoleController : Controller
    {
        ApplicationDbContext ctx;

        public RoleController()
        {
            ctx = new ApplicationDbContext(); 
        }

        // GET: RoleController
        public ActionResult Index()
        {
            var appRoles = ctx.Roles.ToList(); 
            return View(appRoles);
        }

        public ActionResult Create()
        {
            return View();
        }
        /// <summary>
        /// Action method to create Roles
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                ctx.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                ctx.SaveChanges();
                ViewBag.ResultMessage = "Role created successfully !";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(string roleName)
        {
            var Role = ctx.Roles.Where(role => role.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            return View(Role);
        }

        //
        // POST: /Roles/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        //{
        //    try
        //    {
        //        ctx.Entry(role).State = System.Data.Entity.EntityState.Modified;
        //        ctx.SaveChanges();

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        ////
        //// GET: /Roles/Delete/5
        //public ActionResult Delete(string RoleName)
        //{
        //    var thisRole = ctx.Roles.Where(r => r.Name.Equals(RoleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        //    ctx.Roles.Remove(thisRole);
        //    ctx.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        /// <summary>
        /// Assign Users to Role
        /// </summary>
        /// <returns></returns>
        public ActionResult AssignUserToRole()
        {
            var list = ctx.Roles.OrderBy(role => role.Name).ToList().Select(role => new SelectListItem { Value = role.Name.ToString(), Text = role.Name }).ToList();
            ViewBag.Roles = list;
            return View();
        }

        /// <summary>
        /// Add user to Role
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="RoleName"></param>
        /// <returns></returns>

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserAddToRole(string UserName, string RoleName)
        {
            ApplicationUser user = ctx.Users.Where(usr => usr.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            
            // Display Roles in DropDown
            
            var list = ctx.Roles.OrderBy(role => role.Name).ToList().Select(role => new SelectListItem { Value = role.Name.ToString(), Text = role.Name }).ToList();
            ViewBag.Roles = list;

            if (user != null)
            {
                var account = new AccountController();
                account.UserManager.AddToRoleAsync(user.Id, RoleName);

                ViewBag.ResultMessage = "Role created successfully !";

                return View("AssignUserToRole");
            }
            else
            {
                ViewBag.ErrorMessage = "Sorry user is not available";
                return View("AssignUserToRole");
            }
            
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult GetRoles(string UserName)
        //{
        //    if (!string.IsNullOrWhiteSpace(UserName))
        //    {
        //        ApplicationUser user = ctx.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
        //        var account = new AccountController();

        //        ViewBag.RolesForThisUser = account.UserManager.GetRolesAsync(user.Id);

        //        // prepopulat roles for the view dropdown
        //        var list = ctx.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
        //        ViewBag.Roles = list;
        //    }

        //    return View("ManageUserRoles");
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        //{
        //    var account = new AccountController();
        //    ApplicationUser user = ctx.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

        //    //account.UserManager.IsInRole(user.Id, RoleName)
        //    if (account.UserManager.IsInRoleAsync(user.Id, RoleName).Result)
        //    {
        //        account.UserManager.RemoveFromRoleAsync(user.Id, RoleName);
        //        ViewBag.ResultMessage = "Role removed from this user successfully !";
        //    }
        //    else
        //    {
        //        ViewBag.ResultMessage = "This user doesn't belong to selected role.";
        //    }
        //    // prepopulat roles for the view dropdown
        //    var list = ctx.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
        //    ViewBag.Roles = list;

        //    return View("AssignUserToRole");
        //}
    }
}