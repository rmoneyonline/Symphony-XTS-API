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

namespace XTSAPI
{
    public static class Globals
    {
        /// <summary>
        /// Converts a long time stamp to date time structure
        /// </summary>
        /// <param name="timeStamp">Time stamp</param>
        /// <returns></returns>
        public static DateTime ToDateTime(long timeStamp, int exchangeSegment)
        {
            if (timeStamp == 0)
                return DateTime.MinValue;

            switch (exchangeSegment)
            {
                case 1:
                case 2:
                case 3:
                    return new DateTime(1980, 1, 1).AddSeconds(timeStamp);
                default:
                    return new DateTime(1970, 1, 1).AddSeconds(timeStamp);
            }

        }

        public static long FromDateTime(DateTime dateTime, int exchangeSegment)
        {
            switch (exchangeSegment)
            {
                case 1:
                case 2:
                case 3:
                    return (long)dateTime.Subtract(new DateTime(1980, 1, 1)).TotalSeconds;
                default:
                    return (long)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            }
        }


        public static int GetExchangeFromString(string exchangeSegment)
        {
            if (string.IsNullOrEmpty(exchangeSegment))
                return -1;

            exchangeSegment = exchangeSegment.ToUpper();
            /*
            { "NSECM":1,"NSEFO":2,"NSECD":3,"BSECM":11,"BSEFO":12,"BSECD":13,"NCDEX":21,"MSEICM":41,"MSEIFO":42,"MSEICD":43,"MCXFO":51}
            */
            switch (exchangeSegment)
            {
                case "NSECM":
                    return 1;
                case "NSEFO":
                    return 2;
                case "NSECD":
                    return 3;
                case "BSECM":
                    return 11;
                case "BSEFO":
                    return 12;
                case "BSECD":
                    return 13;
                case "NCDEX":
                    return 21;
                case "MSEICM":
                    return 41;
                case "MSEIFO":
                    return 42;
                case "MSEICD":
                    return 43;
                case "MCXFO":
                    return 51;
                default:
                    return -1;
            }
        }


        public static string GetExchangefromInt(int value)
        {
            switch(value)
            {
                case (int)ExchangeSegment.NSECO:
                    return "NSECO";
                    break;
                case (int)ExchangeSegment.NSECD:
                    return "NSECD";
                    break;
                case (int)ExchangeSegment.NSECM:
                    return "NSECM";
                    break;
                case (int)ExchangeSegment.NSEFO:
                    return "NSEFO";
                    break;
                case (int)ExchangeSegment.BSECM:
                    return "BSECM";
                    break;
                case (int)ExchangeSegment.BSEFO:
                    return "BSEFO";
                    break;
                case (int)ExchangeSegment.BSECO:
                    return "BSECO";
                    break;
                case (int)ExchangeSegment.NCDEX:
                    return "NCEDX";
                    break;
                case (int)ExchangeSegment.BSECD:
                    return "BSECD";
                    break;
                case (int)ExchangeSegment.MSEIFO:
                    return "MSEIFO";
                    break;
                case (int)ExchangeSegment.MSEICD:
                    return "MSEICD";
                    break;
                case (int)ExchangeSegment.MCXFO:
                    return "MCXFO";
                    break;
                default:
                    return "NONE";

            }
        }

    }
}
