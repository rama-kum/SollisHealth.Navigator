using SollisHealth.Navigator.Model.GetSignInStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Interface
{
    public interface IGetSigninStatusBO
    {
        Task<GetSignInStatusResponse> GetSigninStatus(GetSignInStatusUserRequest getsigninstatusinput);

    }
}
