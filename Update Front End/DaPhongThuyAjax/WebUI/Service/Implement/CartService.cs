using Domain.Concrete;
using Domain.Define;
using Domain.Helpers;
using Domain.Models;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implement
{
    public class CartService : ICartService
    {
        DaPhongThuyEF context = new DaPhongThuyEF();

        public SanPham GetProductByID(long id)
        {
            return context.SanPhams.Find(id);
        }

        public void ProsessOrder(ShippingDetailsViewModel shippingDetails, Cart cart)
        {
            var customer = context.KhachHangs.FirstOrDefault(x => x.Email == shippingDetails.Email);
            if (customer == null)
            {
                    customer = new KhachHang() {
                    Ten=shippingDetails.Ten,
                    Email= shippingDetails.Email,
                    DiaChi= shippingDetails.DiaChi,
                    SDT= shippingDetails.SDT,
                    NoiDung= shippingDetails.NoiDung
                };
                context.KhachHangs.Add(customer);                
            }
            else
            {
                customer.Ten = shippingDetails.Ten;
                customer.DiaChi = shippingDetails.DiaChi;
                customer.SDT = shippingDetails.SDT;              
            }
            context.SaveChanges();

            DonHang order = new DonHang()
            {         
                CreateDate=DateTime.Now,
                Status=(int)Define.OrderStatus.ChoXacNhan,
                CustomerId = customer.CustomerID,
                ShipName=customer.Ten,
                ShipAdress = customer.DiaChi,
                ShipEmail=customer.Email,
                ShipMobile=customer.SDT              
            };
            context.DonHangs.Add(order);
            context.SaveChanges();

            foreach (var item in cart.LineCollection)
            {
                ChiTietDonHang  orderDetaisl = new ChiTietDonHang();
                if (item.Product.GiaKhuyenMai > 0)
                {

                    orderDetaisl.OrderId = order.OrderId;
                    orderDetaisl.ProductId = item.Product.ProductID;
                    orderDetaisl.Quantity = item.Quantity;
                    orderDetaisl.Price = item.Product.GiaKhuyenMai;
                    orderDetaisl.CreateDate = DateTime.Now;
                    orderDetaisl.CreateBy = customer.Email;
                    orderDetaisl.Status = (int)Define.OrderStatus.ChoXacNhan;             
                }
                else
                {
                    orderDetaisl.OrderId = order.OrderId;
                    orderDetaisl.ProductId = item.Product.ProductID;
                    orderDetaisl.Quantity = item.Quantity;
                    orderDetaisl.Price = item.Product.Gia;
                    orderDetaisl.CreateDate = DateTime.Now;
                    orderDetaisl.CreateBy = customer.Email;
                    orderDetaisl.Status = (int)Define.OrderStatus.ChoXacNhan;
                }

                context.ChiTietDonHangs.Add(orderDetaisl);                
            }
            context.SaveChanges();

            foreach (var item in cart.LineCollection)
            {
                var model = context.SanPhams.Where(x => x.ProductID == item.Product.ProductID).FirstOrDefault();
                model.SoLuong -= item.Quantity;
            }
            context.SaveChanges();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<ol>");
            foreach (var line in cart.LineCollection)
            {
                if (line.Product.GiaKhuyenMai > 0)
                { 
                stringBuilder.Append("<li>");
                stringBuilder.Append(line.Product.Ten);
                stringBuilder.Append("<span>" + line.Quantity + "x" + line.Product.GiaKhuyenMai);
                stringBuilder.Append("= " + line.Quantity * line.Product.GiaKhuyenMai);
                stringBuilder.Append("</span>");
                }
                else
                {
                    stringBuilder.Append("<li>");
                    stringBuilder.Append(line.Product.Ten);
                    stringBuilder.Append("<span>" + line.Quantity + "x" + line.Product.Gia);
                    stringBuilder.Append("= " + line.Quantity * line.Product.Gia);
                    stringBuilder.Append("</span>");
                }
                stringBuilder.Append("</ol>");
                stringBuilder.Append("<h3>Tổng Tiền : <strong>" + cart.ComputerTotal() + "</strong></h3>");
            }
            EmailHelper emailHelper = new EmailHelper();
            emailHelper.Send(
                "Đơn Hàng" + order.OrderId,
                shippingDetails.Email,
                stringBuilder.ToString(),
                null,
                null);
        }

    }
}
