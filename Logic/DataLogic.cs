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
            string path = $"DataReader/06092023.csv";
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




        //public void AddStocksFromcsv(List<StocksResultModel> qrmodel)
        //{
        //    using (SME_DATAEntities1 SMDataEnities = new SME_DATAEntities1())
        //    {
        //        foreach (var item in qrmodel)
        //        {
        //            NSEStock NseStocks = new NSEStock()
        //            {
        //                ISIN = item.ISIN,
        //                TckrSymb = item.TckrSymb,
        //                SctySrs = item.SctySrs,
        //                OpenPrice = item.OpnPric,
        //                HighPrice = item.HghPric,
        //                LowPrice = item.LwPric,
        //                ClosePrice = item.ClsPric,
        //                LastPrice = item.LastPric,
        //                PrviousClosePrice = item.PrvsClsgPric,
        //                TtlTradgVol = item.TtlTradgVol,
        //                TradDt = item.TradDt,
        //                TtlNbOfTxsExctd = item.TtlNbOfTxsExctd.ToString(),
        //            };

        //            List<NSEStock> _nSEStocks = SMDataEnities.NSEStocks.ToList();
        //            if (!_nSEStocks.Contains(NseStocks))
        //            {
        //                SMDataEnities.NSEStocks.Add(NseStocks);
        //                SMDataEnities.SaveChanges();
        //            }
        //        }

        //    }
        //}

        public void UpdateStocksName(List<StocksResultModel> qrmodel)
        {
            //ObservableCollection<StocksResultModel> stocksReport = new ObservableCollection<StocksResultModel>();
            using (SME_DATAEntities1 SMDataEnities = new SME_DATAEntities1())
            {
                foreach (var item in qrmodel)
                {
                    Stock_Names stockName = new Stock_Names()
                    {
                        ISIN = item.ISIN,
                        SName = item.TckrSymb
                    };

                    List<Stock_Names> _stockname = SMDataEnities.Stock_Names.ToList();
                    if (!_stockname.Contains(stockName))
                    {
                        SMDataEnities.Stock_Names.Add(stockName);
                        SMDataEnities.SaveChanges();
                    }
                }


            }

            // return stocksReport;
        }


        public void UpdateDailyStockPrice(List<StocksResultModel> qrmodel)
        {
            using (SME_DATAEntities1 SMDataEnities = new SME_DATAEntities1())
            {
                foreach (var item in qrmodel)
                {
                    //Master NSE Stocks name List in the database
                    List<Stock_Names> _stocknames = SMDataEnities.Stock_Names.ToList();
                    Stock_Names stocknames = _stocknames.Where(X => X.ISIN == item.ISIN).FirstOrDefault();
                    if (stocknames != null)
                    {
                        StockPrice dailyStockPrice = new StockPrice()
                        {
                            ISIN = item.ISIN,
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
                            Stock_NameId = stocknames.ID,
                        };
                        List<StockPrice> stockPrices = SMDataEnities.StockPrices.ToList();

                        StockPrice datesAdded = stockPrices.Where(x => x.TradDt == item.TradDt && x.ISIN==item.ISIN).FirstOrDefault();
                       // if (datesAdded)  Messagebox.show()
                        if (dailyStockPrice != null && datesAdded ==null)
                        {
                            SMDataEnities.StockPrices.Add(dailyStockPrice);
                            SMDataEnities.SaveChanges();
                        }
                    }

                }
            }


        }

        public void GetStockPrice()
        {
            using (SME_DATAEntities1 SMDataEnities = new SME_DATAEntities1())
            {
                List<Stock_Names> _stocknames = SMDataEnities.Stock_Names.ToList();
                List<StockPrice> stockPrices = SMDataEnities.StockPrices.ToList();

                var namePrice1 = (from snmae in _stocknames
                                 join sprice in stockPrices on snmae.ID equals sprice.Stock_NameId
                                 select new
                                 {
                                     snmae.SName,
                                     sprice.TradDt

                                 }).ToList();
                var namePrice2 = _stocknames.GroupJoin(stockPrices,
                      sname => sname.ID, sprice => sprice.Stock_NameId,
                      (sname, group) => new
                      {
                          sname.SName,
                          sname.StockPrices
                      });

            }
        }


    }
}
