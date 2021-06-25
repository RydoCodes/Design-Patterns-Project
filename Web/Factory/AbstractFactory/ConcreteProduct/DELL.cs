using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Factory.AbstractFactory.AbstractProduct;

namespace Web.Factory.AbstractFactory.ConcreteProduct
{
    public class DELL : IBrand
    {
        public string GetBrand() => Enumerations.Brands.DELL.ToString();
    }
}