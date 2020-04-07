/*
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace XTSAPI.MarketData
{

    public class XTSMarketData : XTSBase
    {

        public XTSMarketData(string baseAddress)
            : base(baseAddress)
        { }
           

        
        public override async Task<T> LoginAsync<T>(string appKey, string secretKey, string source = "WebAPI")
        {
            var payload = new LoginPayload()
            {
                appKey = appKey,
                secretKey = secretKey,
                source = source
            };

            T response = await Query<T>(HttpMethodType.POST, Url.Login(), payload: payload).ConfigureAwait(false);

            if (response != null && !string.IsNullOrEmpty(response.token))
            {
                this.HttpClient.DefaultRequestHeaders.Add("authorization", response.token);
                this.Token = response.token;
                this.UserId = response.userID;

                return response;
            }
            else
            {
                this.Token = string.Empty;
                return null;
            }

        }

        /// <summary>
        /// Connect to socket
        /// </summary>
        /// <param name="marketDataPorts">Market data ports</param>
        /// <param name="publishFormatType">Publish format type</param>
        /// <param name="publishFormat">Publish format <see cref="PublishFormat"/> </param>
        /// <param name="broadcastMode">Broadcast mode <see cref="BroadCastMode"/> </param>
        /// <param name="source">Source <see cref="OrderSource"/> </param>
        /// <returns></returns>
        public bool ConnectToSocket(MarketDataPorts[] marketDataPorts, PublishFormat publishFormat = PublishFormat.JSON, BroadcastMode broadcastMode = BroadcastMode.Partial, string source = "WebAPI")
        {
            if (marketDataPorts == null || marketDataPorts.Length == 0)
                return false;

            if (string.IsNullOrEmpty(this.Token))
                return false;

            if (string.IsNullOrWhiteSpace(base.UserId))
                return false;

            HttpClient httpClient = this.HttpClient;

            if (httpClient == null && httpClient.BaseAddress == null)
                return false;

            //Connect to the socket
            Quobject.SocketIoClientDotNet.Client.IO.Options options = new Quobject.SocketIoClientDotNet.Client.IO.Options()
            {
                IgnoreServerCertificateValidation = true,
                Path = "/marketdata/socket.io",
                Query = new Dictionary<string, string>()
                    {
                        { "token", this.Token },
                        { "userID", base.UserId },
                        { "source", string.IsNullOrEmpty(source) ? OrderSource.WebAPI : source },
                        { "publishFormat", publishFormat.ToString() },
                        { "broadcastMode", broadcastMode.ToString() }
                    }
            };

            this.Socket = IO.Socket(httpClient.BaseAddress, options);

            //subscribe to the base methods
            if (!base.SubscribeToConnectionEvents())
                return false;

            string format = publishFormat.ToString().ToLower();
            string mode = broadcastMode.ToString().ToLower();

            if (Array.IndexOf<MarketDataPorts>(marketDataPorts, MarketDataPorts.touchlineEvent) >= 0)
            {
                this.Socket.On($"1501-{format}-{mode}", (data) =>
                {
                    OnData<Touchline>(MarketDataPorts.touchlineEvent, data, publishFormat, broadcastMode);
                });
            }

            if (Array.IndexOf<MarketDataPorts>(marketDataPorts, MarketDataPorts.marketDepthEvent) >= 0)
            {
                this.Socket.On($"1502-{format}-{mode}", (data) =>
                {
                    OnData<MarketDepth>(MarketDataPorts.marketDepthEvent, data, publishFormat, broadcastMode);
                });
            }

            /*
            this.Socket.On($"1503-{publishFormat}", (data) =>
            {
                OnData<>(MarketDataPorts.topGainerLosserEvent, data);
            });
            */

            if (Array.IndexOf<MarketDataPorts>(marketDataPorts, MarketDataPorts.indexDataEvent) >= 0)
            {
                this.Socket.On($"1504-{format}-{mode}", (data) =>
                {
                    OnData<Indices>(MarketDataPorts.indexDataEvent, data, publishFormat, broadcastMode);
                });
            }

            if (Array.IndexOf<MarketDataPorts>(marketDataPorts, MarketDataPorts.candleDataEvent) >= 0)
            {
                this.Socket.On($"1505-{format}-{mode}", (data) =>
                {
                    OnData<Candle>(MarketDataPorts.candleDataEvent, data, publishFormat, broadcastMode);
                });
            }

            /*

            this.Socket.On($"1506-{publishFormat}", (data) =>
            {
                OnJsonData(MarketDataPorts.generalMessageBroadcastEvent, data);
            });

            this.Socket.On($"1507-{publishFormat}", (data) =>
            {
                OnJsonData(MarketDataPorts.exchangeTradingStatusEvent, data);
            });
            */

            if (Array.IndexOf<MarketDataPorts>(marketDataPorts, MarketDataPorts.openInterestEvent) >= 0)
            {
                this.Socket.On($"1510-{format}-{mode}", (data) =>
                {
                    OnData<OI>(MarketDataPorts.openInterestEvent, data, publishFormat, broadcastMode );
                });
            }

            /*

            this.Socket.On($"5505-{publishFormat}", (data) =>
            {
                OnJsonData(MarketDataPorts.instrumentSubscriptionInfo, data);
            });

            this.Socket.On($"5018-{publishFormat}", (data) =>
            {
                OnJsonData(MarketDataPorts.marketDepthEvent100, data);
            });
            */

            return true;
        }



        private void OnData<T>(MarketDataPorts port, object data, PublishFormat publishFormat, BroadcastMode broadcastMode) where T : ListQuotesBase, new()
        {
            if (data == null)
                return;
            
            try
            {
                T quote = null;

                if (broadcastMode == BroadcastMode.Partial)
                {
                    quote = new T();
                    quote.AssignValue(data);
                }
                else
                {
                    string str = data.ToString();
                    if (string.IsNullOrEmpty(str))
                        return;

                    quote = ParseString<T>(str, triggerJsonEvent: false);
                }

                if (quote == null)
                    return;

                OnMarketData(port, quote, data);
            }
            catch (Exception ex)
            {
                OnException(typeof(T), ex);
            }
            
        }

        
        /// <summary>
        /// Returns the client config
        /// </summary>
        /// <returns></returns>
        public async Task<ClientConfigResult> GetConfigAsync()
        {
            return await Query<ClientConfigResult>(HttpMethodType.GET, Url.ClientConfig()).ConfigureAwait(false);
        }

        /// <summary>
        /// Search string
        /// </summary>
        /// <param name="searchString">String to search</param>
        /// <param name="source">Source <see cref="OrderSource"/> </param>
        /// <returns></returns>
        public async Task<SearchByStringResult[]> SearchByStringAsync(string searchString, string source = "WEB")
        {
            return await Query<SearchByStringResult[]>(HttpMethodType.GET, Url.SearchByString(searchString,source: source)).ConfigureAwait(false);
        }

        /// <summary>
        /// Search instrument by id
        /// </summary>
        /// <param name="instruments">Instruments</param>
        /// <param name="source">Source <see cref="OrderSource"/></param>
        /// <returns></returns>
        public async Task<SearchByIdResult[]> SearchByIdAsync(List<Instruments> instruments, string source = "WebAPI")
        {
            if (string.IsNullOrWhiteSpace(base.UserId))
                return null;

            SeachByIdPayload payload = new SeachByIdPayload()
            {
                instruments = instruments,
                source = source
            };

            return await Query<SearchByIdResult[]>(HttpMethodType.POST, Url.SearchInstrumentsById(), payload: payload).ConfigureAwait(false);
        }
    
        
        private async Task<QuoteResult<T>> GetQuotesAsync<T>(HttpMethodType methodType, string url, Payload payload) where T : ListQuotesBase
        {
            return await Query<QuoteResult<T>>(methodType, url, payload: payload).ConfigureAwait(false);
        }

        /// <summary>
        /// Get quotes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="clientId">Clietn id</param>
        /// <param name="marketDataPorts">Market data port <see cref="MarketDataPorts"/></param>
        /// <param name="instruments">Instruments</param>
        /// <param name="publishFormat">Publish format <see cref="PublishFormat"/></param>
        /// <param name="source">Source = <see cref="OrderSource"/></param>
        /// <returns></returns>
        public async Task<QuoteResult<T>> GetQuotesAsync<T>(string clientId,  int xtsMessageCode, List<Instruments> instruments) where T : ListQuotesBase
        {
            QuotePayload payload = new QuotePayload()
            {
                instruments = instruments,
                xtsMessageCode = xtsMessageCode,
                publishFormat = PublishFormat.JSON.ToString()
            };

            return await GetQuotesAsync<T>(HttpMethodType.POST, Url.Quotes(), payload).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to quotes stream
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="clientId">Client id</param>
        /// <param name="marketDataPort">Market data port <see cref="MarketDataPorts"/></param>
        /// <param name="instruments">Instrument</param>
        /// <param name="source">Source <see cref="OrderSource"/></param>
        /// <returns></returns>
        public async Task<QuoteResult<T>> SubscribeAsync<T>(string clientId, int xtsMessageCode, List<Instruments> instruments) where T : ListQuotesBase
        {
            SubscriptionPayload payload = new SubscriptionPayload()
            {
                instruments = instruments,
                xtsMessageCode = xtsMessageCode
            };

            return await GetQuotesAsync<T>(HttpMethodType.POST, Url.Subscription(), payload).ConfigureAwait(false);
        }

        /// <summary>
        /// Unsubscribe from a quotes stream
        /// </summary>
        /// <param name="clientId">Client id</param>
        /// <param name="marketDataPort">Market data port <see cref="MarketDataPorts"/></param>
        /// <param name="instruments">Instruments</param>
        /// <param name="source">Source <see cref="OrderSource"/></param>
        /// <returns></returns>
        public async Task<UnsubscriptionResult> UnsubscribeAsync(string clientId, int xtsMessageCode, List<Instruments> instruments)
        {
            SubscriptionPayload payload = new SubscriptionPayload()
            {
                instruments = instruments,
                xtsMessageCode = xtsMessageCode
            };

            return await Query<UnsubscriptionResult>(HttpMethodType.PUT, Url.Subscription(), payload: payload).ConfigureAwait(false);
        }


        public async Task<string> GetOHLCHistoryAsync(ExchangeSegment exchangeSegment, long exchangeInstrumentId, DateTime startTime, DateTime endTime, int dataType)
        {
            return await Query<string>(HttpMethodType.GET, Url.OHLCHistory(exchangeSegment, exchangeInstrumentId, startTime, endTime, dataType)).ConfigureAwait(false);
        }


        public async Task<IndexList> GetIndexListAsync(ExchangeSegment exchangeSegment)
        {
            return await Query<IndexList>(HttpMethodType.GET, Url.IndexList(exchangeSegment)).ConfigureAwait(false);
        }

        public async Task<string> GetSeriesAsync(ExchangeSegment exchangeSegment)
        {
            return await Query<string>(HttpMethodType.GET, Url.Series(exchangeSegment)).ConfigureAwait(false);
        }

        public async Task<string> GetEquitySymbolAsync(ExchangeSegment exchangeSegment, string symbol)
        {
            return await Query<string>(HttpMethodType.GET, Url.EquitySymbol(exchangeSegment, symbol)).ConfigureAwait(false);
        }

        public async Task<string> GetFuturesSymbolAsync(ExchangeSegment exchangeSegment, string series, string symbol, DateTime expiryDate)
        {
            return await Query<string>(HttpMethodType.GET, Url.FuturesSymbol(exchangeSegment, series, symbol, expiryDate)).ConfigureAwait(false);
        }

        public async Task<string> GetExpiryDateAsync(ExchangeSegment exchangeSegment, string series, string symbol)
        {
            return await Query<string>(HttpMethodType.GET, Url.ExpiryDate(exchangeSegment, series, symbol)).ConfigureAwait(false);
        }

        public async Task<string> GetOptionSymbolAsync(ExchangeSegment exchangeSegment, string series, string symbol, DateTime expiryDate, string optionType, double strikePrice)
        {
            return await Query<string>(HttpMethodType.GET, Url.OptionSymbol(exchangeSegment, series, symbol, expiryDate, optionType, strikePrice)).ConfigureAwait(false);
        }

        public async Task<string> GetOptionTypeAsync(ExchangeSegment exchangeSegment, string series, string symbol, DateTime expiryDate)
        {
            return await Query<string>(HttpMethodType.GET, Url.OptionType(exchangeSegment, series, symbol, expiryDate)).ConfigureAwait(false);
        }




        private void OnMarketData(MarketDataPorts port, ListQuotesBase data, object sourceData)
        {
            this.MarketData?.Invoke(null, new MarketDataEventArgs(port, data, sourceData));
        }

        /// <summary>
        /// Gets the market data events
        /// </summary>
        public event EventHandler<MarketDataEventArgs> MarketData;


    }
}
