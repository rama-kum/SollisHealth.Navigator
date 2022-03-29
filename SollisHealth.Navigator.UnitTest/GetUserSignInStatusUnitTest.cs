using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SollisHealth.Navigator.Controllers;
using SollisHealth.Navigator.Helper;
using SollisHealth.Navigator.Interface;
using SollisHealth.Navigator.Model;
using SollisHealth.Navigator.Model.GetSignInStatus;
using SollisHealth.Navigator.Model.Repository;
using SollisHealth.Navigator.Model.UpdateUserSignOut;
using SollisHealth.Navigator.Model.UserSignIn;
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
    public class GetUserSignInStatusUnitTest
    {
        [TestMethod]
        public void GetUserSignInStatusSuccess_Repository()
        {
            var options = new DbContextOptionsBuilder<SignInUserDbContext>()
                        .UseInMemoryDatabase(databaseName: "NavigatorDataBase")
                        .Options;

            using (var context = new SignInUserDbContext(options))
            {
                GetSignInStatusUserRequest userrequestdata = new GetSignInStatusUserRequest();
                userrequestdata.SignInUserId = 1;
 
                context.vm_user_sign_details.Add(new GetSignInStatusUserOutput
                {
                    User_Sign_id = 1,
                    Sign_Status = 0
                });

                context.SaveChanges();

                GetSignInStatusRepo repoObject = new GetSignInStatusRepo(context);
                Task<GetSignInStatusResponse> result = repoObject.GetSigninStatus(userrequestdata);
               
                Assert.AreEqual(result.Result.success, true);
                Assert.IsTrue(result.IsCompletedSuccessfully);

            }

        }

        [TestMethod]
        public void GetUserSignInStatusFAILURE_Repository()
        {
            var options = new DbContextOptionsBuilder<SignInUserDbContext>()
                        .UseInMemoryDatabase(databaseName: "NavigatorDataBase")
                        .Options;

            using (var context = new SignInUserDbContext(options))
            {
                GetSignInStatusUserRequest userrequestdata = new GetSignInStatusUserRequest();
                userrequestdata.SignInUserId = 77;

                context.vm_user_sign_details.Add(new GetSignInStatusUserOutput
                {
                   
                });

               // context.SaveChanges();

                GetSignInStatusRepo repoObject = new GetSignInStatusRepo(context);
                Task<GetSignInStatusResponse> result = repoObject.GetSigninStatus(userrequestdata);

                Assert.AreEqual(result.Result.success, false);
                Assert.IsTrue(result.IsCompletedSuccessfully);

            }

        }   


        [TestMethod]
        public void GetUserSignInStatusSUCCESS_BO()
        {
            GetSignInStatusUserRequest userrequestdata = new GetSignInStatusUserRequest();
            userrequestdata.SignInUserId = 2;
            var mockusersignstatusRepo = new Mock<IGetSigninStatusRepo>();
            mockusersignstatusRepo.Setup(x => x.GetSigninStatus(userrequestdata)).Returns(GetUsersignstatusValid);

            GetSignInStatusBO usersignoutBO = new GetSignInStatusBO(mockusersignstatusRepo.Object);
            Task<GetSignInStatusResponse> result = usersignoutBO.GetSigninStatus(userrequestdata);

            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(result.Result.success, true);
        }

         private Task<GetSignInStatusResponse> GetUsersignstatusValid()
        {
            GetSignInStatusResponse obj_userresponse = new GetSignInStatusResponse();
            // obj_useridoutput.UserSignInId = null; 
            // obj_userresponse.data = obj_useridoutput;
            obj_userresponse.success = true;

            return Task.FromResult(obj_userresponse);
        }


        [TestMethod]
        public void GetUserSignInStatusFAILURE_BO()
        {
            GetSignInStatusUserRequest userrequestdata = new GetSignInStatusUserRequest();
            userrequestdata.SignInUserId = 112;
            var mockusersignstatusRepo = new Mock<IGetSigninStatusRepo>();
            mockusersignstatusRepo.Setup(x => x.GetSigninStatus(userrequestdata)).Returns(GetUsersignstatusInValid);

            GetSignInStatusBO usersignoutBO = new GetSignInStatusBO(mockusersignstatusRepo.Object);
            Task<GetSignInStatusResponse> result = usersignoutBO.GetSigninStatus(userrequestdata);

            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(result.Result.success, false);
        }

 
        private Task<GetSignInStatusResponse> GetUsersignstatusInValid()
        {
            GetSignInStatusResponse obj_userresponse = new GetSignInStatusResponse();

            obj_userresponse.success = false;
            //obj_userresponse.Message = "User SignIn information successfully registered";

            return Task.FromResult(obj_userresponse);
        }

        [TestMethod]
        public void GetUserSignInStatusSUCCESS_Controller()
        {
            var mockLogger = new Mock<ILogger<UserController>>();
            ILogger<UserController> logger = mockLogger.Object;

            var mockUpdateUserSignOut = new Mock<IUpdateUserSignOutBO>();
            var mockUserSignIn = new Mock<IUserSignInBO>();
            var mockGetSigninStatus = new Mock<IGetSigninStatusBO>();

            GetSignInStatusUserRequest userrequestdata = new GetSignInStatusUserRequest();
            userrequestdata.SignInUserId = 2;

            UserController UserControllerobj = new UserController(logger, mockUserSignIn.Object, mockUpdateUserSignOut.Object, mockGetSigninStatus.Object);
            mockGetSigninStatus.Setup(x => x.GetSigninStatus(userrequestdata)).Returns(GetUsersignstatusValid);
            Task<IActionResult> result = UserControllerobj.GetSignInStatus(userrequestdata);
            ObjectResult obj = (ObjectResult)result.Result;
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(obj.StatusCode, 200);
        }

        /// <summary>
        /// Check User Navigator Controller class if exists member ID
        /// </summary>
        [TestMethod]
        public void GetUserSignInStatusFAILURE_Controller()
        {
            var mockLogger = new Mock<ILogger<UserController>>();
            ILogger<UserController> logger = mockLogger.Object;

            var mockUpdateUserSignOut = new Mock<IUpdateUserSignOutBO>();
            var mockUserSignIn = new Mock<IUserSignInBO>();
            var mockGetSigninStatus = new Mock<IGetSigninStatusBO>();

            GetSignInStatusUserRequest userrequestdata = new GetSignInStatusUserRequest();
            userrequestdata.SignInUserId = 112;

            UserController UserControllerobj = new UserController(logger, mockUserSignIn.Object, mockUpdateUserSignOut.Object, mockGetSigninStatus.Object);
            mockGetSigninStatus.Setup(x => x.GetSigninStatus(userrequestdata)).Returns(GetUsersignstatusInValid);
            Task<IActionResult> result = UserControllerobj.GetSignInStatus(userrequestdata);
            ObjectResult obj = (ObjectResult)result.Result;
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(obj.StatusCode, 400);
        }

    }
}
