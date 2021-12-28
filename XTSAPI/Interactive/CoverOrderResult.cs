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
    public class CoverOrderResult
    {
        /// <summary>
        /// Gets or sets the entry app order id
        /// </summary>
        [DataMember(Name = "EntryAppOrderID")]
        public long EntryAppOrderID { get; set; }

        /// <summary>
        /// Gets or sets the exit/stop app order id
        /// </summary>
        [DataMember(Name = "ExitAppOrderID")]
        public long ExitAppOrderID { get; set; }

        /// <summary>
        /// Gets or sets the unique order identifier
        /// </summary>
        [DataMember(Name = "OrderUniqueIdentifier")]
        public string OrderUniqueIdentifier { get; set; }

        /// <summary>
        /// Gets or sets the client id
        /// </summary>
        [DataMember(Name = "ClientID")]
        public string ClientID { get; set; }

    }
}
