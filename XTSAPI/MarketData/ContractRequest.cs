using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XTSAPI.MarketData
{
    [DataContract]
    public class ContractRequestPayload : Payload
    {

        /// <summary>
        /// Gets or sets the exchangeSegmentList
        /// </summary>
        [DataMember(Name = "exchangeSegmentList")]
        public List<string> exchangeSegmentList { get; set; }

    }
}