using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SollisHealth.Navigator.Controllers;
using SollisHealth.Navigator.Helper;
using SollisHealth.Navigator.Interface;
using SollisHealth.Navigator.Model;
using SollisHealth.Navigator.Model.GetUserRoleDetails;
using SollisHealth.Navigator.Model.Repository;
using SollisHealth.Navigator.Repository;
using SollisHealth.Navigator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.UnitTest
{
    [TestClass]
    public class GetUserDetailsUnitTest
    {
        [TestMethod]
        public void GetUserDetailsSUCCESS_Repository()
        {
            var options = new DbContextOptionsBuilder<SignInUserDbContext>()
                        .UseInMemoryDatabase(databaseName: "NavigatorDataBase")
                        .Options;

            using (var context = new SignInUserDbContext(options))
            {
                GetUserRoleDetailsRequest userrequestdata = new GetUserRoleDetailsRequest();
                userrequestdata.UserEmail = "User3@gmail.com";
                userrequestdata.UserRole = "PA Triage";
        

                context.vm_user_role.Add(new GetUserRoleDetailsOutput
                {
                    user_Id = 1,
                    User_email_id = "User3@gmail.com",
                    Username= "admin",
                    User_Active="Active",
                    Role_Name = "PA Triage",
                    role_ID=1

                });

                context.SaveChanges();

                GetUserRoleDetailsRepo repoObject = new GetUserRoleDetailsRepo(context);
                Task<GetUserRoleDetailsResponse> result = repoObject.getuserroledetails(userrequestdata);
               
                Assert.AreEqual(result.Result.success, true);
                Assert.IsTrue(result.IsCompletedSuccessfully);

            }

        }

        [TestMethod]
        public void GetUserRoleDetailsFAILURE_Repository()
        {
            var options = new DbContextOptionsBuilder<SignInUserDbContext>()
                        .UseInMemoryDatabase(databaseName: "NavigatorDataBase")
                        .Options;

            using (var context = new SignInUserDbContext(options))
            {
                context.vm_user_role.Add(new GetUserRoleDetailsOutput
                {

                });
                GetUserRoleDetailsRequest userrequestdata = new GetUserRoleDetailsRequest();
                userrequestdata.UserEmail = "invalid@gmail.com";
                userrequestdata.UserRole = "admin";

                GetUserRoleDetailsRepo repoObject = new GetUserRoleDetailsRepo(context);
                Task<GetUserRoleDetailsResponse> result = repoObject.getuserroledetails(userrequestdata);

                Assert.AreEqual(result.Result.success, false);
                Assert.IsTrue(result.IsCompletedSuccessfully);

            }

        }   


        [TestMethod]
        public void GetUserSignInStatusSUCCESS_BO()
        {
            GetUserRoleDetailsRequest userrequestdata = new GetUserRoleDetailsRequest();
            userrequestdata.UserEmail = "User1@gmail.com";
            userrequestdata.UserRole = "admin";
            var mockuserRepo = new Mock<IGetUserRoleDetailsRepo>();
            mockuserRepo.Setup(x => x.getuserroledetails(userrequestdata)).Returns(GetUserValid);

            GetUserRoleDetailsBO userBO = new GetUserRoleDetailsBO(mockuserRepo.Object);
            Task<GetUserRoleDetailsResponse> result = userBO.getuserroledetails(userrequestdata);

            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(result.Result.success, true);
        }

         private Task<GetUserRoleDetailsResponse> GetUserValid()
        {
            GetUserRoleDetailsResponse obj_userresponse = new GetUserRoleDetailsResponse();
            // obj_useridoutput.UserSignInId = null; 
            // obj_userresponse.data = obj_useridoutput;
            obj_userresponse.success = true;

            return Task.FromResult(obj_userresponse);
        }


        [TestMethod]
        public void GetUserRoleDetailsFAILURE_BO()
        {
            GetUserRoleDetailsRequest userrequestdata = new GetUserRoleDetailsRequest();
            userrequestdata.UserEmail = "invalid@gmail.com";
            userrequestdata.UserRole = "admin";
            var mockusersRepo = new Mock<IGetUserRoleDetailsRepo>();
            mockusersRepo.Setup(x => x.getuserroledetails(userrequestdata)).Returns(GetUsersInValid);

            GetUserRoleDetailsBO usersBO = new GetUserRoleDetailsBO(mockusersRepo.Object);
            Task<GetUserRoleDetailsResponse> result = usersBO.getuserroledetails(userrequestdata);

            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(result.Result.success, false);
        }

 
        private Task<GetUserRoleDetailsResponse> GetUsersInValid()
        {
            GetUserRoleDetailsResponse obj_userresponse = new GetUserRoleDetailsResponse();

            obj_userresponse.success = false;

            return Task.FromResult(obj_userresponse);
        }

        [TestMethod]
        public void GetUserRoleDetailsSUCCESS_Controller()
        {
            var mockLogger = new Mock<ILogger<UserDetailsController>>();
            ILogger<UserDetailsController> logger = mockLogger.Object;

            var mockUserDetails = new Mock<IGetUserRoleDetailsBO>();

            GetUserRoleDetailsRequest userrequestdata = new GetUserRoleDetailsRequest();
            userrequestdata.UserEmail = "User1@gmail.com";
            userrequestdata.UserRole = "admin";

            UserDetailsController UserControllerobj = new UserDetailsController(logger, mockUserDetails.Object);
            mockUserDetails.Setup(x => x.getuserroledetails(userrequestdata)).Returns(GetUserValid);
            Task<IActionResult> result = UserControllerobj.GetUserDetails(userrequestdata);
            ObjectResult obj = (ObjectResult)result.Result;
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(obj.StatusCode, 200);
        }

        /// <summary>
        /// Check User DetailsController class 
        /// </summary>
        [TestMethod]
        public void GetUserRoleDetailsFAILURE_Controller()
        {
            var mockLogger = new Mock<ILogger<UserDetailsController>>();
            ILogger<UserDetailsController> logger = mockLogger.Object;

            var mockGetUserRoleDetails = new Mock<IGetUserRoleDetailsBO>();

            GetUserRoleDetailsRequest userrequestdata = new GetUserRoleDetailsRequest();
            userrequestdata.UserEmail = "invalid@gmail.com";
            userrequestdata.UserRole = "admin";

            UserDetailsController UserControllerobj = new UserDetailsController(logger, mockGetUserRoleDetails.Object);
            mockGetUserRoleDetails.Setup(x => x.getuserroledetails(userrequestdata)).Returns(GetUsersInValid);
            Task<IActionResult> result = UserControllerobj.GetUserDetails(userrequestdata);
            ObjectResult obj = (ObjectResult)result.Result;
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(obj.StatusCode, 400);
        }

    }
}
