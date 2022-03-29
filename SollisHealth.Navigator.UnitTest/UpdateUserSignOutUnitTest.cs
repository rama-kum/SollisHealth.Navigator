using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SollisHealth.Navigator.Controllers;
using SollisHealth.Navigator.Helper;
using SollisHealth.Navigator.Interface;
using SollisHealth.Navigator.Model;
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
    public class UpdateUserSignOutUnitTest
    {
        [TestMethod]
        public void UpdateUserSignOutSuccess_Repository()
        {
            var options = new DbContextOptionsBuilder<SignInUserDbContext>()
                        .UseInMemoryDatabase(databaseName: "NavigatorDataBase")
                        .Options;

            using (var context = new SignInUserDbContext(options))
            {
                UpdateSignInUserRequest userrequestdata = new UpdateSignInUserRequest();
                userrequestdata.SignInUserId = 1;
 
                context.User_sign_details.Add(new SignInUserRequest
                {
                    Actual_Sign_Out = System.DateTime.Now,
                    Sign_Status = 0
                });

                context.SaveChanges();

                UpdateUserSignOutRepo repoObject = new UpdateUserSignOutRepo(context);
                Task<UserSignOutResponse> result = repoObject.UpdateUserSignout(userrequestdata);
               
                Assert.AreEqual(result.Result.success, true);
                Assert.IsTrue(result.IsCompletedSuccessfully);

            }

        }

        [TestMethod]
        public void UpdateUserSignOutFAILURE_Repository()
        {
            var options = new DbContextOptionsBuilder<SignInUserDbContext>()
                        .UseInMemoryDatabase(databaseName: "NavigatorDataBase")
                        .Options;

            using (var context = new SignInUserDbContext(options))
            {
                UpdateSignInUserRequest userrequestdata = new UpdateSignInUserRequest();
                userrequestdata.SignInUserId = 99;

                context.User_sign_details.Add(new SignInUserRequest
                {
                    Actual_Sign_Out = System.DateTime.Now,
                    Sign_Status = 0
                });

                context.SaveChanges();

                UpdateUserSignOutRepo repoObject = new UpdateUserSignOutRepo(context);
                Task<UserSignOutResponse> result = repoObject.UpdateUserSignout(userrequestdata);

                Assert.AreEqual(result.Result.success, false);
                Assert.IsTrue(result.IsCompletedSuccessfully);

            }

        }   


        [TestMethod]
        public void UpdateUserSignOutSUCCESS_BO()
        {
            UpdateSignInUserRequest userrequestdata = new UpdateSignInUserRequest();
            userrequestdata.SignInUserId = 99;
            var mockusersignoutRepo = new Mock<IUpdateUserSignOutRepo>();
            mockusersignoutRepo.Setup(x => x.UpdateUserSignout(userrequestdata)).Returns(GetUsersignoutValid);

            UpdateUserSignOutBO usersignoutBO = new UpdateUserSignOutBO(mockusersignoutRepo.Object);
            Task<UserSignOutResponse> result = usersignoutBO.UpdateUserSignout(userrequestdata);

            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(result.Result.success, true);
        }

         private Task<UserSignOutResponse> GetUsersignoutValid()
        {
            UserSignOutResponse obj_userresponse = new UserSignOutResponse();
     
            obj_userresponse.success = true;
            //obj_userresponse.Message = "User SignIn information successfully registered";
          
            return Task.FromResult(obj_userresponse);
        }


        [TestMethod]
        public void UpdateUserSignOutFAILURE_BO()
        {
            UpdateSignInUserRequest userrequestdata = new UpdateSignInUserRequest();
            userrequestdata.SignInUserId = 99;
            var mockusersignoutRepo = new Mock<IUpdateUserSignOutRepo>();
            mockusersignoutRepo.Setup(x => x.UpdateUserSignout(userrequestdata)).Returns(GetUsersignoutInValid);

            UpdateUserSignOutBO usersignoutBO = new UpdateUserSignOutBO(mockusersignoutRepo.Object);
            Task<UserSignOutResponse> result = usersignoutBO.UpdateUserSignout(userrequestdata);

            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(result.Result.success, false);
        }

 
        private Task<UserSignOutResponse> GetUsersignoutInValid()
        {
            UserSignOutResponse obj_userresponse = new UserSignOutResponse();

            obj_userresponse.success = false;
            //obj_userresponse.Message = "User SignIn information successfully registered";

            return Task.FromResult(obj_userresponse);
        }

        [TestMethod]
        public void UpdateUserSignOutSUCCESS_Controller()
        {
            var mockLogger = new Mock<ILogger<UserController>>();
            ILogger<UserController> logger = mockLogger.Object;

            var mockUpdateUserSignOut = new Mock<IUpdateUserSignOutBO>();
            var mockUserSignIn = new Mock<IUserSignInBO>();
            var mockGetSigninStatus = new Mock<IGetSigninStatusBO>();

             UpdateSignInUserRequest userrequestdata = new UpdateSignInUserRequest();
            userrequestdata.SignInUserId = 4;

            UserController UserControllerobj = new UserController(logger, mockUserSignIn.Object, mockUpdateUserSignOut.Object, mockGetSigninStatus.Object);
            mockUpdateUserSignOut.Setup(x => x.UpdateUserSignout(userrequestdata)).Returns(GetUsersignoutValid);
            Task<IActionResult> result = UserControllerobj.UpdateUserSignOut(userrequestdata);
            ObjectResult obj = (ObjectResult)result.Result;
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(obj.StatusCode, 200);
        }

        /// <summary>
        /// Check User Navigator Controller class if exists member ID
        /// </summary>
        [TestMethod]
        public void UpdateUserSignOutFAILURE_Controller()
        {
            var mockLogger = new Mock<ILogger<UserController>>();
            ILogger<UserController> logger = mockLogger.Object;

            var mockUpdateUserSignOut = new Mock<IUpdateUserSignOutBO>();
            var mockUserSignIn = new Mock<IUserSignInBO>();
            var mockGetSigninStatus = new Mock<IGetSigninStatusBO>();

            UpdateSignInUserRequest userrequestdata = new UpdateSignInUserRequest();
            userrequestdata.SignInUserId = 88;

            UserController UserControllerobj = new UserController(logger, mockUserSignIn.Object, mockUpdateUserSignOut.Object, mockGetSigninStatus.Object);
            mockUpdateUserSignOut.Setup(x => x.UpdateUserSignout(userrequestdata)).Returns(GetUsersignoutInValid);
            Task<IActionResult> result = UserControllerobj.UpdateUserSignOut(userrequestdata);
            ObjectResult obj = (ObjectResult)result.Result;
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(obj.StatusCode, 400);
        }

    }
}
