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
    public class OI : ListQuotesBase
    {
        /// <summary>
        /// Gets or sets the marekt type
        /// </summary>
        [DataMember(Name = "XTSMarketType")]
        public int XTSMarketType { get; set; }

        /// <summary>
        /// Gets or sets the underlying instrument id
        /// </summary>
        [DataMember(Name = "UnderlyingInstrumentID")]
        public long UnderlyingInstrumentID { get; set; }

        /// <summary>
        /// Gets or sets the underlying exchange segment
        /// </summary>
        [DataMember(Name = "UnderlyingExchangeSegment")]
        public int UnderlyingExchangeSegment { get; set; }

        /// <summary>
        /// Gets or sets the underlying index name
        /// </summary>
        [DataMember(Name = "UnderlyingIDIndexName")]
        public string UnderlyingIDIndexName { get; set; }

        /// <summary>
        /// Gets or sets the underlying total open interest
        /// </summary>
        [DataMember(Name = "UnderlyingTotalOpenInterest")]
        public long UnderlyingTotalOpenInterest { get; set; }

        /// <summary>
        /// Gets or sets the open interest
        /// </summary>
        [DataMember(Name = "OpenInterest")]
        public long OpenInterest { get; set; }

        protected internal override void Parse(string field, string value)
        {
            switch (field)
            {
                case "o":
                    if (long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out long oi))
                    {
                        this.OpenInterest = oi;
                    }
                    break;
                default:
                    base.Parse(field, value);
                    break;
            }
        }
    }
}
