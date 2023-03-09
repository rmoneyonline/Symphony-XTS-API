using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XTSAPI.Interactive
{
    [DataContract]
    public class BracketXTSOrderResult 
    {

        [DataMember(Name = "OrderUniqueIdentifier")]
        public string OrderUniqueIdentifier { get; set; }

        [DataMember(Name = "OrderID")]
        public long OrderID { get; set; }

        /// <summary>
        /// Gets or sets the client id
        /// </summary>
        [DataMember(Name = "ClientID")]
        public string ClientID { get; set; }
    }
}
