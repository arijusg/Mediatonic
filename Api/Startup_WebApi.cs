using System.Net.Http.Formatting;
using System.Web.Http;
using Api.Ninject;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject;

namespace Api
{
    partial class Startup
    {
        public static IKernel Container { get; private set; }

        private HttpConfiguration GetWebApiConfiguration()
        {
            var config = new HttpConfiguration();
         
            config.MapHttpAttributeRoutes();

            Container = new Bootstrapper().Start();
            config.DependencyResolver = new NinjectDependencyResolver(Container);


            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());
            config.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            return config;
        }
    }
}
