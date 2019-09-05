//using Microsoft.AspNet.Identity;
//using Owin;
//using Microsoft.Owin;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Microsoft.Owin.Security.OAuth;
//using BonusRacing.Api.Infrastructure.Authenication;
//using Ninject;
//using Microsoft.Owin.Security.Infrastructure;
//using System.Threading.Tasks;

//namespace BonusRacing.Api
//{
//	public partial class Startup
//	{
//        public string PublicClientId;
//        //public OAuthAuthorizationServerOptions OAuthOptions;
//        public OAuthAuthorizationServerOptions options;

//        public void ConfigureAuth(IAppBuilder app, IKernel kernel)
//        {
//            //app.CreatePerOwinContext(() => kernel);
//            //        app.CreatePerOwinContext(ApplicationUserStore.Create);
//            //        app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

//            //        PublicClientId = "self";
//            //        OAuthOptions = new OAuthAuthorizationServerOptions()
//            //        {
//            //TokenEndpointPath = new PathString("/Token"),
//            //Provider = new ApplicationOAuthProvider(PublicClientId),
//            //            AuthorizeEndpointPath = new PathString("/Account/ExternalLogin"),
//            //            AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
//            //            AllowInsecureHttp = true
//            //        };
//            //        app.UseOAuthBearerTokens(OAuthOptions);
//            //далее было закомментировано 25.03
//            //options = new OAuthAuthorizationServerOptions
//            //{
//            //    TokenEndpointPath = new PathString("/token"),
//            //    Provider = new SimpleAuthorizationServerProvider(),
//            //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
//            //    AllowInsecureHttp = true
//            //};

//            //// Token Generation
//            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
//            //{
//            //    AccessTokenProvider = new AuthenticationTokenProvider()
//            //});
//            //app.UseOAuthBearerTokens(options);


//            OAuthAuthorizationServerOptions oAuthServerOptions = new OAuthAuthorizationServerOptions()
//            {
//                AllowInsecureHttp = true,
//                TokenEndpointPath = new PathString("/token"),
//                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
//                Provider = new SimpleAuthorizationServerProvider()
//            };

//            app.UseOAuthAuthorizationServer(oAuthServerOptions);
//            app.UseOAuthBearerAuthentication
//            (
//                new OAuthBearerAuthenticationOptions
//                {
//                    Provider = new OAuthBearerAuthenticationProvider()
//                }
//            );
//        }
//	}
//}