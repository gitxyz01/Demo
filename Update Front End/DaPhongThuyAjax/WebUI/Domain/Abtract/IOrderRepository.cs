using Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abtract
{
    public interface IOrderRepository
    {
        IQueryable<ChiTietDonHang> Orderdetails { get; }
        IQueryable<KhachHang> Customers { get; }
        IQueryable<DonHang> Orders { get; }

        List<DonHang> GetAllOrder();
        List<DonHang> GetAllOrderActive();
        DonHang GetOrderById(long id);
        void Add(DonHang order);
        void Delete(DonHang order);
        void Save();

        KhachHang GetCustomerById(long id);
        void Add(KhachHang customer);
        void Delete(KhachHang customer);

        ChiTietDonHang GetOrderDetailsById(long id);
        void Add(ChiTietDonHang orderDetails);
        void Delete(ChiTietDonHang orderDetails);
    }
}
