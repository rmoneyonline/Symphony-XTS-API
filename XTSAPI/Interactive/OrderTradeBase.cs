/*
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XTSAPI.Interactive
{
    [DataContract]
    public abstract class OrderTradeBase : ResultBase
    {
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
        /// Gets or sets the app order id
        /// </summary>
        [DataMember(Name = "AppOrderID")]
        public long AppOrderID { get; set; }

        /// <summary>
        /// Gets or sets the reference order id
        /// </summary>
        [DataMember(Name = "OrderReferenceID")]
        public string OrderReferenceID { get; set; }

        /// <summary>
        /// Gets or sets the order generate by
        /// </summary>
        [DataMember(Name = "GeneratedBy")]
        public string GeneratedBy { get; set; }

        /// <summary>
        /// Gets or sets the exchange order id
        /// </summary>
        [DataMember(Name = "ExchangeOrderID")]
        public string ExchangeOrderID { get; set; }

        /// <summary>
        /// Gets or sets the order category type
        /// </summary>
        [DataMember(Name = "OrderCategoryType")]
        public string OrderCategoryType { get; set; }

        /// <summary>
        /// Gets or sets the exhange segment
        /// </summary>
        [DataMember(Name = "ExchangeSegment")]
        public string ExchangeSegment { get; set; }

        /// <summary>
        /// Gets or sets the exchange instrument id
        /// </summary>
        [DataMember(Name = "ExchangeInstrumentID")]
        public long ExchangeInstrumentID { get; set; }

        /// <summary>
        /// Gets or sets the order side
        /// <see cref="XTSAPI.OrderSide"/>
        /// </summary>
        [DataMember(Name = "OrderSide")]
        public string OrderSide { get; set; }

        /// <summary>
        /// Gets or sets the order type
        /// <see cref="XTSAPI.OrderType"/>
        /// </summary>
        [DataMember(Name = "OrderType")]
        public string OrderType { get; set; }

        /// <summary>
        /// Gets or sets the product type
        /// <see cref="XTSAPI.ProductType"/>
        /// </summary>
        [DataMember(Name = "ProductType")]
        public string ProductType { get; set; }

        /// <summary>
        /// Gets or sets the time in force / order duration
        /// <see cref="XTSAPI.TimeInForce"/>
        /// </summary>
        [DataMember(Name = "TimeInForce")]
        public string TimeInForce { get; set; }

        /// <summary>
        /// Gets or sets the order price
        /// </summary>
        [DataMember(Name = "OrderPrice")]
        public double OrderPrice { get; set; }

        /// <summary>
        /// Gets or sets the order quantity
        /// </summary>
        [DataMember(Name = "OrderQuantity")]
        public int OrderQuantity { get; set; }

        /// <summary>
        /// Gets or sets the order stop price
        /// </summary>
        [DataMember(Name = "OrderStopPrice")]
        public double OrderStopPrice { get; set; }

        /// <summary>
        /// Gets or sets the order status
        /// </summary>
        [DataMember(Name = "OrderStatus")]
        public string OrderStatus { get; set; }

        /// <summary>
        /// Gets or sets the average traded price
        /// </summary>
        [DataMember(Name = "OrderAverageTradedPrice")]
        public string OrderAverageTradedPrice { get; set; }

        /// <summary>
        /// Gets or sets the remaining quantity
        /// </summary>
        [DataMember(Name = "LeavesQuantity")]
        public int LeavesQuantity { get; set; }

        /// <summary>
        /// Gets or sets the cumulative quantity
        /// </summary>
        [DataMember(Name = "CumulativeQuantity")]
        public int CumulativeQuantity { get; set; }

        /// <summary>
        /// Gets or sets the disclosed quantity
        /// </summary>
        [DataMember(Name = "OrderDisclosedQuantity")]
        public int OrderDisclosedQuantity { get; set; }

        /// <summary>
        /// Gets or sets the order time
        /// </summary>
        [DataMember(Name = "OrderGeneratedDateTime")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime OrderGeneratedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the exchange transaction time
        /// </summary>
        [DataMember(Name = "ExchangeTransactTime")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime ExchangeTransactTime { get; set; }

        /// <summary>
        /// Gets or sets the last update time
        /// </summary>
        [DataMember(Name = "LastUpdateDateTime")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime LastUpdateDateTime { get; set; }

        /// <summary>
        /// Gets or sets the cancel reject reason
        /// </summary>
        [DataMember(Name = "CancelRejectReason")]
        public string CancelRejectReason { get; set; }

        /// <summary>
        /// Gets or sets the unique order identified
        /// </summary>
        [DataMember(Name = "OrderUniqueIdentifier")]
        public string OrderUniqueIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the order leg status
        /// </summary>
        [DataMember(Name = "OrderLegStatus")]
        public string OrderLegStatus { get; set; }

        /// <summary>
        /// Gets or sets if is spread order or not
        /// </summary>
        [DataMember(Name = "IsSpread")]
        public bool IsSpread { get; set; }

    }
}
