using SollisHealth.Navigator.Interface;
using SollisHealth.Navigator.Model;
using SollisHealth.Navigator.Model.UserSignIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Services
{
    public class UserSignInBO : IUserSignInBO
    {
        private readonly IUserSignInRepo _userRepo;
        public UserSignInBO(IUserSignInRepo userRepo)
        {
            _userRepo = userRepo;
        }

  
        public async Task<UserSignInResponse> AddUserforSignIn(SignInUserUIRequest userInput)
        {
            UserSignInResponse userresponse = await _userRepo.AddUserforSignIn(userInput);
            return userresponse;

        }

 
    }
}
