using Domain.Abtract;
using Domain.Concrete;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
        [Authorize(Roles = "Sửa,Admin")]
        public ActionResult EditCategory(int id)
        {
            var model = DaPhongThuy.DanhMucSanPhams.Where(x => x.CategoryProductID == id).Select(x=> new CategoryModel() {

                CategoryId = x.CategoryProductID,
                Ten=x.Ten,
                ThuTuHienThi=x.ThuTuHienThi,
                TrangThai=x.TrangThai,
            }).FirstOrDefault();

            return Json(new
            {
                status = true,
                data = model
            }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Xóa,Admin")]
        public ActionResult DeleteCategory(int id)
        {
            try { 
            DaPhongThuy.DeleteCategory(new DanhMucSanPham { CategoryProductID = id });
                return Json(new
                {
                    status = true,
                    message = "Xóa Danh Mục Sản Phẩm Thành Công !"
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
        public ActionResult Categories()
        {
            return View();
        }

        public ActionResult LoadData()
        {
            var model = DaPhongThuy.DanhMucSanPhams.Select(x => new CategoryModel() {
                CategoryId = x.CategoryProductID,
                Ten = x.Ten,
                ThuTuHienThi = x.ThuTuHienThi,
                TrangThai = x.TrangThai,
                NgayTao = x.NgayTao.ToString(),
                NgayChinhSua=x.NgayChinhSua.ToString(),
                NguoiTao=x.NguoiTao,
                NguoiChinhSua=x.NguoiChinhSua,
                SoLuongSanPham = x.SanPhams.Count()
            }).ToList();
            return Json(new
            {
                status = true,
                data = model
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SaveCategory(DanhMucSanPham model)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                if (model.CategoryProductID == 0)
                {
                    if (!ModelState.IsValid)
                    {
                        string fail = "Có lỗi xảy ra. Vui lòng kiểm tra lại!";

                        return Json(new { status = false, failReason = js.Serialize(fail) });
                    }
                    model.NgayTao = DateTime.Now;
                    DaPhongThuy.SaveCategory(model);
                    string addSuccess = "Thêm mới Danh Mục Sản Phẩm thành công!";
                    return Json(new { status = true, success = js.Serialize(addSuccess) });
                }
                if (!ModelState.IsValid)
                {
                    string fail = "Có lỗi xảy ra. Vui lòng kiểm tra lại!";
                    return Json(new { status = false, failReason = js.Serialize(fail) });
                }
                DaPhongThuy.SaveCategory(model);
                string updateSuccess = "Cập nhật Danh Mục Sản Phẩm thành công!";
                return Json(new { status = true, success = js.Serialize(updateSuccess) });
            }
            catch (Exception ex)
            {
                string fail = ex.Message;
                return Json(new { status = false, failReason = js.Serialize(fail) });
            }

        }
    }

}