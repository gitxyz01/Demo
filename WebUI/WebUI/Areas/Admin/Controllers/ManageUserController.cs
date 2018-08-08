﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]

    public class ManageUserController : Controller
    {

        ApplicationDbContext context = new ApplicationDbContext();

        // GET: Admin/ManageUser

        public ActionResult Index()

        {

            IEnumerable<ApplicationUser> model = context.Users.AsEnumerable();

            return View(model);

        }

        public ActionResult Edit(string Id)

        {

            ApplicationUser model = context.Users.Find(Id);

            return View(model);

        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult Edit(ApplicationUser model)

        {

            try

            {

                context.Entry(model).State = System.Data.Entity.EntityState.Modified;

                context.SaveChanges();

                return RedirectToAction("Index");

            }

            catch (Exception ex)

            {

                ModelState.AddModelError("", ex.Message);

                return View(model);

            }

        }

        public ActionResult EditRole(string Id)

        {

            ApplicationUser model = context.Users.Find(Id);

            ViewBag.RoleId = new SelectList(context.Roles.ToList().Where(item => model.Roles.FirstOrDefault(r => r.RoleId == item.Id) == null).ToList(), "Id", "Name");

            return View(model);

        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult AddToRole(string UserId, string[] RoleId)

        {

            ApplicationUser model = context.Users.Find(UserId);

            if (RoleId != null && RoleId.Count() > 0)

            {

                foreach (string item in RoleId)

                {

                    IdentityRole role = context.Roles.Find(RoleId);

                    model.Roles.Add(new IdentityUserRole() { UserId = UserId, RoleId = item });

                }

                context.SaveChanges();

            }

            ViewBag.RoleId = new SelectList(context.Roles.ToList().Where(item => model.Roles.FirstOrDefault(r => r.RoleId == item.Id) == null).ToList(), "Id", "Name");

            return RedirectToAction("EditRole", new { Id = UserId });

        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        public ActionResult DeleteRoleFromUser(string UserId, string RoleId)

        {

            ApplicationUser model = context.Users.Find(UserId);

            model.Roles.Remove(model.Roles.Single(m => m.RoleId == RoleId));

            context.SaveChanges();

            ViewBag.RoleId = new SelectList(context.Roles.ToList().Where(item => model.Roles.FirstOrDefault(r => r.RoleId == item.Id) == null).ToList(), "Id", "Name");

            return RedirectToAction("EditRole", new { Id = UserId });

        }

        public ActionResult Delete(string Id)

        {

            var model = context.Users.Find(Id);

            return View(model);

        }

        [HttpPost]

        [ValidateAntiForgeryToken]

        [ActionName("Delete")]

        public ActionResult DeleteConfirmed(string Id)

        {

            ApplicationUser model = null;

            try

            {

                model = context.Users.Find(Id);

                context.Users.Remove(model);

                context.SaveChanges();

                return RedirectToAction("Index");

            }

            catch (Exception ex)

            {

                ModelState.AddModelError("", ex.Message);

                return View("Delete", model);

            }

        }

    }

}