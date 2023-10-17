using ShareMarketData.Entityframework;
using ShareMarketData.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMarketData.Logic
{
   public class DataLogic
    {
        public ObservableCollection<StocksResultModel> GetAllStockResult()
        {
            string path = $"DataReader/09092023.csv";
            StreamReader sr = new StreamReader(path);
            ObservableCollection<StocksResultModel> importingData = new ObservableCollection<StocksResultModel>();

            var source = File.ReadAllLines(path)
                  .Skip(1)
                  .Where(l => l.Length > 1);
            foreach (var line in source)
            {

                var columns = line.Split(',');

                importingData.Add(new StocksResultModel
                {
                    ISIN = columns[0],
                    TckrSymb = columns[1],
                    SctySrs = columns[2],
                    OpnPric = Convert.ToDecimal(columns[3]),
                    HghPric = Convert.ToDecimal(columns[4]),
                    LwPric = Convert.ToDecimal(columns[5]),
                    ClsPric = Convert.ToDecimal(columns[6]),
                    LastPric = Convert.ToDecimal(columns[7]),
                    PrvsClsgPric = Convert.ToDecimal(columns[8]),
                    TtlTradgVol = Convert.ToInt32(columns[9]),
                    TtlTrfVal = Convert.ToDecimal(columns[10]),
                    TradDt = columns[11],
                    TtlNbOfTxsExctd = Convert.ToInt32(columns[12]),
                });
            }

            return importingData;
        }

        public ObservableCollection<string> GetCompareItem()
        {
            ObservableCollection<string> items = new ObservableCollection<string>();
            items.Add("Open Price");
            items.Add("High Price");
            items.Add("Low Price");
            items.Add("Close Price");
            items.Add("Last Price");
            items.Add("Pre close Price");
            return items;

        }




        public void AddStocksFromcsv(List<StocksResultModel> qrmodel)
        {
            using (SME_DATAEntities SMDataEnities = new SME_DATAEntities())
            {
                foreach (var item in qrmodel)
                {
                    NSEStock NseStocks = new NSEStock()
                    {
                        ISIN = item.ISIN,
                        TckrSymb = item.TckrSymb,
                        SctySrs = item.SctySrs,
                        OpenPrice = item.OpnPric,
                        HighPrice = item.HghPric,
                        LowPrice = item.LwPric,
                        ClosePrice = item.ClsPric,
                        LastPrice = item.LastPric,
                        PrviousClosePrice = item.PrvsClsgPric,
                        TtlTradgVol = item.TtlTradgVol,
                        TradDt = item.TradDt,
                        TtlNbOfTxsExctd = item.TtlNbOfTxsExctd.ToString(),
                    };

                    List<NSEStock> _nSEStocks = SMDataEnities.NSEStocks.ToList();
                    if (!_nSEStocks.Contains(NseStocks))
                    {
                        SMDataEnities.NSEStocks.Add(NseStocks);
                        SMDataEnities.SaveChanges();
                    }
                }

            }
        }


    }
}
