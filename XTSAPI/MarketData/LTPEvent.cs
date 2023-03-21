using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using XTSAPI.MarketData;

namespace XTSAPI
{
    public class LTPEvent : ListQuotesBase
    {
        /// <summary>
        /// Gets or sets the marekt type
        /// </summary>
        [DataMember(Name = "XTSMarketType")]
        public int XTSMarketType { get; set; }

        /// <summary>
        /// Gets or sets the BookType
        /// </summary>
        [DataMember(Name = "BookType")]
        public int BookType { get; set; }

        /// <summary>
        /// Gets or sets the LastTradedPrice
        /// </summary>
        [DataMember(Name = "LastTradedPrice")]
        public double LastTradedPrice { get; set; }

        /// <summary>
        /// Gets or sets the LastTradedQunatity
        /// </summary>
        [DataMember(Name = "LastTradedQunatity")]
        public int LastTradedQunatity { get; set; }

        /// <summary>
        /// Gets or sets the LastUpdateTime
        /// </summary>
        [DataMember(Name = "LastUpdateTime")]
        public long LastUpdateTime { get; set; }
        /// <summary>
        /// Gets or sets the percentage change
        /// </summary>
        [DataMember(Name = "PercentChange")]
        public double PercentChange { get; set; }
        /// <summary>
        /// Gets or sets the Close
        /// </summary>
        [DataMember(Name = "Close")]
        public double Close { get; set; }




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
                case "lut":
                    if (long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out long lastUpdateTime))
                    {
                        this.LastUpdateTime = lastUpdateTime;
                    }
                    break;
                case "bt":
                    if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int bookType))
                    {
                        this.BookType = bookType;
                    }
                    break;
                case "mt":
                    if (int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int marketType))
                    {
                        this.XTSMarketType = marketType;
                    }
                    break;
                default:
                    base.Parse(field, value);
                    break;
            }
        }
    }
}
