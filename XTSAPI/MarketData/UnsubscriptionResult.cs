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
    public class UnsubscriptionResult
    {
        /// <summary>
        /// Gets or sets the market data port
        /// </summary>
        [DataMember(Name = "marketDataPort")]
        public int marketDataPort { get; set; }

        /// <summary>
        /// Gets or sets the unsubscription list
        /// </summary>
        [DataMember(Name = "unsubList")]
        public List<UnsubList> unsubList { get; set; }

        /// <summary>
        /// Gets or sets teh xts message code
        /// </summary>
        [DataMember(Name = "xtsMessageCode")]
        public int xtsMessageCode { get; set; }

    }

    [DataContract]
    public class UnsubList
    {
        /// <summary>
        /// Gets or sets the exchange segment
        /// <see cref="ExchangeSegment"/>
        /// </summary>
        [DataMember(Name = "exchangeSegment")]
        public int exchangeSegment { get; set; }

        /// <summary>
        /// Gets or sets the exchange instrument id
        /// </summary>
        [DataMember(Name = "exchangeInstrumentID")]
        public long exchangeInstrumentID { get; set; }
    }
}
