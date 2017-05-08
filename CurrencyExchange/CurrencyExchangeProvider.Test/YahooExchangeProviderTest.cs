using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyExchangeProvider.CurrencyExchangeProviderManager;
using CurrencyExchangeDtos;

namespace CurrencyExchangeProvider.Test
{
    [TestClass]
    public class YahooExchangeProviderTest
    {
        ExchangeProviderManager exchangeManager;
        ExchangeDetailDto exchangeDto;
        public void SetUpInitData()
        {
            exchangeManager = new ExchangeProviderManager();
            exchangeDto = new ExchangeDetailDto();
            exchangeDto.FromCurrency = "GBP";
            exchangeDto.ToCurrency = "INR";
            exchangeDto.Amount = 10.5M;
            exchangeDto.ApiAuthTocken = "apiAuthTocken";
            exchangeDto.Source = "yahoo";
            exchangeDto.Date = DateTime.Now;
        }

        [TestMethod]
        public void ValidateExhangeDetail_yahoo_test_ValidData()
        {
            // arrange 
            SetUpInitData();

            // Act
            var yahooProvider = exchangeManager.Providers["yahoo"];
            var result = yahooProvider.ValidateExhangeDetail(exchangeDto);

            // Asert
            Assert.IsNotNull(result);
            Assert.AreEqual(0, result.Count);

        }

        [TestMethod]
        public void ValidateExhangeDetail_yahoo_test_InValidData()
        {
            // arrange 
            SetUpInitData();
            // change data to invalid value
            exchangeDto.FromCurrency = string.Empty;

            // Act
            var yahooProvider = exchangeManager.Providers["yahoo"];
            var result = yahooProvider.ValidateExhangeDetail(exchangeDto);

            // Asert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Count > 0);

        }


        [TestMethod]
        public void CurrencyConverter_yahoo_test_ValidData()
        {
            // arrange 
            SetUpInitData();

            // Act
            var yahooProvider = exchangeManager.Providers["yahoo"];
            var result = yahooProvider.CurrencyConverter(exchangeDto);

            // Asert
            Assert.IsNotNull(result);
            Assert.AreEqual(123.8M, result);

        }



    }
}
