using BonusRacing.Api.App_Start;
using BonusRacing.Api.Helpers;
using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: OwinStartup(typeof(BonusRacing.Api.Startup))]
namespace BonusRacing.Api
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var httpConfiguration = new HttpConfiguration();
            httpConfiguration.MessageHandlers.Add(new LogRequestHandler());
            WebApiConfig.Register(httpConfiguration);
            AuthConfig.ConfigureAuth(app, httpConfiguration);

            var kernel = ConfigureDependencyResolver(app, httpConfiguration);
            //app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            //app.UseWebApi(httpConfiguration);
        }
    }
}