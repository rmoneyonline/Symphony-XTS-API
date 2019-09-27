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
    public class OrderPayload : CoverOrderPayload
    {
        /// <summary>
        /// Gets or sets the product type
        /// <see cref="ProductType"/>
        /// </summary>
        [DataMember(Name = "productType")]
        public string productType { get; set; }

        /// <summary>
        /// Gets or sets the order type
        /// <see cref="OrderType"/>
        /// </summary>
        [DataMember(Name = "orderType")]
        public string orderType { get; set; }

        /// <summary>
        /// Gets or sets the time in force / order duration
        /// <see cref="TimeInForce"/>
        /// </summary>
        [DataMember(Name = "timeInForce")]
        public string timeInForce { get; set; }

    }
}
