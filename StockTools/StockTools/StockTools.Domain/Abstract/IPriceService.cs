using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Abstract
{
    public interface IPriceService
    {
        double GetCompanyPriceByShortName(string shortName);
        double GetCompanyPriceByFullName(string shortName);
        double GetCompanyPriceByShortNameAndDateTime(string shortName, DateTime dateTime);
        double GetCompanyPriceByFullNameAndDateTime(string shortName, DateTime dateTime);
    }
}
