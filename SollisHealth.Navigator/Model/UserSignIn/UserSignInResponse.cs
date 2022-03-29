using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Model.UserSignIn
{
    /// <summary>
    /// TaskResponse class is used to take list of Task details into TaskOutput list and status is updated
    /// </summary>
    public class UserSignInResponse
    {
        public bool success { get; set; }
        public string Message { get; set; }
        public UserSignInOutput data { get; set; }

    }

 

}
