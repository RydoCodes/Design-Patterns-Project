using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.BuilderDesignPattern.IBuilder;
using Web.BuilderDesignPattern.Product;

namespace Web.BuilderDesignPattern.ConcreteBuilder
{
    public class LaptopBuilder : ISystemBuilder
    {
        ComputerSystem laptop = new ComputerSystem();
        public void AddDrive(string size)
        {
            laptop.HDDSize = size;
        }

        public void AddKeyBoard(string type)
        {
            return;
        }

        public void AddMemory(string memory)
        {
            laptop.RAM = memory;
        }

        public void AddMouse(string type)
        {
            return;
        }

        public void AddTouchScreen(string enabled)
        {
            laptop.TouchScreen = enabled;
        }

        public ComputerSystem GetSystem()
        {
            return laptop;
        }
    }
}