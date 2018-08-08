using Domain.Define;
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
    public class ManageMenuTypeController : Controller
    {

        private IMenuTypeService service;
        public ManageMenuTypeController()
        {
            service = new MenuTypeService();
        }

        // GET: Admin/ManageMenuType

        [Authorize(Roles = "Đọc,Admin")]
        public ActionResult Index()
        {
            var model = service.GetListMenuType();
            return View(model);
        }

        [Authorize(Roles = "Thêm,Admin")]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(MenuTypeViewModel model)
        {
            var check = service.CheckName(model,(int)Define.CheckName.Create);
            if (check == false)
            {
                ModelState.AddModelError("","Tên Menu Đã Tồn Tại Hoặc Bỏ Trống");
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
            return View(service.GetMenuTypeByID(id));
        }

        [HttpPost]
        public ActionResult Edit(MenuTypeViewModel model)
        {
            var check = service.CheckName(model,(int)Define.CheckName.Update);
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
    }
}