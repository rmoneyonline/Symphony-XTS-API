/*
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using Newtonsoft.Json;
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
    public class MarketDepth : ListQuotesBase
    {

        public MarketDepth()
        {
            this.Touchline = new Touchline();
        }

        /// <summary>
        /// Gets or sets the Bids
        /// <see cref="Tops"/>
        /// </summary>
        [DataMember(Name = "Bids")]
        public List<Tops> Bids { get; set; }

        /// <summary>
        /// Gets or sets the Asks
        /// <see cref="Tops"/>
        /// </summary>
        [DataMember(Name = "Asks")]
        public List<Tops> Asks { get; set; }

        /// <summary>
        /// Gets or sets the touchline
        /// <see cref="XTSAPI.MarketData.Touchline"/>
        /// </summary>
        [DataMember(Name = "Touchline")]
        public Touchline Touchline { get; set; }

        /// <summary>
        /// Gets or sets the booktype
        /// </summary>
        [DataMember(Name = "BookType")]
        public int BookType { get; set; }

        /// <summary>
        /// Gets or sets the market type
        /// </summary>
        [DataMember(Name = "XMarketType")]
        public int XMarketType { get; set; }

        protected internal override void Parse(string field, string value)
        {
            switch (field)
            {
                case "ai":
                    this.Asks = ParseTop(value);
                    break;
                case "bi":
                    this.Bids = ParseTop(value);
                    break;
                case "t":
                    base.Parse(field, value);
                    break;
                default:
                    Touchline?.Parse(field, value);
                    break;
            }
        }


        private List<Tops> ParseTop(string value)
        {
            string[] array = value?.Split('|');

            if (array == null || array.Length < 4)
                return null;

            int len = array.Length;
            List<Tops> tmp = new List<Tops>();
            for (int i = 0; i < len; i += 4)
            {
                if (i + 4 > len)
                    break;

                int.TryParse(array[i], NumberStyles.Integer, CultureInfo.InvariantCulture, out int position);
                int.TryParse(array[i + 1], NumberStyles.Integer, CultureInfo.InvariantCulture, out int size);
                double.TryParse(array[i + 2], NumberStyles.Any, CultureInfo.InvariantCulture, out double price);
                int.TryParse(array[i + 3], NumberStyles.Integer, CultureInfo.InvariantCulture, out int totalOrders);

                tmp.Add(new Tops()
                {
                    Position = position,
                    Size = size,
                    Price = price,
                    TotalOrders = totalOrders
                });

            }

            return tmp;
        }
    }


    [DataContract]
    public class Tops
    {
        /// <summary>
        /// Gets or sets the quantity
        /// </summary>
        [DataMember(Name = "Size")]
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets the price
        /// </summary>
        [DataMember(Name = "Price")]
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the total orders
        /// </summary>
        [DataMember(Name = "TotalOrders")]
        public int TotalOrders { get; set; }

        /// <summary>
        /// Gets or sets the buy back market maker
        /// </summary>
        [DataMember(Name = "BuyBackMarketMaker")]
        public int BuyBackMarketMaker { get; set; }


        /// <summary>
        /// Gets or sets the market depth position. The partial stream only broadcasteds the position
        /// </summary>
        [JsonIgnore]
        [IgnoreDataMember]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int Position { get; set; } = 0;

        
    }


    
}
