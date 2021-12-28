using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XTSAPI.Interactive
{
    [DataContract]
    public abstract class BracketOrderPayloadBase : Payload
    {
        /// <summary>
        /// Gets or sets the order quantity
        /// </summary>
        [DataMember(Name = "orderQuantity")]
        public int orderQuantity { get; set; }

        /// <summary>
        /// Gets or sets the limit price
        /// </summary>
        [DataMember(Name = "limitPrice")]
        public double limitPrice { get; set; }


        /// <summary>
        /// Gets or sets the stop price
        /// </summary>
        [DataMember(Name = "stopLossPrice")]
        public double stopLossPrice { get; set; }
    }
}
