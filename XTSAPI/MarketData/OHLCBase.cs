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
    public class OHLCBase : ListQuotesBase
    {
        /// <summary>
        /// Gets or sets the open
        /// </summary>
        [DataMember(Name = "Open")]
        public double Open { get; set; }

        /// <summary>
        /// Gets or sets the high
        /// </summary>
        [DataMember(Name = "High")]
        public double High { get; set; }

        /// <summary>
        /// Gets or sets the low
        /// </summary>
        [DataMember(Name = "Low")]
        public double Low { get; set; }

        /// <summary>
        /// Gets or sets the close
        /// </summary>
        [DataMember(Name = "Close")]
        public double Close { get; set; }

        protected internal override void Parse(string field, string value)
        {
            switch (field)
            {
                case "o":
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double open))
                    {
                        this.Open = open;
                    }
                    break;
                case "h":
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double high))
                    {
                        this.High = high;
                    }
                    break;
                case "l":
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double low))
                    {
                        this.Low = low;
                    }
                    break;
                case "c":
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double close))
                    {
                        this.Close = close;
                    }
                    break;
                default:
                    base.Parse(field, value);
                    break;
            }
        }
    }
}
