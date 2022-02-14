using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAPI.Data
{
   
   public class OrderItem
    {
        public int OrderId { get; set; }
        public int? OrderNumber { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? OrderDate { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
       
        public int Quantity { get; set; }
        public double Cost { get; set; }
        public string Description { get; set; }
        public string UserName { get; set; }
        public string ProductName { get; set; }

    }
}
