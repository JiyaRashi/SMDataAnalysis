using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMarketData.DataReader
{
    public static class DataReaderExtension
    {
        public static IEnumerable<QueryResultModel> ToQueryResult(this IEnumerable<string> source)
        {
            foreach (var line in source)
            {

                var columns = line.Split(',');

                yield return new QueryResultModel
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
                };
            }
        }
    }
    public class QueryResultModel
    { 
        public string ISIN { get; set; }
        public string TckrSymb { get; set; }
        //public string FinInstrmId { get; set; }
        //public string FinInstrmNm { get; set; }
        public string SctySrs { get; set; }

        public decimal OpnPric { get; set; }
        public decimal HghPric { get; set; }
        public decimal LwPric { get; set; }
        public decimal ClsPric { get; set; }
        public decimal LastPric { get; set; }
        public decimal PrvsClsgPric { get; set; }

        public int TtlTradgVol { get; set; }

        public decimal TtlTrfVal { get; set; }       

        public string TradDt { get; set; }

        public int TtlNbOfTxsExctd { get; set; }

        public decimal dummy { get; set; }

    }
}
