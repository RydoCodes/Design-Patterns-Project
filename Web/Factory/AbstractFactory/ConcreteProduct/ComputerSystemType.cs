using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Factory.AbstractFactory.AbstractProduct;

namespace Web.Factory.AbstractFactory.ConcreteProduct
{
    public class Laptop : ISystemType
    {
        public string GetSystemType() => Enumerations.ComputerTypes.Laptop.ToString();
    }
    public class Desktop : ISystemType
    {
        public string GetSystemType() => Enumerations.ComputerTypes.Desktop.ToString();
    }
}