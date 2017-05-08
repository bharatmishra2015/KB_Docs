using CurrencyExchangeDtos;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeProvider.ExchangeProviders
{
    public class OandaExchangeProvider: ExchangeProvider
    {
        private string _connectionString = string.Empty;
        private string _connectionstringName = string.Empty;
        private bool _isActive = true;
        private bool _isAuthRequired = false;
        private string _callerId;
        private string _callerPassword;

        public override void Initialize(string name, NameValueCollection config)
        {
            base.Initialize(name, config);
            this._isActive = Convert.ToBoolean(config["isActive"]);
            this._isAuthRequired = Convert.ToBoolean(config["isAuthRequired"]);
            if (this._isAuthRequired)
            {
                this._callerId = config["callerId"];
                this._callerPassword = config["callerPassword"];
            }
        }

        public override decimal CurrencyConverter(ExchangeDetailDto exchangeDetail)
        {
            try
            {
                decimal result = 0;
                if (_isAuthRequired)
                {
                    // get authtocken by calling login api
                    var authTocken = AuthenticateExchangeRequest(this._callerId, this._callerPassword);
                    if (string.IsNullOrWhiteSpace(authTocken))
                    {
                        throw new Exception("Authentication failed.");
                    }

                    // call currency converter API and pass the required data{ fromCurrency, toCurrency, Date, Amount, & authTocken if required}                    
                    result = 123.8M;

                }


                return result;
            }
            catch
            {
                throw;
            }

        }

        public override List<string> ValidateExhangeDetail(ExchangeDetailDto excahngeDetails)
        {
            var result = new List<string>();
            if (excahngeDetails != null)
            {
                if (string.IsNullOrWhiteSpace(excahngeDetails.FromCurrency))
                {
                    result.Add(StringConstant.fromCurrencyRequired);
                }
                if (string.IsNullOrWhiteSpace(excahngeDetails.ToCurrency))
                {
                    result.Add(StringConstant.toCurrencyRequired);
                }
                if (string.IsNullOrWhiteSpace(excahngeDetails.Amount.ToString()))
                {
                    result.Add(StringConstant.amountRequired);
                }
                if (string.IsNullOrWhiteSpace(excahngeDetails.Date.ToString()))
                {
                    result.Add(StringConstant.dateRequired);
                }
            }
            return result;

        }

        public override string AuthenticateExchangeRequest(string uid, string pwd)
        {
            try
            {
                string authTocken = string.Empty;
                //call authentication service: oanda.com/currencyconvertor/login

                authTocken = "oandaAuthTocken";
                return authTocken;
            }
            catch
            {
                throw;
            }
        }
    }
}
