using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Model.GetSignInStatus
{
    public class GetSignInStatusUserOutput
    {
        [Key]
        public int User_Sign_id { get; set; }
        public int Sign_Status { get; set; }
      

    }

}
