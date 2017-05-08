using CurrencyExchangeProvider.CurrencyExchangeProviderCollections;
using CurrencyExchangeProvider.CurrencyExchangeProviderConfiguration;
using CurrencyExchangeProvider.ExchangeProviders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace CurrencyExchangeProvider.CurrencyExchangeProviderManager
{
    public class ExchangeProviderManager
    {
        private  ExchangeProvider _defaultProvider;
        private  ExchangeProviderCollection _providers;

        public ExchangeProviderManager()
        {
            Initialize();
        }

        private  void Initialize()
        {
            try
            {
                ExcahngeProviderconfiguration configuration = (ExcahngeProviderconfiguration)ConfigurationManager.GetSection("ExchangeProviders");
                if (configuration == null)
                {
                    throw new ConfigurationErrorsException("Provider configuration section is not set correctly.");
                }
                _providers = new ExchangeProviderCollection();
                ProvidersHelper.InstantiateProviders(configuration.Providers, _providers, typeof(ExchangeProvider));
                _providers.SetReadOnly();
                _defaultProvider = _providers[configuration.Default];

                if (_defaultProvider == null)
                {
                    throw new Exception("defaultProvider is not set.");
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Default Provider
        /// </summary>
        public  ExchangeProvider Provider
        {
            get { return _defaultProvider; }
        }

        /// <summary>
        /// Providers
        /// </summary>
        public  ExchangeProviderCollection Providers
        {
            get { return _providers; }
        }
    }
}
