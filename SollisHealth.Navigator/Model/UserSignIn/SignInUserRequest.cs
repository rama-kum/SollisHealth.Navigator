using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Model.UserSignIn
{
    public class SignInUserRequest
    {
        [Key]
        public int User_Sign_id { get; set; }

        [Required]
        public string User_Name { get; set; }

        [Required]
        public string User_Email { get; set; }

        [Required]
        public string Depatment_Name { get; set; }
        public string Contact_Type { get; set; }
        public string Contact_Number { get; set; }
        public string Service { get; set; }

        [Required]
        public string User_Role { get; set; }

        [Required]
        public DateTime Sign_in_date { get; set; }

        [Required]
        public DateTime Sign_Out_Date { get; set; }
        public DateTime Actual_Sign_Out { get; set; }    
        public int Sign_Status { get; set; }

        [Required]
        public string Shift { get; set; }
        public string Comments { get; set; }       
        public DateTime Createddate { get; set; }
        public DateTime Modifieddate { get; set; }    
        public string Createduser { get; set; }
        public string Modifieduser { get; set; }

        [DefaultValue(0)]
        public int Processed { get; set; }

    }

}
