/*
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XTSAPI
{
    [DataContract]
    public class LoginPayload : Payload
    {

        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        [DataMember(Name = "userID")]
        public string userID { get; set; }

        /// <summary>
        /// Gets or sets the password
        /// </summary>
        [DataMember(Name = "password")]
        public string password { get; set; }

        /// <summary>
        /// Gets or sets the public key
        /// </summary>
        [DataMember(Name = "publicKey")]
        public string publicKey { get; set; }

        /// <summary>
        /// Gets or sets the source
        /// </summary>
        [DataMember(Name = "source")]
        public string source { get; set; } = OrderSource.WebAPI;
    }
}
