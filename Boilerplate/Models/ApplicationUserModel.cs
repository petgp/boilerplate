using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Models
{
    public class ApplicationUserModel
    {
        //our model for application user
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        //public string FullName { get; set; }
        //public string PhoneNumber { get; set; }
        //public string Address { get; set; }
    }
}
