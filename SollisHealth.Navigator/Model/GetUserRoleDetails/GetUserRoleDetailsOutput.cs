using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Model.GetUserRoleDetails
{
    public class GetUserRoleDetailsOutput
    {
        [Key]
        public int user_Id { get; set; }
        public string User_email_id { get; set; }
        public string Username { get; set; }
        public string User_Active { get; set; }
        public string Role_Name { get; set; }
        public int role_ID { get; set; }

    }

}
