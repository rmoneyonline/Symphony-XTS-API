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
    public class TradeResult : OrderTradeBase
    {
        /// <summary>
        /// Gets or sets the last traded price
        /// </summary>
        [DataMember(Name = "LastTradedPrice")]
        public double LastTradedPrice { get; set; }

        /// <summary>
        /// Gets or sets the last traded quantity
        /// </summary>
        [DataMember(Name = "LastTradedQuantity")]
        public int LastTradedQuantity { get; set; }

        /// <summary>
        /// Gets or sets the last execution transact time
        /// </summary>
        [DataMember(Name = "LastExecutionTransactTime")]
        public DateTime LastExecutionTransactTime { get; set; }

        /// <summary>
        /// Gets or sets the execution id
        /// </summary>
        [DataMember(Name = "ExecutionID")]
        public long ExecutionID { get; set; }

        /// <summary>
        /// Gets or sets teh execution report index
        /// </summary>
        [DataMember(Name = "ExecutionReportIndex")]
        public int ExecutionReportIndex { get; set; }



    }
}
