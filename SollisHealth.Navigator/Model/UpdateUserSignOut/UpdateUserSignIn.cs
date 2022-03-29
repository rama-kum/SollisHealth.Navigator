using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Model.UpdateUserSignOut
{
    public class UpdateUserSignIn
    {
        [Key]
        public int User_Sign_id { get; set; }
        public string User_Name { get; set; }
        public string User_Email { get; set; }
        public string Depatment_Name { get; set; }
        public string Contact_Type { get; set; }
        public string Contact_Number { get; set; }
        public string Service { get; set; }
        public string User_Role { get; set; }
        public DateTime Sign_in_date { get; set; }
        public DateTime Sign_Out_Date { get; set; }
        public DateTime Actual_Sign_Out { get; set; }

        [DefaultValue(0)]
        public int Sign_Status { get; set; }
        public string Shift { get; set; }
        public string Comments { get; set; }       
        public DateTime? Createddate { get; set; }
        public DateTime Modifieddate { get; set; }       
        public string Createduser { get; set; }
        public string Modifieduser { get; set; }

        [DefaultValue(0)]
        public int Processed { get; set; }

    }

}
