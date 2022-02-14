using EAPI.Data;
using EAPI.DataAccess.Entities;
using EAPI.DataModels.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAPI.DataAccess
{
    public class OrderDataAccess : IOrderDataAccess
    {
        private readonly EAPIContext _dbContext;
        public OrderDataAccess(EAPIContext context)
        {
            _dbContext = context;
        }
        private bool IsOrderValid(OrderItem curOrder)
        {
            if (curOrder.ProductId > 0 && curOrder.UserId > 0)
            {
                if(! _dbContext.Products.Any(s => s.PId == curOrder.ProductId))
                throw new NotFoundError("One of the Item in ur order list was not present. please try again!");
                if (!_dbContext.Users.Any(s => s.UserId == curOrder.UserId))
                throw new NotFoundError("user was not present. please try again!");

                return true;
            }
            return false;
        }
        private int SaveOrders(List<OrderItem> orders)
        {
            if (orders.All(s => IsOrderValid(s)))
            {
                try
                {
                    int curOrderNumber = 1;
                    var orderDateNow = DateTime.Now;
                    var prevOrdersInfo = _dbContext.Orders.Where(s => s.UserId == orders.FirstOrDefault().UserId).ToList();
                    if (prevOrdersInfo.Count > 0)
                    {
                        int prevOrderNum = prevOrdersInfo.Max(s => s.OrderNumber);
                        curOrderNumber= prevOrderNum+1;
                    }

                    var curOrders = orders.Join(_dbContext.Products, (o) => o.ProductId, (p) => p.PId, (o, p) => new { o, p })
                                    .Select(po => new Order
                                    {
                                        OrderDate = orderDateNow,
                                        OrderNumber = curOrderNumber,
                                        PId = po.p.PId,
                                        UserId = po.o.UserId,
                                        ShippingAddress = po.o.ShippingAddress,
                                        Quantity = po.o.Quantity,
                                        Description = po.o.Description,
                                        Cost = po.o.Quantity * po.p.Price
                                    }).ToList();

                     InsertOrders(curOrders);
                    return curOrderNumber;
                }
                catch (Exception)
                {
                    throw;
                }

            }
            throw new Exception("Order must have the valid information");

        }
        private bool InsertOrders(List<Order> curOrders)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.Orders.AddRange(curOrders);
                    _dbContext.SaveChanges();
                    transaction.Commit();
                    return true;
                    
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        private static OrderItem CastOrder(Product p,Order o,User u)
        {
            var caseOrder = new OrderItem()
            {
                OrderId = o.OrderId,
                OrderNumber = o.OrderNumber,
                OrderDate = o.OrderDate,
                ProductId = p.PId,
                ProductName = p.Name,
                UserId = u.UserId,
                UserName =u.UserName,
                Description = o.Description,
                Quantity = o.Quantity,
                Cost = o.Cost,
                ShippingAddress = o.ShippingAddress,
            };
            return caseOrder;
        }
        public List<OrderItem> GetPagedOrders(int pageSize,int pageNumber)
        {
                var totalOrders= _dbContext.Orders.Join(_dbContext.Users,o => o.UserId.Value,u => u.UserId,(o, u) => new { o, u }).ToList()
                                        .Join(_dbContext.Products,(ou) => ou.o.PId,p => p.PId,(ou, p) => CastOrder(p, ou.o, ou.u)).ToList()
                                        .OrderBy(s=>s.OrderId)
                                        //.ThenByDescending(s=>s.OrderDate)
                                        //.ThenByDescending(m=>m.OrderNumber)
                                        //.ThenByDescending(r=>r.OrderId)
                                        .ToList();
            if (pageSize <= 0)
                return totalOrders;

            var pageCount = totalOrders.Count / pageSize;
            var pageNumberCount = pageNumber <= 0 ? 1 : pageNumber - 1;
            var skipRecords = pageCount<1?0: pageSize * pageNumberCount;
            return  totalOrders.Skip(skipRecords).Take(pageSize).ToList();

        }
        public List<OrderItem> GetOrders(int userId)
        {
            if (userId > 0)
            {
                return _dbContext.Orders.Where(s => s.UserId.Value == userId).ToList()
                    .Join(_dbContext.Users, o => o.UserId.Value, u => u.UserId, (o, u) => new { o, u }).ToList()
                    .Join(_dbContext.Products, (ou) => ou.o.PId, p => p.PId, (ou, p) => CastOrder(p,ou.o,ou.u)).ToList();
            }
            throw new Exception("Provide valid user");
        }
        public List<OrderItem> GetOrders(int userId,int orderNum)
        {
            if (userId > 0)
            {
                return _dbContext.Orders.Where(s => s.UserId.Value == userId && s.OrderNumber==orderNum).ToList()
                    .Join(_dbContext.Users, o => o.UserId.Value, u => u.UserId, (o, u) => new { o, u }).ToList()
                    .Join(_dbContext.Products, (ou) => ou.o.PId, p => p.PId, (ou, p) => CastOrder(p, ou.o, ou.u)).ToList();
            }
            throw new Exception("Provide valid user");
        }

        public List<OrderItem> PlaceOrder(List<OrderItem> orders)
        {
            int orderNum= SaveOrders(orders);
            return GetPagedOrders(orders.FirstOrDefault().UserId,orderNum);
        }
        public OrderItem PlaceOrder(OrderItem order)
        {
            var orderList = new List<OrderItem>() { order};
            return PlaceOrder(orderList).FirstOrDefault();

        }
    }
}
