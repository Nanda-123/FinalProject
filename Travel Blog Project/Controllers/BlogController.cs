using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Travel_Blog_App.Models;

namespace RecapMvcApp.Controllers
{

    public class BlogController : Controller
    {
        // GET: Blog
        //[AllowAnonymous]
        public ActionResult AllBlogs()
        {

            var context = new DotNetDBEntities1();
            var data = context.BlogTbls.ToList();
            return View(data);
        }

        public ActionResult AddNew()
        {

            if (Session["currentUser"] == null)
            {
                Console.WriteLine("Please login Before U Post a Blog!!");
                TempData["ErrorInfo"] = "Please login Before U Post a Blog!!!";
                TempData.Keep();
                return RedirectToAction("SignIn", "Register");
            }
            var user = Session["currentUser"] as UserTable;
            var blog = new BlogTbl();
            blog.UserId = user.UserId;
            blog.UserTable = user;
            return PartialView(blog);
        }

        [HttpPost]
        public ActionResult AddNew(BlogTbl blog)
        {
            var context = new DotNetDBEntities1();
            context.BlogTbls.Add(blog);
            context.SaveChanges();
            return RedirectToAction("SignIn", "Register");
        }

    }
}