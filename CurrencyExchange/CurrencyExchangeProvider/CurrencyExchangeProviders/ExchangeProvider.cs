using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyExchangeDtos;


namespace CurrencyExchangeProvider.ExchangeProviders
{
    public abstract class ExchangeProvider : ProviderBase
    {
        public abstract decimal CurrencyConverter(ExchangeDetailDto excahngeDetails);
        public abstract List<string> ValidateExhangeDetail(ExchangeDetailDto excahngeDetails);
        public abstract string AuthenticateExchangeRequest(string uid, string pwd);
    }


}
