using SollisHealth.Navigator.Interface;
using SollisHealth.Navigator.Model;
using SollisHealth.Navigator.Model.UpdateUserSignOut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Services
{
    public class UpdateUserSignOutBO : IUpdateUserSignOutBO
    {
        private readonly IUpdateUserSignOutRepo _userRepo;
        public UpdateUserSignOutBO(IUpdateUserSignOutRepo userRepo)
        {
            _userRepo = userRepo;
        }
        public async Task<UserSignOutResponse> UpdateUserSignout(UpdateSignInUserRequest updateUserInput)
        {
            UserSignOutResponse UserResponse = await _userRepo.UpdateUserSignout(updateUserInput);
            return UserResponse;

        }
    }
}
