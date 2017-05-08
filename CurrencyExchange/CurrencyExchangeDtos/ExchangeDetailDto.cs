using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeDtos
{
    public class ExchangeDetailDto
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Amount { get; set; }
        public string Source { get; set; }
        public DateTime Date { get; set; }
        public string AuthTocken { get; set; }
        public string ApiAuthTocken { get; set; }
    }
}
