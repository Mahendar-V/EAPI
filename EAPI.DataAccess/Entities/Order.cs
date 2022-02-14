using System;
using System.Collections.Generic;

#nullable disable

namespace EAPI.DataAccess.Entities
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int OrderNumber { get; set; }
        public int? UserId { get; set; }
        public int? PId { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime? OrderDate { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }

        public virtual Product PIdNavigation { get; set; }
        public virtual User User { get; set; }
    }
}
