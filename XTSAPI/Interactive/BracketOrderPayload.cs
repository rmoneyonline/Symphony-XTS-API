using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XTSAPI.Interactive
{
    [DataContract]
    public class BracketOrderPayload : BracketOrderPayloadBase
    {
        /// <summary>
        /// Gets or sets the client id
        /// </summary>
        [DataMember(Name = "clientID")]
        public string clientID { get; set; }

        /// <summary>
        /// Gets or sets the exchange segment
        /// </summary>
        [DataMember(Name = "exchangeSegment")]
        public string exchangeSegment { get; set; }

        /// <summary>
        /// Gets or sets the exchange instrument id
        /// </summary>
        [DataMember(Name = "exchangeInstrumentID")]
        public long exchangeInstrumentID { get; set; }

        /// <summary>
        /// Gets or sets the order type
        /// </summary>
        [DataMember(Name = "orderType")]
        public string orderType { get; set; }

        /// <summary>
        /// Gets or sets the order side
        /// </summary>
        [DataMember(Name = "orderSide")]
        public string orderSide { get; set; }

        [DataMember(Name = "stopPrice")]
        public double stopPrice { get; set; }

        /// <summary>
        /// Gets or sets the disclosed quantity
        /// </summary>
        [DataMember(Name = "disclosedQuantity")]
        public int disclosedQuantity { get; set; }


        /// <summary>
        /// Gets or sets the square off value
        /// </summary>
        [DataMember(Name = "squarOff")]
        public double squarOff { get; set; }

        /// <summary>
        /// Gets or sets the trailing stop value
        /// </summary>
        [DataMember(Name = "trailingStoploss")]
        public double trailingStoploss { get; set; }

        /// <summary>
        /// Gets or sets the unique order id
        /// </summary>
        [DataMember(Name = "orderUniqueIdentifier")]
        public string orderUniqueIdentifier { get; set; }
    }
}
