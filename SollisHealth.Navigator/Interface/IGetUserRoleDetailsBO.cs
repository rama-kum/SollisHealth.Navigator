
using SollisHealth.Navigator.Model;
using SollisHealth.Navigator.Model.GetUserRoleDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Interface
{
    /// <summary>
    /// ITaskBO interface is inherited by TaskBO
    /// </summary>
    public interface IGetUserRoleDetailsBO
    {
        Task<GetUserRoleDetailsResponse> getuserroledetails(GetUserRoleDetailsRequest userInput);
    }
}

