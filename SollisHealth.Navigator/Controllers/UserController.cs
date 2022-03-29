using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
    [Route("api/[controller]")]
    [ApiController]
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
        [Route("NewUserRegistration")]
        public async Task<IActionResult> AddUserSignInfo(SignInUserUIRequest userrequest)
        {
            _logger.LogInformation("Navigator Controller is running in " + DateTime.Now);
            UserSignInValidationResponse response = null;
            UserSignInResponse userlistobj = await _IUserSignIn.AddUserforSignIn(userrequest);
            if (userlistobj.success != false)
            {               
                return Ok(userlistobj);
            }
            else
            {
                _logger.LogError("User SignIn Id not found in " + DateTime.Now);
                response = BuildUserSignInResponseMessage(userlistobj.Message, false, 404);
                return BadRequest(response);
            }

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("UpdateUserSignOut")]
        public async Task<IActionResult> UpdateUserSignOut(UpdateSignInUserRequest userrequest)
        {
            _logger.LogInformation("Navigator Controller is running in " + DateTime.Now);
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
            _logger.LogInformation("Navigator Controller is running in " + DateTime.Now);
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
