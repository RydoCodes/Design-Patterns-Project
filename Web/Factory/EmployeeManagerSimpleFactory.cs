using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Web.Manages;

namespace Web.Factory
{
    public class EmployeeManagerSimpleFactory
    {
        public IEmployeeManager GetEmployeeManager(int employeeTypeID)
        {
            IEmployeeManager returnvalue = null;

            if(employeeTypeID==1)
            {
                returnvalue = new PermanentEmployeeManager();
            }
            else if (employeeTypeID == 2)
            {
                returnvalue = new ContractEmployeeManager();
            }
            return returnvalue;
        }
    }
}