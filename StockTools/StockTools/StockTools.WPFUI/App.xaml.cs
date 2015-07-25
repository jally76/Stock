using Ninject;
using StockTools.BusinessLogic.Concrete;
using StockTools.Data.HistoricalData;
using StockTools.Domain.Abstract;
using StockTools.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StockTools.WPFUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IKernel container;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            this.container = new StandardKernel();
            container.Bind<IIntradayDataDownloader>().To<BOSSAIntradayDataDownloader>().InTransientScope();
            container.Bind<IIntradayDataParser>().To<BOSSAIntradayDataParser>().InTransientScope();
            container.Bind<IHistoricalDataProvider>().To<HistoricalDataProvider>().InTransientScope();
            container.Bind<IPriceProvider>().To<PriceProvider>().InTransientScope();
            container.Bind<IBookkeepingService>().To<MbankBookkeepingService>().InTransientScope();
            container.Bind<IPortfolio>().To<Portfolio>().InTransientScope()
                .WithConstructorArgument("chargeFunction", new Func<double, double>(x => ChargeFunc(x)));
        }

        private void ComposeObjects()
        {
            Current.MainWindow = this.container.Get<MainWindow>();
        }

        private double ChargeFunc(double price)
        {
            if (price <= 769)
            {
                return 3.0;
            }
            else
            {
                return price * (0.39 / 100.0);
            }
        }
    }
}
