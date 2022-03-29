
using SollisHealth.Navigator.Model;
using SollisHealth.Navigator.Model.UserSignIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Interface
{
    /// <summary>
    /// ITaskBO interface is inherited by TaskBO
    /// </summary>
    public interface IUserSignInBO
    {
        Task<UserSignInResponse> AddUserforSignIn(SignInUserUIRequest userInput);
    }
}
