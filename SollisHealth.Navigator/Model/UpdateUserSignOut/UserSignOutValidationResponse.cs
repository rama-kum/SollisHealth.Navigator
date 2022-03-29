using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Model
{
    public class UserSignOutValidationResponse
    {
        public bool success { get; set; }
        public int error_code { get; set; }
        public string Message { get; set; }
  
    }
}
