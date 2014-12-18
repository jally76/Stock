using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Abstract
{
    public interface IArchivePriceProvider
    {
        //double GetPriceByShortNameAndDateTime(string shortName, DateTime dateTime);
        double GetPriceByFullNameAndDateTime(string shortName, DateTime dateTime);
    }
}
