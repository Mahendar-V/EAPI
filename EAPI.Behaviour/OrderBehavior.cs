using EAPI.Behaviour.Interfaces;
using EAPI.Data;
using EAPI.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAPI.Behaviour
{
    public class OrderBehavior : IOrderBehaviour
    {
        private readonly IOrderDataAccess _datAccess; 
        public OrderBehavior(IOrderDataAccess dataAccess)
        {
            _datAccess = dataAccess;
        }

        public List<OrderItem> GetOrders(int userId)
        {
           return _datAccess.GetOrders(userId);
        }

        public List<OrderItem> GetPagedOrders(int pageSize, int pageNumber)
        {
            return _datAccess.GetPagedOrders(pageSize, pageNumber);
        }

        public OrderItem PlaceOrder(OrderItem order)
        {
            if (order != null)
            {
              return  _datAccess.PlaceOrder(order);
            }
            throw new Exception("order must not be empty.!");
        }

        public List<OrderItem> PlaceOrder(List<OrderItem> orders)
        {
            if (orders != null)
            {
                return _datAccess.PlaceOrder(orders);
            }
            throw new Exception("orders must not be empty.!");
        }
    }
}
