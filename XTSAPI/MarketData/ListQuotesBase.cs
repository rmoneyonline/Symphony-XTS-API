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
    public abstract class ListQuotesBase : ResultBase
    {
        /// <summary>
        /// Gets or sets the exchange segment
        /// </summary>
        [DataMember(Name = "ExchangeSegment")]
        public int ExchangeSegment { get; set; }

        /// <summary>
        /// Gets or sets the exchange instrument id
        /// </summary>
        [DataMember(Name = "ExchangeInstrumentID")]
        public long ExchangeInstrumentID { get; set; }

        /// <summary>
        /// Gets or sets the exchange time stamp. 
        /// <see cref="Globals.ToDateTime(long)"/>
        /// </summary>
        [DataMember(Name = "ExchangeTimeStamp")]
        public long ExchangeTimeStamp { get; set; }


        public void AssignValue(object data)
        {
            if (data == null)
                return;

            string[] array = data.ToString().Split(',');

            foreach (var item in array)
            {
                if (string.IsNullOrEmpty(item))
                    continue;

                string[] array1 = item.Split(':');
                if (array1.Length != 2)
                    continue;

                this.Parse(array1[0], array1[1]);
            }

        }

        protected internal virtual void Parse(string field, string value)
        {
            switch (field)
            {
                case "t":
                    string[] array = value.Split('_');

                    if (int.TryParse(array[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out int exchange))
                    {
                        this.ExchangeSegment = exchange;
                    }

                    if (long.TryParse(array[1], NumberStyles.Integer, CultureInfo.InvariantCulture, out long token))
                    {
                        this.ExchangeInstrumentID = token;
                    }

                    break;
            }
        }

    }

    
}
