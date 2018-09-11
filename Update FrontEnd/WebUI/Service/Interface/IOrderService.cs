using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IOrderService
    {
        List<OrderViewModel> GetListOrderActive(long customerId=0);
        OrderViewModel GetOrderByID(long id);
        void Delete(long id);
        List<CustomerViewModel> GetListCustomer();
        void Update(OrderViewModel order);
    }
}
