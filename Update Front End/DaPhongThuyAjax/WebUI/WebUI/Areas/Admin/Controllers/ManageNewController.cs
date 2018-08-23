using Domain.Abtract;
using Domain.Concrete;
using Domain.Entities;
using Domain.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{   
    public class ManageNewController : Controller
    {
        
        private INewRepository newRepository;
        public List<DanhMucTinTuc> GetViewbagNewCategory()
        {
            INewCategoryRepository newCategoryRepository = new NewCategoryRepository();
            var newCategory = newCategoryRepository.GetAllActive("All");
            return ViewBag.newCategory = newCategory;
        }

        public ManageNewController()
        {
            newRepository = new NewRepository();
        }
        // GET: Admin/ManageNew

        [Authorize(Roles = "Đọc,Admin")]
        public ActionResult Index(int newCategoryId=0)
        {
           AdminNewViewModel model = new AdminNewViewModel();
           model.ListNew = newRepository.GetListNewAdmin(newCategoryId);
            //var model = newRepository.News.Where(x => x.TrangThai == true).ToList();
            return View(model);
        }

        [Authorize(Roles = "Thêm,Admin")]
        public ViewResult Create()
        {
            GetViewbagNewCategory();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TinTuc model)
        {
            if (string.IsNullOrWhiteSpace(model.Ten))
            {
                ModelState.AddModelError("", "Vui lòng nhập tên danh mục");
            }
            if (ModelState.IsValid)
            {
                model.NguoiTao = User.Identity.GetUserName();
                newRepository.SaveNew(model);
                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize(Roles = "Sửa,Admin")]
        public ActionResult Edit(long id)
        {
            GetViewbagNewCategory();
            var model = newRepository.GetNewById(id);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(TinTuc model)
        {

            if (ModelState.IsValid)
            {
                model.NguoiChinhSua = User.Identity.GetUserName();
                newRepository.SaveNew(model);
                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize(Roles = "Xóa,Admin")]
        public ViewResult ConfirmDelete(long id)
        {
            var model =newRepository.GetNewById(id);
            return View(model);
        }

        public ActionResult Delete(long id)
        {
            newRepository.Delete(new TinTuc()
            {
                NewID = id
            });
            return RedirectToAction("Index");
        }

    }
}
    