using UnitsOfWork;
using UnitsOfWork.Interfaces;

namespace BLL.Ninject
{
    public static class DependencyResolver
    {
        static UnitOfWork UoW;

        static DependencyResolver()
        {
            UoW = new UnitOfWork();
        }

        public static IUnitOfWork ResolveUoW()
        {
            return UoW;
        }
    }
}
