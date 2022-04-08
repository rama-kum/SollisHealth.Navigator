using SollisHealth.Navigator.Model;
using SollisHealth.Navigator.Model.GetUserRoleDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Interface
{
    //ITaskRepo interface is inherited by TaskInfoRepo
    public interface IGetUserRoleDetailsRepo
    {
        Task<GetUserRoleDetailsResponse> getuserroledetails(GetUserRoleDetailsRequest userInput);
    }
}
