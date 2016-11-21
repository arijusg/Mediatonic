using Api.DAL;
using Api.Helpers;
using Api.Interfaces;
using Api.Mappers;
using Api.Services;
using Ninject.Modules;

namespace Api.Ninject
{
    public class NinjectServicesModule : NinjectModule
    {
        public override void Load()
        {
            Kernel.Bind<IUserService>().To<UserService>();
            Kernel.Bind<IAnimalService>().To<AnimalService>();
            Kernel.Bind<ITestableDateTime>().To<TestableDateTime>();
            Kernel.Bind<IUserMapper>().To<UserMapper>();
            Kernel.Bind<IAnimalMapper>().To<AnimalMapper>();
            Kernel.Bind<IAnimalProcessor>().To<AnimalProcessor>();

            Kernel.Bind<GameContext>().ToSelf();
        }
    }
}
