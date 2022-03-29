using Microsoft.EntityFrameworkCore;
using SollisHealth.Navigator.Model;
using SollisHealth.Navigator.Model.GetSignInStatus;
using SollisHealth.Navigator.Model.UserSignIn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator.Helper
{
    /// <summary>
    /// TaskDbContext class is used as a database context to connect to database for to perform database operations
    /// </summary>
    public class SignInUserDbContext : DbContext
    {
        public SignInUserDbContext(DbContextOptions<SignInUserDbContext> options) : base(options)
        {

        }
        public DbSet<SignInUserRequest> User_sign_details { get; set; }
        public DbSet<GetSignInStatusUserOutput> vm_user_sign_details { get; set; }


    }

}
