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

namespace XTSAPI.MarketData
{
    [DataContract]
    public abstract class SearchBase
    {
        /// <summary>
        /// Gets or sets the exchange segment
        /// <see cref="XTSAPI.ExchangeSegment"/>
        /// </summary>
        [DataMember(Name = "ExchangeSegment")]
        public int ExchangeSegment { get; set; }

        /// <summary>
        /// Gets or sets the exchange instrument id
        /// </summary>
        [DataMember(Name = "ExchangeInstrumentID")]
        public long ExchangeInstrumentID { get; set; }

        /// <summary>
        /// Gets or sets the instrument type
        /// </summary>
        [DataMember(Name = "InstrumentType")]
        public int InstrumentType { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [DataMember(Name = "Name")]
        public string Name { get; set; }


        /// <summary>
        /// Gets or sets the display name
        /// </summary>
        [DataMember(Name = "DisplayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        [DataMember(Name = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the series
        /// </summary>
        [DataMember(Name = "Series")]
        public string Series { get; set; }

        /// <summary>
        /// Gets or sets the name with series
        /// </summary>
        [DataMember(Name = "NameWithSeries")]
        public string NameWithSeries { get; set; }

        /// <summary>
        /// Gets or sets the instrument id
        /// </summary>
        [DataMember(Name = "InstrumentID")]
        public long InstrumentID { get; set; }

        /// <summary>
        /// Gets or sets the price band
        /// </summary>
        [DataMember(Name = "PriceBand")]
        public PriceBand PriceBand { get; set; }

        /// <summary>
        /// Gets or sets the freeze quantity
        /// </summary>
        [DataMember(Name = "FreezeQty")]
        public double FreezeQty { get; set; }

        /// <summary>
        /// Gets or sets the ticksize
        /// </summary>
        [DataMember(Name = "TickSize")]
        public double TickSize { get; set; }

        /// <summary>
        /// Gets or sets the lot size
        /// </summary>
        [DataMember(Name = "LotSize")]
        public int LotSize { get; set; }
    }

    [DataContract]
    public class PriceBand
    {
        /// <summary>
        /// Gets or sets the higher circuit price
        /// </summary>
        [DataMember(Name = "High")]
        public double High { get; set; }

        /// <summary>
        /// Gets or sets the lower circuit price
        /// </summary>
        [DataMember(Name = "Low")]
        public double Low { get; set; }

        /// <summary>
        /// Gets or sets the high string
        /// </summary>
        [DataMember(Name = "HighString")]
        public string HighString { get; set; }

        /// <summary>
        /// Gets or sets the low string
        /// </summary>
        [DataMember(Name = "LowString")]
        public string LowString { get; set; }

        /// <summary>
        /// Gets or sets the credit rating
        /// </summary>
        [DataMember(Name = "CreditRating")]
        public string CreditRating { get; set; }
    }
}
