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
    public class PositionConvertResult
    {
        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        [DataMember(Name = "UserID")]
        public string UserID { get; set; }

        /// <summary>
        /// Gets or sets the client id
        /// </summary>
        [DataMember(Name = "ClientID")]
        public string ClientID { get; set; }

        /// <summary>
        /// Gets or sets the old product type
        /// </summary>
        [DataMember(Name = "OldProductType")]
        public string OldProductType { get; set; }

        /// <summary>
        /// Gets or sets the new product type
        /// </summary>
        [DataMember(Name = "NewProductType")]
        public string NewProductType { get; set; }

        /// <summary>
        /// Gets or sets the exchange
        /// </summary>
        [DataMember(Name = "ExchangeSegment")]
        public string ExchangeSegment { get; set; }

        /// <summary>
        /// Gets or sets the exchange instrument id
        /// </summary>
        [DataMember(Name = "ExchangeInstrumentId")]
        public string ExchangeInstrumentId { get; set; }

        /// <summary>
        /// Gets or sets the converted quantity
        /// </summary>
        [DataMember(Name = "CovertedQty")]
        public string CovertedQty { get; set; }

    }
}
