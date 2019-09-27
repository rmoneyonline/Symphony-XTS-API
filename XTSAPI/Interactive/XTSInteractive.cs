/*
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Quobject.SocketIoClientDotNet.Client;


namespace XTSAPI.Interactive
{
    public class XTSInteractive : XTSBase
    {
        public XTSInteractive(string userId, string baseAddress)
            : base(userId, baseAddress)
        { }


        /// <summary>
        /// Login to XTS Interactive API 
        /// </summary>
        /// <typeparam name="T"><see cref="InteractiveLoginResult"/></typeparam>
        /// <param name="password">Password</param>
        /// <param name="publicKey">Public key</param>
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

            T response = await Query<T>(HttpMethodType.POST, Url.Session(), payload: payload).ConfigureAwait(false);
            
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
        /// Connect to the socket
        /// </summary>
        /// <returns></returns>
        public bool ConnectToSocket()
        {
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
                Path = "/interactive/socket.io",
                Query = new Dictionary<string, string>() { { "token", this.Token }, { "userID", base.UserId } }
            };

            this.Socket = IO.Socket(httpClient.BaseAddress, options);

            //subscribe to the base methods
            if (!base.SubscribeToConnectionEvents())
                return false;

            //and the native methods

            this.Socket.On("order", (order) =>
            {
                OnPostback<OrderResult>(InteractiveMessageType.Order, order);
            });

            this.Socket.On("trade", (trade) =>
            {
                OnPostback<TradeResult>(InteractiveMessageType.Trade, trade);
            });

            this.Socket.On("position", (position) =>
            {
                OnPostback<PositionResult>(InteractiveMessageType.Position, position);
            });

            return true;
        }

        /// <summary>
        /// Logout from the interactive host
        /// </summary>
        /// <returns></returns>
        public async Task LogoutAsync()
        {
            this.Socket?.Disconnect();

            await Query<Response<string>>(HttpMethodType.DELETE, Url.Session()).ConfigureAwait(false);

            this.HttpClient.Dispose();
            this.HttpClient = null;

        }

        /// <summary>
        /// Get the user profile
        /// </summary>
        /// <returns></returns>
        public async Task<ProfileResult> GetProfileAsync()
        {
            return await Query<ProfileResult>(HttpMethodType.GET, Url.Profile()).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the account summery
        /// </summary>
        /// <returns></returns>
        public async Task<BalanceResult> GetBalanceAsync()
        {
            return await Query<BalanceResult>(HttpMethodType.GET, Url.Balance()).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the order book
        /// </summary>
        /// <returns></returns>
        public async Task<OrderResult[]> GetOrderAsync()
        {
            return await Query<OrderResult[]>(HttpMethodType.GET, Url.Order()).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the 'day' position book
        /// </summary>
        /// <returns></returns>
        public async Task<PositionResult[]> GetDayPositionAsync()
        {
            return await Query<PositionResult[]>(HttpMethodType.GET, Url.Positions()).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the 'net' position book
        /// </summary>
        /// <returns></returns>
        public async Task<PositionResult[]> GetNetPositionAsync()
        {
            return await Query<PositionResult[]>(HttpMethodType.GET, Url.Positions("net")).ConfigureAwait(false);
        }

        /// <summary>
        /// Convert position
        /// </summary>
        /// <param name="payload">Payload</param>
        /// <returns></returns>
        public async Task ConvertPositionAsync(long executionId, long appOrderId, string oldProductType, string newProductType)
        {
            PositionConvertPayload payload = new PositionConvertPayload()
            {
                appOrderID = appOrderId,
                executionID = executionId,
                oldProductType = oldProductType,
                newProductType = newProductType
            };

            await Query<object>(HttpMethodType.PUT, Url.PositionConvert(), payload: payload).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the trade book
        /// </summary>
        /// <returns></returns>
        public async Task<TradeResult[]> GetTradesAsync()
        {
            return await Query<TradeResult[]>(HttpMethodType.GET, Url.Trade()).ConfigureAwait(false);
        }

        /// <summary>
        /// Get the holding book
        /// </summary>
        /// <returns></returns>
        public async Task<HoldingsResult> GetHoldingsAsync()
        {
            return await Query<HoldingsResult>(HttpMethodType.GET, Url.Holdings()).ConfigureAwait(false);
        }

        /// <summary>
        /// Place an order
        /// </summary>
        /// <param name="exchangeSegment">Exchange <see cref="ExchangeSegment"/></param>
        /// <param name="exchangeInstrumentId">Exchange instrument id</param>
        /// <param name="orderSide">Order side <see cref="OrderSide"/></param>
        /// <param name="orderType">Order type <see cref="OrderType"/></param>
        /// <param name="quantity">Quantity</param>
        /// <param name="limitPrice">Limit price</param>
        /// <param name="stopPrice">Stop price</param>
        /// <param name="productType">Product type <see cref="ProductType"/></param>
        /// <param name="timeInForce">Time in force <see cref="TimeInForce"/></param>
        /// <param name="disclosedQty">Disclosed quantity</param>
        /// <param name="orderUniqueIdentifier">Unique order identifier</param>
        /// <returns></returns>
        public async Task<OrderIdResult> PlaceOrderAsync(string exchangeSegment, long exchangeInstrumentId, string orderSide, string orderType,
            int quantity, double limitPrice, double stopPrice, string productType, string timeInForce, int disclosedQty = 0, string orderUniqueIdentifier = "123abc")
        {
            OrderPayload payload = new OrderPayload()
            {
                exchangeSegment = exchangeSegment,
                exchangeInstrumentID = exchangeInstrumentId,
                limitPrice = limitPrice,
                stopPrice = stopPrice,
                orderQuantity = quantity,
                disclosedQuantity = disclosedQty,
                orderSide = orderSide,
                orderType = orderType,
                orderUniqueIdentifier = orderUniqueIdentifier,
                productType = productType,
                timeInForce = timeInForce
            };

            return await PlaceOrderAsync(payload).ConfigureAwait(false);
        }

        /// <summary>
        /// Place an order
        /// </summary>
        /// <param name="payload">Order payload <see cref="OrderPayload"/></param>
        /// <returns></returns>
        public async Task<OrderIdResult> PlaceOrderAsync(OrderPayload payload)
        {
            return await Query<OrderIdResult>(HttpMethodType.POST, Url.Order(), payload: payload).ConfigureAwait(false);
        }

        /// <summary>
        /// Modify an order
        /// </summary>
        /// <param name="appOrderId">App order id</param>
        /// <param name="orderType">Order type <see cref="OrderType"/></param>
        /// <param name="quantity">Quantity</param>
        /// <param name="limitPrice">Limit price</param>
        /// <param name="stopPrice">Stop price</param>
        /// <param name="productType">Product type <see cref="ProductType"/></param>
        /// <param name="timeInForce">Time in force <see cref="TimeInForce"/></param>
        /// <param name="disclosedQty">Disclosed quantity</param>
        /// <param name="orderUniqueIdentifier">Unique order id</param>
        /// <returns></returns>
        public async Task<OrderIdResult> ModifyOrderAsync(long appOrderId, string orderType, int quantity, double limitPrice, double stopPrice,
            string productType, string timeInForce, int disclosedQty = 0, string orderUniqueIdentifier = "123abc")
        {
            ModifyOrderPayload payload = new ModifyOrderPayload()
            {
                appOrderID = appOrderId,
                orderUniqueIdentifier = orderUniqueIdentifier,
                modifiedOrderType = orderType,
                modifiedOrderQuantity = quantity,
                modifiedProductType = productType,
                modifiedTimeInForce = timeInForce,
                modifiedLimitPrice = limitPrice,
                modifiedStopPrice = stopPrice,
                modifiedDisclosedQuantity = disclosedQty
            };

            return await ModifyOrderAsync(payload).ConfigureAwait(false);
        }

        /// <summary>
        /// Modify an order
        /// </summary>
        /// <param name="payload">Order modify payload <see cref="ModifyOrderPayload"/></param>
        /// <returns></returns>
        public async Task<OrderIdResult> ModifyOrderAsync(ModifyOrderPayload payload)
        {
            return await Query<OrderIdResult>(HttpMethodType.PUT, Url.Order(), payload: payload).ConfigureAwait(false);
        }

        /// <summary>
        /// Cancel an order
        /// </summary>
        /// <param name="appOrderID">App order id</param>
        /// <returns></returns>
        public async Task<OrderIdResult> CancelOrderAsync(long appOrderID)
        {
            return await Query<OrderIdResult>(HttpMethodType.DELETE, Url.Order(appOrderID)).ConfigureAwait(false);
        }

        /// <summary>
        /// Place cover order
        /// </summary>
        /// <param name="exchangeSegment">Exchange <see cref="ExchangeSegment"/></param>
        /// <param name="exchangeInstrumentId">Exchange instrument id</param>
        /// <param name="orderSide">Order side <see cref="OrderSide"/></param>
        /// <param name="quantity">Quantity</param>
        /// <param name="limitPrice">Limit price</param>
        /// <param name="stopPrice">Stop price</param>
        /// <param name="disclosedQty">Disclosed quantity</param>
        /// <param name="orderUniqueIdentifier">Unique order identifier</param>
        /// <returns></returns>
        public async Task<CoverOrderResult> PlaceCoverOrderAsync(string exchangeSegment, long exchangeInstrumentId, string orderSide,
            int quantity, double limitPrice, double stopPrice, int disclosedQty = 0, string orderUniqueIdentifier = "123abc")
        {
            CoverOrderPayload payload = new CoverOrderPayload()
            {
                disclosedQuantity = disclosedQty,
                exchangeSegment = exchangeSegment,
                exchangeInstrumentID = exchangeInstrumentId,
                limitPrice = limitPrice,
                stopPrice = stopPrice,
                orderQuantity = quantity,
                orderSide = orderSide,
                orderUniqueIdentifier = orderUniqueIdentifier
            };

            return await PlaceCoverOrderAsync(payload).ConfigureAwait(false);
        }

        /// <summary>
        /// Place a cover order
        /// </summary>
        /// <param name="payload">Cover order payload <see cref="CoverOrderPayload"/></param>
        /// <returns></returns>
        public async Task<CoverOrderResult> PlaceCoverOrderAsync(CoverOrderPayload payload)
        {
            return await Query<CoverOrderResult>(HttpMethodType.POST, Url.CoverOrder(), payload: payload).ConfigureAwait(false);
        }

        //TO DO
        /// <summary>
        /// Exit a cover order
        /// </summary>
        /// <param name="payload">Exit cover order payload <see cref="ExitCoverOrderPayload"/></param>
        /// <returns></returns>
        public async Task ExitCoverOrderAsync(long appOrderId)
        {
            ExitCoverOrderPayload payload = new ExitCoverOrderPayload()
            {
                appOrderID = appOrderId
            };

            await Query<object>(HttpMethodType.PUT, Url.CoverOrder(), payload: payload).ConfigureAwait(false);
        }

        /// <summary>
        /// Square off 
        /// </summary>
        /// <param name="exchangeSegment">Exchange <see cref="ExchangeSegment"/></param>
        /// <param name="exchangeInstrumentId">Exchange instrument id</param>
        /// <param name="productType">Product type <see cref="ProductType"/></param>
        /// <param name="squareOffMode">Square off mode</param>
        /// <param name="squareOffQtyValue">Squre off quantity value</param>
        /// <param name="positionSquareOffQtyType">Position square off quantity type</param>
        /// <returns></returns>
        public async Task SquareOff(string exchangeSegment, long exchangeInstrumentId, string productType, string squareOffMode,
            int squareOffQtyValue, string positionSquareOffQtyType)
        {
            SquareoffPayload payload = new SquareoffPayload()
            {
                exchangeSegment = exchangeSegment,
                exchangeInstrumentID = exchangeInstrumentId,
                productType = productType,
                squareoffMode = squareOffMode,
                squareOffQtyValue = squareOffQtyValue,
                positionSquareOffQuantityType = positionSquareOffQtyType
            };

            await SquareOff(payload).ConfigureAwait(false);

        }

        /// <summary>
        /// Square off a position
        /// </summary>
        /// <param name="payload">Square off payload <see cref="SquareoffPayload"/></param>
        /// <returns></returns>
        public async Task SquareOff(SquareoffPayload payload)
        {
            await Query<object>(HttpMethodType.PUT, Url.SquareOff(), payload: payload).ConfigureAwait(false);
        }


        protected virtual void OnPostback<T>(InteractiveMessageType messageType, object data)
        {
            if (data == null)
                return;

            try
            {
                T obj = base.ParseString<T>(data.ToString());

                if (obj != null)
                {
                    this.Interactive?.Invoke(null, new InteractiveEventArgs(messageType, obj));
                }
            }
            catch (Exception ex)
            {
                OnException(typeof(T), ex);
            }

        }

        /// <summary>
        /// Gets the interactive event
        /// </summary>
        public event EventHandler<InteractiveEventArgs> Interactive;
    }
}
