using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using System.Web.Http;
using BonusRacing.IoC;
using System.Configuration;

namespace BonusRacing.Api
{
    public partial class Startup
    {
        public IKernel ConfigureDependencyResolver(IAppBuilder app, HttpConfiguration httpConfiguration)
        {
            var kernel = CreateKernel();
            app.UseNinjectMiddleware(() => kernel)
                .UseNinjectWebApi(httpConfiguration);
            return kernel;
        }

        private IKernel CreateKernel()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["main"].ConnectionString;

            var kernel = new StandardKernel();
            kernel.Load(new DataModule(connectionString));

            return kernel;
        }
    }
    
}