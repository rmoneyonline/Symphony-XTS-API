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

        public XTSMarketData(string userId, string baseAddress)
            : base(userId, baseAddress)
        { }
           

        
        /// <summary>
        /// Login to the XTS Market Data API
        /// </summary>
        /// <typeparam name="T"><see cref="MarketDataLoginResult"/></typeparam>
        /// <param name="password">Password</param>
        /// <param name="publicKey">Public Key</param>
        /// <param name="source">Source <see cref="OrderSource"/></param>
        /// <returns></returns>
        public override async Task<T> LoginAsync<T>(string password, string publicKey, string source = "WebAPI")
        {
            if (string.IsNullOrWhiteSpace(base.UserId))
                return null;

            var payload = new LoginPayload()
            {
                userID = base.UserId,
                password = password,
                publicKey = publicKey,
                source = source
            };

            T response = await Query<T>(HttpMethodType.POST, Url.Login(), payload: payload).ConfigureAwait(false);

            if (response != null && !string.IsNullOrEmpty(response.token))
            {
                this.HttpClient.DefaultRequestHeaders.Add("authorization", response.token);
                this.Token = response.token;
                
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
        /// Logout of the socket and close the http client
        /// </summary>
        /// <param name="source">Source <see cref="OrderSource"/></param>
        /// <returns></returns>
        public async Task LogoutAsync(string source = "WebAPI")
        {
            if (string.IsNullOrWhiteSpace(base.UserId))
                return;

            MarketDataPayload payload = new MarketDataPayload()
            {
                userID = base.UserId,
                source = source
            };

            this.Socket?.Disconnect();

            await Query<Response<string>>(HttpMethodType.POST, Url.Logout(), payload: payload).ConfigureAwait(false);

            this.HttpClient.Dispose();
            this.HttpClient = null;
        }


        /// <summary>
        /// Returns the client config
        /// </summary>
        /// <param name="source">Source <see cref="OrderSource"/></param>
        /// <returns></returns>
        public async Task<ClientConfigResult> GetConfigAsync(string source = "WebAPI")
        {
            if (string.IsNullOrWhiteSpace(base.UserId))
                return null;

            MarketDataPayload payload = new MarketDataPayload()
            { 
                userID = base.UserId,
                source = source
            };

            return await Query<ClientConfigResult>(HttpMethodType.POST, Url.ClientConfig(), payload: payload).ConfigureAwait(false);
        }

        /// <summary>
        /// Search string
        /// </summary>
        /// <param name="searchString">String to search</param>
        /// <param name="userId">User id</param>
        /// <param name="source">Source <see cref="OrderSource"/> </param>
        /// <returns></returns>
        public async Task<SearchByStringResult[]> SearchByStringAsync(string searchString, string userId = "guest", string source = "WEB")
        {

            return await Query<SearchByStringResult[]>(HttpMethodType.GET, Url.SearchByString(searchString, userId: userId, source: source)).ConfigureAwait(false);
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
                userID = base.UserId,
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
        public async Task<QuoteResult<T>> GetQuotesAsync<T>(string clientId,  string marketDataPorts, List<Instruments> instruments, string source = "WebAPI") where T : ListQuotesBase
        {
            QuotePayload payload = new QuotePayload()
            {
                clientID = clientId,
                userID = base.UserId,
                instruments = instruments,
                marketDataPort = marketDataPorts,
                publishFormat = PublishFormat.JSON.ToString(),
                source = source
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
        public async Task<QuoteResult<T>> SubscribeAsync<T>(string clientId, string marketDataPort, List<Instruments> instruments, 
            string source = "WebAPI") where T : ListQuotesBase
        {
            if (string.IsNullOrWhiteSpace(base.UserId))
                return null;

            SubscriptionPayload payload = new SubscriptionPayload()
            {
                userID = base.UserId,
                clientID = clientId,
                instruments = instruments,
                marketDataPort = marketDataPort,
                source = source
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
        public async Task<UnsubscriptionResult> UnsubscribeAsync(string clientId, string marketDataPort, List<Instruments> instruments, string source = "WebAPI")
        {
            if (string.IsNullOrWhiteSpace(base.UserId))
                return null;

            SubscriptionPayload payload = new SubscriptionPayload()
            {
                userID = base.UserId,
                clientID = clientId,
                instruments = instruments,
                marketDataPort = marketDataPort,
                source = source
            };

            return await Query<UnsubscriptionResult>(HttpMethodType.PUT, Url.Subscription(), payload: payload).ConfigureAwait(false);
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
