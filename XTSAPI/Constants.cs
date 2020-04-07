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
    /* Client config response
    {"type":"success","code":"s-user-0001","description":"Valid User.","result":
    {
        "enums":
            {
                "socketEvent":
                    ["joined","error","warning","success","order","trade","logout","position"],
                "orderSide":
                    ["BUY","SELL"],
                "orderSource":
                    ["TWSAPI","WebAPI","MobileAndroidAPI","MobileWindowsAPI","MobileIOSAPI"],
                "positionSqureOffMode":
                    ["DayWise","NetWise"],
                "positionSquareOffQuantityType":
                    ["Percentage","ExactQty"],
                "dayOrNet":
                    ["DAY","NET"],
                "exchangeInfo":
                    {"NSECM":
                        {"productType":
                            ["CNC","MIS","NRML"],
                        "orderType":
                            ["StopLimit","StopMarket","Limit","Market"],
                        "timeInForce":["DAY","IOC"]
                        },
                    "NSEFO":
                        {"productType":
                            ["MIS","NRML"],
                        "orderType":
                            ["StopLimit","StopMarket","Limit","Market"],
                        "timeInForce":
                            ["DAY","IOC"]
                         }
                    }
                },
        "clientCodes":
            ["CientCode"],
        "token":"xxxTOKENxxx",
        "isInvestorClient":true}
    }*/

    public static class OrderSource
    {
        /// <summary>
        /// Gets the TWSAPI source
        /// </summary>
        public static readonly string TWSAPI = "TWSAPI";
        /// <summary>
        /// Gets the WebAPI source
        /// </summary>
        public static readonly string WebAPI = "WebAPI";
        /// <summary>
        /// Gets the MobileAndrridAPI
        /// </summary>
        public static readonly string MobileAndroidAPI = "MobileAndroidAPI";
        /// <summary>
        /// Gets the MobileWindowsAPI
        /// </summary>
        public static readonly string MobileWindowsAPI = "MobileWindowsAPI";
        /// <summary>
        /// Gets teh MobileIOSAPI
        /// </summary>
        public static readonly string MobileIOSAPI = "MobileIOSAPI";
    }

    public static class OrderSide
    {
        /// <summary>
        /// Gets the BUY order side
        /// </summary>
        public static readonly string BUY = "BUY";
        /// <summary>
        /// Gets the SELL order side
        /// </summary>
        public static readonly string SELL = "SELL";
    }

    public static class PositionMode
    {
        /// <summary>
        /// Gets the DayWise position square off mode
        /// </summary>
        public static readonly string DayWise = "DayWise";
        /// <summary>
        /// Gets the NetWise position square off mode
        /// </summary>
        public static readonly string NetWise = "NetWise";
    }

    public static class PositionSquareOffQuantityType
    {
        /// <summary>
        /// Gets the Percentage position square off quantity type
        /// </summary>
        public static readonly string Percentage = "Percentage";
        /// <summary>
        /// Gets the ExactQty position square off quantity type
        /// </summary>
        public static readonly string ExactQty = "ExactQty";
    }

    public static class DayOrNet
    {
        /// <summary>
        /// Gets the DAY day or net
        /// </summary>
        public static readonly string DAY = "DAY";
        /// <summary>
        /// Gets the NET day or net
        /// </summary>
        public static readonly string NET = "NET";
    }

    public static class ProductType
    {
        /// <summary>
        /// Gets the CNC product type
        /// </summary>
        public static readonly string CNC = "CNC";
        /// <summary>
        /// Gets the NRML product type
        /// </summary>
        public static readonly string NRML = "NRML";
        /// <summary>
        /// Gets the MIS product type
        /// </summary>
        public static readonly string MIS = "MIS";
        /// <summary>
        /// Gets the CO product type
        /// </summary>
        public static readonly string CO = "CO";
    }

    public static class OrderType
    {
        /// <summary>
        /// Gets the DAY order type
        /// </summary>
        public static readonly string DAY = "DAY";
        /// <summary>
        /// Gets the IOC order type
        /// </summary>
        public static readonly string IOC = "IOC";
    }

    public static class TimeInForce
    {
        /// <summary>
        /// Gets the DAY time in force
        /// </summary>
        public static readonly string DAY = "DAY";
        /// <summary>
        /// Gets the IOC time in force
        /// </summary>
        public static readonly string IOC = "IOC";
    }


    public enum ExchangeSegment
    {
        /// <summary>
        /// NSE cash
        /// </summary>
        NSECM = 1,
        /// <summary>
        /// NSE FnO
        /// </summary>
        NSEFO = 2,
        /// <summary>
        /// NSE CDS
        /// </summary>
        NSECD = 3,
        /// <summary>
        /// BSE Cash
        /// </summary>
        BSECM = 11,
        /// <summary>
        /// BSE FnO
        /// </summary>
        BSEFO = 12,
        /// <summary>
        /// BSE CDS
        /// </summary>
        BSECD = 13,
        /// <summary>
        /// NCDEX
        /// </summary>
        NCDEX = 21,
        /// <summary>
        /// MSEI Cash
        /// </summary>
        MSEICM = 41,
        /// <summary>
        /// MSEI FnO
        /// </summary>
        MSEIFO = 42,
        /// <summary>
        /// MSEI CDS
        /// </summary>
        MSEICD = 43,
        /// <summary>
        /// MCX FnO
        /// </summary>
        MCXFO = 51
    }

    



}
