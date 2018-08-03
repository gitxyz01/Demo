using Domain.Abtract;
using Domain.Concrete;
using Domain.Define;
using Domain.Entities;
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
    public class OrderService: IOrderService
    {
        private IOrderRepository orderRepository = new OrderRepository();
        private IMainRepository productRepository = new Repository();

        public void Delete(long id)
        {
            var orderToDelete = orderRepository.GetOrderById(id);
            orderRepository.Delete(orderToDelete);
            var listOrderDetailsToDelete = orderRepository.Orderdetails.Where(x => x.OrderId == id).ToList();
            foreach (var item in listOrderDetailsToDelete)
            {
                orderRepository.Delete(item);
                var product = productRepository.SanPhams.Where(x => x.ProductID == item.ProductId).FirstOrDefault();
                product.SoLuong += item.Quantity;
            }
            orderRepository.Save();
            productRepository.save();           
        }

        public List<CustomerViewModel> GetListCustomer()
        {
            return orderRepository.Customers.Where(x=>x.DonHangs.Count()>0).Select(x => new CustomerViewModel()
            {
                CustomerID = x.CustomerID,
                Ten = x.Ten,
                Email = x.Email,
                DiaChi = x.DiaChi,
                SDT = x.SDT,
            }).ToList();
        }

        public List<OrderViewModel> GetListOrderActive(long customerId=0)
        {

            List<OrderViewModel> model = orderRepository.Orders.Where(x=>customerId==0||x.CustomerId==customerId).ToList().Select(x => new OrderViewModel() {
                OrderId = x.OrderId,
                CustomerId = x.CustomerId,
                ShipName = x.ShipName,
                ShipAdress = x.ShipAdress,
                ShipEmail = x.ShipEmail,
                ShipMobile = x.ShipMobile,
                CreateBy = x.CreateBy,
                CreateDate = x.CreateDate,
                Status = EnumHelper.GetDescriptionFromEnum((Define.OrderStatus)x.Status),
                ModifiedBy = x.ModifiedBy,
                ModifiedDate = x.ModifiedDate,                
            }).ToList();
            return model;


        }

        public OrderViewModel GetOrderByID(long id)
        {
            var x =  orderRepository.GetOrderById(id);
            OrderViewModel model = new OrderViewModel() {
                OrderId = x.OrderId,
                CustomerId = x.CustomerId,
                ShipName = x.ShipName,
                ShipAdress = x.ShipAdress,
                ShipEmail = x.ShipEmail,
                ShipMobile = x.ShipMobile,
                CreateBy = x.CreateBy,
                CreateDate = x.CreateDate,
                IntStatus=x.Status,
                Status=EnumHelper.GetDescriptionFromEnum((Define.OrderStatus)x.Status),
                Note=x.Note,                                              
            };
            model.ListOrderDetails = orderRepository.Orderdetails.Where(z => z.OrderId == model.OrderId).ToList();
            decimal total = model.ListOrderDetails.Sum(a => a.Quantity * a.SanPham.Gia).GetValueOrDefault();
            model.TotalOrder = String.Format(System.Globalization.CultureInfo.GetCultureInfo("vi-VN"), "{0:C0}", total);
            return model;
        }

        public void Update(OrderViewModel order)
        {
            var orderToUpdate = orderRepository.GetOrderById(order.OrderId);
            if (orderToUpdate != null)
            {
                orderToUpdate.Status = order.IntStatus;
                orderRepository.Save();
            }
        }
    }
}
