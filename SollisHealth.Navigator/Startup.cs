using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SollisHealth.Common.Helpers;
using SollisHealth.Common.Helpers.Security;
using SollisHealth.Navigator.Helper;
using SollisHealth.Navigator.Interface;
using SollisHealth.Navigator.Model.Repository;
using SollisHealth.Navigator.Repository;
using SollisHealth.Navigator.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SollisHealth.Navigator
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Encrypted string from appsettings.json file is taken and decrypted for connecting to database
            services = new StartupCommon(Configuration).ConfigureServices(services);
            AesCryptoUtil _AesCryptoUtil = new AesCryptoUtil();
            var connectionstring = Configuration.GetConnectionString("NavigatorConnection");
            //   var deconnectionstring1 = _AesCryptoUtil.Encrypt(connectionstring);
            // var deconnectionstring = _AesCryptoUtil.Decrypt(deconnectionstring1);
           // var deconnectionstring = _AesCryptoUtil.Decrypt(connectionstring);
          //  deconnectionstring = deconnectionstring.Replace("\v", "");
          //  services.AddDbContextPool<UserDbContext>(options => options.UseMySQL(deconnectionstring));
              services.AddDbContextPool<SignInUserDbContext>(options => options.UseMySQL(connectionstring));
            services.AddScoped<IUserSignInBO, UserSignInBO>();
            services.AddScoped<IUserSignInRepo, UserSignInRepo>();
            services.AddScoped<IUpdateUserSignOutBO, UpdateUserSignOutBO>();
            services.AddScoped<IUpdateUserSignOutRepo, UpdateUserSignOutRepo>();
            services.AddScoped<IGetSigninStatusBO, GetSignInStatusBO>();
            services.AddScoped<IGetSigninStatusRepo, GetSignInStatusRepo>();
            // services.AddScoped<MysqlHelper>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            //HTTP request pipeline configuration is done from SollisHealth.common.Helper common package 
            app = new StartupCommon(Configuration).Configure(app, env, provider);

        }
    }
}
