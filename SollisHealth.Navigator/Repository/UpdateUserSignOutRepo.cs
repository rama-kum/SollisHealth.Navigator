using Microsoft.EntityFrameworkCore;
using SollisHealth.Navigator.Helper;
using SollisHealth.Navigator.Interface;
using SollisHealth.Navigator.Model;
using SollisHealth.Navigator.Model.UpdateUserSignOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Repository
{
  
    public class UpdateUserSignOutRepo : IUpdateUserSignOutRepo
    {
        private readonly SignInUserDbContext _userdbcontext;

        public UpdateUserSignOutRepo(SignInUserDbContext userdbcontext)
        {
            _userdbcontext = userdbcontext;

        }

        /// <summary>
        /// This method returns all tasks as output 
        /// </summary>
        /// <returns>if data exists returns tasks with Task details and status =1 and if data not exits returns status 0 </returns>
        public async Task<UserSignOutResponse> UpdateUserSignout(UpdateSignInUserRequest updateUserInput)
        {
            UserSignOutResponse obj_userresponse = new UserSignOutResponse();

            int userupdatecheck = 0;
            var userdataforuserid = await _userdbcontext.User_sign_details.FindAsync(updateUserInput.SignInUserId);
            if (userdataforuserid != null)
            {
                userdataforuserid.Actual_Sign_Out = System.DateTime.Now;
                userdataforuserid.Sign_Status = 0;

                _userdbcontext.User_sign_details.Update(userdataforuserid);
                userupdatecheck = await _userdbcontext.SaveChangesAsync();
            }

            if (userupdatecheck != 0)
            {
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


