using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
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
