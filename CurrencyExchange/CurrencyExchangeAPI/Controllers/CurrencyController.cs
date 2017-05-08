using CurrencyExchangeDtos;
using CurrencyExchangeProvider.CurrencyExchangeProviderManager;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CurrencyExchangeAPI.Controllers
{
    public class CurrencyController : ApiController
    {

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        List<string> errorList = new List<string>();

        // POST api/currency/ConvertCurrency
        [HttpPost]
        public HttpResponseMessage ConvertCurrency(ExchangeDetailDto exchangeDetails)
        {
            try
            {
                Logger.Log(LogLevel.Info, "Api call started");
                if (ValidateApiTocken())
                {
                    var exchangeProvider = new ExchangeProviderManager().Providers[exchangeDetails.Source];
                    errorList = exchangeProvider.ValidateExhangeDetail(exchangeDetails);
                    if (errorList.Count > 0)
                    {
                        return Request.CreateResponse<CurrencyResponse>(HttpStatusCode.BadRequest, new CurrencyResponse { ErrorList = errorList });
                    }
                    exchangeDetails.Date = DateTime.Now;
                    // call provider to get the exchanged amount
                    var amount = exchangeProvider.CurrencyConverter(exchangeDetails);
                    exchangeDetails.AuthTocken = string.Empty;
                    exchangeDetails.ApiAuthTocken = string.Empty;
                    return Request.CreateResponse<CurrencyResponse>(HttpStatusCode.OK, new CurrencyResponse { Amount = amount, ExchangeDetail = exchangeDetails, ErrorList = errorList });

                }
                else
                {
                    errorList.Add("API validation failed.");
                    return Request.CreateResponse<CurrencyResponse>(HttpStatusCode.BadRequest, new CurrencyResponse { ErrorList = errorList });
                }
            }
            catch (Exception ex)
            {
                errorList.Add(ex.Message);
                Logger.Log(LogLevel.Error, ex.Message);
                return Request.CreateResponse<CurrencyResponse>(HttpStatusCode.BadRequest, new CurrencyResponse { ErrorList = errorList });
            }

        }

        // POST api/currency/ValidateRequest
        [HttpPost]
        public HttpResponseMessage ValidateRequest(UserDetail udetail)
        {
            // validate the uid and pwd; and return the authtocken
            string apiAuthTocken = "apiAuthTocken";
            return Request.CreateResponse(HttpStatusCode.OK, new { tocken = apiAuthTocken });            
        }

        private bool ValidateApiTocken()
        {
            bool result = false;
            var xTocken = Request.Headers.GetValues("X-ApiTocken");
            string authTocken = ((string[])(xTocken))[0];
            if (authTocken != null)
            {
                result = authTocken.Equals("apiAuthTocken");
            }
            return result;            
        }
    }
}
