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

namespace XTSAPI.Interactive
{
    [DataContract]
    public class ModifyOrderPayload : Payload
    {
        /// <summary>
        /// Gets or sets the app order id
        /// </summary>
        [DataMember(Name = "appOrderID")]
        public long appOrderID { get; set; }

        /// <summary>
        /// Gets or sets the modified clientID{For pro : ******, client: "clientID"
        /// <see cref="clientID"/>
        /// </summary>
        [DataMember(Name = "clientID")]
        public string clientID { get; set; }

        /// <summary>
        /// Gets or sets the modified product type
        /// <see cref="ProductType"/>
        /// </summary>
        [DataMember(Name = "modifiedProductType")]
        public string modifiedProductType { get; set; }

        /// <summary>
        /// Gets or sets the modified order type
        /// <see cref="OrderType"/>
        /// </summary>
        [DataMember(Name = "modifiedOrderType")]
        public string modifiedOrderType { get; set; }

        /// <summary>
        /// Gets or sets the modified order quantity
        /// </summary>
        [DataMember(Name = "modifiedOrderQuantity")]
        public int modifiedOrderQuantity { get; set; }

        /// <summary>
        /// Gets or sets the modified disclosed quantity
        /// </summary>
        [DataMember(Name = "modifiedDisclosedQuantity")]
        public int modifiedDisclosedQuantity { get; set; }
        
        /// <summary>
        /// Gets or sets the modified limti pricce
        /// </summary>
        [DataMember(Name = "modifiedLimitPrice")]
        public double modifiedLimitPrice { get; set; }

        /// <summary>
        /// Gets or sets the modified stop price
        /// </summary>
        [DataMember(Name = "modifiedStopPrice")]
        public double modifiedStopPrice { get; set; }

        /// <summary>
        /// Gets or sets the modified time in force / order duration
        /// <see cref="TimeInForce"/>
        /// </summary>
        [DataMember(Name = "modifiedTimeInForce")]
        public string modifiedTimeInForce { get; set; }

        /// <summary>
        /// Gets or sets the unique order identifier
        /// </summary>
        [DataMember(Name = "orderUniqueIdentifier")]
        public string orderUniqueIdentifier { get; set; }

    }
}
