using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using formAuthentication.Models;


namespace formAuthentication.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult login()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult login(Models.Membership member)
        {
            using (var context=new formAuthenticationEntities())
            {
                bool isValid = context.tblLogins.Any(x => x.userName == member.userName && x.password == member.password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(member.userName,false);
                    return RedirectToAction("Index","Home");
                }

                ModelState.AddModelError("", "Invalid username or password");
                return View();

            }
            
        }

        public ActionResult signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult signup(tblLogin member)
        {
            using (var context=new formAuthenticationEntities())
            {
                member.status = true;
                context.tblLogins.Add(member);
                context.SaveChanges();
            }
            return RedirectToAction("login");
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}