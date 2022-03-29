using SollisHealth.Navigator.Model;
using SollisHealth.Navigator.Model.UserSignIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Interface
{
    //ITaskRepo interface is inherited by TaskInfoRepo
    public interface IUserSignInRepo
    {
        Task<UserSignInResponse> AddUserforSignIn(SignInUserUIRequest userInput);
    }
}
