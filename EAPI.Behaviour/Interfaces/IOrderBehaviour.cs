using EAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAPI.Behaviour.Interfaces
{
   public interface IOrderBehaviour
    {
        OrderItem PlaceOrder(OrderItem order);
        List<OrderItem> PlaceOrder(List<OrderItem> order);
        List<OrderItem> GetOrders(int userId);
        List<OrderItem> GetPagedOrders(int pageSize,int pageNumber);
        
    }
}
