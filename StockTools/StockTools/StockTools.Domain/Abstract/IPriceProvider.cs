using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Abstract
{
    public interface IPriceProvider
    {
        void LoadPrices(string path);
        void LoadPrices(Uri url);

        double GetPriceByShortName(string shortName);
        double GetPriceByFullName(string shortName);
        double GetPriceByShortNameAndDateTime(string shortName, DateTime dateTime);
        double GetPriceByFullNameAndDateTime(string shortName, DateTime dateTime);
    }
}
