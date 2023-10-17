namespace ShareMarketData.Model
{
    public class StocksResultModel
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
