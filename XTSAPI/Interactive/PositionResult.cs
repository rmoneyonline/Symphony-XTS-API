/*
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XTSAPI.Interactive
{
    [DataContract]
    public class PositionResult : ResultBase
    {
        /// <summary>
        /// Gets or sets the exchange
        /// </summary>
        [DataMember(Name = "ExchangeSegment")]
        public string ExchangeSegment { get; set; }

        /// <summary>
        /// Gets or sets the exchange instrument id
        /// </summary>
        [DataMember(Name = "ExchangeInstrumentID")]
        public long ExchangeInstrumentID { get; set; }

        /// <summary>
        /// Gets or sets the login id
        /// </summary>
        [DataMember(Name = "LoginID")]
        public string LoginID { get; set; }

        /// <summary>
        /// Gets or sets the client id
        /// </summary>
        [DataMember(Name = "ClientID")]
        public string ClientID { get; set; }

        /// <summary>
        /// Gets or sets the product type
        /// <see cref="XTSAPI.ProductType"/>
        /// </summary>
        [DataMember(Name = "ProductType")]
        public string ProductType { get; set; }

        /// <summary>
        /// Gets or sets the long position
        /// </summary>
        [DataMember(Name = "LongPosition")]
        public int LongPosition { get; set; }

        /// <summary>
        /// Gets or sets the short position
        /// </summary>
        [DataMember(Name = "ShortPosition")]
        public int ShortPosition { get; set; }

        /// <summary>
        /// Gets or sets the net position
        /// </summary>
        [DataMember(Name = "NetPosition")]
        public int NetPosition { get; set; }

        /// <summary>
        /// Gets or sets the buy average price
        /// </summary>
        [DataMember(Name = "BuyAveragePrice")]
        public string BuyAveragePrice { get; set; }

        /// <summary>
        /// Gets or sets the sell average price
        /// </summary>
        [DataMember(Name = "SellAveragePrice")]
        public string SellAveragePrice { get; set; }

        /// <summary>
        /// Gets or sets the buy value
        /// </summary>
        [DataMember(Name = "BuyValue")]
        public string BuyValue { get; set; }

        /// <summary>
        /// Gets or sets the sell value
        /// </summary>
        [DataMember(Name = "SellValue")]
        public string SellValue { get; set; }

        /// <summary>
        /// Gets or sets the net value
        /// </summary>
        [DataMember(Name = "NetValue")]
        public string NetValue { get; set; }

        /// <summary>
        /// Gets or sets the unrealized mark to market (MTM)
        /// </summary>
        [DataMember(Name = "UnrealizedMTM")]
        public string UnrealizedMTM { get; set; }

        /// <summary>
        /// Gets or sets the realized mark to market (MTM)
        /// </summary>
        [DataMember(Name = "RealizedMTM")]
        public string RealizedMTM { get; set; }

        /// <summary>
        /// Gets or sets the mark to market (MTM)
        /// </summary>
        [DataMember(Name = "MTM")]
        public string MTM { get; set; }

        /// <summary>
        /// Gets or sets the breakeven point
        /// </summary>
        [DataMember(Name = "BEP")]
        public string BEP { get; set; }

        /// <summary>
        /// Gets or sets the sum of traded quantity and buy price
        /// </summary>
        [DataMember(Name = "SumOfTradedQuantityAndPriceBuy")]
        public string SumOfTradedQuantityAndPriceBuy { get; set; }

        /// <summary>
        /// Gets or sets the sum of traded qantity and sell price
        /// </summary>
        [DataMember(Name = "SumOfTradedQuantityAndPriceSell")]
        public string SumOfTradedQuantityAndPriceSell { get; set; }

        /// <summary>
        /// Gets or sets the unique key
        /// </summary>
        [DataMember(Name = "UniqueKey")]
        public string UniqueKey { get; set; }

        

        
    }
}
