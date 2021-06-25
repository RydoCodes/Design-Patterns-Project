using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.BuilderDesignPattern.IBuilder;
using Web.BuilderDesignPattern.Product;

namespace Web.BuilderDesignPattern.ConcreteBuilder
{
    public class DesktopBuilder : ISystemBuilder
    {
        ComputerSystem desktop = new ComputerSystem();
        public void AddDrive(string size)
        {
            desktop.HDDSize = size;
        }
        public void AddKeyBoard(string type)
        {
            desktop.KeyBoard = type;
        }
        public void AddMemory(string memory)
        {
            desktop.RAM = memory;
        }
        public void AddMouse(string type)
        {
            desktop.Mouse = type;
        }
        public void AddTouchScreen(string enabled)
        {
            return;
        }
        public ComputerSystem GetSystem()
        {
            return desktop;
        }
    }
}