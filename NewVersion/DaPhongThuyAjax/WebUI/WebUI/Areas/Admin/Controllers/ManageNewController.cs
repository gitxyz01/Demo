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
using System.Web.Script.Serialization;
using WebUI.Areas.Admin.Models;

namespace WebUI.Areas.Admin.Controllers
{   
    public class ManageNewController : Controller
    {
        
        private INewRepository newRepository;
        public List<DanhMucTinTuc> GetViewbagNewCategory()
        {
            INewCategoryRepository newCategoryRepository = new NewCategoryRepository();
            var newCategory = newCategoryRepository.NewCategory.Where(x => x.ParentID != 0 && x.TrangThai==true).ToList();
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

        public ActionResult LoadData(long id =0)
        {
            var model = newRepository.GetListNewAdmin(id);
            return Json(new
            {
                status = true,
                data = model,
            }, JsonRequestBehavior.AllowGet
            );


        }

        public ActionResult News()
        {
             GetViewbagNewCategory();
            return View();

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveNew(TinTuc model)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                if (model.NewCategoryID == 0)
                {
                    if (!ModelState.IsValid)
                    {
                        string fail = "Có lỗi xảy ra. Vui lòng kiểm tra lại!";

                        return Json(new { status = false, failReason = js.Serialize(fail) });
                    }
                    model.NgayTao = DateTime.Now;
                    newRepository.SaveNew(model);
                    string addSuccess = "Thêm mới Tin Tức thành công!";
                    return Json(new { status = true, success = js.Serialize(addSuccess) });
                }
                if (!ModelState.IsValid)
                {
                    string fail = "Có lỗi xảy ra. Vui lòng kiểm tra lại!";
                    return Json(new { status = false, failReason = js.Serialize(fail) });
                }
                newRepository.SaveNew(model);
                string updateSuccess = "Cập nhật  Tin Tức thành công!";
                return Json(new { status = true, success = js.Serialize(updateSuccess) });
            }
            catch (Exception ex)
            {
                string fail = ex.Message;
                return Json(new { status = false, failReason = js.Serialize(fail) });
            }
        }

        public ActionResult DeleteNewCategory(long id)
        {
            try
            {
                newRepository.Delete(new TinTuc { NewID = id });
                return Json(new
                {
                    status = true,
                    message = "XóaTin Tức Thành Công !"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        //[HttpGet]
        //public ActionResult LoadDropDown(long id = -1)
        //{
        //    var data = newRepository.GetAllNews();
        //    var listParentId = data.Where(x => x.ParentID == 0 && x.TrangThai == true && x.NewCategoryID != id).ToList();
        //    return Json(new
        //    {
        //        status = true,
        //        data = listParentId,
        //    }, JsonRequestBehavior.AllowGet
        //    );
        //}

        public ActionResult LoadDetail(long id)
        {
            var model = newRepository.GetNewByIds(id);
            return Json(
                new
               {
                    status = true,
                    data = model
                }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteNew(long id)
        {
            try
            {
                newRepository.Delete(new TinTuc { NewID = id });
                return Json(new
                {
                    status = true,
                    message = "Xóa Tin Tức Thành Công !"
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    status = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
   
}

    