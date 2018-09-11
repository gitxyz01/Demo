using Domain.Abtract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class NewController : Controller
    {
        private INewCategoryRepository newCategoryRepository = new NewCategoryRepository();
        public void GetViewBagNewCategory(int newOrBlog =1)
        {
            if (newOrBlog == 1)
            {
                var model = newCategoryRepository.GetAllActive("News");
                ViewBag.ListNewCategory = model;
            }
            else
            {
                var model = newCategoryRepository.GetAllActive("Blogs");
                ViewBag.ListNewCategory = model;
            }
        }



        private INewRepository newRepository;
        
        public NewController()
        {
            newRepository = new NewRepository();
        }

        public void GetViewBagRecentNew(int newOrBlog = 1)
        {
            if (newOrBlog == 1)
            {           
            var model = newRepository.News.Where(x=>x.TrangThai==true&&x.DanhMucTinTuc.ThuTuHienThi < 1000).OrderByDescending(x => x.NewID)
                .Take(3).ToList();
            ViewBag.ListRecentNews = model;
            }
            else
            {
                var model = newRepository.News.Where(x => x.TrangThai == true && x.DanhMucTinTuc.ThuTuHienThi >= 1000 && x.DanhMucTinTuc.ThuTuHienThi < 2000).OrderByDescending(x => x.NewID)
                .Take(3).ToList();
                ViewBag.ListRecentNews = model;
            }
        }   

        public void GetViewBagRelateNew(long? id, int newOrBlog=1)
        {
            if (newOrBlog == 1) { 
            var model = newRepository.News.Where(x=>x.NewCategoryID==id&&x.TrangThai==true&&x.NewID!=id && x.DanhMucTinTuc.ThuTuHienThi < 1000).OrderByDescending(x => x.NewID).Take(3).ToList();
            ViewBag.ListRelateNews = model;
            }
            else
            {
                var model = newRepository.News.Where(x => x.NewCategoryID == id && x.TrangThai == true && x.NewID != id && x.DanhMucTinTuc.ThuTuHienThi >= 1000).OrderByDescending(x => x.NewID).Take(3).ToList();
                ViewBag.ListRelateNews = model;
            }
        }

        // GET: New
        public ActionResult Index(int page=1, long newCategoryId=0, int newOrBlog=1)
        {
            if (newOrBlog == 1)
            {           
            GetViewBagRecentNew(1);
            GetViewBagNewCategory(1);
           
            var model =newRepository.GetAllNewCategoryIndex(page,"News",newCategoryId);
            return View(model);
            }
            else
            {
                GetViewBagRecentNew(2);
                GetViewBagNewCategory(2);
                var model = newRepository.GetAllNewCategoryIndex(page, "Blog", newCategoryId);
                return View(model);
            }
        }

        public ActionResult Details(long id)
        {
            var model = newRepository.GetNewById(id);         
            if (model != null && model.DanhMucTinTuc.ThuTuHienThi < 1000)
            {
                GetViewBagNewCategory();
                GetViewBagRecentNew();
                GetViewBagRelateNew(model.NewCategoryID);
                model.LuotXem += 1;
                newRepository.SaveNew(model);
                return View(model);
            }
            if (model != null && model.DanhMucTinTuc.ThuTuHienThi >=1000)
            {
                GetViewBagNewCategory(2);
                GetViewBagRecentNew(2);              
                GetViewBagRelateNew(model.NewCategoryID,2);
                model.LuotXem += 1;
                newRepository.SaveNew(model);
                return View(model);
            }
            else return View();           
        }

        public ViewResult About()
        {
            var model = newRepository.GetAboutNew();
            if (model != null)
            {
                return View(model);
            }
            else
            {
                return View("NotFound");
            }
        }
       
    }
}