using Microsoft.Owin.Cors;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;

namespace RadiantTulip.API
{
    public class StartUp
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "API",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            var corsOptions = new CorsOptions
            {
                PolicyProvider = new CorsPolicyProvider
                {
                    PolicyResolver = c => Task.FromResult(new CorsPolicy
                        {
                            AllowAnyHeader = true,
                            AllowAnyMethod = true,
                            AllowAnyOrigin = true,
                            SupportsCredentials = true
                        })
                }
            };

            appBuilder.UseCors(corsOptions);
            appBuilder.UseWebApi(config);
        }
    }
}
