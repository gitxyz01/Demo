using Domain.Abtract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        private IMainRepository DaPhongThuy;
        public ProductController()
        {
            DaPhongThuy = new Repository();
        }

        public void GetListCategory()
        {
            var model = DaPhongThuy.DanhMucSanPhams.Where(x => x.TrangThai == true).ToList();
            ViewBag.ListCategory = model;
        }
        public void GetNewProductsList()
        {
            var model = DaPhongThuy.SanPhams.Where(x=>x.TrangThai==true).OrderByDescending(x=>x.ProductID).Take(8).ToList();
            ViewBag.NewProducts = model;
        }
        public ActionResult Index(int page = 1, int category = 0, int sortBy = 0)
        {
            
            GetNewProductsList();
            GetListCategory();
            HomeIndexViewModel model = new HomeIndexViewModel();
            if (sortBy == 0)
            {          
            model.Sanphams = DaPhongThuy.SanPhams.Where(x => category == 0 && x.TrangThai == true || category == x.CategoryProductID && x.TrangThai == true)
                .OrderBy(x => x.ProductID)
                .Skip((page - 1) * 10)
                .Take(10)
                .ToList();
                model.CurentSortBy = sortBy;
            model.CurentCategory = category;
            model.TenDanhMuc = DaPhongThuy.DanhMucSanPhams.Where(x => x.CategoryProductID == model.CurentCategory).Select(x => x.Ten).FirstOrDefault();
            model.PagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemPerPage = 10,
                TotalItem = DaPhongThuy.SanPhams.Where(x => category == 0 && x.TrangThai == true || category == x.CategoryProductID && x.TrangThai == true).Count()

            };             
            }
            if (sortBy == 1)
            {

                model.Sanphams = DaPhongThuy.SanPhams.Where(x => category == 0 && x.TrangThai == true || category == x.CategoryProductID && x.TrangThai == true)
                .OrderBy(x => x.Gia)
                .Skip((page - 1) * 10)
                .Take(10)
                .ToList();
                model.CurentSortBy = sortBy;
                model.CurentCategory = category;
                model.TenDanhMuc = DaPhongThuy.DanhMucSanPhams.Where(x => x.CategoryProductID == model.CurentCategory).Select(x => x.Ten).FirstOrDefault();
                model.PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemPerPage = 10,
                    TotalItem = DaPhongThuy.SanPhams.Where(x => category == 0 && x.TrangThai == true || category == x.CategoryProductID && x.TrangThai == true).Count()

                };

            }
             if (sortBy==2){
                model.Sanphams = DaPhongThuy.SanPhams.Where(x => category == 0 && x.TrangThai == true || category == x.CategoryProductID && x.TrangThai == true)
                .OrderByDescending(x => x.Gia)
                .Skip((page - 1) * 10)
                .Take(10)
                .ToList();
                model.CurentSortBy = sortBy;
                model.CurentCategory = category;
                model.TenDanhMuc = DaPhongThuy.DanhMucSanPhams.Where(x => x.CategoryProductID == model.CurentCategory).Select(x => x.Ten).FirstOrDefault();
                model.PagingInfo = new PagingInfo()
                {
                    CurrentPage = page,
                    ItemPerPage = 10,
                    TotalItem = DaPhongThuy.SanPhams.Where(x => category == 0 && x.TrangThai == true || category == x.CategoryProductID && x.TrangThai == true).Count()

                };
            }                       
            return View(model);           
        }
                               
        public ActionResult ProductDetails(int id)
        {
            GetNewProductsList();
            var product = DaPhongThuy.SanPhams.FirstOrDefault(x => x.ProductID == id);
            if (product != null)
            {
                return View(product);
            }
            return View();
        }
        public ActionResult _ProductCategoryPartial()
        {
            var model = DaPhongThuy.DanhMucSanPhams.Where(x=> x.TrangThai == true).ToList();
            return PartialView(model);
        }
        public ActionResult HotProduct(int page = 1)
        {
            GetNewProductsList();
            GetListCategory();
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.Sanphams = DaPhongThuy.SanPhams.Where(x => x.SPHot == true && x.TrangThai == true)
                .OrderBy(x => x.ProductID)
                .Skip((page - 1) * 10)
                .Take(10)
                .ToList();
            model.PagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemPerPage = 10,
                TotalItem = DaPhongThuy.SanPhams.Where(x => x.SPHot == true && x.TrangThai == true).Count()
            };
            return View(model);
        }

        
        public ActionResult SearchProductByPrice( decimal minPrice, decimal maxPrice, int page=1)
        {
            GetNewProductsList();
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.Sanphams = DaPhongThuy.SanPhams.Where(x => x.Gia >= minPrice && x.Gia <= maxPrice).OrderBy(x=>x.Gia)
                .Skip((page-1)*10)
                .Take(10)
                .ToList();
            model.CurentMinPrice = minPrice;
            model.CurentMaxPrice = maxPrice;
            model.PagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemPerPage = 10,
                TotalItem = DaPhongThuy.SanPhams.Where(x => x.Gia >= minPrice && x.Gia <= maxPrice).OrderBy(x => x.Gia).Count()

            };
            return View(model);
        }
        public ActionResult SearchProduct(string searchString, int page = 1)
        {
            GetNewProductsList();
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.Sanphams = DaPhongThuy.SanPhams.Where(x => x.Ten.Contains(searchString)||x.DanhMucSanPham.Ten.Contains(searchString)).OrderBy(x => x.ProductID)
                .Skip((page - 1) * 10)
                .Take(10)
                .ToList();
            ViewBag.CurentSearchString = searchString;
            model.PagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                ItemPerPage = 10,
                TotalItem = DaPhongThuy.SanPhams.Where(x => x.Ten.Contains(searchString)).OrderBy(x => x.ProductID).Count()

            };
            return View(model);
        }
    }
}