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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Collections.ObjectModel;
using XTSAPI;
using XTSAPI.Interactive;
using XTSAPI.MarketData;
using System.Net.Http;
using System.Net;
using System.Globalization;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        /*
         *  Please enter your login credentials here 
        */


        
        const string INTERACTIVE_URL = "https://xts.rmoneyindia.co.in:3000/interactive";
        const string MARKET_URL = "https://xts.rmoneyindia.co.in:3000/marketdata";
        const string USER_ID = "35A1";

        const string MARKET_APPKEY = "73cc2a77b072d7b8fb6354";
        const string MARKET_SECRET = "Lcpv323@eU";

        const string TRADING_APPKEY = "1586dda2fe3a3ed8c65559";
        const string TRADING_SECRET = "Ucbe577@Az";
        



        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;    //Wilt thou forgive that sin where I begun
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private const string connect = "Connect";
        private const string disconnect = "Disconnect";

        private string connectStr = connect;

        public string ConnectStr
        {
            get { return connectStr; }
            set
            {
                connectStr = value;
                NotifyPropertyChanged();
            }
        }

        private string connectMarketData = connect;

        public string ConnectMarketData
        {
            get { return connectMarketData; }
            set
            {
                connectMarketData = value;
                NotifyPropertyChanged();
            }
        }

        
        public ObservableCollection<string> Logs { get; private set; } = new ObservableCollection<string>();

        public MarketDataPorts[] Ports
        {
            get
            {
                return (MarketDataPorts[])Enum.GetValues(typeof(MarketDataPorts));
            }
        }

        public MarketDataPorts MarketDataPorts { get; set; } = MarketDataPorts.marketDepthEvent;

        //StreamWriter writer = new StreamWriter("Logs.txt", true);

        private void Log(string msg, [CallerMemberName]string methodName = null)
        {
            if (string.IsNullOrEmpty(msg))
                return;

            if (this.Dispatcher.CheckAccess())
            {
                this.Logs.Add($"{methodName}: {msg}");

                //writer?.WriteLine($"{DateTime.Now} : {msg}");
                //writer?.Flush();

            }
            else
            {
                this.Dispatcher.InvokeAsync(() =>
                {
                    Log(msg, methodName);
                });
            }
        }

        #region Interactive

        InteractiveLoginResult login = null;
        XTSInteractive interactive = null;

        private async void Button_Connect(object sender, RoutedEventArgs e)
        {
            if (this.ConnectStr == connect)
            {
                if (this.interactive != null)
                {
                    this.interactive.Json -= OnJson;
                    this.interactive.Interactive -= OnInteractive;
                    this.interactive.Exception -= OnException;
                    this.interactive.ConnectionState -= OnConnection;
                }


                this.interactive = new XTSInteractive(INTERACTIVE_URL);
                this.interactive.Interactive += OnInteractive;
                this.interactive.Json += OnJson;
                this.interactive.Exception += OnException;
                this.interactive.ConnectionState += OnConnection;
                ;
                InteractiveLoginResult login = await interactive.LoginAsync<InteractiveLoginResult>(TRADING_APPKEY, TRADING_SECRET);

                if (login != null)
                {
                    this.login = login;

                    if (this.interactive.ConnectToSocket())
                    {
                        this.ConnectStr = disconnect;
                    }
                }
            }
            else if (this.ConnectStr == disconnect)
            {
                if (this.login == null)
                {
                    return;
                }
                await Task.Run(() => this.interactive.LogoutAsync());

                this.ConnectStr = connect;
            }
        }

        private async void Button_MarketStatus(object sender, RoutedEventArgs e)
        {
            if (this.interactive == null)
                return;

            await this.interactive.GetExchangeStatusAsync(this.interactive.UserId);
        }




        private async void Profile_Click(object sender, RoutedEventArgs e)
        {
            if (this.interactive == null)
                return;

            await this.interactive.GetProfileAsync(this.interactive.UserId);
        }

        private async void Balance_Click(object sender, RoutedEventArgs e)
        {
            if (this.interactive == null)
                return;

            await this.interactive.GetBalanceAsync(this.interactive.UserId);
        }

        private async void Orders_Click(object sender, RoutedEventArgs e)
        {

            if (this.interactive == null)
                return;

            await this.interactive?.GetOrderAsync();
        }

        private async void Trades_Click(object sender, RoutedEventArgs e)
        {
            if (this.interactive == null)
                return;

            await this.interactive.GetTradesAsync();
        }

        private async void Positions_Click(object sender, RoutedEventArgs e)
        {
            if (this.interactive == null)
                return;

            await this.interactive.GetDayPositionAsync();

            await this.interactive.GetNetPositionAsync();
        }

        private async void Holdings_Click(object sender, RoutedEventArgs e)
        {
            if (this.interactive == null)
                return;

            await this.interactive.GetHoldingsAsync(this.interactive.UserId);
        }

        private string GenerateOrderTag()
        {
            string id = Guid.NewGuid().ToString();
            if (id.Length > 20)
            {
                id = id.Substring(0, 20);
            }

            return id;
        }

        OrderIdResult orderId;

        private async void Button_Buy(object sender, RoutedEventArgs e)
        {

            this.orderId = await this.interactive?.PlaceOrderAsync("NSECM", 26001
                , "BUY", "LIMIT", 1, 910.0d, 0.0d, "MIS", "DAY", orderUniqueIdentifier: GenerateOrderTag());
        }

        private async void Button_Modify(object sender, RoutedEventArgs e)
        {
            if (this.orderId == null)
                return;

            var modify = await this.interactive?.ModifyOrderAsync(this.orderId.AppOrderID, "LIMIT", 1, 920.0, 0.0d, "MIS", "DAY", orderUniqueIdentifier: GenerateOrderTag());
        }

        private async void Button_Cancel(object sender, RoutedEventArgs e)
        {

            if (this.orderId == null)
                return;

            try
            {
                await this.interactive?.CancelOrderAsync(orderId.AppOrderID);

            }
            catch (Exception ex)
            {

            }
            
        }

        CoverXTSOrderResult co;

        private async void Button_PlaceCO(object sender, RoutedEventArgs e)
        {
            CoverOrderPayload payload = new CoverOrderPayload()
            {
                disclosedQuantity = 0,
                exchangeSegment = "NSECM",
                exchangeInstrumentID = 26001,
                limitPrice = 1200,
                stopPrice = 1190,
                orderQuantity = 1,
                orderSide = "SELL",
                orderUniqueIdentifier = GenerateOrderTag()
            };

            var co = await this.interactive?.PlaceCoverOrderAsync(payload);

        }

        private async void Button_ModifyCO(object sender, RoutedEventArgs e)
        {
            if (this.co == null)
                return;

            var result = await this.interactive?.ModifyOrderAsync(this.co.EntryAppOrderID, "LIMIT", 1, 1000.0d, 0.0d, "MIS", "DAY",orderUniqueIdentifier: GenerateOrderTag());
        }

        private async void Button_ExitCO(object sender, RoutedEventArgs e)
        {
            if (this.co == null)
                return;

            await this.interactive?.ExitCoverOrderAsync(this.co.ExitAppOrderID);
        }

        [Obsolete]
        private async void Button_SquareOff(object sender, RoutedEventArgs e)
        {
            /*
            PositionList positions = await this.interactive?.GetDayPositionAsync();
            if (positions == null || positions.positionList.Length == 0)
                return;


            if (!long.TryParse(positions.positionList[0].ExchangeInstrumentID, out long instrumentId))
                return;

            await this.interactive?.SquareOff(positions.positionList[0].ExchangeSegment, instrumentId, positions.positionList[0].ProductType,
                PositionMode.DayWise, 100, PositionSquareOffQuantityType.Percentage);
            */
        }
        

        private async void Button_ConvertPosition(object sender, RoutedEventArgs e)
        {
            TradeResult[] trades = await this.interactive?.GetTradesAsync();
            if (trades == null || trades.Length == 0)
                return;

            await this.interactive?.ConvertPositionAsync(trades[0].ExchangeSegment, trades[0].ExchangeInstrumentID, trades[0].ProductType, trades[0].ProductType == "MIS" ? "CNC" : "MIS", trades[0].CumulativeQuantity, false);
        }

        private async void OrderHistory_Click(object sender, RoutedEventArgs e)
        {
            var history = await this.interactive?.GetOrderAsync(3401190656);

        }

        #endregion

        #region MarketData


        MarketDataLoginResult marketDataLogin = null;
        XTSMarketData marketData = null;
        
        private async void Button_ConnectMarketData(object sender, RoutedEventArgs e)
        {
            if (this.ConnectMarketData == connect)
            {
                if (this.marketData != null)
                {
                    this.marketData.MarketData -= OnMarketData;
                    this.marketData.ConnectionState -= OnConnection;
                    this.marketData.Json -= OnJson;
                    this.marketData.Exception -= OnException;
                }


                this.marketData = new XTSMarketData(MARKET_URL);
                this.marketData.MarketData += OnMarketData;
                this.marketData.ConnectionState += OnConnection;
                this.marketData.Json += OnJson;
                this.marketData.Exception += OnException;

                this.marketDataLogin = await this.marketData.LoginAsync<MarketDataLoginResult>(MARKET_APPKEY, MARKET_SECRET);
                
                if (this.marketDataLogin != null)
                {
                    if (this.marketData.ConnectToSocket((MarketDataPorts[])Enum.GetValues(typeof(MarketDataPorts)), PublishFormat.JSON, BroadcastMode.Partial))
                    {
                        this.ConnectMarketData = disconnect;
                    }
                }
            }
            else if (this.ConnectMarketData == disconnect)
            {
                await Task.Run(async () =>
                    {
                        await this.marketData?.LogoutAsync();
                    });


                this.ConnectMarketData = connect;
            }
            
        }

        

        private async void Button_Config(object sender, RoutedEventArgs e)
        {
            ClientConfigResult config = await this.marketData?.GetConfigAsync();
        }

        private async void Button_SearchByString(object sender, RoutedEventArgs e)
        {
            
            ContractInfo[] result = await this.marketData?.SearchByStringAsync("PNB");
        }

        private async void Button_SearchById(object sender, RoutedEventArgs e)
        {
            SearchByIdResult[] result = await this.marketData?.SearchByIdAsync(new List<Instruments>()
                {
                    new Instruments()
                    {
                        exchangeInstrumentID = 26001,
                        exchangeSegment = (int)ExchangeSegment.NSECM
                    }
                });
        }

        private List<Instruments> GetInstruments(MarketDataPorts port)
        {
            int exchange = (int)ExchangeSegment.NSECM;
            long exchangeInstrumentId = 26001;   //reliance

            if (this.MarketDataPorts == MarketDataPorts.openInterestEvent)
            {
                exchange = (int)ExchangeSegment.NSEFO;
                exchangeInstrumentId = 44330; //nifty aug 20 fut
            }
            else if (this.MarketDataPorts == MarketDataPorts.indexDataEvent)
            {
                exchangeInstrumentId = 0;
            }

            return new List<Instruments>()
            {
                new Instruments()
                {
                    exchangeInstrumentID = exchangeInstrumentId,
                    exchangeSegment = exchange
                }
            };

        }

        private async void Button_SubscribeRT(object sender, RoutedEventArgs e)
        {
            if (this.marketData == null)
                return;


            object result = null;
            switch (this.MarketDataPorts)
            {
                case MarketDataPorts.touchlineEvent:
                    result = await Subscribe<Touchline>();
                    break;
                case MarketDataPorts.marketDepthEvent:
                    result = await Subscribe<MarketDepth>();
                    break;
                case MarketDataPorts.indexDataEvent:
                    result = await Subscribe<Indices>();
                    break;
                case MarketDataPorts.candleDataEvent:
                    result = await Subscribe<Candle>();
                    break;
                case MarketDataPorts.exchangeTradingStatusEvent:
                    break;
                case MarketDataPorts.openInterestEvent:
                    result = await Subscribe<OI>();
                    break;
                case MarketDataPorts.LTPEvent:
                    result = await Subscribe<LTPEvent>();
                    break;

                default:
                    break;
            }

        }

        private async Task<QuoteResult<T>> Subscribe<T>() where T : ListQuotesBase
        {
            if (this.marketData == null)
                return null;

            return await this.marketData.SubscribeAsync<T>(((int)MarketDataPorts), GetInstruments(this.MarketDataPorts)).ConfigureAwait(false);
        } 

        private async void Button_Unsubscribe(object sender, RoutedEventArgs e)
        {
            UnsubscriptionResult response = await this.marketData?.UnsubscribeAsync(((int)this.MarketDataPorts), GetInstruments(this.MarketDataPorts));
        }


        private async void Button_Quotes(object sender, RoutedEventArgs e)
        {
            object result = null;

            switch (this.MarketDataPorts)
            {
                case MarketDataPorts.touchlineEvent:
                    result = await GetQuotes<Touchline>();
                    break;
                case MarketDataPorts.marketDepthEvent:
                    result = await GetQuotes<MarketDepth>();
                    break;
                case MarketDataPorts.indexDataEvent:
                    result = await GetQuotes<Indices>();
                    break;
                case MarketDataPorts.candleDataEvent:
                    result = await GetQuotes<Candle>();
                    break;
                case MarketDataPorts.exchangeTradingStatusEvent:
                    break;
                case MarketDataPorts.openInterestEvent:
                    result = await GetQuotes<OI>();
                    break;
                default:
                    break;
            }

        }

        private async Task<QuoteResult<T>> GetQuotes<T>() where T : ListQuotesBase
        {
            if (this.marketData == null)
                return null;

            return await this.marketData.GetQuotesAsync<T>(((int)this.MarketDataPorts), GetInstruments(this.MarketDataPorts)).ConfigureAwait(false);
        }

        private class OHLC
        { 
            public DateTime TimeStamp { get; set; }
            public double Open { get; set; }
            public double High { get; set; }
            public double Low { get; set; }
            public double Close { get; set; }
            public long Volume { get; set; }
            public long OI { get; set; }
        }
        private async void Button_GetHistory(object sender, RoutedEventArgs e)
        {
            

            /*
            {"type":"error","code":"e-app-001","description":"Bad Request","result":{"status":400,"statusText":"Bad Request",
            "errors":[{"field":["startTime"],"location":"query","messages":["\"startTime\" is required"],"types":["any.required"]},{"field":["endTime"],
            "location":"query","messages":["\"endTime\" is required"],"types":["any.required"]},{"field":["fromDate"],"location":"query",
            "messages":["\"fromDate\" is not allowed"],"types":["object.allowUnknown"]},{"field":["toDate"],"location":"query",
            "messages":["\"toDate\" is not allowed"],"types":["object.allowUnknown"]},{"field":["compressionType"],"location":"query",
            "messages":["\"compressionType\" is not allowed"],"types":["object.allowUnknown"]},{"field":["source"],"location":"query",
            "messages":["\"source\" is not allowed"],"types":["object.allowUnknown"]}]}}
            */

            if (this.marketData == null)
                return;

            var result = await this.marketData.GetOHLCHistoryAsync(
                ExchangeSegment.NSECM, //ExchangeSegment.NSEFO, 
                26001, //11037, //44330, 
                DateTime.Now.Date.AddDays(-2), DateTime.Now, 60);

            if (result == null || string.IsNullOrWhiteSpace(result.dataResponse))
                return;

            List<OHLC> tmp = new List<OHLC>();

            string data = result.dataResponse; //"1596618959|2168|2193.1|2167|2188.8|1265232|0|,1596619019|2189.55|2189.55|2181.75|2184.05|544014|0|,1596619079|2184.7|2186.1|2180.3|2185.05|323878|0|,1596619139|2185|2185.3|2180|2180|281615|0|,1596619199|2180|2188|2179.35|2187|239687|0|,1596619259|2186.8|2193|2186.15|2188.6|400306|0|,1596619319|2189|2191.5|2181.2|2182.7|331634|0|,1596619379|2182.5|2186.7|2176.25|2186.45|343235|0|,1596619439|2186.6|2189.55|2186.2|2187.8|188604|0|,1596619499|2187|2192.4|2185.35|2191.3|249541|0|,1596619559|2191.65|2195.9|2191|2195|347287|0|,1596619619|2195|2195.8|2189|2193.75|259200|0|,1596619679|2193|2194.5|2190|2190.15|183748|0|,1596619739|2190.45|2194.4|2190.45|2191.7|150684|0|,1596619799|2191.85|2192|2182.9|2186.05|299382|0|,1596619859|2187.3|2190.7|2185.75|2188.3|130236|0|,1596619919|2188.3|2189|2185|2187.9|137332|0|,1596619979|2187.95|2188.8|2183|2183.25|143076|0|,1596620039|2183.95|2187.5|2183.4|2186|74328|0|,1596620099|2186.2|2189.9|2186|2187.1|130543|0|,1596620159|2186|2189.4|2186|2188|79483|0|,1596620219|2188|2189|2186.35|2186.65|69782|0|,1596620279|2186|2187.55|2185.6|2186.45|68577|0|,1596620339|2186.45|2187|2184.15|2185|121740|0|,1596620399|2184.6|2185.25|2181.4|2182.05|156254|0|,1596620459|2181.75|2182.35|2177|2179|269104|0|,1596620519|2179.75|2182|2178.3|2179|164278|0|,1596620579|2179|2179|2173.6|2175.45|280709|0|,1596620639|2175|2180|2175|2178.65|133810|0|,1596620699|2178.6|2178.85|2171.6|2173.05|170063|0|,1596620759|2172.55|2172.95|2162.9|2164.7|482069|0|,1596620819|2164.55|2168.75|2160.1|2160.1|297196|0|,1596620879|2159|2161.35|2152.55|2161.35|550042|0|,1596620939|2160.8|2167.1|2160.6|2167.1|330518|0|,1596620999|2167|2169.6|2164.25|2169.6|149035|0|,1596621059|2169.8|2170|2165.6|2169.2|154932|0|,1596621119|2169|2177|2169|2175|285590|0|,1596621179|2175.4|2177.7|2174.05|2175|220589|0|,1596621239|2174.5|2177|2170.9|2171.95|140747|0|,1596621299|2172.35|2172.8|2167.9|2167.9|144147|0|,1596621359|2168.2|2170.75|2165|2165.75|139517|0|,1596621419|2166.2|2170.8|2165.75|2169.35|87022|0|,1596621479|2169.35|2170|2166.85|2167|67824|0|,1596621539|2167|2168.05|2165.05|2165.85|87604|0|,1596621599|2166.9|2169.9|2165.85|2169.9|75401|0|,1596621659|2169.8|2170|2167.4|2168.05|62764|0|,1596621719|2168.65|2168.9|2165.5|2166|53267|0|,1596621779|2166|2167|2162|2162.1|166454|0|,1596621839|2162.5|2166.95|2162.1|2163.6|73860|0|,1596621899|2163.5|2166.5|2163.1|2165.4|52920|0|,1596621959|2165.25|2167|2165.05|2165.1|43555|0|,1596622019|2166.05|2168|2165|2168|58693|0|,1596622079|2168.35|2174.5|2167.85|2173.55|154670|0|,1596622139|2173.5|2173.55|2171|2171.9|79346|0|,1596622199|2171.9|2172|2169.2|2169.2|54747|0|,1596622259|2170|2170|2167|2169.45|45229|0|,1596622319|2169.6|2170|2168.4|2170|33250|0|,1596622379|2169.6|2170|2167.75|2167.75|38622|0|,1596622439|2167.75|2169.6|2167|2169|27774|0|,1596622499|2168.15|2170|2168.15|2169.25|37149|0|,1596622559|2169|2172.5|2169|2171|52447|0|,1596622619|2171.35|2171.9|2168.1|2168.5|49735|0|,1596622679|2169|2169|2167|2167.05|47188|0|,1596622739|2167.05|2170|2167|2169.45|33092|0|,1596622799|2170|2171|2167.6|2167.6|28512|0|,1596622859|2167.7|2168.85|2167|2168.3|44172|0|,1596622919|2168.3|2168.3|2163|2163.6|108609|0|,1596622979|2163.7|2166|2163|2164.5|59300|0|,1596623039|2164.95|2165.55|2163|2163|60858|0|,1596623099|2163|2164.6|2161.2|2163.8|109515|0|,1596623159|2163.8|2164.6|2162|2162.5|41371|0|,1596623219|2162.6|2166|2162.1|2165.85|63561|0|,1596623279|2166|2166.55|2164.2|2164.35|37007|0|,1596623339|2165|2165.85|2163.3|2164|29820|0|,1596623399|2163.25|2163.95|2163|2163.2|20099|0|,1596623459|2163.15|2165|2163.15|2164|27366|0|,1596623519|2164|2164.6|2160|2160|122519|0|,1596623579|2160|2162|2160|2160.2|68387|0|,1596623639|2160.4|2160.75|2157.5|2158.05|140793|0|,1596623699|2158|2159|2154|2158.55|213290|0|,1596623759|2158.55|2161.6|2158|2160.8|98005|0|,1596623819|2160.2|2162.55|2160.15|2161.85|70093|0|,1596623879|2161.15|2162|2159|2161.9|57926|0|,1596623939|2161.1|2162.4|2158|2158.3|60199|0|,1596623999|2158.3|2158.45|2155.5|2156|55240|0|,1596624059|2156|2159.6|2155|2159.6|81166|0|,1596624119|2159.4|2161|2157|2158|48637|0|,1596624179|2157|2159|2157|2159|43714|0|,1596624239|2159|2159|2156|2157.6|60989|0|,1596624299|2157.55|2158.15|2156.85|2157.05|47818|0|,1596624359|2157.1|2157.6|2151.85|2156|235139|0|,1596624419|2155|2155.1|2152.65|2153.8|76287|0|,1596624479|2153.9|2154.05|2145.4|2145.7|394293|0|,1596624539|2145.85|2146.25|2137.55|2143.1|511046|0|,1596624599|2142.9|2144.15|2140|2143.6|154979|0|,1596624659|2144|2146.75|2141.4|2142|176889|0|,1596624719|2142.3|2142.4|2138|2140|214076|0|,1596624779|2139.5|2140.95|2136|2137.75|178066|0|,1596624839|2138.2|2138.75|2133.8|2133.8|190997|0|,1596624899|2134|2135|2130.65|2132.55|281153|0|,1596624959|2133|2139.3|2132|2136.5|169670|0|,1596625019|2137.5|2142|2136|2140.2|176886|0|,1596625079|2140.1|2140.1|2136.9|2137.95|83414|0|,1596625139|2138|2138.8|2135|2136.7|83823|0|,1596625199|2136.75|2140.5|2136.1|2139|66168|0|,1596625259|2137.7|2139.4|2136.4|2138|51712|0|,1596625319|2138.55|2148.8|2138.1|2148.1|212784|0|,1596625379|2146.2|2148|2145.6|2147|151124|0|,1596625439|2146.95|2157|2146.8|2156.4|376996|0|,1596625499|2156.1|2161.6|2155.7|2158.35|277292|0|,1596625559|2158|2159|2155|2155|168816|0|,1596625619|2155|2155.9|2152|2154.5|148308|0|,1596625679|2154.4|2154.4|2150|2153.4|125046|0|,1596625739|2153.65|2154.15|2152|2153.1|44772|0|,1596625799|2152.6|2157.7|2152.6|2156.7|92728|0|,1596625859|2156.1|2156.5|2154.35|2156|51304|0|,1596625919|2156|2156|2151.7|2152.25|50612|0|,1596625979|2153.2|2154|2151.2|2152.6|43940|0|,1596626039|2152.2|2154.35|2152|2154|31876|0|,1596626099|2153.25|2155|2153.2|2153.8|33052|0|,1596626159|2154|2154.5|2147.65|2148.5|105366|0|,1596626219|2147.3|2148.5|2142.9|2144|170646|0|,1596626279|2144.2|2145.4|2141.65|2141.65|79850|0|,1596626339|2143.5|2144.5|2142|2144.5|67612|0|,1596626399|2144.65|2146.95|2144|2145|72300|0|,1596626459|2144.65|2147.9|2144|2147.5|41692|0|,1596626519|2147.8|2149.8|2145.25|2148.85|69008|0|,1596626579|2149|2149|2145.7|2146.3|24594|0|,1596626638|2146|2147.25|2145.8|2146.6|22694|0|,1596626699|2146.25|2146.7|2143.55|2145.8|41368|0|,1596626759|2145|2149|2144|2148.6|52014|0|,1596626819|2149|2149.45|2147|2148.4|31276|0|,1596626879|2147.65|2148.4|2146.1|2146.5|21792|0|,1596626939|2146.5|2147|2145|2145.7|25710|0|,1596626999|2145.7|2147.35|2145.55|2146.45|20522|0|,1596627059|2147|2147.7|2146|2146|21044|0|,1596627119|2146.05|2146.4|2137.55|2138.4|154960|0|,1596627179|2138.3|2142.55|2138.15|2142|68712|0|,1596627239|2142.4|2142.4|2139|2139.6|62158|0|,1596627299|2139.5|2140|2136.9|2139.8|90528|0|,1596627359|2139|2142.7|2138|2141.75|58908|0|,1596627419|2142.55|2142.95|2138.75|2139.65|42204|0|,1596627479|2140|2141|2139|2140|44502|0|,1596627539|2140|2141.05|2139|2141.05|29918|0|,1596627599|2141|2141.05|2139.05|2139.05|30664|0|,1596627659|2139.05|2140.35|2136.55|2137.35|66892|0|,1596627719|2136.5|2137.3|2135|2136.95|111648|0|,1596627779|2136|2139.65|2136|2137|62248|0|,1596627839|2136.25|2138.5|2136.25|2137.35|27782|0|,1596627899|2137.55|2138|2136.1|2137.15|34806|0|,1596627959|2137.05|2137.05|2134|2135.85|103524|0|,1596628019|2135.4|2136|2126.05|2128|333696|0|,1596628079|2128.2|2131.45|2125.35|2130.2|168320|0|,1596628139|2130.55|2131.8|2128.35|2128.7|85032|0|,1596628199|2129.4|2132.8|2128|2131.2|132894|0|,1596628259|2131.4|2138|2131.35|2136.6|149630|0|,1596628319|2137|2137.4|2135|2136.8|67266|0|,1596628379|2136|2138|2136|2137.45|55370|0|,1596628439|2137.4|2137.95|2135.1|2135.3|49712|0|,1596628499|2135.05|2136|2133|2133.5|51070|0|,1596628559|2133.5|2137.8|2133.1|2137.5|54704|0|,1596628619|2137|2137.95|2135|2135.05|42782|0|,1596628679|2135|2135.85|2128|2128.55|123846|0|,1596628739|2128.05|2133.2|2128|2132.8|61334|0|,1596628799|2132.25|2132.25|2128|2128|61160|0|,15966260019|2128.6|2133|2128|2133|109460|0|,1596628919|2132.95|2136.95|2132.05|2134.5|100340|0|,1596628979|2134.45|2136|2133|2134.45|48260|0|,1596629039|2134.7|2135.95|2132.2|2134.4|44694|0|,1596629099|2134.95|2135|2132|2132.6|26154|0|,1596629159|2133|2136|2132|2135.15|56736|0|,1596629219|2135.15|2135.5|2134|2134.15|31916|0|,1596629279|2134.15|2134.65|2132|2132.05|34116|0|,1596629339|2132.5|2136|2132.5|2135.9|65378|0|,1596629399|2135.9|2141.5|2135.8|2140.3|121876|0|,1596629459|2141.4|2141.7|2136.7|2137.1|103252|0|,1596629519|2137.55|2138|2136.35|2137.2|35570|0|,1596629579|2137|2138.65|2136.5|2137.65|33292|0|,1596629639|2138|2140|2136.55|2139.45|38232|0|,1596629699|2140|2140|2138|2139|27990|0|,1596629759|2139|2145|2138.5|2144.9|141140|0|,1596629819|2145.7|2145.7|2142.5|2145.65|107746|0|,1596629879|2145|2149|2144|2147|135586|0|,1596629939|2147|2147.55|2143.45|2144.8|81494|0|,1596629999|2144.7|2145.95|2142.15|2143.2|50484|0|,1596630059|2143.3|2145.45|2142.15|2143.4|60962|0|,1596630119|2144.5|2146.95|2143.55|2145.75|40926|0|,1596630179|2145.45|2145.95|2143.3|2143.85|31614|0|,1596630239|2143.85|2144|2141.7|2141.85|44716|0|,1596630299|2142.25|2143|2136.25|2137|102202|0|,1596630359|2137.15|2139.6|2135.3|2139.2|68522|0|,1596630419|2139.5|2139.7|2138.2|2139.45|28598|0|,1596630479|2139.05|2142|2138.35|2141.65|46312|0|,1596630539|2141.65|2143.9|2140.85|2142.7|38786|0|,1596630599|2142.7|2142.8|2141.5|2142.25|20460|0|,1596630659|2142.3|2142.95|2138.1|2139|47568|0|,1596630719|2139|2141.1|2138.6|2140.6|27288|0|,1596630779|2140|2141|2138.95|2140.15|19458|0|,1596630839|2140|2140.85|2139.55|2139.55|20284|0|,1596630899|2139.55|2140|2139|2139.5|15512|0|,1596630959|2139.5|2142.95|2139.5|2142.2|40586|0|,1596631019|2142.5|2146|2142.5|2145|85532|0|,1596631079|2145.45|2148.6|2144.9|2148.6|88646|0|,1596631139|2148.3|2148.5|2144.35|2145.95|64324|0|,1596631199|2145.3|2147.95|2145|2146.5|37184|0|,1596631259|2146.6|2152.65|2146|2152.45|177460|0|,1596631318|2152|2155|2151.25|2152.35|152914|0|,1596631379|2153|2153.5|2147.3|2149.35|127510|0|,1596631439|2149.35|2149.75|2146.5|2147|47080|0|,1596631499|2147|2148.3|2146|2148|61544|0|,1596631559|2148|2149.35|2147|2148.75|29818|0|,1596631619|2148.65|2149|2143.1|2143.35|78580|0|,1596631679|2144.05|2144.8|2143.35|2144.7|35434|0|,1596631739|2144.6|2146.5|2144.2|2144.45|25014|0|,1596631799|2144.9|2144.9|2141.4|2141.5|39566|0|,1596631859|2140.9|2142.55|2139|2141.1|77386|0|,1596631919|2141.1|2144|2141.1|2142.85|24756|0|,1596631979|2143.8|2144.95|2143|2144.3|19540|0|,1596632039|2143.55|2145.4|2143.55|2144.8|18136|0|,1596632099|2145|2147.95|2144.55|2147.1|38948|0|,1596632159|2147.95|2147.95|2143.4|2143.4|32036|0|,1596632219|2143.45|2144.3|2142|2142.7|29370|0|,1596632279|2143|2144.6|2142.7|2143.05|20230|0|,1596632339|2144.15|2145.85|2143|2143.85|20262|0|,1596632399|2143.85|2144.25|2143.05|2143.65|12960|0|,1596632459|2143.1|2146|2143|2145.15|23422|0|,1596632519|2144.45|2145.95|2144|2145.8|15932|0|,1596632579|2145.4|2146|2144.1|2145.4|21214|0|,1596632639|2146.25|2147.5|2145.8|2146.45|35266|0|,1596632699|2147|2150|2146.2|2149|62656|0|,1596632759|2148.95|2150.5|2147.35|2147.8|62156|0|,1596632819|2147.95|2147.95|2146.2|2146.5|32254|0|,1596632878|2147|2147.45|2146.5|2146.8|19750|0|,1596632939|2147|2148.3|2146.75|2147.65|18770|0|,1596632999|2147.45|2150|2147.45|2149.2|47050|0|,1596633059|2149.2|2151|2148.5|2149.25|82192|0|,1596633119|2149.3|2150.5|2147|2147|44322|0|,1596633179|2147|2147.65|2145.1|2145.1|53234|0|,1596633239|2145.1|2145.7|2143.1|2144|73898|0|,1596633299|2143.7|2146.9|2143.5|2146.9|39702|0|,1596633359|2145.75|2147|2144.95|2145.05|27990|0|,1596633419|2145.05|2146.95|2145|2146.65|24816|0|,1596633479|2146.2|2148|2145.45|2145.65|22248|0|,1596633539|2145.95|2147|2145.95|2147|12660|0|,1596633599|2146.4|2147.05|2146|2146|12660|0|,1596633659|2146.25|2146.35|2146|2146.35|12160|0|,1596633719|2146.1|2147|2146|2146.7|13514|0|,1596633779|2147|2148|2144|2144.05|50812|0|,1596633839|2144.05|2145|2142.7|2142.7|40522|0|,1596633899|2142.55|2144.3|2140.1|2143.85|67540|0|,1596633959|2144|2144|2141.8|2142|22242|0|,1596634019|2142|2143|2141.05|2143|28768|0|,1596634079|2143|2143|2141.2|2141.5|20512|0|,1596634139|2141.5|2141.5|2138|2139.35|112192|0|,1596634199|2139|2141.45|2138.35|2141|32350|0|,1596634258|2141|2142|2140.45|2140.6|37392|0|,1596634319|2140.8|2140.8|2138.35|2138.65|48414|0|,1596634379|2139|2139|2135.25|2136.2|118124|0|,1596634439|2135.6|2136.5|2133.4|2135.1|110832|0|,1596634499|2135.25|2140.55|2135|2139.6|85608|0|,1596634559|2139.4|2142|2138.75|2141.2|52080|0|,1596634619|2141.15|2142|2140|2140|30468|0|,1596634679|2140|2140.05|2137.2|2137.95|35416|0|,1596634739|2137.95|2138|2136.05|2137.5|41998|0|,1596634799|2137.05|2138.95|2136.7|2138.8|33590|0|,1596634859|2138.35|2138.95|2137.6|2138.35|18688|0|,1596634919|2138.4|2140|2137.1|2139.95|28464|0|,1596634979|2139.5|2142.15|2139.4|2141.1|47396|0|,1596635039|2141.7|2142|2139.35|2140.8|34362|0|,1596635099|2140.05|2141|2139.65|2140.1|13066|0|,1596635159|2140.15|2140.55|2138|2139.1|28536|0|,1596635219|2139.85|2141.15|2139.1|2140.7|31066|0|,1596635279|2140.5|2141.75|2139.25|2141.7|20602|0|,1596635339|2141.75|2141.8|2137.1|2138|31370|0|,1596635399|2138|2138|2136.1|2137.45|57446|0|,1596635459|2137.4|2137.55|2136|2137|35476|0|,1596635519|2137.05|2138.7|2136.9|2138|19028|0|,1596635579|2137.85|2138.4|2132.95|2133.2|93128|0|,1596635639|2133.5|2136|2133.3|2134.85|47494|0|,1596635698|2134.25|2134.85|2130.15|2133.75|109588|0|,1596635759|2133|2136.55|2132|2136.5|75620|0|,1596635819|2136.6|2136.85|2135.05|2135.95|29432|0|,1596635879|2135.5|2138.65|2135.5|2137.9|49426|0|,1596635939|2138|2140|2137.9|2139|54838|0|,1596635999|2139|2140.8|2138|2140.8|36676|0|,1596636059|2140.6|2145|2140.2|2144|130876|0|,1596636119|2144.05|2145|2141.3|2142.5|78278|0|,1596636179|2142.5|2142.95|2138.15|2139|49736|0|,1596636239|2138.55|2139.25|2136.25|2138|47206|0|,1596636299|2138.4|2139.45|2137.55|2138.35|21654|0|,1596636359|2138.85|2142|2138.35|2141.05|34602|0|,1596636419|2141.95|2141.95|2140|2141|26974|0|,1596636479|2141|2141|2138|2138.3|34110|0|,1596636539|2138.3|2139.95|2138.3|2139.15|18062|0|,1596636599|2138.7|2139|2138.3|2138.5|16260|0|,1596636659|2138.95|2139.85|2138|2138|17214|0|,1596636719|2138.35|2139.75|2138|2139.75|21070|0|,1596636779|2138.8|2139.55|2138.5|2138.9|17472|0|,1596636839|2138.75|2139.4|2138.5|2138.8|20988|0|,1596636899|2138.8|2139.4|2138.5|2139.4|12692|0|,1596636959|2139.85|2142|2139.4|2142|45178|0|,1596637019|2142.55|2142.6|2141|2141.5|31672|0|,1596637079|2141.5|2141.55|2138.6|2140.5|25262|0|,1596637139|2140.8|2141.2|2139.15|2139.3|20616|0|,1596637199|2139.3|2139.8|2139|2139.15|11184|0|,1596637259|2139.05|2141|2139.05|2141|20554|0|,1596637319|2140.8|2141.2|2140.05|2141|19798|0|,1596637379|2141|2141.5|2140|2140.1|29600|0|,1596637439|2140.05|2140.2|2139|2139.1|28990|0|,1596637499|2139.1|2140.3|2139.1|2140.25|20436|0|,1596637559|2140.25|2144.95|2140|2144.55|79468|0|,1596637619|2145|2146.5|2142|2142.2|101176|0|,1596637679|2142.1|2143|2139.1|2139.55|37324|0|,1596637739|2139.45|2140.1|2136|2136.4|101946|0|,1596637799|2136.5|2138.9|2136|2138.85|71520|0|,1596637859|2139.85|2139.85|2137|2137.35|30284|0|,1596637919|2137|2137.3|2136|2136.6|42852|0|,1596637979|2136.9|2138.4|2136.5|2137|24138|0|,1596638039|2137.05|2140|2137|2139.35|33498|0|,1596638098|2139.35|2139.6|2138.5|2139.5|22434|0|,1596638159|2139.05|2139.95|2138.55|2139.6|16104|0|,1596638219|2139|2140|2137.5|2137.65|20176|0|,1596638279|2137.8|2139.5|2137.55|2138.15|20858|0|,1596638339|2139|2139.4|2138|2139|11148|0|,1596638399|2139|2139|2138|2138|13766|0|,1596638459|2138.05|2138.6|2137.1|2137.65|23168|0|,1596638519|2137.9|2138.95|2137.65|2138.05|21350|0|,1596638579|2138.05|2138.2|2133.15|2133.85|110058|0|,1596638639|2134.7|2134.95|2133|2134|58140|0|,1596638699|2134.95|2137.15|2134.1|2135.55|37228|0|,1596638759|2135.55|2135.55|2130.25|2132.35|117718|0|,1596638819|2133|2134|2132|2132.4|52678|0|,1596638879|2132.6|2133|2130.5|2130.65|72404|0|,1596638939|2131|2131|2126.55|2128.65|203710|0|,1596638999|2128.15|2133.7|2128|2132.8|109806|0|,1596639059|2131.8|2133.6|2131.8|2132.65|54520|0|,1596639119|2132.7|2135.85|2132.7|2134.05|76018|0|,1596639179|2134.05|2136|2133.7|2136|55420|0|,1596639239|2136|2136|2133|2133|50492|0|,1596639299|2133|2133|2130.25|2130.25|39588|0|,1596639359|2130.7|2134.75|2130.1|2133.6|51952|0|,1596639419|2133|2134.1|2132.3|2132.7|45988|0|,1596639479|2134|2134|2132.3|2133|30760|0|,1596639539|2133.4|2134.95|2132.8|2134.1|35616|0|,1596639599|2134|2134.6|2131|2131.6|40360|0|,1596639659|2131.6|2131.6|2128|2130.75|126364|0|,1596639719|2130.9|2130.9|2128.8|2129.95|67736|0|,1596639779|2129.95|2132|2129|2132|62814|0|,1596639839|2131.9|2132|2130.5|2131|40072|0|,1596639899|2131.05|2131.5|2127.5|2129.8|126938|0|,1596639959|2129.8|2130.9|2128.65|2130.85|64916|0|,1596640019|2130.65|2131.75|2128.25|2128.95|61648|0|,1596640079|2128.9|2129.95|2123.2|2125.9|226396|0|,1596640139|2125.95|2125.95|2120.95|2125|204186|0|,1596640199|2124.85|2125|2122.8|2123.45|135840|0|,1596640259|2124.35|2124.95|2118.9|2118.9|327218|0|,1596640319|2120.75|2123.6|2118.45|2120.7|276278|0|,1596640379|2120.5|2126.25|2120.25|2126.25|136290|0|,1596640439|2126.85|2128.7|2124.45|2128.7|98456|0|,1596640499|2128.65|2129|2126.1|2128.85|120394|0|,1596640559|2128.4|2129.95|2126|2127.7|174160|0|,1596640619|2128|2128.45|2123.95|2126.8|264586|0|,1596640679|2127|2127.7|2125|2127|101576|0|,1596640739|2126.8|2129|2126.2|2127.6|88382|0|,1596640799|2128.2|2128.6|2126.4|2128.4|86224|0|,1596640859|2127.95|2129.2|2126.35|2128.6|117128|0|,1596640919|2129|2132.85|2123.5|2127.5|294416|0|,1596640979|2127.05|2129|2127.05|2128.9|43888|0|,1596641039|2129|2129.7|2126.7|2129.3|51284|0|,1596641099|2129.35|2131|2128.8|2130.65|38316|0|,1596641159|2131.2|2132|2130.05|2132|40632|0|,1596641219|2131.8|2132.5|2131.05|2131.85|48392|0|,1596641279|2131.1|2132|2130.6|2131.05|45608|0|,1596641339|2131.45|2132.8|2131.2|2131.7|67784|0|,1596641399|2132.05|2133|2131.1|2133|60164|0|,1596641400|2132.1|2132.1|2132.1|2132.1|16|0|,1596704872|2157|2157|2157|2157|211481|0|,1596705359|2154.9|2155.45|2142.8|2143.85|670098|0|,1596705419|2144.8|2144.9|2134|2141.95|422891|0|,1596705479|2142|2144|2140.15|2142.9|164644|0|,1596705539|2142.85|2143.05|2137|2140|173019|0|,1596705599|2139.75|2139.8|2137.05|2139.5|118933|0|,1596705659|2139.8|2139.85|2130.3|2132|251202|0|,1596705719|2131.2|2134.2|2131.1|2132.75|124236|0|,1596705779|2132.55|2134.45|2126.3|2128.45|205625|0|,1596705839|2128.3|2128.6|2121.35|2122.85|206597|0|,1596705899|2122|2122.85|2116.15|2119|282107|0|,1596705959|2118.95|2119.8|2108.1|2117.6|377194|0|,1596706019|2117|2123.8|2116.85|2122.1|220674|0|,1596706079|2122.25|2123|2120|2121.2|99060|0|,1596706139|2121.2|2125|2117.4|2123.55|167861|0|,1596706199|2124|2125|2120.8|2122.95|90878|0|,1596706259|2122.8|2123|2121|2121.8|66731|0|,1596706319|2121.5|2121.55|2115.8|2116|145478|0|,1596706379|2116.2|2120.85|2116|2118.05|84691|0|,1596706439|2118|2122.25|2118|2121.75|75738|0|,1596706499|2121.95|2126.8|2121.75|2125.95|150152|0|,1596706559|2125.95|2131.65|2125|2130.8|185251|0|,1596706619|2130.8|2131.4|2127.6|2129.55|129854|0|,1596706679|2130|2133|2129.95|2131.45|146190|0|,1596706739|2131.35|2132|2128.05|2128.25|104111|0|,1596706799|2128.25|2131|2128.25|2129.95|56267|0|,1596706859|2129.95|2134.2|2129.95|2132.8|104802|0|,1596706919|2132.8|2135|2132.8|2133|85312|0|,1596706979|2133.65|2134.65|2128.8|2130|93819|0|,1596707039|2130|2130.7|2126.1|2127.1|67164|0|,1596707099|2126.5|2130|2126.5|2128.15|58460|0|,1596707159|2128.45|2132.95|2128.45|2132.5|45282|0|,1596707219|2132.25|2133|2131.65|2132.5|45232|0|,1596707279|2132.5|2138|2132.5|2136.7|150537|0|,1596707339|2136.9|2138|2135.1|2135.9|87325|0|,1596707399|2135.7|2137.5|2134.25|2136.9|79691|0|,1596707459|2136.9|2139.6|2135.05|2136|99142|0|,1596707519|2136|2137.9|2136|2137.25|41350|0|,1596707579|2137.65|2139|2137|2137.6|66973|0|,1596707639|2137.7|2138.6|2136|2137|56325|0|,1596707699|2137.35|2137.65|2133.3|2135|69978|0|,1596707759|2135.05|2137|2135|2135.2|40894|0|,1596707819|2135.95|2136|2133.15|2134|45778|0|,1596707879|2133.5|2134|2131.55|2132|59420|0|,1596707939|2132.8|2134.5|2132|2132.65|30698|0|,1596707999|2132.65|2134|2132.5|2133.45|19387|0|,1596708059|2133.8|2135.8|2133.5|2134.95|32453|0|,1596708119|2135.75|2136.8|2135|2136|35924|0|,1596708179|2135.8|2136.8|2135|2135.9|27766|0|,1596708239|2135.75|2135.9|2129.1|2129.55|106860|0|,1596708299|2129.45|2132|2129.45|2131|51193|0|,1596708359|2131.45|2131.5|2127.95|2127.95|65486|0|,1596708419|2126.95|2128.8|2124.25|2128|120692|0|,1596708479|2127.45|2128.9|2127.35|2128|33732|0|,1596708539|2127.3|2130.7|2127.25|2130.7|34599|0|,1596708599|2130.7|2131.8|2130|2131.35|30624|0|,1596708659|2131.8|2135.3|2131.35|2134.7|76017|0|,1596708719|2135|2135.6|2131.5|2133.45|45476|0|,1596708779|2133.7|2134|2133.3|2133.75|11903|0|,1596708839|2133.9|2135|2133.3|2135|24163|0|,1596708899|2135|2135|2132|2132.45|21777|0|,1596708959|2132.45|2134.6|2132.45|2133.5|22430|0|,1596709019|2133.9|2134.35|2133.5|2133.9|12650|0|,1596709079|2134|2135.6|2133.6|2135.5|63464|0|,1596709139|2135.5|2136|2135.35|2136|39651|0|,1596709199|2136|2138.95|2135.7|2136.35|82468|0|,1596709259|2137|2138.15|2136.4|2137|33331|0|,1596709319|2136.6|2137|2135.5|2136.6|27128|0|,1596709379|2135.85|2137.85|2135.4|2137.05|35710|0|,1596709439|2137.5|2137.75|2136|2136.5|24169|0|,1596709499|2136.7|2137|2136|2136|19281|0|,1596709559|2135.8|2136.4|2134|2135|27135|0|,1596709619|2135.05|2136|2134.6|2135.3|17629|0|,1596709679|2135.3|2137|2135|2136.4|23349|0|,1596709739|2136.8|2136.8|2135.5|2136|13196|0|,1596709799|2136.5|2136.5|2135.35|2135.5|12353|0|,1596709859|2135.8|2136|2135.5|2135.5|12958|0|,1596709919|2135.5|2136.4|2135.5|2136.1|14343|0|,1596709979|2136.1|2136.75|2136|2136.35|13413|0|,1596710039|2136.2|2137.7|2136.2|2137|34790|0|,1596710099|2137.4|2137.7|2136.5|2137.35|20750|0|,1596710159|2137.35|2137.8|2137|2137.5|16019|0|,1596710219|2137.5|2143.3|2137.5|2142.45|232961|0|,1596710279|2142.65|2145|2142.65|2143.9|126023|0|,1596710339|2143.9|2144|2141.5|2142|68661|0|,1596710399|2142|2146.7|2142|2145.05|119195|0|,1596710459|2145.05|2148|2145.05|2148|95935|0|,1596710519|2148|2148|2145.1|2145.5|77178|0|,1596710579|2145.5|2145.9|2142.5|2144.5|81965|0|,1596710639|2144.5|2145|2143.3|2143.85|39196|0|,1596710699|2143.7|2144.75|2143.55|2143.75|36413|0|,1596710759|2143.75|2144|2139.65|2141.65|80137|0|,1596710819|2142|2142.75|2141.1|2142.1|28117|0|,1596710879|2142.1|2142.55|2141.55|2141.95|19555|0|,1596710939|2142.2|2142.65|2141.5|2142.1|25508|0|,1596710999|2142.1|2142.65|2141.7|2142.5|17036|0|,1596711059|2142.15|2142.65|2142|2142.15|16812|0|,1596711119|2142.9|2146.55|2142.8|2146|58214|0|,1596711179|2146.3|2146.3|2145.1|2146|34974|0|,1596711239|2146|2146|2144.05|2144.9|30442|0|,1596711299|2144.9|2145|2143.8|2144|19818|0|,1596711359|2144.7|2145.85|2144|2145.7|34491|0|,1596711419|2145.55|2149|2145.05|2147|96612|0|,1596711479|2147.4|2150|2147.4|2148.95|93376|0|,1596711539|2148.9|2148.9|2146.3|2147.05|47742|0|,1596711599|2147|2147.5|2145.45|2145.6|39840|0|,1596711659|2145.55|2145.9|2142.95|2144.3|53628|0|,1596711719|2144.7|2145|2144|2144.35|21844|0|,1596711779|2144.4|2146.9|2144.35|2146.45|22278|0|,1596711839|2146.6|2148|2146.2|2147.8|25447|0|,1596711899|2147.1|2147.95|2144.15|2144.95|35874|0|,1596711959|2144.95|2146|2144.4|2145.4|19677|0|,1596712019|2145.4|2145.4|2137.4|2140.3|117194|0|,1596712079|2139|2139.35|2137.05|2137.95|74070|0|,1596712139|2138|2138|2132.7|2137.35|151772|0|,1596712199|2137.5|2139.8|2136|2139|50955|0|,1596712259|2139.4|2139.6|2137|2138|43808|0|,1596712319|2137.65|2138|2137|2137.35|21182|0|,1596712379|2137.35|2138.25|2137.1|2138.25|25766|0|,1596712439|2138.3|2141.5|2138.3|2140.05|41322|0|,1596712499|2140.05|2140.8|2138.55|2139.55|24723|0|,1596712559|2139.55|2140|2138.6|2139.5|15860|0|,1596712619|2139.5|2139.9|2139|2139.8|13103|0|,1596712679|2139.8|2142.75|2139.7|2141.55|36337|0|,1596712739|2141.55|2142.8|2141.05|2141.8|21929|0|,1596712799|2141.05|2142|2141.05|2141.65|12667|0|,1596712859|2141.75|2141.75|2140|2140.6|23265|0|,1596712919|2140.55|2141|2140.1|2140.5|9388|0|,1596712979|2140.05|2141.5|2140|2140.5|12554|0|,1596713039|2140.85|2141.5|2140.45|2140.85|6823|0|,1596713099|2140.7|2141.5|2140.2|2141.2|14417|0|,1596713159|2141.5|2143.8|2141.2|2143.8|25280|0|,1596713219|2143.65|2144.35|2143.35|2143.55|25032|0|,1596713279|2143.55|2143.95|2142.85|2142.85|10739|0|,1596713339|2142.65|2143.2|2141|2142.7|25732|0|,1596713399|2142.2|2143|2140.5|2141.4|20552|0|,1596713459|2142.55|2142.95|2141.75|2142.15|10138|0|,1596713519|2141.85|2143|2141.8|2142.5|12199|0|,1596713579|2142.5|2144|2142.5|2144|18769|0|,1596713639|2143.75|2144|2143.5|2143.55|10786|0|,1596713699|2143.55|2144|2143.5|2143.5|12588|0|,1596713759|2143.5|2143.9|2143.05|2143.65|8918|0|,1596713819|2143.65|2143.65|2141|2141.6|15664|0|,1596713879|2141.5|2141.65|2141|2141.1|10750|0|,1596713939|2141.1|2141.5|2140.2|2140.65|16562|0|,1596713999|2140.65|2141|2140.5|2140.55|9964|0|,1596714059|2140.9|2141|2140.2|2140.25|10948|0|,1596714119|2140.55|2140.55|2137.3|2137.7|56173|0|,1596714179|2138|2140|2137.6|2139.4|18288|0|,1596714238|2139.7|2141|2139.1|2141|13055|0|,1596714299|2140.7|2141|2140|2140.1|11382|0|,1596714359|2140|2141|2137.85|2139.45|24315|0|,1596714419|2139.5|2140|2138|2138.65|10489|0|,1596714479|2138.8|2140|2138.25|2139.9|11624|0|,1596714539|2139.9|2141|2139.15|2139.5|20466|0|,1596714599|2139|2140|2138.1|2138.8|12825|0|,1596714659|2139.25|2139.45|2138.1|2139.1|11221|0|,1596714719|2139.1|2139.8|2138.75|2139.5|7821|0|,1596714779|2139.3|2139.95|2139.3|2139.85|7770|0|,1596714839|2139.95|2140|2138.1|2138.25|14112|0|,1596714899|2138.4|2139.5|2138|2139|22099|0|,1596714959|2139|2139|2138.1|2138.6|8840|0|,1596715019|2138.6|2138.95|2137.85|2138|20035|0|,1596715079|2137.35|2141.4|2137|2141|67270|0|,1596715139|2141|2145|2141|2142.1|88415|0|,1596715199|2142.1|2143|2140.05|2142.5|19563|0|,1596715258|2142.5|2143|2141.05|2142|20090|0|,1596715319|2142.7|2143.5|2141.5|2142.6|52961|0|,1596715379|2142.9|2142.9|2133|2136.8|132258|0|,1596715439|2136|2140.55|2136|2140.1|47279|0|,1596715499|2140.05|2142.55|2139.65|2141.15|24655|0|,1596715559|2141.15|2141.9|2140.15|2140.4|23240|0|,1596715619|2141.2|2141.5|2139.05|2139.9|26947|0|,1596715679|2139.9|2141.05|2139|2140.15|16181|0|,1596715739|2139.7|2140.7|2138.75|2139.15|17078|0|,1596715799|2138.75|2140.15|2138.75|2139.35|11010|0|,1596715859|2139.35|2140|2138.2|2138.2|12020|0|,1596715919|2138.6|2138.95|2135.25|2138|43040|0|,1596715979|2138|2139.5|2137.4|2139.25|16577|0|,1596716039|2138.4|2139.3|2136.7|2137.35|21462|0|,1596716099|2137.35|2138.95|2137.25|2138.05|13934|0|,1596716159|2138.9|2138.95|2138|2138.8|11907|0|,1596716219|2138.8|2140|2138.05|2139.9|23051|0|,1596716279|2140|2141.4|2139.3|2140.95|35510|0|,1596716339|2140.95|2142|2140|2141.6|19297|0|,1596716399|2141.6|2143|2141.5|2143|40293|0|,1596716459|2142.35|2143|2141|2141.95|20935|0|,1596716519|2141.2|2142|2140.8|2141.8|10027|0|,1596716579|2141.8|2141.9|2141.45|2141.55|7053|0|,1596716639|2141.55|2141.75|2140.1|2141.5|21488|0|,1596716699|2141.45|2141.7|2140|2140.75|14242|0|,1596716759|2140.15|2145|2140.15|2145|74951|0|,1596716819|2145|2147.75|2144.55|2147.7|77583|0|,1596716879|2146.85|2149.9|2146.3|2148.5|95825|0|,1596716939|2148.9|2150|2146.2|2147.55|88723|0|,1596716999|2147.55|2148|2143|2146.1|54829|0|,1596717059|2146.1|2147|2145.25|2145.9|27128|0|,1596717119|2145.75|2146.35|2144|2144.2|26532|0|,1596717179|2144|2144.2|2142|2143.15|33417|0|,1596717239|2143|2144.55|2142.7|2144|18223|0|,1596717298|2144.35|2144.7|2143.5|2143.65|15590|0|,1596717359|2143.65|2144|2142.1|2142.15|28512|0|,1596717419|2142.15|2142.85|2140.35|2142|48227|0|,1596717479|2141.55|2143.15|2141.55|2142.8|14756|0|,1596717539|2142.8|2144.95|2142.4|2144.9|15011|0|,1596717599|2145|2145.9|2144.5|2145.9|23084|0|,1596717659|2145.4|2146.5|2145|2146.5|24344|0|,1596717719|2146.15|2147|2145.4|2145.8|25855|0|,1596717779|2145.95|2146.1|2145|2145.7|23632|0|,1596717839|2145.65|2147.15|2145.65|2146.2|38464|0|,1596717899|2146.1|2146.2|2145|2145.35|18876|0|,1596717959|2145.35|2147.6|2145.35|2146.5|32109|0|,1596718019|2146.85|2147.75|2146.2|2146.65|26254|0|,1596718079|2146.5|2147.3|2144|2145|36351|0|,1596718139|2145|2145|2142.5|2142.95|24626|0|,1596718199|2142.95|2145|2142.75|2144.8|17765|0|,1596718259|2144.8|2147|2144.5|2145.9|22516|0|,1596718319|2146.6|2146.7|2145|2145|17401|0|,1596718379|2145|2145.65|2144.9|2145|12712|0|,1596718439|2145|2146.2|2145|2145.9|19806|0|,1596718499|2145.85|2147|2145.6|2147|25627|0|,1596718559|2147|2149.75|2146.8|2149.75|98534|0|,1596718619|2149.55|2154.8|2149.15|2152.9|248052|0|,1596718679|2152.85|2159.05|2152.85|2155.8|288260|0|,1596718739|2155.7|2156.5|2152|2152.1|108160|0|,1596718799|2152|2154.95|2151.15|2154.95|61085|0|,1596718859|2154.4|2158.5|2154.4|2157.5|106819|0|,1596718919|2158.25|2164|2157.6|2163.8|278096|0|,1596718979|2163.85|2167.6|2162.15|2165.8|240259|0|,1596719039|2165.3|2166.2|2163|2164.85|154016|0|,1596719099|2164.05|2165.55|2160.75|2162.75|105654|0|,1596719159|2162.45|2163.6|2162|2163|63962|0|,1596719219|2163.4|2163.4|2159.3|2159.3|119178|0|,1596719279|2159.55|2159.6|2153.6|2157|161034|0|,1596719339|2157.05|2161|2156.85|2160.45|95507|0|,1596719399|2160.9|2162|2156.65|2157.25|105646|0|,1596719458|2158.2|2160.9|2157.65|2159.9|35056|0|,1596719519|2159.1|2159.85|2158|2159|38135|0|,1596719578|2159|2160|2159|2159.35|25109|0|,1596719639|2159.35|2162|2159|2161.7|70835|0|,1596719699|2161.7|2163.65|2161.1|2163.65|51122|0|,1596719759|2162.95|2165|2162.95|2164.2|101949|0|,1596719819|2164.35|2165.65|2161|2163.25|99894|0|,1596719879|2162.4|2163.35|2161.1|2162.1|78089|0|,1596719939|2161.95|2161.95|2158|2159.05|86448|0|,1596719999|2159.05|2163|2158.7|2162.6|39353|0|,1596720059|2162.9|2162.9|2160.4|2160.6|58133|0|,1596720119|2160.4|2160.85|2159.15|2160.6|71041|0|,1596720179|2160.3|2160.9|2156.25|2156.3|77670|0|,1596720239|2156.6|2157.55|2155.2|2155.2|65715|0|,1596720299|2155.25|2155.85|2151.5|2154.35|1260011|0|,1596720359|2154.35|2157.5|2154.05|2156|62040|0|,1596720419|2156|2157|2153.4|2154|46965|0|,1596720479|2154.5|2155|2153|2155|28863|0|,1596720539|2155|2155|2153|2153|30581|0|,1596720599|2153|2153.3|2145|2145|176504|0|,1596720659|2145.65|2152.55|2144.6|2151.2|125693|0|,1596720719|2152.4|2153.05|2148|2148|84443|0|,1596720779|2148.1|2149.9|2146.5|2148|59070|0|,1596720838|2147.15|2148.75|2146.6|2147.2|61046|0|,1596720899|2147.5|2147.55|2143.25|2143.75|92989|0|,1596720959|2143.85|2147.9|2143.4|2147.35|58443|0|,1596721019|2147.6|2149|2146.7|2149|41738|0|,1596721079|2149|2151|2148.65|2148.65|51385|0|,1596721139|2148.05|2150.8|2148.05|2150.65|33909|0|,1596721199|2150.65|2150.9|2147.1|2148.6|38718|0|,1596721259|2148|2148.7|2145.65|2148.25|32322|0|,1596721319|2147.3|2148.25|2146.75|2147|11568|0|,1596721378|2147|2149|2146.6|2148.2|17079|0|,1596721439|2148.7|2148.7|2146.2|2147.65|17967|0|,1596721499|2147|2147.85|2146.3|2146.95|14554|0|,1596721559|2146.35|2147|2142.1|2143.2|111683|0|,1596721619|2143.65|2145|2142.5|2145|40453|0|,1596721679|2145|2147|2144.2|2146.25|40968|0|,1596721739|2147|2147|2145.95|2146.5|17102|0|,1596721799|2146.8|2146.8|2145|2145|21912|0|,1596721859|2145.4|2145.4|2142|2142.9|55987|0|,1596721919|2142|2143.15|2137|2139|165635|0|,1596721979|2139|2142.5|2138.15|2142|75266|0|,1596722039|2142.8|2143|2142.05|2142.2|30928|0|,1596722099|2142.25|2143.95|2142|2143.75|30412|0|,1596722159|2143.5|2143.95|2142.05|2143.95|22322|0|,1596722219|2143.9|2145|2143|2143.7|37918|0|,1596722279|2143.5|2143.9|2143|2143.8|13637|0|,1596722338|2143.5|2145.9|2143.35|2145.9|37805|0|,1596722399|2146|2147.8|2145.25|2147.1|43253|0|,1596722459|2147.45|2147.95|2144.15|2145|54914|0|,1596722519|2145|2146.35|2144.5|2146.1|21501|0|,1596722579|2146|2147.1|2146|2147.1|22212|0|,1596722639|2147|2147.1|2146|2146.35|17111|0|,1596722699|2146.35|2147|2142.45|2143.25|28584|0|,1596722759|2143.2|2143.9|2142|2142.9|32779|0|,1596722818|2142.6|2144|2142.25|2143.75|11638|0|,1596722879|2143.75|2144|2142.05|2143.65|20586|0|,1596722939|2143.65|2143.65|2142.3|2143.2|11374|0|,1596722998|2143.2|2143.6|2141|2142.1|38665|0|,1596723059|2142.1|2142.15|2138.1|2139.5|58832|0|,1596723119|2139.75|2141.55|2138.35|2140.45|45481|0|,1596723179|2140.7|2141.35|2140|2140|19764|0|,1596723239|2140|2141.25|2140|2141|26202|0|,1596723298|2141.25|2143.05|2141|2142.35|36345|0|,1596723359|2141.4|2141.75|2139.2|2139.2|25072|0|,1596723419|2139.2|2140|2138|2138|60671|0|,1596723479|2138.4|2138.4|2135.45|2136.65|111186|0|,1596723539|2136.7|2137|2135.25|2135.7|64914|0|,1596723599|2136.45|2136.8|2133|2133.6|96229|0|,1596723659|2133|2133|2125.95|2127.7|296674|0|,1596723719|2128.5|2132|2127.55|2130|115769|0|,1596723779|2129.45|2130.05|2127.25|2129|68535|0|,1596723839|2128.15|2131.25|2127.4|2130.4|87450|0|,1596723899|2131.6|2131.6|2127|2129.9|80360|0|,1596723959|2129.85|2131|2120.55|2122.4|191955|0|,1596724019|2122.4|2126.4|2120|2124.85|147110|0|,1596724079|2126.05|2128|2125.15|2126.6|76279|0|,1596724139|2126.5|2127.65|2122.8|2124.95|64752|0|,1596724199|2125|2127.95|2124.2|2127.2|51404|0|,1596724259|2127.2|2127.7|2123.4|2123.7|57457|0|,1596724319|2124|2126|2123.4|2125|43942|0|,1596724379|2125.9|2127.55|2125|2127.4|41043|0|,1596724439|2127.55|2131.5|2127|2129.3|107674|0|,1596724499|2130.45|2132|2130|2130.1|86217|0|,1596724559|2130.05|2132|2130|2131.8|49554|0|,1596724619|2131.5|2134.95|2131.2|2133.7|70925|0|,1596724679|2133.7|2134|2130.75|2132|53169|0|,1596724739|2132.5|2133|2131.6|2132.25|31561|0|,1596724799|2132|2133.85|2132|2133.3|36695|0|,1596724859|2133.3|2138.45|2133.3|2136.05|135284|0|,1596724919|2136.2|2136.65|2134.55|2135|45935|0|,1596724979|2135|2137.2|2135|2137.2|37951|0|,1596725039|2136.6|2137.2|2135|2135.5|35140|0|,1596725099|2135.6|2135.6|2132.25|2133|60378|0|,1596725159|2133|2133|2128|2130|89234|0|,1596725219|2130|2130|2128.8|2129.7|32350|0|,1596725279|2129.85|2131.85|2129.8|2131.25|31105|0|,1596725339|2131.25|2131.4|2129.5|2129.9|35106|0|,1596725399|2130.2|2130.55|2129.5|2129.65|27231|0|,1596725459|2130|2131.65|2129.5|2131.65|24797|0|,1596725519|2131.65|2134.5|2131.45|2133.5|44316|0|,1596725579|2133.9|2134|2133.05|2133.05|17105|0|,1596725639|2133.05|2133.6|2130.05|2130.8|27610|0|,1596725699|2130.8|2132|2130.8|2131.55|15378|0|,1596725759|2131.9|2132|2130|2130.55|36943|0|,1596725819|2130.85|2130.85|2129|2130|30786|0|,1596725879|2129.1|2130|2129|2130|28200|0|,1596725939|2130|2130|2129.6|2129.6|23184|0|,1596725999|2129.95|2130|2124.95|2127|87299|0|,1596726059|2127.25|2130|2127|2128.55|41125|0|,1596726119|2128.5|2134|2128.5|2132.55|129720|0|,1596726179|2132.55|2134|2131.55|2133.8|61821|0|,1596726238|2133.5|2135|2133.5|2135|69901|0|,1596726299|2134.3|2134.95|2132.1|2133.45|50975|0|,1596726359|2133|2134.8|2133|2133|48427|0|,1596726419|2133|2134|2131.7|2134|44059|0|,1596726479|2133.85|2133.85|2132.35|2132.8|26562|0|,1596726539|2132.8|2133.95|2132.45|2133.6|40601|0|,1596726598|2133.4|2135|2132|2132.05|87599|0|,1596726659|2132.6|2134.8|2131.25|2134.5|145796|0|,1596726719|2134.15|2134.7|2132|2133.75|71584|0|,1596726779|2133.9|2134.15|2133|2134|36002|0|,1596726839|2134.15|2134.15|2133.7|2133.95|22315|0|,1596726899|2133.95|2133.95|2132|2132.5|51302|0|,1596726959|2133.5|2134|2132.2|2132.9|71335|0|,1596727019|2133.3|2133.8|2130.55|2132.65|164131|0|,1596727079|2133.5|2134|2131.65|2133.95|91848|0|,1596727139|2134.1|2135.95|2133.1|2135.95|57257|0|,1596727199|2135.95|2139.15|2135.95|2138|111406|0|,1596727259|2138|2138|2136|2136.4|53661|0|,1596727319|2136.2|2139.85|2131.65|2134.95|163022|0|,1596727379|2135.1|2136.65|2134.8|2136.4|25660|0|,1596727439|2136.4|2137.8|2135.7|2137.45|37727|0|,1596727499|2137.45|2138.85|2136.8|2138.7|24474|0|,1596727559|2138.8|2139|2137|2137.7|25713|0|,1596727619|2137.65|2137.9|2136|2136.3|22532|0|,1596727679|2136.8|2137.45|2136.2|2137|24943|0|,1596727739|2136.9|2138|2136.45|2137.1|43103|0|,1596727799|2137.1|2138|2134.1|2134.1|30055|0|";

            string[] lines = data.Split(',');
            foreach (var line in lines)
            {
                string[] quotes = line.Split('|');

                if (quotes.Length < 7)
                    continue;

                if (!long.TryParse(quotes[0], NumberStyles.Any, CultureInfo.InvariantCulture, out long time))
                    continue;

                if (!double.TryParse(quotes[1], NumberStyles.Any, CultureInfo.InvariantCulture, out double open))
                    continue;

                if (!double.TryParse(quotes[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double high))
                    continue;

                if (!double.TryParse(quotes[3], NumberStyles.Any, CultureInfo.InvariantCulture, out double low))
                    continue;

                if (!double.TryParse(quotes[4], NumberStyles.Any, CultureInfo.InvariantCulture, out double close))
                    continue;

                if (!long.TryParse(quotes[5], NumberStyles.Any, CultureInfo.InvariantCulture, out long volume))
                    continue;

                if (!long.TryParse(quotes[6], NumberStyles.Any, CultureInfo.InvariantCulture, out long oi))
                    continue;

                DateTime timeStamp = Globals.ToDateTime(time, (int)ExchangeSegment.BSECM);

                tmp.Add(new OHLC() { TimeStamp = timeStamp, Open = open, High = high, Low = low, Close = close, Volume = volume, OI = oi });

            }



        }

        private async void Button_IndexList(object sender, RoutedEventArgs e)
        {
            if (this.marketData == null)
                return;

            var result = await this.marketData.GetIndexListAsync(ExchangeSegment.NSECM);
        }


        private async void Button_InstrumentDump(object sender, RoutedEventArgs e)
        {
            //XTSBase xtsBase = this.interactive == null ? this.marketData as XTSBase : this.interactive as XTSBase;
            //if (xtsBase == null)
            //    return;

            XTSBase xtsBase = new XTSInteractive(INTERACTIVE_URL);

            Log(XTSBase.IsDownloadingInstrumentDump.ToString());

            ContractInfo[] result = await xtsBase.DownloadInstrumentDumpAsync(new List<string>()
                {
                    "NSECM",
                    //"NSEFO", //takes time
                    //"NSECD"
                }, System.IO.Path.Combine(Directory.GetCurrentDirectory(), "dump.txt"));

            Log(XTSBase.IsDownloadingInstrumentDump.ToString());
            
        }

        

        #endregion

        #region API Events

        private void OnConnection(object sender, ConnectionEventArgs e)
        {
            Log($"OnConnection : {e.ConnectionState} = {e.Data}");
        }

        private void OnException(object sender, XTSAPI.ExceptionEventArgs e)
        {
            Log($"OnException => {e.Exception.Message}");
        }

        private void OnJson(object sender, JsonEventArgs e)
        {
            Log($"OnJson : {e.Json}");
        }

        private void OnInteractive(object sender, InteractiveEventArgs e)
        {
            Log($"OnInteractive : {e.InteractiveMessageType} : {e.Data}");
        }

        private void OnMarketData(object sender, MarketDataEventArgs e)
        {
            Log($"==> {e.SourceData}");
        }





        #endregion

        BracketXTSOrderResult bo = null;

        private async void Button_PlaceBO(object sender, RoutedEventArgs e)
        {
            if (this.interactive == null)
                return;

            var payload = new BracketOrderPayload()
            {
                clientID = USER_ID,
                disclosedQuantity = 0,
                exchangeInstrumentID = 26001,
                exchangeSegment = ExchangeSegment.NSECM.ToString(),
                limitPrice = 1980,
                stopPrice = 1979,
                orderQuantity = 1,
                orderSide = "BUY",
                orderType = "StopMarket",
                stopLossPrice = 10.50d,
                squarOff = 9.5d,
                trailingStoploss = 0,
                orderUniqueIdentifier = GenerateOrderTag()
            };

            this.bo = await this.interactive.PlaceBracketOrderAsync(payload);


        }

        private async void Button_ModifyBO(object sender, RoutedEventArgs e)
        {
            if (this.interactive == null)
                return;

            //if (this.bo == null)
            //    return;

            var payload = new ModifyBOOrderPayload()
            {
                appOrderID = this.bo.OrderID,
                limitPrice = 1900,
                stopLossPrice = 10.0d,
                orderQuantity = 2
            };

            await this.interactive.ModifyBOOrderAsync(payload);
        }

        private async void Button_ExitBO(object sender, RoutedEventArgs e)
        {
            if (this.interactive == null)
                return;

            //if (this.bo == null)
            //    return;

            await this.interactive.CancelBOOrderAsync(USER_ID, this.bo.OrderID);
        }
    }
}
