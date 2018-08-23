using Domain.Concrete;
using Domain.Models;
using Service.Implement;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private ICartService service { get; set; }
        public CartController()
        {
            service = new CartService();
        }
        private Cart getCart()
        {
            Cart cart = Session["cart"] as Cart;
            if (cart == null)
            {
                cart = new Cart();
                Session["cart"] = cart;
            }
            return cart;
        }


        // GET: Cart
        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpPost]
        public ActionResult AddToCart(long id)
        {
            var cart = getCart();
            var product = service.GetProductByID(id);
            if (product != null)
            { 
            cart.Add(product, 1);
            return Redirect("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Update(long id,int quantity)
        {
            var cart = getCart();
            var product = service.GetProductByID(id);
            if (product != null)
            {
                cart.Update(product, quantity);
                return View("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Delete(long id)
        {
            var cart = getCart();
            var product = service.GetProductByID(id);
            if (product != null)
            {
                cart.Delete(product);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ViewResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckOut(ShippingDetailsViewModel shippingDetails)
        {
            Cart cart = getCart();
            if (cart == null || cart.LineCollection.Count == 0)
                {
                    ModelState.AddModelError("", "Giỏ Hàng Của Bạn Trống");
                }
                                        
            if (ModelState.IsValid)
            {
                service.ProsessOrder(shippingDetails, cart);
                cart.Clear();
                Session["Cart"] = null;
                return RedirectToAction("Complete","Home");
            }
            return View();
        }

        [HttpGet]
        public JsonResult AjaxCartIndex()
        {
            var model = getCart();

            return Json(new { data = (from x in model.LineCollection select new {
                Id = x.Product.ProductID,
                TenSP = x.Product.Ten,
                SoLuong = x.Quantity,
                DonGia = String.Format(System.Globalization.CultureInfo.GetCultureInfo("vi-VN"), "{0:C0}", x.Product.Gia) ,
                GiaKhuyenMai = String.Format(System.Globalization.CultureInfo.GetCultureInfo("vi-VN"), "{0:C0}", x.Product.GiaKhuyenMai),
                HinhAnh = x.Product.HinhAnh,
                ThanhTien = String.Format(System.Globalization.CultureInfo.GetCultureInfo("vi-VN"), "{0:C0}",(x.Product.Gia*x.Quantity)),
                TongSoLuong = model.ComputerTotalQuantity(),
                TongThanhTien = String.Format(System.Globalization.CultureInfo.GetCultureInfo("vi-VN"), "{0:C0}",model.ComputerTotal())
            })
            },
                JsonRequestBehavior.AllowGet
            );
        }
        public ActionResult TestAjaxCart()
        {
            return View();
        }
    }
}