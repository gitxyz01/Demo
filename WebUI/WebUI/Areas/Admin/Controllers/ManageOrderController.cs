using Domain.Abtract;
using Domain.Define;
using Domain.Entities;
using Domain.Helpers;
using Domain.Models;
using Service.Implement;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Domain.Define.Define;

namespace WebUI.Areas.Admin.Controllers
{
    public class ManageOrderController : Controller
    {
        private IOrderService service;


        public ManageOrderController()
        {
            service = new OrderService();          
        }

        private void GetDropdowlistOrderStatus(OrderStatus option = OrderStatus.ChoXacNhan)
        {
            IEnumerable<OrderStatus> values = Enum.GetValues(typeof(OrderStatus)).Cast<OrderStatus>();
            IEnumerable<SelectListItem> items = from value in values
                                                select new SelectListItem
                                                {
                                                    Text = EnumHelper.GetDescriptionFromEnum((OrderStatus)value),
                                                    Value = ((int)value).ToString(),
                                                    Selected=value==option,
                                                };
            ViewBag.GetDropdowlistOrderStatus = items;
        }

        // GET: Admin/ManageOrder
        public ActionResult Index(long customerId=0)
        {
            GetDropdowlistOrderStatus();
            var model = service.GetListOrderActive(customerId);
            return View(model);
        }

        public ActionResult OrderDetails(long orderId)
        {
            var model = service.GetOrderByID(orderId);

            return View(model);
        }

        public ViewResult ConfirmDelete()
        {
            return View();
        }

        public ActionResult Delete(long orderId)
        {
            service.Delete(orderId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(OrderViewModel model)
        {
            GetDropdowlistOrderStatus();
            service.Update(model);
            return RedirectToAction("Index");
        }

        public ActionResult ListCustomers()
        {
            var model = service.GetListCustomer();
            return View(model);
        }
    }
}