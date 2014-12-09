using StockTools.BusinessLogic.Abstract;
using StockTools.BusinessLogic.Concrete;
using System;
using System.Collections.Generic;
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
    }
}
