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
    public class SquareoffPayload : Payload
    {
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
        /// Gets or sets the product type
        /// <see cref="ProductType"/>
        /// </summary>
        [DataMember(Name = "productType")]
        public string productType { get; set; }

        /// <summary>
        /// Gets or sets the square off mode
        /// </summary>
        [DataMember(Name = "squareoffMode")]
        public string squareoffMode { get; set; }

        /// <summary>
        /// Gets or sets the position square off quantity type
        /// </summary>
        [DataMember(Name = "positionSquareOffQuantityType")]
        public string positionSquareOffQuantityType { get; set; }

        /// <summary>
        /// Gets or sets the square off quantity value
        /// </summary>
        [DataMember(Name = "squareOffQtyValue")]
        public int squareOffQtyValue { get; set; }

    }
}
