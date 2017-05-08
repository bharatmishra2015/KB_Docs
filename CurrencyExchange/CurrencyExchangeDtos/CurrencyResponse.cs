using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeDtos
{
    public class CurrencyResponse
    {
        public decimal Amount { get; set; }
        public List<string> ErrorList { get; set; }
        public ExchangeDetailDto ExchangeDetail { get; set; }

        public CurrencyResponse()
        {
            ErrorList = new List<string>();
            ExchangeDetail = null;
        }


    }
}
