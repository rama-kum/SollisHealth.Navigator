using SollisHealth.Navigator.Helper;
using SollisHealth.Navigator.Interface;
using SollisHealth.Navigator.Model.GetSignInStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Model.Repository
{
    /// <summary>
    /// TaskResponse class is used to take list of Task details into TaskOutput list and status is updated
    /// </summary>
    public class GetSignInStatusBO : IGetSigninStatusBO
    {
        private readonly IGetSigninStatusRepo _userRepo;

        public GetSignInStatusBO(IGetSigninStatusRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<GetSignInStatusResponse> GetSigninStatus(GetSignInStatusUserRequest getsigninstatusinput)
        {
            GetSignInStatusResponse UserResponse = await _userRepo.GetSigninStatus(getsigninstatusinput);
            return UserResponse;

        }

    }
}
