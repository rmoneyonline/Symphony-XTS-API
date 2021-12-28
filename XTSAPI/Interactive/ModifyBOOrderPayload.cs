using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XTSAPI.Interactive
{
    [DataContract]
    public class ModifyBOOrderPayload : BracketOrderPayloadBase
    {
        /// <summary>
        /// Gets or sets the app order id
        /// </summary>
        [DataMember(Name = "appOrderID")]
        public long appOrderID { get; set; }

        

    }
}
