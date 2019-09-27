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
            return $"/marketData/config/clientConfig";
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
        public static string SearchByString(string searchString, string userId = "guest", string source = "WEB")
        {
            return $"/marketdata/search/instruments/?searchString={searchString}&userID={userId}&source={source}";
        }

        /// <summary>
        /// Returns the instrument dump url
        /// </summary>
        /// <returns></returns>
        public static string InstrumentDump()
        {
            return "marketdata/instruments/master";
        }
    }
}
