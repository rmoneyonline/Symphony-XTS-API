/*
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using Quobject.SocketIoClientDotNet.Client;
using System;
using System.Collections.Generic;
using System.Globalization;
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

            T response = await Query<T>(HttpMethodType.POST, $"{base.PathAndQuery}/auth/login", payload: payload).ConfigureAwait(false);

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

            if (string.IsNullOrWhiteSpace(this.PathAndQuery))
                return false;

            HttpClient httpClient = this.HttpClient;

            if (httpClient == null && httpClient.BaseAddress == null)
                return false;

            //Connect to the socket
            Quobject.SocketIoClientDotNet.Client.IO.Options options = new Quobject.SocketIoClientDotNet.Client.IO.Options()
            {
                IgnoreServerCertificateValidation = true,
                Path = $"{this.PathAndQuery}/socket.io",
                Query = new Dictionary<string, string>()
                    {
                        { "token", this.Token },
                        { "userID", base.UserId },
                        { "source", string.IsNullOrEmpty(source) ? OrderSource.WebAPI : source },
                        { "publishFormat", publishFormat.ToString() },
                        { "broadcastMode", broadcastMode.ToString() }
                    },
                Timeout = 5000
                
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
            */

            /*
            this.Socket.On($"5018-{format}-{mode}", (data) =>
            {
                OnJson(null, data.ToString());
            });
            */

            return true;
        }


        public override async Task LogoutAsync()
        {
            this.Socket?.Disconnect();

            await Query<Response<string>>(HttpMethodType.DELETE, $"{this.PathAndQuery}/auth/logout").ConfigureAwait(false);

            this.HttpClient?.Dispose();
            this.HttpClient = null;
        }

        /// <summary>
        /// Returns the client config
        /// </summary>
        /// <returns></returns>
        public async Task<ClientConfigResult> GetConfigAsync()
        {
            return await Query<ClientConfigResult>(HttpMethodType.GET, $"{this.PathAndQuery}/config/clientConfig").ConfigureAwait(false);
        }


        private async Task<QuoteResult<T>> GetQuotesAsync<T>(HttpMethodType methodType, string url, Payload payload) where T : ListQuotesBase
        {
            return await Query<QuoteResult<T>>(methodType, url, payload: payload).ConfigureAwait(false);
        }

        /// <summary>
        /// Get quotes
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xtsMessageCode">Market data port <see cref="MarketDataPorts"/></param>
        /// <param name="instruments">Instruments</param>
        /// <returns></returns>
        public async Task<QuoteResult<T>> GetQuotesAsync<T>(int xtsMessageCode, List<Instruments> instruments) where T : ListQuotesBase
        {
            QuotePayload payload = new QuotePayload()
            {
                instruments = instruments,
                xtsMessageCode = xtsMessageCode,
                publishFormat = PublishFormat.JSON.ToString()
            };

            return await GetQuotesAsync<T>(HttpMethodType.POST, $"{this.PathAndQuery}/instruments/quotes", payload).ConfigureAwait(false);
        }

        /// <summary>
        /// Get Contract
        /// </summary>
        /// <param name="exchangeSegmentList">exchangeSegmet lis</param>
        /// <returns></returns>
        public async Task<ContractInfo> GetContractAsync<T>(List<string> exchangeSegmentList)
        {
            ContractRequestPayload payload = new ContractRequestPayload()
            {
                exchangeSegmentList = exchangeSegmentList
            };
            return await GetContractAsync<ContractInfo>(HttpMethodType.POST, $"{this.PathAndQuery}/instruments/master", payload).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute Contract request
        /// </summary>
        public async Task<ContractInfo> GetContractAsync<T>(HttpMethodType methodType, string url, Payload payload)
        {
            return await Query<ContractInfo>(methodType,url, payload : payload).ConfigureAwait(false);
        }

        /// <summary>
        /// Subscribe to quotes stream
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="xtsMessageCode">Market data port <see cref="MarketDataPorts"/></param>
        /// <param name="instruments">Instrument</param>
        /// <returns></returns>
        public async Task<QuoteResult<T>> SubscribeAsync<T>(int xtsMessageCode, List<Instruments> instruments) where T : ListQuotesBase
        {
            SubscriptionPayload payload = new SubscriptionPayload()
            {
                instruments = instruments,
                xtsMessageCode = xtsMessageCode
            };

            return await GetQuotesAsync<T>(HttpMethodType.POST, $"{this.PathAndQuery}/instruments/subscription", payload).ConfigureAwait(false);
        }

        /// <summary>
        /// Unsubscribe from a quotes stream
        /// </summary>
        /// <param name="xtsMessageCode">Market data port <see cref="MarketDataPorts"/></param>
        /// <param name="instruments">Instruments</param>
        /// <returns></returns>
        public async Task<UnsubscriptionResult> UnsubscribeAsync(int xtsMessageCode, List<Instruments> instruments)
        {
            SubscriptionPayload payload = new SubscriptionPayload()
            {
                instruments = instruments,
                xtsMessageCode = xtsMessageCode
            };

            return await Query<UnsubscriptionResult>(HttpMethodType.PUT, $"{this.PathAndQuery}/instruments/subscription", payload: payload).ConfigureAwait(false);
        }


        public async Task<string> GetSeriesAsync(ExchangeSegment exchangeSegment)
        {
            return await Query<string>(HttpMethodType.GET, $"{this.PathAndQuery}/instruments/instrument/series?exchangeSegment={(int)exchangeSegment}").ConfigureAwait(false);
        }

        public async Task<string> GetEquitySymbolAsync(ExchangeSegment exchangeSegment, string symbol)
        {
            return await Query<string>(HttpMethodType.GET, $"{this.PathAndQuery}/instruments/instrument/symbol?exchangeSegment={(int)exchangeSegment}&series=EQ&symbol={symbol}").ConfigureAwait(false);
        }


        public async Task<string> GetExpiryDateAsync(ExchangeSegment exchangeSegment, string series, string symbol)
        {
            return await Query<string>(HttpMethodType.GET, $"{this.PathAndQuery}/instruments/instrument/expiryDate?exchangeSegment={(int)exchangeSegment}&series={series}&symbol={symbol}").ConfigureAwait(false);
        }

        public async Task<string> GetFuturesSymbolAsync(ExchangeSegment exchangeSegment, string series, string symbol, DateTime expiryDate)
        {
            return await Query<string>(HttpMethodType.GET, string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}/instruments/instrument/futureSymbol?exchangeSegment={1}&series={2}&symbol={3}&expiryDate={4:ddMMMyyyy}", this.PathAndQuery, (int)exchangeSegment, series, symbol, expiryDate)).ConfigureAwait(false);
        }

        public async Task<string> GetOptionSymbolAsync(ExchangeSegment exchangeSegment, string series, string symbol, DateTime expiryDate, string optionType, double strikePrice)
        {
            return await Query<string>(HttpMethodType.GET, string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}/instruments/instrument/optionSymbol?exchangeSegment={1}&series={2}&symbol={3}&expiryDate={4:ddMMMyyyy}&optionType={5}&strikePrice={6}",
                this.PathAndQuery, (int)exchangeSegment, series, symbol, expiryDate, optionType, strikePrice)).ConfigureAwait(false);
        }

        public async Task<string> GetOptionTypeAsync(ExchangeSegment exchangeSegment, string series, string symbol, DateTime expiryDate)
        {
            return await Query<string>(HttpMethodType.GET, string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}/instruments/instrument/optionType?exchangeSegment={1}&series={2}&symbol={3}&expiryDate={4:ddMMMyyyy}",
                this.PathAndQuery, (int)exchangeSegment, series, symbol, expiryDate)).ConfigureAwait(false);
        }


        public async Task<IndexList> GetIndexListAsync(ExchangeSegment exchangeSegment)
        {
            return await Query<IndexList>(HttpMethodType.GET, $"{this.PathAndQuery}/instruments/indexlist/?exchangeSegment={(int)exchangeSegment}").ConfigureAwait(false);
        }

        /// <summary>
        /// Search instrument by id
        /// </summary>
        /// <param name="instruments">Instruments</param>
        /// <param name="source">Source <see cref="OrderSource"/></param>
        /// <returns></returns>
        public async Task<SearchByIdResult[]> SearchByIdAsync(List<Instruments> instruments, string source = "WebAPI", bool isTradeSymbol = false)
        {

            SeachByIdPayload payload = new SeachByIdPayload()
            {
                instruments = instruments,
                source = source,
                isTradeSymbol = isTradeSymbol
            };

            return await Query<SearchByIdResult[]>(HttpMethodType.POST, $"{this.PathAndQuery}/search/instrumentsbyid", payload: payload).ConfigureAwait(false);
        }


        /// <summary>
        /// Search string
        /// </summary>
        /// <param name="searchString">String to search</param>
        /// <param name="source">Source <see cref="OrderSource"/> </param>
        /// <returns></returns>
        public async Task<ContractInfo[]> SearchByStringAsync(string searchString, string source = "WEB")
        {
            return await Query<ContractInfo[]>(HttpMethodType.GET, $"{this.PathAndQuery}/search/instruments/?searchString={searchString}&source={source}").ConfigureAwait(false);
        }


        public async Task<OHLCResult> GetOHLCHistoryAsync(ExchangeSegment exchangeSegment, long exchangeInstrumentId, DateTime startTime, DateTime endTime, int compressionValue)
        {
            return await Query<OHLCResult>(HttpMethodType.GET, string.Format(CultureInfo.InvariantCulture, "{0}/instruments/ohlc?exchangeSegment={1}&exchangeInstrumentID={2}&startTime={3:MMM dd yyyy HHmmss}&endTime={4:MMM dd yyyy HHmmss}&compressionValue={5}",
                this.PathAndQuery, exchangeSegment.ToString(), exchangeInstrumentId, startTime, endTime, compressionValue)).ConfigureAwait(false);
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
