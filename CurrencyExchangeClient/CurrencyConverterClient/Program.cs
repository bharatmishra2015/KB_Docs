using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverterClient
{
    class Program
    {
        static void Main(string[] args)
        {
            CurrencyClient client = new CurrencyClient();
            client.ConvertCurrency();

        }
    }

    public class CurrencyClient
    {
        private static void SetWebRequest(HttpWebRequest httpWReq, string postData, string apiTocken)
        {
            httpWReq.KeepAlive = false;
            Encoding encoding = new UTF8Encoding();
            byte[] data = encoding.GetBytes(postData);
            httpWReq.ContentType = "application/json";
            httpWReq.ContentLength = data.Length;
            if (!string.IsNullOrWhiteSpace(apiTocken.Trim()))
            {
                httpWReq.Headers.Add("X-ApiTocken", apiTocken);
            }
            httpWReq.Method = "POST";
            using (Stream stream = httpWReq.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Close();
            }
        }

      

        public void ConvertCurrency()
        {
            // for simplicity we are fixing the value with mock data instead of geting it dynamicaly.
            var exchangeDetails = new
            {
                FromCurrency = "GBP",
                ToCurrency = "INR",
                Amount = 10.5,
                ApiAuthTocken = "apiAuthTocken",
                Source = "yahoo",
                Date = DateTime.Now
            };

            var userLogin = new
            {
                UserId = "bharat",
                Password = "123456"
            };

            try
            {
                // first get authTocken from API; so that to be able to use currency converter
                string servicepathValidate = @"http://localhost:57236/api/currency/ValidateRequest";
                string JsonData = JsonConvert.SerializeObject(userLogin);
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(servicepathValidate);
                SetWebRequest(httpWReq, JsonData, string.Empty);
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                var result = new StreamReader(response.GetResponseStream()).ReadToEnd();
                var autTocken = JsonConvert.DeserializeObject<AuthTocken>(result).tocken;


                // provide thsi authTocken to the service call for currency conversion

                string servicepathCurrencyConverter = @"http://localhost:57236/api/currency/ConvertCurrency";
                string JsonDataExchangeDetail = JsonConvert.SerializeObject(exchangeDetails);
                HttpWebRequest httpWReqCC = (HttpWebRequest)WebRequest.Create(servicepathCurrencyConverter);
                SetWebRequest(httpWReqCC, JsonDataExchangeDetail, autTocken);
                HttpWebResponse responseCurrencyConverter = (HttpWebResponse)httpWReqCC.GetResponse();
                var resultCurrencyConverter = new StreamReader(responseCurrencyConverter.GetResponseStream()).ReadToEnd();
                var currencyConverterResponse = JsonConvert.DeserializeObject<CurrencyResponse>(resultCurrencyConverter);

                if (currencyConverterResponse != null)
                {
                    if (currencyConverterResponse.ErrorList != null & currencyConverterResponse.ErrorList.Count > 0)
                    {
                        Console.WriteLine("Errors found:");
                        currencyConverterResponse.ErrorList.ForEach(item => Console.WriteLine(item));
                    }
                    else
                    {
                        Console.WriteLine("{0} to {1} for {2}: {3}", currencyConverterResponse.ExchangeDetail.FromCurrency, currencyConverterResponse.ExchangeDetail.ToCurrency
                            , currencyConverterResponse.ExchangeDetail.Amount, currencyConverterResponse.Amount);
                    }

                }

                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            
            }

        }
    }

    public class AuthTocken
    {
        public string tocken { get; set; }
    }


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
