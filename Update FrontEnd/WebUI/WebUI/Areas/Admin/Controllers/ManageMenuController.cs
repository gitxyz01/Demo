using Domain.Models;
using Service.Implement;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Areas.Admin.Controllers
{
 
    public class ManageMenuController : Controller
    {
        // GET: Admin/ManageMenu
        private IMenuService service;
        private IMenuTypeService menuTypeService;
        public ManageMenuController()
        {
            service = new MenuService();
            menuTypeService = new MenuTypeService();
        }
        // GET: Admin/ManageMenuType

        [Authorize(Roles = "Đọc,Admin")]
        public ActionResult Index(long menuTypeId=1)
        {
            var model = service.GetListMenus(menuTypeId);
            return View(model);
        }


        [Authorize(Roles = "Thêm,Admin")]
        public ViewResult Create()
        {
            ViewBag.MenuType = menuTypeService.GetListMenuType();
            return View();
        }

        [HttpPost]
        public ActionResult Create(MenuViewModel model)
        {
            ViewBag.MenuType = menuTypeService.GetListMenuType();
            var check = service.CheckName(model,1);
            if (check == false)
            {
                ModelState.AddModelError("", "Tên Menu Đã Tồn Tại Hoặc Bỏ Trống");
            }
            else
            {
                service.Create(model);
                return RedirectToAction("Index");
            }
            return View();
        }

        [Authorize(Roles = "Sửa,Admin")]
        public ViewResult Edit(long id)
        {
            ViewBag.MenuType = menuTypeService.GetListMenuType();
            return View(service.GetMenuByID(id));
        }

        [HttpPost]
        public ActionResult Edit(MenuViewModel model)
        {
            ViewBag.MenuType = menuTypeService.GetListMenuType();
            var check = service.CheckName(model,2);
            if (check == false)
            {
                ModelState.AddModelError("", "Bạn Chưa Điền Tên Menu");
            }
            else
            {
                service.Update(model);
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult _ListMenuTypePartial()
        {
            return View(menuTypeService.GetListMenuType());
        }
    }
}