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
    public class Indices : ListQuotesBase
    {
        //Ignore for now
        public static Dictionary<string, ContractInfo> Symbols { get; private set; } = new Dictionary<string, ContractInfo>()
        {
            { "NIFTY 50", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            /*{ "NIFTY NEXT 50", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 1 } },
            { "NIFTY 100", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY 200", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY 500", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY MIDCAP 50", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY MIDCAP 100", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY SMLCAP 100", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "INDIA VIX", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY MIDCAP 150", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY SMLCAP 50", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY SMLCAP 250", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY MIDSML 400", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY BANK", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY AUTO", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY FIN SERVICE", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY FMCG", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY IT", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY MEDIA", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY METAL", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY PHARMA", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY PSU BANK", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY PVT BANK", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY REALTY", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY DIV OPPS 50", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY GROWSECT 15", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY100 QUALTY30", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY50 VALUE 20", new ContractInfo()  { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY50 TR 2X LEV", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY50 PR 2X LEV", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY50 TR 1X INV", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY50 PR 1X INV", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY50 DIV POINT", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY ALPHA 50", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY50 EQL WGT", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY100 EQL WGT", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY100 LOWVOL30", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY COMMODITIES", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY CONSUMPTION", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY CPSE", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY ENERGY", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY INFRA", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY100 LIQ 15", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY MID LIQ 15", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY MNC", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY PSE", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY SERV SECTOR", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY GS 8 13YR", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY GS 10YR", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY GS 10YR CLN", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY GS 4 8YR", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY GS 11 15YR", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY GS 15YRPLUS", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } },
            { "NIFTY GS COMPSITE", new ContractInfo() { ExchangeSegment = 1, ExchangeInstrumentID = 0 } }*/
        };

        /// <summary>
        /// Gets or sets the index name
        /// </summary>
        [DataMember(Name = "IndexName")]
        public string IndexName { get; set; }

        /// <summary>
        /// Gets or sets the index value
        /// </summary>
        [DataMember(Name = "IndexValue")]
        public double IndexValue { get; set; }

        /// <summary>
        /// Gets or sets the open index
        /// </summary>
        [DataMember(Name = "OpeningIndex")]
        public double OpeningIndex { get; set; }

        /// <summary>
        /// Gets or sets the closing index
        /// </summary>
        [DataMember(Name = "ClosingIndex")]
        public double ClosingIndex { get; set; }

        /// <summary>
        /// Gets or sets the high index
        /// </summary>
        [DataMember(Name = "HighIndexValue")]
        public double HighIndexValue { get; set; }

        /// <summary>
        /// Gets or sets the low index
        /// </summary>
        [DataMember(Name = "LowIndexValue")]
        public double LowIndexValue { get; set; }

        /// <summary>
        /// Gets or sets the percentage change
        /// </summary>
        [DataMember(Name = "PercentChange")]
        public double PercentChange { get; set; }

        /// <summary>
        /// Gets or sets the yearly high
        /// </summary>
        [DataMember(Name = "YearlyHigh")]
        public double YearlyHigh { get; set; }

        /// <summary>
        /// Gets or sets the yearly low
        /// </summary>
        [DataMember(Name = "YearlyLow")]
        public double YearlyLow { get; set; }

        /// <summary>
        /// Gets or sets the number of upmoves
        /// </summary>
        [DataMember(Name = "NoOfUpmoves")]
        public double NoOfUpmoves { get; set; }

        /// <summary>
        /// Gets or sets the number of downmoves
        /// </summary>
        [DataMember(Name = "NoOfDownmoves")]
        public double NoOfDownmoves { get; set; }

        /// <summary>
        /// Gets or sets the market capitalization
        /// </summary>
        [DataMember(Name = "MarketCapitalisation")]
        public double MarketCapitalisation { get; set; }


        protected internal override void Parse(string field, string value)
        {
            switch (field)
            {
                case "t":
                    string[] array = value.Split('_');

                    if (int.TryParse(array[0], NumberStyles.Integer, CultureInfo.InvariantCulture, out int exchange))
                    {
                        this.ExchangeSegment = exchange;
                    }

                    this.IndexName = array[1];

                    break;
                case "ltp":
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double indexValue))
                    {
                        this.IndexValue = indexValue;
                    }
                    break;
                case "lut":
                    if (long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out long lut))
                    {
                        this.ExchangeTimeStamp = lut;
                    }
                    break;
                case "pc":
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double pc))
                    {
                        this.PercentChange = pc;
                    }
                    break;
                case "o":
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double open))
                    {
                        this.OpeningIndex = open;
                    }
                    break;
                case "h":
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double high))
                    {
                        this.HighIndexValue = high;
                    }
                    break;
                case "l":
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double low))
                    {
                        this.LowIndexValue = low;
                    }
                    break;
                case "c":
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double close))
                    {
                        this.ClosingIndex = close;
                    }
                    break;
                case "yh":
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double yearlyHigh))
                    {
                        this.YearlyHigh = yearlyHigh;
                    }
                    break;
                case "yl":
                    if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double yearlyLow))
                    {
                        this.YearlyLow = yearlyLow;
                    }
                    break;
                default:
                    //base.Parse(field, value);
                    break;
            }
        }

    }
}


