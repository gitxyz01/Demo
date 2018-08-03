using Domain.Abtract;
using Domain.Concrete;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Index(int DanhmucId = 0, int page = 1)
        {
            
            AdminProductListModel model = new AdminProductListModel();
            model.SanPhams = DaPhongThuy.SanPhams.Where(x => DanhmucId == 0&&x.TrangThai==true || DanhmucId == x.CategoryProductID && x.TrangThai == true)        
                .ToList();
            return View(model);
        }


        public ActionResult HotProduct()
        {
            AdminProductListModel model = new AdminProductListModel();
            model.SanPhams = DaPhongThuy.SanPhams.Where(x => x.SPHot == true && x.TrangThai == true).ToList();
            return View(model);
        }
        public ActionResult AddProduct()
        {
            var danhMuc = DaPhongThuy.DanhMucSanPhams.Where(x => x.TrangThai == true).ToList();
            ViewBag.DanhMuc = danhMuc;
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(SanPham sanPham)
        {
           
            var danhMuc = DaPhongThuy.DanhMucSanPhams.Where(x=>x.TrangThai==true).ToList();
            ViewBag.DanhMuc = danhMuc;
            if (string.IsNullOrWhiteSpace(sanPham.Ten))
            {
                ModelState.AddModelError("", "Vui long nhap ten san pham");
            }
            if (Request.Files.Count == 0 || Request.Files[0].ContentLength == 0)
            {
                ModelState.AddModelError("", "vui long chon file");
            }
            if (ModelState.IsValid)
            {
                var file = Request.Files[0];
                string root = Server.MapPath("~/Uploads");
                file.SaveAs(Path.Combine(root, file.FileName));
                sanPham.HinhAnh = "/Uploads/" + file.FileName;
                DaPhongThuy.SaveProduct(sanPham);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ViewResult EditProduct(int id)
        {
            var danhMuc = DaPhongThuy.DanhMucSanPhams.Where(x => x.TrangThai == true).ToList();
            ViewBag.DanhMuc = danhMuc;
            var model = DaPhongThuy.SanPhams.FirstOrDefault(x => x.ProductID == id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditProduct(SanPham sanPham)
        {
            var danhMuc = DaPhongThuy.DanhMucSanPhams.Where(x => x.TrangThai == true).ToList();
            ViewBag.DanhMuc = danhMuc;
            if (string.IsNullOrWhiteSpace(sanPham.Ten))
            {
                ModelState.AddModelError("", "Vui long nhap ten san pham");
            }
            if (ModelState.IsValid)
            {
                if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
                {
                    try
                    {
                        FileInfo f = new FileInfo(Server.MapPath("~" + sanPham.HinhAnh));
                        f.Delete();
                    }
                    catch (Exception ex)
                    {
                        
                    }
                    var file = Request.Files[0];
                    string root = Server.MapPath("~/Uploads");
                    file.SaveAs(Path.Combine(root, file.FileName));
                    sanPham.HinhAnh = "/Uploads/" + file.FileName;
                }
                DaPhongThuy.SaveProduct(sanPham);
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult DeleteProduct(int id)
        {
            DaPhongThuy.DeleteProduct(new SanPham() { ProductID = id });
            return RedirectToAction("Index");
        }
        public PartialViewResult Category()
        {
            int totalSPHot;
            totalSPHot = DaPhongThuy.SanPhams.Where(x => x.SPHot == true && x.TrangThai == true).Count();
            ViewBag.TotalSPHot = totalSPHot;
            var listCategory = DaPhongThuy.DanhMucSanPhams.Where(x => x.TrangThai == true).ToList();
            return PartialView(listCategory);
        }
    }
}