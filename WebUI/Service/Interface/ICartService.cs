using Domain.Concrete;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface ICartService
    {
        SanPham GetProductByID(long id);
        void ProsessOrder(ShippingDetailsViewModel shippingDetails, Cart cart);
    }
}
