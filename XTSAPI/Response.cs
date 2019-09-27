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
    public class Response
    {
        /// <summary>
        /// Gets or sets the type
        /// </summary>
        [DataMember(Name = "type")]
        public string type { get; set; }

        /// <summary>
        /// Gets or sets the type
        /// </summary>
        [DataMember(Name = "code")]
        public string code { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        [DataMember(Name = "description")]
        public string description { get; set; }
    }


    [DataContract]
    public class Response<T> : Response
    {
        /// <summary>
        /// Gets or sets the result
        /// </summary>
        [DataMember(Name = "result")]
        public T result { get; set; }
    }
}
