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
using System.Net;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Runtime.CompilerServices;
using XTSAPI.Interactive;
using System.Net.Http;

using Quobject.SocketIoClientDotNet.Client;

namespace XTSAPI
{
    
    
    public abstract class XTSBase
    {
        
        /// <summary>
        /// Gets if the instrument dump is being is downloaded
        /// </summary>
        public static bool IsDownloadingInstrumentDump { get; private set; }


        /// <summary>
        /// Parses a json string 
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="str">json string</param>
        /// <returns></returns>
        public static T ParseJson<T>(string str, bool doCleanup = true)
        {
            if (string.IsNullOrEmpty(str))
                return default(T);

            if (doCleanup)
            {
                str = str.Replace(@"\", string.Empty).Replace("\"{", "{").Replace("}\"", "}");
            }

            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str, new Newtonsoft.Json.JsonSerializerSettings()
                {
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                    MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        /// <summary>
        /// Gets the http client
        /// </summary>
        protected HttpClient HttpClient { get; set; } = null;
        /// <summary>
        /// Gets the socket
        /// </summary>
        protected Socket Socket { get; set; } = null;

        /// <summary>
        /// Gets the Uri Path and query
        /// </summary>
        protected string PathAndQuery { get; private set; }

        public XTSBase(string baseAddress)
        {
            if (Uri.TryCreate(baseAddress, UriKind.Absolute, out Uri result))
            {
                string authority = result.GetLeftPart(UriPartial.Authority);

                if (!string.IsNullOrWhiteSpace(result.PathAndQuery) && result.PathAndQuery.Length > 1)
                {
                    this.PathAndQuery = result.PathAndQuery;
                    if (this.PathAndQuery.Length > 1 && this.PathAndQuery.EndsWith("/"))
                    {
                        this.PathAndQuery = this.PathAndQuery.Substring(0, this.PathAndQuery.Length - 1);
                    }
                }

                this.HttpClient = new HttpClient();
                this.HttpClient.BaseAddress = new Uri(authority);
                this.HttpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
        }
        
        /// <summary>
        /// Login to the XTS API
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="password">Password</param>
        /// <param name="publicKey">Public Key</param>
        /// <param name="source">Source</param>
        /// <returns></returns>
        public abstract Task<T> LoginAsync<T>(string password, string publicKey, string source = "WebAPI") where T : LoginResultBase;

        /// <summary>
        /// Connect to the socket
        /// </summary>
        /// <returns></returns>
        protected virtual bool SubscribeToConnectionEvents()
        {

            Socket socket = this.Socket;
            if (socket == null)
                return false;


            socket.On(Socket.EVENT_CONNECT, (data) =>
            {
                this.OnConnectionState(ConnectionEvents.connect, data);
            });

            socket.On("joined", (data) =>
            {
                this.isConnectedToSocket = true;
                this.OnConnectionState(ConnectionEvents.joined, data);
            });

            socket.On("success", (data) =>
            {
                this.OnConnectionState(ConnectionEvents.success, data);
            });

            socket.On("warning", (data) =>
            {
                OnConnectionState(ConnectionEvents.warning, data);
            });

            socket.On("error", (data) =>
            {
                OnConnectionState(ConnectionEvents.error, data);
            });

            socket.On("logout", (data) =>
            {
                this.isConnectedToSocket = false;
                OnConnectionState(ConnectionEvents.logout, data);
            });

            socket.On("disconnect", (data) =>
            {
                OnConnectionState(ConnectionEvents.logout, data);
            });

            return true;
        }


        
        /// <summary>
        /// Logs out of the XTS API
        /// </summary>
        /// <returns></returns>
        public abstract Task LogoutAsync();


        private bool isConnectedToSocket = false;
        /// <summary>
        /// Gets if connected to the Socket
        /// </summary>
        public bool IsConnectedToSocket
        {
            get
            {
                return isConnectedToSocket;
            }
        }

        
        /// <summary>
        /// Trigger when the connection state changes
        /// </summary>
        /// <param name="connectionState">Connection state <see cref="ConnectionState"/></param>
        /// <param name="data">Data</param>
        protected virtual void OnConnectionState(ConnectionEvents connectionState, object data)
        {
            this.ConnectionState?.Invoke(null, new ConnectionEventArgs(connectionState, data));
        }

        /// <summary>
        /// Trigger when the json message
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="json">Json message from the api</param>
        protected virtual void OnJson(Type type, string json)
        {
            this.Json?.Invoke(null, new JsonEventArgs(type, json));
        }

        /// <summary>
        /// Trigger when an exception is raised
        /// </summary>
        /// <param name="type">Type</param>
        /// <param name="exception">Exception</param>
        protected virtual void OnException(Type type, Exception exception)
        {
            if (exception == null)
                return;

            this.Exception?.Invoke(null, new ExceptionEventArgs(type, exception));
        }


        /// <summary>
        /// Gets the token
        /// </summary>
        public string Token { get; protected set; }

        /// <summary>
        /// Gets the userId
        /// </summary>
        public string UserId { get; protected set; }


        /// <summary>
        /// Gets the connection events
        /// </summary>
        public event EventHandler<ConnectionEventArgs> ConnectionState;

        /// <summary>
        /// Gets the raw json string
        /// </summary>
        public event EventHandler<JsonEventArgs> Json;

        /// <summary>
        /// Gets the exceptions
        /// </summary>
        public event EventHandler<ExceptionEventArgs> Exception;

        /// <summary>
        /// Http query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="httpMethod">Method type</param>
        /// <param name="requestUri">Url</param>
        /// <param name="payload">Payload in case of POST and PUT queries</param>
        /// <returns></returns>
        protected async Task<T> Query<T>(HttpMethodType httpMethod, string requestUri, Payload payload = null)
        {
            
            HttpClient client = this.HttpClient;
            if (client == null)
                return default(T);

            HttpResponseMessage response = null;
            try
            {
                switch (httpMethod)
                {
                    case HttpMethodType.GET:
                        response = await client.GetAsync(requestUri).ConfigureAwait(false);
                        break;
                    case HttpMethodType.POST:
                        response = await client.PostAsync(requestUri, payload?.GetHttpContent()).ConfigureAwait(false);
                        break;
                    case HttpMethodType.PUT:
                        response = await client.PutAsync(requestUri, payload?.GetHttpContent()).ConfigureAwait(false);
                        break;
                    case HttpMethodType.DELETE:
                        response = await client.DeleteAsync(requestUri).ConfigureAwait(false);
                        break;
                    default:
                        break;
                }
            }
            catch (AggregateException aex)
            {
                if (aex != null && aex.InnerExceptions != null)
                {
                    foreach (var inner in aex.InnerExceptions)
                    {
                        OnException(typeof(T), inner);
                    }
                }
            }
            catch (HttpRequestException hex)
            {
                OnException(typeof(T), hex);
                if (hex.InnerException != null)
                {
                    OnException(typeof(T), hex.InnerException);
                    if (hex.InnerException.InnerException != null)
                    {
                        OnException(typeof(T), hex.InnerException.InnerException);
                    }
                }
            }
            catch (Exception ex)
            {
                OnException(typeof(T), ex);

                if (ex.InnerException != null)
                {
                    OnException(typeof(T), ex.InnerException);
                }

            }
            

            if (response == null)
                return default(T);

            string txt = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                T obj = ParseResponse<T>(txt);
                return obj;
            }
            else
            {
                OnException(typeof(T), new System.Exception(txt));
            }

            return default(T);
        }

        protected T ParseResponse<T>(string str)
        {
            Response<T> response = this.ParseString<Response<T>>(str, triggerJsonEvent: false);

            OnJson(typeof(T), str);

            if (response == null)
                return default(T);

            return response.result;
        }

        
        /// <summary>
        /// Parse the json string
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="str">json string</param>
        /// <param name="triggerJsonEvent">If the <see cref="Json"/> event be invoked</param>
        /// <returns></returns>
        protected T ParseString<T>(string str, bool triggerJsonEvent = true)
        {
            if (string.IsNullOrEmpty(str))
                return default(T);

            if (triggerJsonEvent)
            {
                OnJson(typeof(T), str);
            }

            try
            {
                return ParseJson<T>(str);
                /*
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str, new Newtonsoft.Json.JsonSerializerSettings()
                {
                    NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                    MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore
                });
                */
            }
            catch (Exception ex)
            {
                OnException(typeof(T), ex);
            }

            return default(T);
        }


        /// <summary>
        /// Gets the instrument dump. This will run on an independent httpclient
        /// </summary>
        /// <param name="exchanges">Exchanges for which the instruments will be downloaded</param>
        /// <param name="masterDataPath">Master data path</param>
        /// <param name="filePath">FilePath, if defined will save the json response in the defined file</param>
        /// <returns></returns>
        public async Task<MarketData.ContractInfo[]> DownloadInstrumentDumpAsync(List<string> exchanges, string filePath = null, string pathAndQuery = null)
        {
            if (exchanges == null || exchanges.Count == 0)
                return null;

            InstrumentDumpPayload payload = new InstrumentDumpPayload()
            {
                exchangeSegmentList = exchanges
            };

            return await DownloadInstrumentDumpAsync(payload, filePath: filePath, pathAndQuery: pathAndQuery).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the instrument dump. This will run on an independent httpclient
        /// </summary>
        /// <param name="payload">Instrument dump payload <see cref="InstrumentDumpPayload"/></param>
        /// <param name="masterDataPath">Market data path</param>
        /// <param name="filePath">FilePath, if defined will save the json response in the defile file</param>
        /// <returns></returns>
        public async Task<MarketData.ContractInfo[]> DownloadInstrumentDumpAsync(InstrumentDumpPayload payload, string filePath = null, string pathAndQuery = null)
        {
            if (payload == null || payload.exchangeSegmentList == null || payload.exchangeSegmentList.Count == 0)
                return null;

            if (this.HttpClient == null || this.HttpClient.BaseAddress == null)
                return null;

            if (!string.IsNullOrEmpty(pathAndQuery) && pathAndQuery.Length > 1)
            {
                if (pathAndQuery.EndsWith("/"))
                {
                    pathAndQuery = pathAndQuery.Substring(0, pathAndQuery.Length - 1);
                }
            }
            
            if (IsDownloadingInstrumentDump)
                return null;

            IsDownloadingInstrumentDump = true;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.HttpClient.BaseAddress;
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = System.Threading.Timeout.InfiniteTimeSpan;

                try
                {
                    HttpResponseMessage response = await client.PostAsync($"{(string.IsNullOrWhiteSpace(pathAndQuery) ? this.PathAndQuery : pathAndQuery)}/instruments/master", payload.GetHttpContent()).ConfigureAwait(false);

                    string txt = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                    if (response.IsSuccessStatusCode)
                    {
                        if (!string.IsNullOrEmpty(filePath) && Directory.Exists(Path.GetDirectoryName(filePath)))
                        {
                            using (StreamWriter writer = new StreamWriter(filePath))
                            {
                                writer.Write(txt);
                            }
                        }

                        var res = XTSBase.ParseJson<Response<string>>(txt, doCleanup: false);


                        if (res != null && !string.IsNullOrEmpty(res.result))
                        {
                            List<MarketData.ContractInfo> tmp = new List<MarketData.ContractInfo>();
                            string[] lines = res.result.Split('\n');
                            foreach (var line in lines)
                            {
                                MarketData.ContractInfo search = new MarketData.ContractInfo();
                                try
                                {
                                    search.Parse(line);
                                    tmp.Add(search);
                                }
                                catch (Exception ex)
                                {
                                    OnException(typeof(MarketData.ContractInfo), ex);
                                }
                            }

                            return tmp.ToArray();
                        }

                    }
                    else
                    {
                        OnJson(typeof(MarketData.ContractInfo), txt);
                    }
                }
                catch (Exception ex)
                {
                    OnException(typeof(MarketData.ContractInfo), ex);
                }
                finally
                {
                    IsDownloadingInstrumentDump = false;
                }
            }

            return null;
        }


    }

}

