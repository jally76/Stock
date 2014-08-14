using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StooqProxy.Model
{
    public class MCompany
    {
        double PriceLast { get; set; }
        DateTime PriceDate { get; set; }
        double PriceBid { get; set; }
        double PriceAsk { get; set; }
        double Volume { get; set; }
        double Turnover { get; set; }
        int OpenInt { get; set; }
    }
}
