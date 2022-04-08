using SollisHealth.Navigator.Interface;
using SollisHealth.Navigator.Model;
using SollisHealth.Navigator.Model.GetUserRoleDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Services
{
    public class GetUserRoleDetailsBO : IGetUserRoleDetailsBO
    {
        private readonly IGetUserRoleDetailsRepo _userRepo;
        public GetUserRoleDetailsBO(IGetUserRoleDetailsRepo userRepo)
        {
            _userRepo = userRepo;
        }

 /// <summary>
 /// Adds user signin information with userinput 
 /// </summary>
 /// <param name="userInput"></param>
 /// <returns></returns> 
        public async Task<GetUserRoleDetailsResponse> getuserroledetails(GetUserRoleDetailsRequest userInput)
        {
            GetUserRoleDetailsResponse userresponse = await _userRepo.getuserroledetails(userInput);
            return userresponse;

        }

 
    }
}
