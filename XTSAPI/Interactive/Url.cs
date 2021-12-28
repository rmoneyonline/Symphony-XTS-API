///*
//    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
//    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
//    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
//    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//*/

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace XTSAPI.Interactive
//{
//    public static class Url
//    {
//        /// <summary>
//        /// Returns the sessio url
//        /// </summary>
//        /// <returns></returns>
//        public static string Session()
//        {
//            return $"/interactive/user/session";
//        }

//        /// <summary>
//        /// Returns the profile url
//        /// </summary>
//        /// <returns></returns>
//        public static string Profile()
//        {
//            return $"interactive/user/profile";
//        }

//        /// <summary>
//        /// Returns the balance url
//        /// </summary>
//        /// <returns></returns>
//        public static string Balance()
//        {
//            return $"interactive/user/balance";
//        }

//        /// <summary>
//        /// Returns the order url
//        /// </summary>
//        /// <returns></returns>
//        public static string Order()
//        {
//            return $"interactive/orders";
//        }

//        /// <summary>
//        /// Returns the order url to cancel the order
//        /// </summary>
//        /// <param name="appOrderId">App order id of the order</param>
//        /// <returns></returns>
//        public static string Order(long appOrderId)
//        {
//            return $"interactive/orders?appOrderID={appOrderId}";
//        }

//        /// <summary>
//        /// Gets or sets the order url
//        /// </summary>
//        /// <param name="appOrderId">App order id</param>
//        /// <param name="orderUniqueIdentifier">Unique identifier of the order</param>
//        /// <returns></returns>
//        public static string Order(long appOrderId, string orderUniqueIdentifier)
//        {
//            return $"interactive/orders?appOrderID={appOrderId}&orderUniqueIdentifier={orderUniqueIdentifier}";
//        }

//        /// <summary>
//        /// Returns the cover order url
//        /// </summary>
//        /// <returns></returns>
//        public static string CoverOrder()
//        {
//            return $"interactive/orders/cover";
//        }

//        /// <summary>
//        /// Returns the trade url
//        /// </summary>
//        /// <returns></returns>
//        public static string Trade()
//        {
//            return $"interactive/orders/trades";
//        }

//        /// <summary>
//        /// Returns the holding url
//        /// </summary>
//        /// <returns></returns>
//        public static string Holdings()
//        {
//            return $"/interactive/portfolio/holdings";
//        }

//        /// <summary>
//        /// Gets the position url
//        /// </summary>
//        /// <param name="dayOrNet">Day or net</param>
//        /// <returns></returns>
//        public static string Positions(string dayOrNet = "NetWise")
//        {
//            return $"interactive/portfolio/positions?dayOrNet={dayOrNet}";
//        }

//        /// <summary>
//        /// Returns the convert position url
//        /// </summary>
//        /// <returns></returns>
//        public static string PositionConvert()
//        {
//            return $"/interactive/portfolio/positions/convert";
//        }

//        /// <summary>
//        /// Returns the square off url
//        /// </summary>
//        /// <returns></returns>
//        public static string SquareOff()
//        {
//            return $"/interactive/portfolio/squareoff";
//        }

//        public static string MarketStatus(string userId)
//        {
//            return $"interactive/status/exchange?userID={userId}";
//        }

//        public static string Message(string exchange)
//        {
//            return $"interactive/messages/exchange?exchangeSegment={exchange}";
//        }
//    }
//}
