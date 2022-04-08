using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SollisHealth.Navigator.Controllers;
using SollisHealth.Navigator.Helper;
using SollisHealth.Navigator.Interface;
using SollisHealth.Navigator.Model;
using SollisHealth.Navigator.Model.UserSignIn;
using SollisHealth.Navigator.Repository;
using SollisHealth.Navigator.Services;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.UnitTest
{
    [TestClass]
    public class UserSignInUnitTest
    {
        /// <summary>
        /// Check Member Details Repository class if exists member ID
        /// </summary>
        [TestMethod]
        public void AddUserSigninSuccess_Repository()
        {
            var options = new DbContextOptionsBuilder<SignInUserDbContext>()
                        .UseInMemoryDatabase(databaseName: "NavigatorDataBase")
                        .Options;

            using (var context = new SignInUserDbContext(options))
            {
                SignInUserUIRequest userrequestdata = new SignInUserUIRequest();
                userrequestdata.SignInDate = System.DateTime.Now;
                userrequestdata.SignOutDate = System.DateTime.Now;

                context.User_sign_details.Add(new SignInUserRequest
                {
                    User_Name = userrequestdata.UserName = "Phills",
                    User_Email = userrequestdata.UserEmail = "validemail@gmail.com",
                    Depatment_Name = userrequestdata.DepartmentName = "eye",
                    Contact_Type = userrequestdata.ContactType = "mobile",
                    Contact_Number = userrequestdata.ContactNumber = "234-232",
                    Service = userrequestdata.Service = "aaa",
                    User_Role = userrequestdata.UserRole = "admin",
                    Sign_in_date = userrequestdata.SignInDate,
                    Sign_Out_Date = userrequestdata.SignOutDate,
                    Sign_Status = 1,
                    Shift = userrequestdata.Shift = "night",
                    Comments = userrequestdata.Comments = "comment",
                    Createddate = System.DateTime.Now,
                    Createduser = userrequestdata.UserName = "Phills",
                    Modifieduser = null,
                    Processed = 0

                });

                context.SaveChanges();

                UserSignInRepo repoObject = new UserSignInRepo(context);
                Task<UserSignInResponse> result = repoObject.AddUserforSignIn(userrequestdata);

                var actualResult = result.Result.data;
                Assert.AreEqual(result.Result.success, true);
                Assert.IsTrue(result.IsCompletedSuccessfully);

            }

        }
  

        [TestMethod]
        public void AddUserSigninSUCCESS_BO()
        {
            SignInUserUIRequest userrequestdata = new SignInUserUIRequest();
            userrequestdata = UserSigninValidData();
            var mockusersigninRepo = new Mock<IUserSignInRepo>();
            mockusersigninRepo.Setup(x => x.AddUserforSignIn(userrequestdata)).Returns(GetUsersigninID);

            UserSignInBO memberBO = new UserSignInBO(mockusersigninRepo.Object);
            Task<UserSignInResponse> result = memberBO.AddUserforSignIn(userrequestdata);

            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(result.Result.success, true);
        }

        private SignInUserUIRequest UserSigninValidData()
        {
            SignInUserUIRequest userrequestdata = new SignInUserUIRequest();
            userrequestdata.UserName = "Phills";
            userrequestdata.UserEmail = "validemail@gmail.com";
            userrequestdata.DepartmentName = "eye";
            userrequestdata.ContactType = "mobile";
            userrequestdata.ContactNumber = "234-232";
            userrequestdata.Service = "aaa";
            userrequestdata.UserRole = "admin";
            userrequestdata.SignInDate = System.DateTime.Now;
            userrequestdata.SignOutDate = System.DateTime.Now;
            userrequestdata.Shift = "night";
            userrequestdata.Comments = "comment";
            return userrequestdata;
        }
        private Task<UserSignInResponse> GetUsersigninID()
        {
            UserSignInOutput obj_useridoutput = new UserSignInOutput();
            UserSignInResponse obj_userresponse = new UserSignInResponse();
            obj_useridoutput.UserSignInId = 4;

            if (obj_useridoutput != null)
            {
                obj_userresponse.data = obj_useridoutput;
                obj_userresponse.success = true;
                obj_userresponse.Message = "User SignIn information successfully registered";
            }
            return Task.FromResult(obj_userresponse);
        }

        [TestMethod]
        public void AddUserSigninFAILURE_BO()
        {
            SignInUserUIRequest userrequestdata = new SignInUserUIRequest();
            userrequestdata = UserSigninInValidData();
            var mockusersigninRepo = new Mock<IUserSignInRepo>();
            mockusersigninRepo.Setup(x => x.AddUserforSignIn(userrequestdata)).Returns(GetUsersigninIDInvalid);

            UserSignInBO memberBO = new UserSignInBO(mockusersigninRepo.Object);
            Task<UserSignInResponse> result = memberBO.AddUserforSignIn(userrequestdata);

            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(result.Result.data.UserSignInId, 0);
        }


        private SignInUserUIRequest UserSigninInValidData()
        {
            SignInUserUIRequest userrequestdata = new SignInUserUIRequest();
          
            userrequestdata.UserName = "";
            userrequestdata.UserEmail = "Invalidemail";
            userrequestdata.DepartmentName = "";
            userrequestdata.ContactType = "";
            userrequestdata.ContactNumber = "234-232";
            userrequestdata.Service = "aaa";
            userrequestdata.UserRole = "admin";
            userrequestdata.SignInDate = System.DateTime.Now;
            userrequestdata.SignOutDate = System.DateTime.Now;
            userrequestdata.Shift = "night";
            userrequestdata.Comments = "comment";
            return userrequestdata;
        }
        private Task<UserSignInResponse> GetUsersigninIDInvalid()
        {
            UserSignInOutput obj_useridoutput = new UserSignInOutput();
            UserSignInResponse obj_userresponse = new UserSignInResponse();
             obj_useridoutput.UserSignInId = 0; 
             obj_userresponse.data = obj_useridoutput;
            obj_userresponse.success = false;

            return Task.FromResult(obj_userresponse);
        }

        [TestMethod]
        public void UsersigninSUCCESSControllerTest()
        {
            var mockLogger = new Mock<ILogger<UserController>>();
            ILogger<UserController> logger = mockLogger.Object;

            var mockUpdateUserSignOut = new Mock<IUpdateUserSignOutBO>();
            var mockUserSignIn = new Mock<IUserSignInBO>();
            var mockGetSigninStatus = new Mock<IGetSigninStatusBO>();

            SignInUserUIRequest userrequestdata = new SignInUserUIRequest();
            userrequestdata = UserSigninValidData();

            UserController UserControllerobj = new UserController(logger, mockUserSignIn.Object, mockUpdateUserSignOut.Object, mockGetSigninStatus.Object);
            mockUserSignIn.Setup(x => x.AddUserforSignIn(userrequestdata)).Returns(GetUsersigninID);
            Task<IActionResult> result = UserControllerobj.AddUserSignInfo(userrequestdata);
            ObjectResult obj = (ObjectResult)result.Result;
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(obj.StatusCode, 200);
        }

        /// <summary>
        /// Check Member Details Controller class if exists member ID
        /// </summary>
        [TestMethod]
        public void UsersigninFAILUREControllerTest()
        {
            var mockLogger = new Mock<ILogger<UserController>>();
            ILogger<UserController> logger = mockLogger.Object;

            var mockUpdateUserSignOut = new Mock<IUpdateUserSignOutBO>();
            var mockUserSignIn = new Mock<IUserSignInBO>();
            var mockGetSigninStatus = new Mock<IGetSigninStatusBO>();

            SignInUserUIRequest userrequestdata = new SignInUserUIRequest();
            userrequestdata = UserSigninInValidData();
            var mockuserlistobj = new Mock<UserSignInResponse>();

            UserController UserControllerobj = new UserController(logger, mockUserSignIn.Object, mockUpdateUserSignOut.Object, mockGetSigninStatus.Object);
            mockUserSignIn.Setup(x => x.AddUserforSignIn(userrequestdata)).Returns(GetUsersigninIDInvalid);
            Task<IActionResult> result = UserControllerobj.AddUserSignInfo(userrequestdata);
            ObjectResult obj = (ObjectResult)result.Result;
            Assert.IsTrue(result.IsCompletedSuccessfully);
            Assert.AreEqual(obj.StatusCode, 400);
        }

    }
}