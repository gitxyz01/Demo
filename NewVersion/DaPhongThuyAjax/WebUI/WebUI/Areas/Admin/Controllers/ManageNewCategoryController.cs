using Domain.Abtract;
using Domain.Concrete;
using Domain.Entities;
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
    public class ManageNewCategoryController : Controller
    {
        private INewCategoryRepository newCategoryRepository;
        public ManageNewCategoryController()
        {
            newCategoryRepository = new NewCategoryRepository();
        }
        // GET: Admin/ManageNewCategory

        [Authorize(Roles = "Đọc,Admin")]
        public ActionResult Index()
        {
            AdminCategoryViewModel model = new AdminCategoryViewModel();
            model.ListNewCategory = newCategoryRepository.NewCategory.Where(x=>x.TrangThai==true).ToList();      
            return View(model);
            
        }

        //[Authorize(Roles = "Thêm,Admin")]
        //public ViewResult Create()
        //{
        //    return View();
        //}
        
        //[HttpPost]
        //public ActionResult Create(DanhMucTinTuc model)
        //{
        //    if (string.IsNullOrWhiteSpace(model.Ten))
        //    {
        //        ModelState.AddModelError("", "Vui lòng nhập tên danh mục");
        //    }
        //    if (ModelState.IsValid)
        //    {
        //        model.NguoiTao = User.Identity.GetUserName();
        //        newCategoryRepository.Save(model);
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        //[Authorize(Roles = "Sửa,Admin")]
        //public ViewResult Edit(long id)
        //{
        //    var model = newCategoryRepository.GetById(id);

        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult Edit(DanhMucTinTuc model)
        //{
        //    if (string.IsNullOrWhiteSpace(model.Ten))
        //    {
        //        ModelState.AddModelError("", "Vui lòng nhập tên danh mục");
        //    }
        //    if (ModelState.IsValid)
        //    {
                
        //        model.NguoiChinhSua = User.Identity.GetUserName();
        //        newCategoryRepository.Save(model);
        //        return RedirectToAction("Index");
        //    }
        //    return View();            
        //}

        //[Authorize(Roles = "Xóa,Admin")]
        //public ViewResult ConfirmDelete(long id)
        //{
        //    var model = newCategoryRepository.GetById(id);
        //    return View(model);
        //}

        public ActionResult Delete(long id)
        {
            newCategoryRepository.Delete(new DanhMucTinTuc()
            {
                NewCategoryID = id
            });
            return RedirectToAction("Index");
        }       

        public ActionResult LoadData()
        {
            var model = newCategoryRepository.GetNewCategories();
            var data1 = model.OrderBy(x => x.ParentID).ThenBy(x => x.Ten).ToList();

            var stack = new Stack<NewCategoryViewModel>();
            foreach (var section in model.Where(x => x.ParentID == default(long)).Reverse())
            {
                stack.Push(section);
                data1.RemoveAt(0);
            }
            var output = new List<NewCategoryViewModel>();
            while (stack.Any())
            {
                var currentSection = stack.Pop();
                var children = model.Where(x => x.ParentID == currentSection.NewCategoryID).Reverse();
                foreach (var section in children)
                {
                    stack.Push(section);
                    data1.Remove(section);
                }
                output.Add(currentSection);
            }
            data1 = output;
            return Json(new
            {
                status = true,
                data = data1,
            }, JsonRequestBehavior.AllowGet
            );


        }

        public ActionResult NewCategories()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveNewCategory(DanhMucTinTuc model)
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
                    newCategoryRepository.Save(model);
                    string addSuccess = "Thêm mới Danh Mục Tin Tức thành công!";
                    return Json(new { status = true, success = js.Serialize(addSuccess) });
                }
                if (!ModelState.IsValid)
                {
                    string fail = "Có lỗi xảy ra. Vui lòng kiểm tra lại!";
                    return Json(new { status = false, failReason = js.Serialize(fail) });
                }
                newCategoryRepository.Save(model);
                string updateSuccess = "Cập nhật Danh Mục Tin Tức thành công!";
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
                newCategoryRepository.Delete(new DanhMucTinTuc { NewCategoryID = id });
                return Json(new
                {
                    status = true,
                    message = "Xóa Danh Mục Tin Tức Thành Công !"
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

        [HttpGet]
        public ActionResult LoadDropDown(long id=-1)
        {
            var data = newCategoryRepository.GetNewCategories();
            var listParentId = data.Where(x => x.ParentID == 0&& x.TrangThai == true && x.NewCategoryID!=id).ToList();
            return Json(new
            {
                status = true,
                data = listParentId,
            }, JsonRequestBehavior.AllowGet
            );
        }

        public ActionResult LoadDetails(long id)
        {
            var model = newCategoryRepository.LoadDetail(id);
            return Json(
                new
                { status = true,
                    data = model
            },JsonRequestBehavior.AllowGet);
        }
    }
}