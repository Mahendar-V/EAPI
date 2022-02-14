using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAPI.Data
{
   public class UserItem
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "User Name Should not be empty")]
        [StringLength(30, ErrorMessage = "User Name is not allowed more than 30 characters")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Email is required.")]
        [StringLength(50, ErrorMessage = "Email is not allowed more than 50 characters")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Provide valid Email.")]
        public string Email { get; set; }
        public string Address { get; set; }
        
    }
}
