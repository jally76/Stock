﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTools.BusinessLogic.Abstract
{
    public interface ICurrentPriceProvider
    {
        double GetPriceByShortName(string shortName);
        double GetPriceByFullName(string shortName);
    }
}