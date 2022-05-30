using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;

namespace WebApp.Ninject
{
    public class Binder : NinjectModule
    {
        public override void Load()
        {
            Bind<IUserService>().To<UserService>();
            Bind<ITransportService>().To<TransportService>();
            Bind<ITourService>().To<TourService>();
            Bind<IHotelService>().To<HotelService>();
        }
    }
}