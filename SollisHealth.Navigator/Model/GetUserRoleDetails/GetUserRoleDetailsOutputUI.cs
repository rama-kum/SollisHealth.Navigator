using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Model.GetUserRoleDetails
{
    public class GetUserRoleDetailsOutputUI
    {
        [Key]
        public int UserId { get; set; }
        public string UserEmailID { get; set; }
        public string UserName { get; set; }
       // public string UserActive { get; set; }
        public string RoleName { get; set; }
        public int RoleID { get; set; }

    }
}
