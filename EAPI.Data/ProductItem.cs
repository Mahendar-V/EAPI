using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAPI.DataModels
{
   public class ProductItem
    {
        public int PId { get; set; }

        [Required(ErrorMessage ="Product Name Should not be empty")]
        [StringLength(50, ErrorMessage = "Name Should not be more than 50 charactors")]
        public string Name { get; set; }
        [StringLength(500, ErrorMessage = "Description Should not be more than 500 charactors")]
        public string Description { get; set; }

        [Range(minimum:1,maximum:double.MaxValue,ErrorMessage = "Price must be greter than zero !")]
        public double Price { get; set; }
    }
}
