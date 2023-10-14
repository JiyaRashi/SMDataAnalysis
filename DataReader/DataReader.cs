using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShareMarketData.DataReader
{
    public class DataReader
    {
       
        public IList<QueryResultModel> GetAllQueryResult()
        {

            string path = $"DataReader/05092023.csv";
            var query =

                File.ReadAllLines(path)
                    .Skip(1)
                    .Where(l => l.Length > 1)
                    .ToQueryResult();

            return query.ToList();
        }
    }

    
}
