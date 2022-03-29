using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Model.GetSignInStatus
{
    public class GetSignInStatusResponse
    {
        public bool success { get; set; }
        public string Message { get; set; }
        public List<GetSignInStatusUserOutputUI> data { get; set; }

    }
 
  
}
