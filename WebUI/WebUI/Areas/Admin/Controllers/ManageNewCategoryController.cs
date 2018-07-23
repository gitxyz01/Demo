using Domain.Abtract;
using Domain.Concrete;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{
    public class ManageNewCategoryController : Controller
    {
        private INewCategoryRepository newCategoryRepository;
        public ManageNewCategoryController()
        {
            newCategoryRepository = new NewCategoryRepository();
        }
        // GET: Admin/ManageNewCategory
        public ActionResult Index()
        {
            AdminCategoryViewModel model = new AdminCategoryViewModel();
            model.ListNewCategory = newCategoryRepository.NewCategory.Where(x=>x.TrangThai==true).ToList();      
            return View(model);
            
        }

        public ViewResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(DanhMucTinTuc model)
        {
            if (string.IsNullOrWhiteSpace(model.Ten))
            {
                ModelState.AddModelError("", "Vui lòng nhập tên danh mục");
            }
            if (ModelState.IsValid)
            {
                model.NguoiTao = User.Identity.GetUserName();
                newCategoryRepository.Save(model);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ViewResult Edit(long id)
        {
            var model = newCategoryRepository.GetById(id);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(DanhMucTinTuc model)
        {
            if (string.IsNullOrWhiteSpace(model.Ten))
            {
                ModelState.AddModelError("", "Vui lòng nhập tên danh mục");
            }
            if (ModelState.IsValid)
            {
                
                model.NguoiChinhSua = User.Identity.GetUserName();
                newCategoryRepository.Save(model);
                return RedirectToAction("Index");
            }
            return View();            
        }

        public ViewResult ConfirmDelete(long id)
        {
            var model = newCategoryRepository.GetById(id);
            return View(model);
        }

        public ActionResult Delete(long id)
        {
            newCategoryRepository.Delete(new DanhMucTinTuc()
            {
                NewCategoryID = id
            });
            return RedirectToAction("Index");
        }       
    }
}