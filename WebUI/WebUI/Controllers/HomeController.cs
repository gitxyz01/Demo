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
    public class HomeController : Controller
    {
        private IMainRepository DaPhongThuy;

        
        public HomeController()
        {
            DaPhongThuy = new Repository();
        }        
        public void HotProductFeature()
        {           
             var model = DaPhongThuy.SanPhams.Where(x => x.SPHot == true && x.TrangThai == true)
                .OrderBy(x => x.ProductID)
                .Take(8)
                .ToList();
            ViewBag.HotProducts = model;            
        }
        public ActionResult Index()
        {
            DaPhongThuy.RestoreAll();
            var model = DaPhongThuy.SanPhams.Where(x=>x.TrangThai == true)
                .OrderByDescending(x => x.ProductID)
                .Take(10)
                .ToList();
            ViewBag.NewProducts = model;
            HotProductFeature();
            
            return View();
        }      
        public ActionResult _HomeHotProductPartial()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.Sanphams = DaPhongThuy.SanPhams.Where(x => x.SPHot == true && x.TrangThai == true)
                .OrderBy(x => x.ProductID)
                .Take(8)
                .ToList();
            ViewBag.HotProducts = model.Sanphams;
            return PartialView(model);
        }
        public ActionResult _HomeMenuCategoryPartial()
        {
            var model = DaPhongThuy.DanhMucSanPhams.Where(x=> x.TrangThai == true).ToList();
            return PartialView(model);
        }
        public ActionResult _LeftMenuNewProductPartial()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.Sanphams = DaPhongThuy.SanPhams.Where(x=>x.TrangThai==true).OrderByDescending(x => x.ProductID).Take(10).ToList();
            return PartialView(model);
        }       
        public ActionResult _HomeSaleProductPartial()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            model.Sanphams = DaPhongThuy.SanPhams.Where(x => x.GiaKhuyenMai > 0 && x.TrangThai == true).ToList();
            return PartialView(model);
        }        

        public ActionResult _HeaderPartial()
        {
            var model = DaPhongThuy.DanhMucSanPhams.Where(x=>x.TrangThai==true).ToList();
            return PartialView(model);
        }
    }
}