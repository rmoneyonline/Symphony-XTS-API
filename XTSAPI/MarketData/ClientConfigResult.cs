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

namespace XTSAPI.MarketData
{
    [DataContract]
    public class ClientConfigResult
    {
        /// <summary>
        /// Gets or sets the exchange segments
        /// </summary>
        [DataMember(Name = "exchangeSegments")]
        public Dictionary<string, int> exchangeSegments { get; set; }

        /// <summary>
        /// Gets or sets the market data ports
        /// </summary>
        [DataMember(Name = "xtsMessageCode")]
        public Dictionary<string, string> xtsMessageCode { get; set; }

        /// <summary>
        /// Gets or sets the instrument types
        /// </summary>
        [DataMember(Name = "instrumentType")]
        public Dictionary<string, string> instrumentType { get; set; }

        /// <summary>
        /// Gets or sets the publish format
        /// <see cref="PublishFormat"/>
        /// </summary>
        [DataMember(Name = "publishFormat")]
        public List<string> publishFormat { get; set; }

        /// <summary>
        /// Gets or sets the broadcast mode
        /// <see cref="BroadCastMode"/>
        /// </summary>
        [DataMember(Name = "broadcastMode")]
        public List<string> broadcastMode { get; set; }
    }

}
