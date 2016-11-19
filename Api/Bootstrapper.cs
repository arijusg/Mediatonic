using System.Collections.Generic;
using System.Web.Http;
using Api.Ninject;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace Api
{
    public class Bootstrapper
    {
        private StandardKernel _kernel;

        public StandardKernel Kernel
        {
            get { return _kernel; }
        }

        public IKernel Start(params INinjectModule[] additionalModules)
        {
            _kernel = new StandardKernel();
            _kernel.Bind(x => x.FromThisAssembly()
                .Select(y => y.BaseType == typeof(ApiController))
                .BindToSelf());

            var modules = new List<INinjectModule>();
            modules.Add(new NinjectServicesModule());

            if (additionalModules != null && additionalModules.Length > 0)
                modules.AddRange(additionalModules);

            _kernel.Load(modules.ToArray());

            return _kernel;

        }
    }
}
