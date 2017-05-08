using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeProvider.CurrencyExchangeProviderConfiguration
{
    public class ExcahngeProviderconfiguration : ConfigurationSection
    {
        /// <summary>
        /// Providers
        /// </summary>
        [ConfigurationProperty("providers")]
        public ProviderSettingsCollection Providers
        {
            get { return (ProviderSettingsCollection)base["providers"]; }
        }

        /// <summary>
        /// Get/Set Default Provider
        /// </summary>
        [ConfigurationProperty("default", DefaultValue = "yahoo")]
        public string Default
        {
            get { return (string)base["default"]; }
            set { base["default"] = value; }
        }
    }
}
