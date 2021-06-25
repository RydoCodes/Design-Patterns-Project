using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Manages
{
    public class ContractEmployeeManager : IEmployeeManager
    {
        public decimal GetBonus()
        {
            return 5;
        }

        public decimal GetPay()
        {
            return 10;
        }

        public decimal GetMedicalAllowance()
        {
            return 100;
        }
    }
}