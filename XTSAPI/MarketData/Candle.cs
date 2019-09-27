/*
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XTSAPI.MarketData
{
    [DataContract]
    public class Candle : OHLCBase
    {
        /// <summary>
        /// Gets or sets the bar time
        /// </summary>
        [DataMember(Name = "BarTime")]
        public long BarTime { get; set; }

        /// <summary>
        /// Gets or sets the bar volume
        /// </summary>
        [DataMember(Name = "BarVolume")]
        public long BarVolume { get; set; }

        /// <summary>
        /// Gets or sets the open interest. If 
        /// </summary>
        [DataMember(Name = "OpenInterest")]
        public long OpenInterest { get; set; }

        /// <summary>
        /// Gets or sets the sum of quantity to price
        /// </summary>
        [DataMember(Name = "SumOfQtyInToPrice")]
        public double SumOfQtyInToPrice { get; set; }


        protected internal override void Parse(string field, string value)
        {
            switch (field)
            {

                case "bt":
                    if (long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out long barTime))
                    {
                        this.BarTime = barTime;
                    }
                    break;
                case "bv":
                    if (long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out long volume))
                    {
                        this.BarVolume = volume;
                    }
                    break;
                case "pv":
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double totalValue))
                    {
                        this.SumOfQtyInToPrice = totalValue;
                    }
                    break;
                default:
                    base.Parse(field, value);
                    break;
            }
        }
    }
}
