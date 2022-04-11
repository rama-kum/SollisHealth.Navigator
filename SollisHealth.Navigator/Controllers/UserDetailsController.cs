using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SollisHealth.Navigator.Helper;
using SollisHealth.Navigator.Interface;
using SollisHealth.Navigator.Model.GetUserRoleDetails;
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

    public class UserDetailsController : ControllerBase
    {
        private readonly IGetUserRoleDetailsBO _IUserDetails;

        private readonly Microsoft.Extensions.Logging.ILogger<UserDetailsController> _logger;
        public IConfiguration _configuration { get; }

        public UserDetailsController(ILogger<UserDetailsController> logger, IGetUserRoleDetailsBO IUserDetails)
        {
            _logger = logger;
            _IUserDetails = IUserDetails;

        }


        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("GetUserDetails")]
        public async Task<IActionResult> GetUserDetails(GetUserRoleDetailsRequest userrequest)
        {
            _logger.LogInformation("User Details Controller is running in " + DateTime.Now);
            GetUserRoleDetailsValidationResponse obj_userresponse = new GetUserRoleDetailsValidationResponse();
            GetUserRoleDetailsValidationResponse obj_userresponserepo = new GetUserRoleDetailsValidationResponse();
            obj_userresponse.success = true;
            obj_userresponse.Message = "";

            obj_userresponse = validation_func(userrequest);
 

            if (obj_userresponse.success == false)
            {
                _logger.LogError("User Details not found in " + DateTime.Now);
                obj_userresponse = BuildGetUserRoleDetailsResponseMessage(obj_userresponse.Message, false, 400);
                return BadRequest(obj_userresponse);
            }
            else
            {
                GetUserRoleDetailsResponse userlistobj = await _IUserDetails.getuserroledetails(userrequest);
                if (userlistobj.success != false)
                {
                    return Ok(userlistobj);
                }
                else
                {
                    _logger.LogError("User Details not found in " + DateTime.Now);
                    obj_userresponserepo = BuildGetUserRoleDetailsResponseMessage(userlistobj.Message, false, 404);
                    return BadRequest(obj_userresponserepo);
                }
            }

        }
        private GetUserRoleDetailsValidationResponse validation_func(GetUserRoleDetailsRequest userrequest)
        {
            GetUserRoleDetailsValidationResponse validationresponse = new GetUserRoleDetailsValidationResponse();
            validationresponse.success = true;
            validationresponse.Message = "";
           
            if (userrequest.UserEmail.Trim() == "")
            {
                validationresponse.success = false;
                validationresponse.Message = userrequest.UserEmail + "UserEmail address should not be empty";
            }
            else if (userrequest.UserRole.Trim() == "")
            {
                validationresponse.success = false;
                validationresponse.Message = userrequest.UserRole + "UserRole should not be empty";

            }
            else if (userrequest.UserEmail.Trim() != "")
            {
                Validations emailcontext = new Validations();
                bool email_validate = emailcontext.ValidateEmail(userrequest.UserEmail.Trim());

                if (email_validate == false)
                {
                    validationresponse.success = false;
                    validationresponse.Message = userrequest.UserEmail + " is Invalid Email Address";
                }
            }
            return validationresponse;
        }

        private GetUserRoleDetailsValidationResponse BuildGetUserRoleDetailsResponseMessage(string message, bool boolmsg, int statuscode)
        {
            GetUserRoleDetailsValidationResponse response = new GetUserRoleDetailsValidationResponse();

            response.Message = message;
            response.success = boolmsg;
            response.error_code = statuscode;
            return response;
        }


    }
}
