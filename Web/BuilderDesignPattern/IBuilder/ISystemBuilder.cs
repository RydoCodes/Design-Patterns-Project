using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.BuilderDesignPattern.Product;

namespace Web.BuilderDesignPattern.IBuilder
{
    public interface ISystemBuilder
    {
        void AddMemory(string memory);
        void AddDrive(string size);
            
        void AddKeyBoard(string type);
        void AddMouse(string type);

        void AddTouchScreen(string enabled);

        ComputerSystem GetSystem();
    }
}
