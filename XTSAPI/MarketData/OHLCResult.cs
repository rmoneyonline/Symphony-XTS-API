using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XTSAPI.MarketData
{
    [DataContract]
    public class OHLCResult
    {
        [DataMember(Name = "exchangeSegment")]
        public string exchangeSegment { get; set; }

        [DataMember(Name = "exchangeInstrumentID")]
        public string exchangeInstrumentID { get; set; }

        [DataMember(Name = "dataResponse")]
        public string dataResponse { get; set; }

    }
}
