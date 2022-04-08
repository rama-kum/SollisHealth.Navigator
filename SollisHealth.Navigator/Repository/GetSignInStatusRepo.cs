using Microsoft.EntityFrameworkCore;
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
    public class GetSignInStatusRepo: IGetSigninStatusRepo
    {
        private readonly SignInUserDbContext _userdbcontext;

        public GetSignInStatusRepo(SignInUserDbContext userdbcontext)
        {
            _userdbcontext = userdbcontext;

        }

        /// <summary>
        /// This method returns all tasks as output 
        /// </summary>
        /// <returns>if data exists returns tasks with Task details and status =1 and if data not exits returns status 0 </returns>
        public async Task<GetSignInStatusResponse> GetSigninStatus(GetSignInStatusUserRequest getsigninstatusinput)
        {
            GetSignInStatusResponse obj_userresponse = new GetSignInStatusResponse();
            
            var taskdata = await _userdbcontext.vm_user_sign_details.Where(m => m.User_Sign_id == getsigninstatusinput.SignInUserId)
                              .Select(p => new GetSignInStatusUserOutputUI
                              {
                                  UserSignInid = p.User_Sign_id,
                                  UserSignInStatus = p.Sign_Status == 0 ? false : true
                                 
                              }).ToListAsync();

            if (taskdata.Count()!= 0)
            {
                obj_userresponse.data = taskdata;
                obj_userresponse.success = true;
            }
            else
            {
                obj_userresponse.success = false;
            }
            return obj_userresponse; 
        }

    }

}
