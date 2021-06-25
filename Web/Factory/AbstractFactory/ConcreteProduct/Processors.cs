using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Factory.AbstractFactory.AbstractProduct;


namespace Web.Factory.AbstractFactory.ConcreteProduct
{
    public class I7 : IProcessor
    {
        public string GetProcessor() => Enumerations.Processors.I7.ToString();
    }
    public class I5 : IProcessor
    {
        public string GetProcessor() => Enumerations.Processors.I5.ToString();
    }
}