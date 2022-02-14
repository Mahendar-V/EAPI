using System;
using System.Collections.Generic;

#nullable disable

namespace EAPI.DataAccess.Entities
{
    public partial class Product
    {
        public Product()
        {
            Orders = new HashSet<Order>();
        }

        public int PId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
