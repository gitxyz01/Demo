using Domain.Abtract;
using Domain.Concrete;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{

    public class ManageCategoryController : Controller
    {
        private IMainRepository DaPhongThuy;
        public ManageCategoryController()
        {
            DaPhongThuy = new Repository();
        }
        // GET: Admin/ManageCategory
        [Authorize(Roles = "Đọc,Admin")]
        public ActionResult Index()
        {
            AdminProductListModel model = new AdminProductListModel();
            model.DanhMucSanPhams = DaPhongThuy.DanhMucSanPhams.Where(x=>x.TrangThai == true)
                .OrderBy(x => x.ThuTuHienThi)
                .ToList();
            return View(model);
        }

        [Authorize(Roles = "Thêm,Admin")]
        public ViewResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(DanhMucSanPham danhMuc)
        {
            danhMuc.NguoiTao = User.Identity.GetUserName();
            danhMuc.NgayTao = DateTime.Now;
            danhMuc.TrangThai = true;
            if (string.IsNullOrWhiteSpace(danhMuc.Ten))
            {
                ModelState.AddModelError("", "Vui Lòng Nhập Tên Danh Mục");
            }
            if (ModelState.IsValid)
            {
                DaPhongThuy.SaveCategory(danhMuc);
                return RedirectToAction("Index");
            }
            return View();
        }
        [Authorize(Roles = "Sửa,Admin")]
        public ViewResult EditCategory(int id)
        {
            var model = DaPhongThuy.DanhMucSanPhams.FirstOrDefault(x => x.CategoryProductID == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditCategory(DanhMucSanPham danhSach)
        {
            if (ModelState.IsValid)
            {
                DaPhongThuy.SaveCategory(danhSach);
                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize(Roles = "Xóa,Admin")]
        public ActionResult DeleteCategory(int id)
        {
            DaPhongThuy.DeleteCategory(new DanhMucSanPham { CategoryProductID = id });
            return RedirectToAction("Index");
        }
    }

}