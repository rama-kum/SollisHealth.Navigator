using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Model.GetUserRoleDetails
{
    public class GetUserRoleDetailsRequest
    {
        [Key]
        public string UserEmail { get; set; }

        public string UserRole { get; set; }


    }

}
