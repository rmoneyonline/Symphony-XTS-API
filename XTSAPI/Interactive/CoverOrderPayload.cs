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
    public class CoverOrderPayload : Payload
    {
        /// <summary>
        /// Gets or sets the exchange
        /// </summary>
        [DataMember(Name = "exchangeSegment")]
        public string exchangeSegment { get; set; }

        /// <summary>
        /// Gets or sets the exchange instrument id
        /// </summary>
        [DataMember(Name = "exchangeInstrumentID")]
        public long exchangeInstrumentID { get; set; }

        /// <summary>
        /// Gets or sets the order side
        /// <see cref="OrderSide"/>
        /// </summary>
        [DataMember(Name = "orderSide")]
        public string orderSide { get; set; }

        /// <summary>
        /// Gets or sets the disclosed quantity
        /// </summary>
        [DataMember(Name = "disclosedQuantity")]
        public int disclosedQuantity { get; set; }

        /// <summary>
        /// Gets or sets the quantity
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
        [DataMember(Name = "stopPrice")]
        public double stopPrice { get; set; }

        /// <summary>
        /// Gets or sets the unique order id
        /// </summary>
        [DataMember(Name = "orderUniqueIdentifier")]
        public string orderUniqueIdentifier { get; set; }
    }
}
