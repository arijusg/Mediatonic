using Api.Services;
using Ninject.Extensions.Conventions;
using Ninject.Modules;

namespace Api.Ninject
{
    public class NinjectServicesModule : NinjectModule
    {
        public override void Load()
        {
            //Kernel.Bind(x => x
            //    .From(GetType().Assembly)
            //    .SelectAllClasses().InNamespaceOf(GetType())
            //    .BindAllInterfaces());

            Kernel.Bind<IUserService>().To<UserService>();
            Kernel.Bind<IAnimalService>().To<AnimalService>();
            Kernel.Bind<ITestableDateTime>().To<TestableDateTime>();
            Kernel.Bind<IAnimalFactory>().To<AnimalFacotry>();
        }
    }
}
