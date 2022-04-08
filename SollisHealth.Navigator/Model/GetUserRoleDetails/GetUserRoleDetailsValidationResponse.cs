using SollisHealth.Navigator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Model.GetUserRoleDetails
{

    public class GetUserRoleDetailsValidationResponse
    {
        public bool success { get; set; }
        public int error_code { get; set; }
        public string Message { get; set; }
        public GetUserRoleDetailsOutput data { get; set; }
    }
}
