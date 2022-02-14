using EAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAPI.DataAccess
{
    public interface IOrderDataAccess
    {
        List<OrderItem> PlaceOrder(List<OrderItem> orders);
        OrderItem PlaceOrder(OrderItem order);
        List<OrderItem> GetPagedOrders(int pageSize,int pageNumber);
        List<OrderItem> GetOrders(int userId);
        List<OrderItem> GetOrders(int userId, int orderNumber);

    }
}
