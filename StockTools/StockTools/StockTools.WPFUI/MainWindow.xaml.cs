using Microsoft.Win32;
using Moq;
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
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using StockTools.BusinessLogic.Concrete;
using StockTools.Data.HistoricalData;

namespace StockTools.WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region DI

        IIntradayDataDownloader IntradayDataDownloader { get; set; }
        IIntradayDataParser IntradayDataParser { get; set; }
        IPortfolio Portfolio { get; set; }
        IBookkeepingService BookkeepingService { get; set; }
        string IntradayDataPath { get; set; }

        public MainWindow(IIntradayDataDownloader intradayDataDownloader, IIntradayDataParser intradayDataParser, IPortfolio portfolio, IBookkeepingService bookkeepingService)
        {
            IntradayDataDownloader = intradayDataDownloader;
            IntradayDataParser = intradayDataParser;
            Portfolio = portfolio;
            BookkeepingService = bookkeepingService;

            InitializeComponent();
        } 

        #endregion       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            IntradayDataDownloader.Address = @"http://bossa.pl/index.jsp?layout=intraday&page=1&news_cat_id=875&dirpath=/mstock/daily/";
            IntradayDataDownloader.DownloadToFileParallel(dialog.SelectedPath);
        }

        private PlotModel PrepareModel(Dictionary<DateTime, double> realisedGrossProfitData, Dictionary<DateTime, double> grossProfitData)
        {
            var Plot = new PlotModel { Title = "Profit" };
            //this.MyModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));

            #region Axis

            var axisX = new DateTimeAxis()
                {
                    Position = AxisPosition.Bottom,
                    Title = "Date"

                };

            Plot.Axes.Add(axisX);

            #endregion

            #region Series

            var realisedGrossProfit = new LineSeries { Title = "Realised gross profit", MarkerType = MarkerType.Triangle };

            foreach (var point in realisedGrossProfitData)
            {
                realisedGrossProfit.Points.Add(new DataPoint(DateTimeAxis.ToDouble(point.Key), point.Value));
            }

            Plot.Series.Add(realisedGrossProfit);

            var grossProfit = new LineSeries { Title = "Gross profit", MarkerType = MarkerType.Triangle };

            foreach (var point in realisedGrossProfitData)
            {
                grossProfit.Points.Add(new DataPoint(DateTimeAxis.ToDouble(point.Key), point.Value));
            }

            Plot.Series.Add(grossProfit);

            #endregion

            return Plot;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var file = File.ReadAllBytes(openFileDialog.FileName);
                MemoryStream stream = new MemoryStream(file);
                var transactions = BookkeepingService.ReadTransactionHistory(stream);
                foreach (var transaction in transactions)
                {
                    Portfolio.AddTransaction(transaction);
                }
                profit.Content = Portfolio.GetRealisedGrossProfit().ToString();

                var realisedGrossProfitByTime = Portfolio.GetRealisedGrossProfitTable();
                var grossProfitByTime = Portfolio.GetGrossProfitTable();

                this.TransactionProfitPlot.Model = PrepareModel(realisedGrossProfitByTime, grossProfitByTime);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();
            IntradayDataPath = dialog.SelectedPath;
        }
    }
}
