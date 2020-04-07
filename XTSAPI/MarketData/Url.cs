/*
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTSAPI.MarketData
{
    public static class Url
    {
        /// <summary>
        /// Returns the market data login url
        /// </summary>
        /// <returns></returns>
        public static string Login()
        {
            return $"/marketdata/auth/login";
        }

        /// <summary>
        /// Returns the market data logout url
        /// </summary>
        /// <returns></returns>
        public static string Logout()
        {
            return $"/marketdata/auth/logout";
        }

        /// <summary>
        /// Returns the client config url
        /// </summary>
        /// <returns></returns>
        public static string ClientConfig()
        {
            return $"/marketdata/config/clientConfig";
        }

        /// <summary>
        /// Returns the quotes url
        /// </summary>
        /// <returns></returns>
        public static string Quotes()
        {
            return $"/marketdata/instruments/quotes";
        }

        /// <summary>
        /// Returns the subscription url
        /// </summary>
        /// <returns></returns>
        public static string Subscription()
        {
            return $"/marketdata/instruments/subscription";
        }

        /// <summary>
        /// Returns the search instrument by id url
        /// </summary>
        /// <returns></returns>
        public static string SearchInstrumentsById()
        {
            return $"/marketdata/search/instrumentsbyid";
        }

        /// <summary>
        /// Returns the search by string url
        /// </summary>
        /// <param name="searchString">Search string</param>
        /// <param name="userId">userid</param>
        /// <param name="source">source</param>
        /// <returns></returns>
        public static string SearchByString(string searchString,string source = "WEB")
        {
            return $"/marketdata/search/instruments/?searchString={searchString}&source={source}";
        }

        /// <summary>
        /// Returns the instrument dump url
        /// </summary>
        /// <returns></returns>
        public static string Master()
        {
            return "marketdata/instruments/master";
        }


        public static string OHLCHistory(ExchangeSegment exchangeSegment, long exchangeInstrumentId, DateTime startTime, DateTime endTime, int compressionType)
        {
            long from = (long)startTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            long to = (long)endTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            return $"instruments/ohlc?exchangeSegment={exchangeSegment.ToString()}&exchangeInstrumentID={exchangeInstrumentId}&startTime={from}&endTime={to}&compressionValue=1";
        }

        public static string IndexList(ExchangeSegment exchangeSegment)
        {
            return $"instruments/indexlist/optionType?exchangeSegment={(int)exchangeSegment}";
        }

        public static string EquitySymbol(ExchangeSegment exchangeSegment, string symbol)
        {
            return $"instruments/instrument/symbol?exchangeSegment={(int)exchangeSegment}&series=EQ&symbol={symbol}";
        }

        public static string Series(ExchangeSegment exchangeSegment)
        {
            return $"instruments/instrument/series?exchangeSegment={(int)exchangeSegment}";
        }

        public static string ExpiryDate(ExchangeSegment exchangeSegment, string series, string symbol)
        {
            return $"instruments/instrument/expiryDate?exchangeSegment={(int)exchangeSegment}&series={series}&symbol={symbol}";
        }

        public static string FuturesSymbol(ExchangeSegment exchangeSegment, string series, string symbol, DateTime expiryDate)
        {

            return string.Format(System.Globalization.CultureInfo.InvariantCulture,"instruments/instrument/futureSymbol?exchangeSegment={0}&series={1}&symbol={2}&expiryDate={3:ddMMMyyyy}",
                (int)exchangeSegment, series, symbol, expiryDate);
        }

        public static string OptionSymbol(ExchangeSegment exchangeSegment, string series, string symbol, DateTime expiryDate, string optionType, double strikePrice)
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "instruments/instrument/optionSymbol?exchangeSegment={0}&series={1}&symbol={2}&expiryDate={3:ddMMMyyyy}&optionType={4}&strikePrice={5}",
                (int)exchangeSegment, series, symbol, expiryDate, optionType, strikePrice);
        }

        public static string OptionType(ExchangeSegment exchangeSegment, string series, string symbol, DateTime expiryDate)
        {
            return string.Format(System.Globalization.CultureInfo.InvariantCulture, "instruments/instrument/optionType?exchangeSegment={0}&series={1}&symbol={2}&expiryDate={3:ddMMMyyyy}",
                (int)exchangeSegment, series, symbol, expiryDate);
        }

    }
}
