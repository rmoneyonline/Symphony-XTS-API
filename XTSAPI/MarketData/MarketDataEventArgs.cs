/*
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTSAPI.MarketData
{
    public class MarketDataEventArgs : EventArgs
    {
        public MarketDataEventArgs(MarketDataPorts port, ListQuotesBase value, object sourceData)
        {
            this.SourceData = sourceData;
            this.MarketDataPorts = port;
            this.Value = value;
        }

        /// <summary>
        /// Gets the original data sent by the datafeed. It can be Json string for full mode or ',' separated string for partial mode data, or binary data
        /// </summary>
        public object SourceData { get; private set; }

        /// <summary>
        /// Gets the market data ports
        /// <see cref="XTSAPI.MarketData.MarketDataPorts"/>
        /// </summary>
        public MarketDataPorts MarketDataPorts { get; private set; }

        /// <summary>
        /// Gets the value duly converted to its object
        /// </summary>
        public ListQuotesBase Value { get; private set; }
    }
}
