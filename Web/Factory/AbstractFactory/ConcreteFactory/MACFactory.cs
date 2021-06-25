using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Factory.AbstractFactory.AbstractInterface;
using Web.Factory.AbstractFactory.AbstractProduct;
using Web.Factory.AbstractFactory.ConcreteProduct;

namespace Web.Factory.AbstractFactory.ConcreteFactory
{
    public class MACFactory : IComputerFactory
    {
        public IBrand Brand()
        {
            return new MAC();
        }

        public IProcessor Processor()
        {
            return new I7();
        }

        public virtual ISystemType SystemType()
        {
            return new Desktop();
        }
    }
    public class MACLaptopFactory : MACFactory
    {

        public override ISystemType SystemType()
        {
            return new Laptop();
        }
    }
}