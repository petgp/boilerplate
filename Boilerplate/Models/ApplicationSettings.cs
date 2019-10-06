using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Models
{
    public class ApplicationSettings
    {
        //settings in startup
        public string JWT_Secret { get; set; }
        public string Client_URL { get; set; }
    }
}
