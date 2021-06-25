using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Factory.AbstractFactory.AbstractInterface;
using Web.Factory.AbstractFactory.AbstractProduct;
using Web.Factory.AbstractFactory.ConcreteProduct;
using Web.Factory.AbstractFactory.ConcreteFactory;

namespace Web.Factory.AbstractFactory.Client
{
    public class EmployeeSystemManager
    {
        IComputerFactory _IComputerFactory;
        public EmployeeSystemManager(IComputerFactory iComputerFactory)
        {
            _IComputerFactory = iComputerFactory;
        }
        public string GetSystemDetails()
        {
            IBrand brand = _IComputerFactory.Brand();
            IProcessor processor = _IComputerFactory.Processor();
            ISystemType systemType = _IComputerFactory.SystemType();
            string returnValue = string.Format("{0} {1} {2}", brand.GetBrand(),
                systemType.GetSystemType(), processor.GetProcessor());
            return returnValue;
        }
    }
}