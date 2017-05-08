using CurrencyExchangeProvider.ExchangeProviders;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeProvider.CurrencyExchangeProviderCollections
{
    public class ExchangeProviderCollection : ProviderCollection
    {
        /// <summary>
        /// Return an instance of DataProvider for a specified provider name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        new public ExchangeProvider this[string name]
        {
            get
            { 
                return (ExchangeProvider)base[name]; 
            }
        }
    }
}
