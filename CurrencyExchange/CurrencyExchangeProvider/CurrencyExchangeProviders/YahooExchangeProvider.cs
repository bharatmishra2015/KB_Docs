using CurrencyExchangeDtos;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchangeProvider.ExchangeProviders
{
    public class YahooExchangeProvider: ExchangeProvider
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
            if(this._isAuthRequired)
            {
                this._callerId = config["callerId"];
                this._callerPassword = config["callerPassword"];
            }
        }

        public override decimal CurrencyConverter(ExchangeDetailDto exchangeDetail)
        {
            try
            {
               if( ValidateExhangeDetail(exchangeDetail).Count > 0)
               {
                   throw new Exception("Data is not valid.");
               }

                decimal result = 0;
                if(_isAuthRequired)
                {
                    // get authtocken by calling login api
                    var authTocken = AuthenticateExchangeRequest(this._callerId, this._callerPassword);
                    if(string.IsNullOrWhiteSpace(authTocken))
                    {
                        throw new Exception("Authentication failed.");
                    }
                    // update exchangeDetail with outhtocken from source
                    exchangeDetail.AuthTocken = authTocken;
                }

                // call currency converter API and pass the required data{ fromCurrency, toCurrency, Date, Amount, & authTocken } 
                result = 123.8M;

                return result;
            }
            catch
            {
                throw;
            }

        }

        public override List<string> ValidateExhangeDetail(ExchangeDetailDto exchangeDetail)
        {
            try
            {
                var result = new List<string>();
                if (exchangeDetail != null)
                {
                    if (string.IsNullOrWhiteSpace(exchangeDetail.FromCurrency))
                    {
                        result.Add(StringConstant.fromCurrencyRequired);
                    }
                    if (string.IsNullOrWhiteSpace(exchangeDetail.ToCurrency))
                    {
                        result.Add(StringConstant.toCurrencyRequired);
                    }
                    if (string.IsNullOrWhiteSpace(exchangeDetail.Amount.ToString()))
                    {
                        result.Add(StringConstant.amountRequired);
                    }
                    if (string.IsNullOrWhiteSpace(exchangeDetail.Date.ToString()))
                    {
                        result.Add(StringConstant.dateRequired);
                    }
                    if (string.IsNullOrWhiteSpace(exchangeDetail.Source))
                    {
                        result.Add(StringConstant.sourceRequired);
                    }
                    if (!_isActive)
                    {
                        result.Add(StringConstant.sourceIsInactive);
                    }
                }
                return result;
            }
            catch
            {
                throw;
            }
        }

        public override string AuthenticateExchangeRequest(string uid, string pwd)
        {
            try
            {
                string authTocken = string.Empty;

                if(uid.Equals("bharat") && pwd.Equals("123456"))
                {
                    //call authentication service: yahoo.com/currencyconvertor/login

                    authTocken = "yahooAuthTocken";                    
                }
                return authTocken;
            }
            catch
            {
                throw;
            }
        }
    }
}
