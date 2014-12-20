using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace StockTools.WPFUI.Models
{
    public class PlotViewModel
    {
        public PlotViewModel()
        {
            
        }

        public PlotModel Plot { get; private set; }
    }
}
