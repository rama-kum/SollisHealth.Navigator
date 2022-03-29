using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Model.UserSignIn
{
    public class SignInUserUIRequest
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string DepartmentName { get; set; }
        public string ContactType { get; set; }
        public string ContactNumber { get; set; }
        public string Service { get; set; }

        [Required]
        public string UserRole { get; set; }

        [Required]
        public DateTime SignInDate { get; set; }//format date

        [Required]
        public DateTime SignOutDate { get; set; }//format 

        [Required]
        public string Shift { get; set; }
        public string Comments { get; set; }
    }
}
