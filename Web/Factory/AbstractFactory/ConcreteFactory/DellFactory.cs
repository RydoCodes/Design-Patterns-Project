using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Factory.AbstractFactory.AbstractInterface;
using Web.Factory.AbstractFactory.AbstractProduct;
using Web.Factory.AbstractFactory.ConcreteProduct;


namespace Web.Factory.AbstractFactory.ConcreteFactory
{
    public class DellFactory : IComputerFactory
    {
        public IBrand Brand()
        {
            return new DELL();
        }

        public IProcessor Processor()
        {
            return new I5();
        }

        public virtual ISystemType SystemType()
        {
            return new Desktop();
        }
    }
    public class DellLaptopFactory : DellFactory
    {
        public override ISystemType SystemType()
        {
            return new Laptop();
        }
    }
}
