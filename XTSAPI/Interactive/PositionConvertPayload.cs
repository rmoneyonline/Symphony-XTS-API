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
    public class PositionConvertPayload : Payload
    {
        /// <summary>
        /// Gets or sets the app order id
        /// </summary>
        [DataMember(Name = "appOrderID")]
        public long appOrderID { get; set; }

        /// <summary>
        /// Gets or sets the execution id
        /// </summary>
        [DataMember(Name = "executionID")]
        public long executionID { get; set; }

        /// <summary>
        /// Gets or sets the old product type
        /// <see cref="ProductType"/>
        /// </summary>
        [DataMember(Name = "oldProductType")]
        public string oldProductType { get; set; }

        /// <summary>
        /// Gets or sets the new product type
        /// <see cref="ProductType"/>
        /// </summary>
        [DataMember(Name = "newProductType")]
        public string newProductType { get; set; }
    }
}

