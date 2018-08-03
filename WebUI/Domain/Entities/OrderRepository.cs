using Domain.Abtract;
using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderRepository : IOrderRepository
    {
        private DaPhongThuyEF context = new DaPhongThuyEF();

        public IQueryable<DonHang> Orders => context.DonHangs;

        public IQueryable<ChiTietDonHang> Orderdetails => context.ChiTietDonHangs;

        public IQueryable<KhachHang> Customers => context.KhachHangs;

        public void Add(DonHang order)
        {
            context.DonHangs.Add(order);
        }

        public void Add(KhachHang customer)
        {
            context.KhachHangs.Add(customer);
        }

        public void Add(ChiTietDonHang orderDetails)
        {
            context.ChiTietDonHangs.Add(orderDetails);
        }

        public void Delete(DonHang order)
        {
            context.DonHangs.Remove(order);
        }

        public void Delete(KhachHang customer)
        {
            context.KhachHangs.Remove(customer);
        }

        public void Delete(ChiTietDonHang orderDetails)
        {
            context.ChiTietDonHangs.Remove(orderDetails);
        }

        public List<DonHang> GetAllOrder()
        {
            return context.DonHangs.ToList();
        }

        public KhachHang GetCustomerById(long id)
        {
            return context.KhachHangs.Find(id);
        }

        public DonHang GetOrderById(long id)
        {
            return context.DonHangs.Find(id);
        }

        public ChiTietDonHang GetOrderDetailsById(long id)
        {
            return context.ChiTietDonHangs.Find(id);
        }

        public void Save()
        {
            context.SaveChanges();
        }
        List<DonHang> IOrderRepository.GetAllOrderActive()
        {
            return context.DonHangs.Where(x => x.Status != (int)Define.Define.OrderStatus.Huy).ToList();
        }
    }
}
