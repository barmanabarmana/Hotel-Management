using BLL.Interfaces;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Ninject
{
    public static class UIDependencyResolver<T>
    {
        public static dynamic ResolveDependency()
        {
            if (typeof(T) == typeof(IUserService))
                return new UserService();
            else if (typeof(T) == typeof(ITransportService))
                return new TransportService();
            else if (typeof(T) == typeof(ITourService))
                return new TourService();
            else if (typeof(T) == typeof(IHotelService))
                return new HotelService();
            else return null;
        }
    }
}