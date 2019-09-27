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
    public class QuoteResult
    {
        /// <summary>
        /// Gets or sets the market data port
        /// <see cref="MarketDataPorts"/>
        /// </summary>
        [DataMember(Name = "mdp")]
        public int mdp { get; set; }

        /// <summary>
        /// Gets or sets the quotes list
        /// </summary>
        [DataMember(Name = "quotesList")]
        public List<QuoteList> quotesList { get; set; }

        /// <summary>
        /// Gets or sets the list quotes
        /// </summary>
        [DataMember(Name = "listQuotes")]
        public List<string> listQuotes { get; set; }

    }

    [DataContract]
    public class QuoteResult<T> where T : ListQuotesBase
    {
        /// <summary>
        /// Gets or sets the market data ports
        /// <see cref="MarketDataPorts"/>
        /// </summary>
        [DataMember(Name = "mdp")]
        public int mdp { get; set; }

        /// <summary>
        /// Gets or sets the quotes list
        /// </summary>
        [DataMember(Name = "quotesList")]
        public List<QuoteList> quotesList { get; set; }

        /// <summary>
        /// Gets or sets the list quotes
        /// </summary>
        [DataMember(Name = "listQuotes")]
        public List<T> listQuotes { get; set; }

    }

    [DataContract]
    public class QuoteList
    {
        /// <summary>
        /// Gets or sets the exchange
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
