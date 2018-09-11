using Domain.Abtract;
using Domain.Concrete;
using Domain.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace  WebUI.Areas.Admin.Controllers
{
 
    [Authorize]
    public class HomeAdminController : Controller
    {
        private IMainRepository DaPhongThuy;
        private INewCategoryRepository newCategoryRepository;
        public HomeAdminController()
        {
            DaPhongThuy = new Repository();
        }
        public ActionResult Index()
        {
            DaPhongThuy.SetAllSalePriceNull();
            return View();
        }
        public ActionResult _AdminMenuCategoryPartial()
        {
            var model = DaPhongThuy.DanhMucSanPhams.Where(x=>x.TrangThai == true).ToList();
            return PartialView(model);
        }
        public ActionResult _AdminMenuNewCategoryPartial()
        {
            newCategoryRepository = new NewCategoryRepository();
            var model = newCategoryRepository.NewCategory.Where(x=>x.TrangThai==true).ToList();
            return PartialView(model);
        }

   
    }
}