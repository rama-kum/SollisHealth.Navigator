using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Model.GetSignInStatus
{
    public class GetSignInStatusUserRequest
    {
        [Key]
        public int SignInUserId { get; set; }

    }
}
