using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SollisHealth.Navigator.Helper;
using SollisHealth.Navigator.Interface;
using SollisHealth.Navigator.Model;
using SollisHealth.Navigator.Model.GetSignInStatus;
using SollisHealth.Navigator.Model.UpdateUserSignOut;
using SollisHealth.Navigator.Model.UserSignIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Controllers
{
    [ApiController]
    //[Authorize]

    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}")]

    public class UserController : ControllerBase
    {
        private readonly IUserSignInBO _IUserSignIn;
        private readonly IUpdateUserSignOutBO _IUpdateUserSignOut;
        private readonly IGetSigninStatusBO _IgetSigninStatus;

        private readonly Microsoft.Extensions.Logging.ILogger<UserController> _logger;
        public IConfiguration _configuration { get; }

        public UserController(ILogger<UserController> logger, IUserSignInBO IUserSignIn, IUpdateUserSignOutBO IUpdateUserSignOut,
            IGetSigninStatusBO IgetSigninStatus)
        {
            _logger = logger;
            _IUserSignIn = IUserSignIn;
            _IUpdateUserSignOut = IUpdateUserSignOut;
            _IgetSigninStatus = IgetSigninStatus;

        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AddUserSignInDetails")]
        public async Task<IActionResult> AddUserSignInfo(SignInUserUIRequest userrequest)
        {
            _logger.LogInformation("User Navigator Controller is running in " + DateTime.Now);
            UserSignInValidationResponse obj_userresponse = new UserSignInValidationResponse();
            UserSignInValidationResponse obj_userresponserepo = new UserSignInValidationResponse();
            obj_userresponse.success = true;
            obj_userresponse.Message = "";

            obj_userresponse = validation_func(userrequest); 

            if (obj_userresponse.success == false)
            {
                _logger.LogError("User SignIn Id not found in " + DateTime.Now);
                obj_userresponse = BuildUserSignInResponseMessage(obj_userresponse.Message, false, 404);
                return BadRequest(obj_userresponse);
            }
            else
            {
                UserSignInResponse userlistobj = await _IUserSignIn.AddUserforSignIn(userrequest);
                if (userlistobj.success != false)
                {
                    return Ok(userlistobj);
                }
                else
                {
                    _logger.LogError("User SignIn Id not found in " + DateTime.Now);
                    obj_userresponserepo = BuildUserSignInResponseMessage(userlistobj.Message, false, 404);
                    return BadRequest(obj_userresponserepo);
                }
            }

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("UserSignOut")]
        public async Task<IActionResult> UpdateUserSignOut(UpdateSignInUserRequest userrequest)
        {
            _logger.LogInformation("User Navigator Controller is running in " + DateTime.Now);
            UserSignOutValidationResponse response = null;

            UserSignOutResponse userlistobj = await _IUpdateUserSignOut.UpdateUserSignout(userrequest);
            if (userlistobj.success != false)
            {              
                userlistobj.Message = "SignOut Information successfully updated";
                return Ok(userlistobj);
            }
            else
            {
                _logger.LogError("User SignOut Id is not found in " + DateTime.Now);
                response = BuildUserSignOutResponseMessage("SignOut Information updation failed", false, 404);
                return BadRequest(response);
            }

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetSignInStatus")]
        public async Task<IActionResult> GetSignInStatus(GetSignInStatusUserRequest userrequest)
        {
            _logger.LogInformation("User Navigator Controller is running in " + DateTime.Now);
            GetSignInStatusValidationResponse response = null;


            GetSignInStatusResponse userlistobj = await _IgetSigninStatus.GetSigninStatus(userrequest);


            if (userlistobj.success != false)
            {
                userlistobj.Message = "Get SignIn Status Information is successfull";
                return Ok(userlistobj);
            }
            else
            {
                _logger.LogError("User SignIn Status Information is not found in " + DateTime.Now);

                response = BuildUserSignInStatusResponseMessage("Getting SignIn Status Information is not successfull", false, 404);
                return BadRequest(response);
            }

        }
        private UserSignInValidationResponse validation_func(SignInUserUIRequest userrequest)
        {
            UserSignInValidationResponse validationresponse = new UserSignInValidationResponse();
            Validations validationsignindate = new Validations();
            Validations validationsignoutdate = new Validations();

            validationresponse.success = true;
            validationresponse.Message = "";

            if (userrequest.UserEmail.Trim() != "")
            {
                Validations emailcontext = new Validations();
                bool email_validate = emailcontext.ValidateEmail(userrequest.UserEmail.Trim());

                if (email_validate == false)
                {
                    validationresponse.success = false;
                    validationresponse.Message = userrequest.UserEmail + " is Invalid Email Address";
                    return validationresponse;
                }
            }
            if (validationsignindate.ValidateDateTime(userrequest.SignInDate.ToUniversalTime())==false)
            {  
                validationresponse.success = false;
                validationresponse.Message = "Enter time value in SignIn Date";                
            }
            else if (validationsignoutdate.ValidateDateTime(userrequest.SignOutDate.ToUniversalTime())==false)
            {
                validationresponse.success = false;
                validationresponse.Message = "Enter time value in SignOut Date";
                
            }
            return validationresponse;
        }


        //This method is used build response to send to client
        private GetSignInStatusValidationResponse BuildUserSignInStatusResponseMessage(string message, bool boolmsg, int statuscode)
        {
            GetSignInStatusValidationResponse response = new GetSignInStatusValidationResponse();

            response.Message = message;
            response.success = boolmsg;
            response.error_code = statuscode;
            return response;
        }
        private UserSignInValidationResponse BuildUserSignInResponseMessage(string message, bool boolmsg, int statuscode)
        {
            UserSignInValidationResponse response = new UserSignInValidationResponse();

            response.Message = message;
            response.success = boolmsg;
            response.error_code = statuscode;
            return response;
        }
        private UserSignOutValidationResponse BuildUserSignOutResponseMessage(string message, bool boolmsg, int statuscode)
        {
            UserSignOutValidationResponse response = new UserSignOutValidationResponse();

            response.Message = message;
            response.success = boolmsg;
            response.error_code = statuscode;
            return response;
        }

    }
}
