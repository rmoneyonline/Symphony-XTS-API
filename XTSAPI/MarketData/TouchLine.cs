/*
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XTSAPI.MarketData
{
    [DataContract]
    public class Touchline : OHLCBase
    {
        /// <summary>
        /// Gets or sets the bids
        /// <see cref="Tops"/>
        /// </summary>
        [DataMember(Name = "BidInfo")]
        public Tops BidInfo { get; set; }

        /// <summary>
        /// Gets or sets the asks
        /// <see cref="Tops"/>
        /// </summary>
        [DataMember(Name = "AskInfo")]
        public Tops AskInfo { get; set; }

        /// <summary>
        /// Gets or sets the last traded price
        /// </summary>
        [DataMember(Name = "LastTradedPrice")]
        public double LastTradedPrice { get; set; }

        /// <summary>
        /// Gets or sets the last traded quantity
        /// </summary>
        [DataMember(Name = "LastTradedQunatity")]
        public int LastTradedQunatity { get; set; }

        /// <summary>
        /// Gets or sets the total buy quantity
        /// </summary>
        [DataMember(Name = "TotalBuyQuantity")]
        public long TotalBuyQuantity { get; set; }

        /// <summary>
        /// Gets or sets the total sell quantity
        /// </summary>
        [DataMember(Name = "TotalSellQuantity")]
        public long TotalSellQuantity { get; set; }

        /// <summary>
        /// Gets or sets the total traded quantity
        /// </summary>
        [DataMember(Name = "TotalTradedQuantity")]
        public long TotalTradedQuantity { get; set; }

        /// <summary>
        /// Gets or sets the average traded price
        /// </summary>
        [DataMember(Name = "AverageTradedPrice")]
        public double AverageTradedPrice { get; set; }

        /// <summary>
        /// Gets or sets the last traded time
        /// <see cref="Globals.ToDateTime(long)"/>
        /// </summary>
        [DataMember(Name = "LastTradedTime")]
        public long LastTradedTime { get; set; }

        /// <summary>
        /// Gets or sets the last update time
        /// <see cref="Globals.ToDateTime(long)"/>
        /// </summary>
        [DataMember(Name = "LastUpdateTime")]
        public long LastUpdateTime { get; set; }

        /// <summary>
        /// Gets or sets the percentage change
        /// </summary>
        [DataMember(Name = "PercentChange")]
        public double PercentChange { get; set; }

        /// <summary>
        /// Gets or sets the total value traded
        /// </summary>
        [DataMember(Name = "TotalValueTraded")]
        public double? TotalValueTraded { get; set; }

        /// <summary>
        /// Gets or sets the buy back total buy
        /// </summary>
        [DataMember(Name = "BuyBackTotalBuy")]
        public double BuyBackTotalBuy { get; set; }

        /// <summary>
        /// Gets or sets the buy back total sell
        /// </summary>
        [DataMember(Name = "BuyBackTotalSell")]
        public double BuyBackTotalSell { get; set; }


        protected internal override void Parse(string field, string value)
        {
            switch (field)
            {
                case "ltp":
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double price))
                    {
                        this.LastTradedPrice = price;
                    }
                    break;
                case "ltq":
                    if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int lastTradedQty))
                    {
                        this.LastTradedQunatity = lastTradedQty;
                    }
                    break;
                case "tb":
                    if (long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out long totalBuy))
                    {
                        this.TotalBuyQuantity = totalBuy;
                    }
                    break;
                case "ts":
                    if (long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out long totalSell))
                    {
                        this.TotalSellQuantity = totalSell;
                    }
                    break;
                case "v":
                    if (long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out long totalVol))
                    {
                        this.TotalTradedQuantity = totalVol;
                    }
                    break;
                case "ltt":
                    if (long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out long lastTradedTime))
                    {
                        this.LastTradedTime = lastTradedTime;
                    }
                    break;
                case "lut":
                    if (long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out long lastUpdateTime))
                    {
                        this.LastUpdateTime = lastUpdateTime;
                    }
                    break;
                case "ap":
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double avgPrice))
                    {
                        this.AverageTradedPrice = avgPrice;
                    }
                    break;
                case "pc":
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double change))
                    {
                        this.PercentChange = change;
                    }
                    break;
                case "vp":
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double totalvalueTraded))
                    {
                        this.TotalValueTraded = totalvalueTraded;
                    }
                    break;
                    /*
                case "ai":
                    this.ai = ParseTop(value);
                    break;
                case "bi":
                    this.bi = ParseTop(value);
                    break;
                    */
                default:
                    base.Parse(field, value);
                    break;
            }
        }

    }
}
