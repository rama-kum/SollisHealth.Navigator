using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Model.GetSignInStatus
{
    public class GetSignInStatusUserOutputUI
    {
        [Key]
        public int UserSignInid { get; set; }
        public int UserSignInStatus { get; set; }
      

    }
 
}
