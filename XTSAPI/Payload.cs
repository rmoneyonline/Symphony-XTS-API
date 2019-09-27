/*
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace XTSAPI
{
    [DataContract]
    public abstract class Payload
    {
        public string GetJsonString()
        {
            /*
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(this.GetType(), new DataContractJsonSerializerSettings()
            {
                KnownTypes = new Type[] { typeof(string), typeof(int[]), typeof(int) }
            });

            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, this);
                return ms.ToArray();
            }
            */

            return Newtonsoft.Json.JsonConvert.SerializeObject(this);

        }

        public virtual HttpContent GetHttpContent()
        {
            string json = GetJsonString();

            return new StringContent(json, Encoding.UTF8, "application/json");
        }
    }
}
