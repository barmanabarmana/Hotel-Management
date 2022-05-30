using Ninject.Modules;
using UnitsOfWork;
using UnitsOfWork.Interfaces;

namespace LogicTests
{
    public class TestBinder : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}
