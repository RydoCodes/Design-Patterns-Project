using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Factory.AbstractFactory.AbstractProduct;

namespace Web.Factory.AbstractFactory.AbstractInterface
{
    public interface IComputerFactory
    {
        IProcessor Processor();
        IBrand Brand();
        ISystemType SystemType();
    }
}

