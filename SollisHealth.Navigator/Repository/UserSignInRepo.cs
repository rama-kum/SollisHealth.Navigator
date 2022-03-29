using Microsoft.EntityFrameworkCore;
//using SollisHealth.Navigator.Controllers;
using SollisHealth.Navigator.Helper;
using SollisHealth.Navigator.Interface;
using SollisHealth.Navigator.Model;
using SollisHealth.Navigator.Model.UserSignIn;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Repository
{
    public class UserSignInRepo : IUserSignInRepo
    {
        private readonly SignInUserDbContext _userdbcontext;
        
        private readonly Validations _validationcontext;

        public UserSignInRepo(SignInUserDbContext userdbcontext)
        {
            _userdbcontext = userdbcontext;
           // _validationcontext = validationcontext;

        }

        /// <summary>
        /// This method returns all tasks as output 
        /// </summary>
        /// <returns>if data exists returns tasks with Task details and status =1 and if data not exits returns status 0 </returns>
        public async Task<UserSignInResponse> AddUserforSignIn(SignInUserUIRequest userrequestdata)
        {
            UserSignInResponse obj_userresponse = new UserSignInResponse();
            UserSignInOutput obj_useridoutput = new UserSignInOutput();
            SignInUserRequest signinuser = new SignInUserRequest();

            obj_userresponse.success = true;
            obj_userresponse.Message = "";

            signinuser.User_Name = userrequestdata.UserName.Trim();

            //Email validation
            Validations emailcontext = new Validations();
            bool email_validate = emailcontext.ValidateEmail(userrequestdata.UserEmail.Trim());
            if (email_validate == true)
            {
                signinuser.User_Email = userrequestdata.UserEmail.Trim();
            }
            else
            {
                obj_userresponse.success = false;
                obj_userresponse.Message = userrequestdata.UserEmail + " is Invalid Email Address";
            }
           
            signinuser.Depatment_Name = userrequestdata.DepartmentName.Trim();
            signinuser.Contact_Type = userrequestdata.ContactType.Trim();
            signinuser.Contact_Number = userrequestdata.ContactNumber.Trim();
            signinuser.Service = userrequestdata.Service.Trim();
            signinuser.User_Role = userrequestdata.UserRole.Trim();

            //signin date validation
            Validations validationcontext = new Validations();
            bool signin_validate = validationcontext.ValidateDateTime(userrequestdata.SignInDate.ToUniversalTime());                    
            if (signin_validate ==true)
            signinuser.Sign_in_date = userrequestdata.SignInDate.ToUniversalTime();
            else
            {
                obj_userresponse.success = false;
                obj_userresponse.Message = "Enter time value in SignIn Date";
            }

            //signout date validation
            bool signout_validate = validationcontext.ValidateDateTime(userrequestdata.SignOutDate.ToUniversalTime());
            if (signout_validate == true)
                signinuser.Sign_Out_Date = userrequestdata.SignOutDate.ToUniversalTime();
            else
            {
                obj_userresponse.success = false;
                obj_userresponse.Message = "Enter time value in SignOut Date";
            }

           
            signinuser.Sign_Status = 1;
            signinuser.Shift = userrequestdata.Shift.Trim();
            signinuser.Comments = userrequestdata.Comments.Trim();
            signinuser.Createddate = System.DateTime.Now;
            signinuser.Createduser = userrequestdata.UserName.Trim();
            signinuser.Modifieduser = null;
            signinuser.Processed = 0;

            if (obj_userresponse.success == true)
            {

                _userdbcontext.User_sign_details.Add(signinuser);
                var userid = await _userdbcontext.SaveChangesAsync();
                var useridresponse = _userdbcontext.User_sign_details.Max(p => p.User_Sign_id);

                obj_useridoutput.UserSignInId = useridresponse;

                if (obj_useridoutput != null)
                {
                    obj_userresponse.data = obj_useridoutput;
                    obj_userresponse.success = true;
                    obj_userresponse.Message = "User SignIn information successfully registered";
                }
                else
                {
                    obj_userresponse.Message = "User SignIn registration is not successfull";
                    obj_userresponse.success = false;
                }
            }
                return obj_userresponse;
 

           
        }    

    }
}
