using Microsoft.Owin.Cors;
using Microsoft.Practices.Unity;
using Owin;
using RadiantTulip.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Cors;
using System.Web.Http;
using Unity.SelfHostWebApiOwin;

namespace RadiantTulip.API
{
    public class StartUp
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var container = ConfigureContainer();
            var config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "API",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.DependencyResolver = new UnityDependencyResolver(container);


            config.Formatters.JsonFormatter.SerializerSettings.Formatting
                = Newtonsoft.Json.Formatting.Indented;

            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add
                (new Newtonsoft.Json.Converters.StringEnumConverter());

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

        private IUnityContainer ConfigureContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IGroundReader, JsonGroundReader>();
            container.RegisterType<IFileSystem, FileSystem>();

            return container;
        }
    }
}
