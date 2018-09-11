using Domain.Abtract;
using Domain.Concrete;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebUI.Areas.Admin.Models;
using WebUI.Models;

namespace WebUI.Areas.Admin.Controllers
{
    public class ManageProductController : Controller
    {
        private IMainRepository DaPhongThuy;
        public ManageProductController()
        {
            DaPhongThuy = new Repository();
        }
        // GET: Admin/ManageProduct

        [Authorize(Roles = "Đọc,Admin")]
        public ActionResult Index(int DanhmucId = 0, int page = 1)
        {

            AdminProductListModel model = new AdminProductListModel();
            model.SanPhams = DaPhongThuy.SanPhams.Where(x => DanhmucId == 0 && x.TrangThai == true || DanhmucId == x.CategoryProductID && x.TrangThai == true)
                .ToList();
            return View(model);
        }


        [Authorize(Roles = "Đọc,Admin")]
        public ActionResult HotProduct()
        {
            AdminProductListModel model = new AdminProductListModel();
            model.SanPhams = DaPhongThuy.SanPhams.Where(x => x.SPHot == true && x.TrangThai == true).ToList();
            return View(model);
        }

        [Authorize(Roles = "Thêm,Admin")]
        [ValidateInput(false)]
        public ActionResult AddProduct()
        {
            var danhMuc = DaPhongThuy.DanhMucSanPhams.Where(x => x.TrangThai == true).ToList();
            ViewBag.DanhMuc = danhMuc;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddProduct(SanPham sanPham)
        {

            var danhMuc = DaPhongThuy.DanhMucSanPhams.Where(x => x.TrangThai == true).ToList();
            ViewBag.DanhMuc = danhMuc;
            if (string.IsNullOrWhiteSpace(sanPham.Ten))
            {
                ModelState.AddModelError("", "Bạn Chưa Nhập Tên Sản Phẩm");
            }
            //if (Request.Files.Count == 0 || Request.Files[0].ContentLength == 0)
            //{
            //    ModelState.AddModelError("", "vui long chon file");
            //}
            if (ModelState.IsValid)
            {
                //var file = Request.Files[0];
                //string root = Server.MapPath("~/Uploads");
                //file.SaveAs(Path.Combine(root, file.FileName));
                //sanPham.HinhAnh = "/Uploads/" + file.FileName;
                DaPhongThuy.SaveProduct(sanPham);
                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize(Roles = "Sửa,Admin")]
        public ViewResult EditProduct(int id)
        {
            var danhMuc = DaPhongThuy.DanhMucSanPhams.Where(x => x.TrangThai == true).ToList();
            ViewBag.DanhMuc = danhMuc;
            var model = DaPhongThuy.SanPhams.FirstOrDefault(x => x.ProductID == id);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditProduct(SanPham sanPham)
        {
            var danhMuc = DaPhongThuy.DanhMucSanPhams.Where(x => x.TrangThai == true).ToList();
            ViewBag.DanhMuc = danhMuc;
            if (string.IsNullOrWhiteSpace(sanPham.Ten))
            {
                ModelState.AddModelError("", "Bạn Chưa Nhập Tên Sản Phẩm");
            }
            if (ModelState.IsValid)
            {
                //if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                //{
                //    try
                //    {
                //        FileInfo f = new FileInfo(Server.MapPath("~" + sanPham.HinhAnh));
                //        f.Delete();
                //    }
                //    catch (Exception ex)
                //    {

                //    }
                //    var file = Request.Files[0];
                //    string root = Server.MapPath("~/Uploads");
                //    file.SaveAs(Path.Combine(root, file.FileName));
                //    sanPham.HinhAnh = "/Uploads/" + file.FileName;
                //}
                DaPhongThuy.SaveProduct(sanPham);
                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize(Roles = "Xóa,Admin")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                DaPhongThuy.DeleteProduct(new SanPham { ProductID = id });
                return Json(new
                {
                    status = true,
                    message = "Xóa Sản Phẩm Thành Công !"
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
        public PartialViewResult Category()
        {
            int totalSPHot;
            totalSPHot = DaPhongThuy.SanPhams.Where(x => x.SPHot == true && x.TrangThai == true).Count();
            ViewBag.TotalSPHot = totalSPHot;
            var listCategory = DaPhongThuy.DanhMucSanPhams.Where(x => x.TrangThai == true).ToList();
            return PartialView(listCategory);
        }

        public ActionResult LoadData(long categoryId = 0)
        {
            var products = DaPhongThuy.SanPhams.Where(x => categoryId == 0 || x.CategoryProductID == categoryId).Select(x => new ProductViewModel()
            {
                ProductID =x.ProductID,
                CategoryId = x.CategoryProductID,
                Ten = x.Ten,
                MaSP = x.MaSP,
                Mota = x.Mota,
                HinhAnh = x.HinhAnh,
                Gia = x.Gia,
                SoLuongNhap = x.SoLuong,
                TrangThai = x.TrangThai,
                CategoryProduct = x.DanhMucSanPham.Ten,
                SPHot=x.SPHot,
                NgayTao=x.NgayTao.ToString(),
                NgayChinhSua=x.NgayChinhSua.ToString(),
            }).ToList();

            return Json(new
            {
                status = true,
                data = products
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Products()
        {
            var danhMuc = DaPhongThuy.DanhMucSanPhams.Where(x => x.TrangThai == true).ToList();
            ViewBag.DanhMuc = danhMuc;
            return View();
        }

        [HttpPost]
        public ActionResult SaveProduct(SanPham model)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                if (model.ProductID == 0)
                {
                    if (!ModelState.IsValid)
                    {
                        string fail = "Có lỗi xảy ra. Vui lòng kiểm tra lại!";

                        return Json(new { status = false, failReason = js.Serialize(fail) });
                    }
                    model.NgayTao = DateTime.Now;
                    DaPhongThuy.SaveProduct(model);
                    string addSuccess = "Thêm mới Sản Phẩm thành công!";
                    return Json(new { status = true, success = js.Serialize(addSuccess) });
                }
                if (!ModelState.IsValid)
                {
                    string fail = "Có lỗi xảy ra. Vui lòng kiểm tra lại!";
                    return Json(new { status = false, failReason = js.Serialize(fail) });
                }
                DaPhongThuy.SaveProduct(model);
                string updateSuccess = "Cập nhật Sản Phẩm thành công!";
                return Json(new { status = true, success = js.Serialize(updateSuccess) });
            }
            catch (Exception ex)
            {
                string fail = ex.Message;
                return Json(new { status = false, failReason = js.Serialize(fail) });
            }
        }

        public ActionResult GetDetail(long id)
        {
            var model = DaPhongThuy.SanPhams.Where(x => x.ProductID == id).Select(x => new ProductViewModel()
            {
                ProductID=x.ProductID,
                CategoryId = x.CategoryProductID,
                Ten = x.Ten,
                MaSP = x.MaSP,
                Mota = x.Mota,
                HinhAnh = x.HinhAnh,
                Gia = x.Gia,
                SoLuongNhap = x.SoLuong,
                TrangThai = x.TrangThai,
                CategoryProduct = x.DanhMucSanPham.Ten,
                NoiDungSanPham=x.NoiDungSanPham,
                SPHot=x.SPHot
            }).FirstOrDefault();
            if (model != null)
            {
                return Json(new
                {
                    status = true,
                    data = model
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, message ="Không Tìm Thấy Sản Phẩm Bạn Yêu Cầu !" });
        }
    }
}