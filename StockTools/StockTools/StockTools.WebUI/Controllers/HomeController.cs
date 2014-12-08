using Ionic.Zip;
using StockTools.BusinessLogic.Abstract;
using StockTools.BusinessLogic.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockTools.WebUI.Controllers
{
    public partial class HomeController : Controller
    {
        //TODO IoC
        IIntradayDataDownloader _intradayDataDownloader = new BOSSAIntradayDataDownloader();
        IIntradayDataParser _intradayDataParser = new BOSSAIntradayDataParser();

        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public virtual ActionResult DownloadBOSSAIntraday()
        {
            var outputStream = new MemoryStream();
            _intradayDataDownloader.Address = "http://bossa.pl/index.jsp?layout=intraday&page=1&news_cat_id=875&dirpath=/mstock/daily/";
            _intradayDataDownloader.Parser = _intradayDataParser;
            var files = _intradayDataDownloader.DownloadToMemory();

            using (var zip = new ZipFile())
            {
                foreach (var key in files.Keys)
                {
                    zip.AddEntry(key, files[key]);
                }
                zip.Save(outputStream);
            }

            outputStream.Position = 0;
            return File(outputStream, "application/zip", "BOSSAIntraday.zip");

        }
    }
}
