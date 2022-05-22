using Ninject.Modules;
using UnitsOfWork.Interfaces;
using UnitsOfWork;

namespace BLL.Ninject
{
    public class UoWBinder : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
