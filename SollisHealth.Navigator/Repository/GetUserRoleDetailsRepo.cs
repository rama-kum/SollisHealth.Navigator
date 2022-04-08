using Microsoft.EntityFrameworkCore;
//using SollisHealth.Navigator.Controllers;
using SollisHealth.Navigator.Helper;
using SollisHealth.Navigator.Interface;
using SollisHealth.Navigator.Model;
using SollisHealth.Navigator.Model.GetUserRoleDetails;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Repository
{
    public class GetUserRoleDetailsRepo : IGetUserRoleDetailsRepo
    {
        private readonly SignInUserDbContext _userdbcontext;        
       public GetUserRoleDetailsRepo(SignInUserDbContext userdbcontext)
        {
            _userdbcontext = userdbcontext;
           // _validationcontext = validationcontext;

        }

        /// <summary>
        /// This method returns all tasks as output 
        /// </summary>
        /// <returns>if data exists returns tasks with Task details and status =1 and if data not exits returns status 0 </returns>
        public async Task<GetUserRoleDetailsResponse> getuserroledetails(GetUserRoleDetailsRequest userrequestdata)
        {
            GetUserRoleDetailsResponse obj_userresponse = new GetUserRoleDetailsResponse();
            GetUserRoleDetailsOutputUI obj_useridoutput = new GetUserRoleDetailsOutputUI();
            GetUserRoleDetailsRequest signinuser = new GetUserRoleDetailsRequest();

            obj_userresponse.success = true;
            obj_userresponse.Message = "";

            var userdata = await _userdbcontext.vm_user_role.Where(m => m.User_email_id == userrequestdata.UserEmail.Trim() && m.Role_Name== userrequestdata.UserRole.Trim() && m.User_Active=="Active")
                              .Select(p => new GetUserRoleDetailsOutputUI
                              {
                                  UserId = p.user_Id,
                                  UserEmailID = p.User_email_id,
                                  UserName = p.Username,
                                  RoleName = p.Role_Name,
                                  RoleID = p.role_ID

                              }).ToListAsync();

            if (userdata.Count() != 0)
            {
                obj_userresponse.data = userdata;
                obj_userresponse.success = true;
                obj_userresponse.Message = "Getting User Details is successful";
            }
            else
            {
                obj_userresponse.Message = "User Details not found";
                obj_userresponse.success = false;
            }
            return obj_userresponse;

        }    

    }
}
