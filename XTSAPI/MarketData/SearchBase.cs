/*
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using CsvHelper.Configuration.Attributes;
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
        [Index(0)]
        public int ExchangeSegment { get; set; }

        /// <summary>
        /// Gets or sets the exchange name
        /// <see cref="XTSAPI.ExchangeName"/>
        /// </summary>
        [DataMember(Name = "ExchangeName")]
        [Index(20)]
        public string ExchangeName { get; set; }
        /// <summary>
        /// Gets or sets the exchange instrument id
        /// </summary>
        [DataMember(Name = "ExchangeInstrumentID")]
        [Index(1)]
        public long ExchangeInstrumentID { get; set; }

        /// <summary>
        /// Gets or sets the instrument type
        /// </summary>
        [DataMember(Name = "InstrumentType")]
        [Index(2)]
        public int InstrumentType { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [DataMember(Name = "Name")]
        [Index(3)]
        public string Name { get; set; }


        /// <summary>
        /// Gets or sets the display name
        /// </summary>
        [DataMember(Name = "DisplayName")]
        [Index(19)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        [DataMember(Name = "Description")]
        [Index(4)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the series
        /// </summary>
        [DataMember(Name = "Series")]
        [Index(5)]
        public string Series { get; set; }

        /// <summary>
        /// Gets or sets the name with series
        /// </summary>
        [DataMember(Name = "NameWithSeries")]
        [Index(6)]
        public string NameWithSeries { get; set; }

        /// <summary>
        /// Gets or sets the instrument id
        /// </summary>
        [DataMember(Name = "InstrumentID")]
        [Index(7)]
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
        [Index(10)]
        public double FreezeQty { get; set; }

        /// <summary>
        /// Gets or sets the ticksize
        /// </summary>
        [DataMember(Name = "TickSize")]
        [Index(11)]
        public double TickSize { get; set; }

        /// <summary>
        /// Gets or sets the lot size
        /// </summary>
        [DataMember(Name = "LotSize")]
        [Index(12)]
        public int LotSize { get; set; }

        /// <summary>
        /// Gets or sets the lot size
        /// </summary>
        [DataMember(Name = "MultiPlier")]
        [Index(13)]
        public double MultiPlier { get; set; }
    }

    [DataContract]
    public class PriceBand
    {
        /// <summary>
        /// Gets or sets the higher circuit price
        /// </summary>
        [DataMember(Name = "High")]
        [Index(8)]
        public double High { get; set; }

        /// <summary>
        /// Gets or sets the lower circuit price
        /// </summary>
        [DataMember(Name = "Low")]
        [Index(9)]
        public double Low { get; set; }

        ///// <summary>
        ///// Gets or sets the high string
        ///// </summary>
        //[DataMember(Name = "HighString")]
        //public string HighString { get; set; }

        ///// <summary>
        ///// Gets or sets the low string
        ///// </summary>
        //[DataMember(Name = "LowString")]
        //public string LowString { get; set; }

        ///// <summary>
        ///// Gets or sets the credit rating
        ///// </summary>
        //[DataMember(Name = "CreditRating")]
        //public string CreditRating { get; set; }
    }
}
