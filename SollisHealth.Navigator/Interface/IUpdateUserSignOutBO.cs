using SollisHealth.Navigator.Model;
using SollisHealth.Navigator.Model.UpdateUserSignOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Interface
{
    public interface IUpdateUserSignOutBO
    {
        Task<UserSignOutResponse> UpdateUserSignout(UpdateSignInUserRequest updateUserInput);
    }
}
