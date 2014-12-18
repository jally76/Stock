using Microsoft.Win32;
using Moq;
using StockTools.BusinessLogic.Abstract;
using StockTools.BusinessLogic.Concrete;
using StockTools.Domain.Abstract;
using StockTools.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StockTools.WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IIntradayDataDownloader _intradayDataDownloader = new BOSSAIntradayDataDownloader();
        IIntradayDataParser _intradayDataParser = new BOSSAIntradayDataParser();


        public double ChargeFunc(double price)
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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            //var outputStream = new MemoryStream();
            _intradayDataDownloader.Address = @"http://bossa.pl/index.jsp?layout=intraday&page=1&news_cat_id=875&dirpath=/mstock/daily/";
            _intradayDataDownloader.Parser = _intradayDataParser;
            _intradayDataDownloader.DownloadToFileParallel(dialog.SelectedPath);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //// Create OpenFileDialog 
            //Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //// Display OpenFileDialog by calling ShowDialog method 
            //Nullable<bool> result = dlg.ShowDialog();

            //// Get the selected file name and display in a TextBox 
            //if (result == true)
            //{
            //    // Open document 
            //    string filename = dlg.FileName;

            //}

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                //txtEditor.Text = File.ReadAllText();
                //var file = File.Open(openFileDialog.FileName, FileMode.Open);

                Mock<ICurrentPriceProvider> mock = new Mock<ICurrentPriceProvider>();
                mock.Setup(x => x.GetPriceByFullName(It.IsAny<string>())).Returns(1.0);
                //mock.Setup(x => x.GetPriceByFullNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);
                mock.Setup(x => x.GetPriceByShortName(It.IsAny<string>())).Returns(1.0);
                //mock.Setup(x => x.GetPriceByShortNameAndDateTime(It.IsAny<string>(), It.IsAny<DateTime>())).Returns(1.0);

                IPortfolio portfolio = new BasicPortfolio(mock.Object, ChargeFunc);
                IBookkeepingService bookkeepingService = new MbankBookkeepingService();
                var file = File.ReadAllBytes(openFileDialog.FileName);
                MemoryStream stream = new MemoryStream(file);
                var transactions = bookkeepingService.ReadTransactionHistory(stream);
                portfolio.Transactions = transactions;
                profit.Content = portfolio.GetRealisedGrossProfit().ToString();
            }

            //TODO Create profit time-plot
            //http://blog.bartdemeyer.be/2013/03/creating-graphs-in-wpf-using-oxyplot/
        }
    }
}
