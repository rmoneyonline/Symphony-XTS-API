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
        const string URL = "url";
        const string USER_ID = "userId";
        const string PASSWORD = "password";
        const string MARKET_KEY = "marketKey";
        const string INTERACTIVE_KEY = "interactiveKey";
        

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


                this.interactive = new XTSInteractive(USER_ID, URL);
                this.interactive.Interactive += OnInteractive;
                this.interactive.Json += OnJson;
                this.interactive.Exception += OnException;
                this.interactive.ConnectionState += OnConnection;
                ;
                InteractiveLoginResult login = await interactive.LoginAsync<InteractiveLoginResult>(PASSWORD, INTERACTIVE_KEY);

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

        


        private async void Profile_Click(object sender, RoutedEventArgs e)
        {
            if (this.interactive == null)
                return;

            await this.interactive.GetProfileAsync();
        }

        private async void Balance_Click(object sender, RoutedEventArgs e)
        {
            if (this.interactive == null)
                return;

            await this.interactive.GetBalanceAsync();
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

            await this.interactive.GetHoldingsAsync();
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

            this.orderId = await this.interactive?.PlaceOrderAsync("NSECM", 2885, "BUY", "LIMIT", 1, 1100.0d, 0.0d, "MIS", "DAY", orderUniqueIdentifier: GenerateOrderTag());
        }

        private async void Button_Modify(object sender, RoutedEventArgs e)
        {
            if (this.orderId == null)
                return;

            var modify = await this.interactive?.ModifyOrderAsync(this.orderId.AppOrderID, "LIMIT", 1, 1101.0d, 0.0d, "MIS", "DAY", orderUniqueIdentifier: GenerateOrderTag());
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

        CoverOrderResult co;

        private async void Button_PlaceCO(object sender, RoutedEventArgs e)
        {

            var co = await this.interactive?.PlaceCoverOrderAsync("NSECM", 2885, "BUY", 1, 1000.0d, 0.0d, orderUniqueIdentifier: GenerateOrderTag());

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

        private async void Button_SquareOff(object sender, RoutedEventArgs e)
        {
            PositionResult[] positions = await this.interactive?.GetDayPositionAsync();
            if (positions == null || positions.Length == 0)
                return;

            await this.interactive?.SquareOff(positions[0].ExchangeSegment, positions[0].ExchangeInstrumentID, positions[0].ProductType,
                PositionSquareOffMode.DayWise, 100, PositionSquareOffQuantityType.Percentage);
        }

        private async void Button_ConvertPosition(object sender, RoutedEventArgs e)
        {
            TradeResult[] trades = await this.interactive?.GetTradesAsync();
            if (trades == null || trades.Length == 0)
                return;

            await this.interactive?.ConvertPositionAsync(trades[0].ExecutionID, trades[0].AppOrderID, "MIS", "CNC");
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


                this.marketData = new XTSMarketData(USER_ID, URL);
                this.marketData.MarketData += OnMarketData;
                this.marketData.ConnectionState += OnConnection;
                this.marketData.Json += OnJson;
                this.marketData.Exception += OnException;

                this.marketDataLogin = await this.marketData.LoginAsync<MarketDataLoginResult>(PASSWORD, MARKET_KEY);
                
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
            
            SearchByStringResult[] result = await this.marketData?.SearchByStringAsync("NIFTY50");
        }

        private async void Button_SearchById(object sender, RoutedEventArgs e)
        {
            SearchByIdResult[] result = await this.marketData?.SearchByIdAsync(new List<Instruments>()
                {
                    new Instruments()
                    {
                        exchangeInstrumentID = 2885,
                        exchangeSegment = (int)ExchangeSegment.NSECM
                    }
                });
        }

        private List<Instruments> GetInstruments(MarketDataPorts port)
        {
            int exchange = (int)ExchangeSegment.NSECM;
            long exchangeInstrumentId = 2885;   //reliance

            if (this.MarketDataPorts == MarketDataPorts.openInterestEvent)
            {
                exchange = (int)ExchangeSegment.NSEFO;
                exchangeInstrumentId = 44461; //nifty sep 19 fut
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
                case MarketDataPorts.topGainerLosserEvent:
                    break;
                case MarketDataPorts.indexDataEvent:
                    result = await Subscribe<Indices>();
                    break;
                case MarketDataPorts.candleDataEvent:
                    result = await Subscribe<Candle>();
                    break;
                case MarketDataPorts.generalMessageBroadcastEvent:
                    break;
                case MarketDataPorts.exchangeTradingStatusEvent:
                    break;
                case MarketDataPorts.openInterestEvent:
                    result = await Subscribe<OI>();
                    break;
                case MarketDataPorts.instrumentSubscriptionInfo:
                    break;
                case MarketDataPorts.marketDepthEvent100:
                    break;
                default:
                    break;
            }

        }

        private async Task<QuoteResult<T>> Subscribe<T>() where T : ListQuotesBase
        {
            if (this.marketData == null)
                return null;

            return await this.marketData.SubscribeAsync<T>(USER_ID, ((int)MarketDataPorts).ToString(), GetInstruments(this.MarketDataPorts)).ConfigureAwait(false);
        } 

        private async void Button_Unsubscribe(object sender, RoutedEventArgs e)
        {
            UnsubscriptionResult response = await this.marketData?.UnsubscribeAsync(USER_ID, ((int)this.MarketDataPorts).ToString(), GetInstruments(this.MarketDataPorts));
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
                case MarketDataPorts.topGainerLosserEvent:
                    break;
                case MarketDataPorts.indexDataEvent:
                    result = await GetQuotes<Indices>();
                    break;
                case MarketDataPorts.candleDataEvent:
                    result = await GetQuotes<Candle>();
                    break;
                case MarketDataPorts.generalMessageBroadcastEvent:
                    break;
                case MarketDataPorts.exchangeTradingStatusEvent:
                    break;
                case MarketDataPorts.openInterestEvent:
                    result = await GetQuotes<OI>();
                    break;
                case MarketDataPorts.instrumentSubscriptionInfo:
                    break;
                case MarketDataPorts.marketDepthEvent100:
                    break;
                default:
                    break;
            }

        }

        private async Task<QuoteResult<T>> GetQuotes<T>() where T : ListQuotesBase
        {
            if (this.marketData == null)
                return null;

            return await this.marketData.GetQuotesAsync<T>(USER_ID, ((int)this.MarketDataPorts).ToString(), GetInstruments(this.MarketDataPorts)).ConfigureAwait(false);
        }



        private async void Button_InstrumentDump(object sender, RoutedEventArgs e)
        {
           
            XTSBase xtsBase = this.interactive == null ? this.marketData as XTSBase : this.interactive as XTSBase;
            if (xtsBase == null)
                return;

            Log(XTSBase.IsDownloadingInstrumentDump.ToString());

            SearchByStringResult[] result = await xtsBase.DownloadInstrumentDumpAsync(new List<string>()
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

    }
}
