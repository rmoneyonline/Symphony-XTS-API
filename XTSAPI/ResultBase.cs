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
    public abstract class ResultBase
    {
        /// <summary>
        /// Gets or sets the message code
        /// </summary>
        [DataMember(Name = "MessageCode")]
        public int MessageCode { get; set; }

        /// <summary>
        /// Gets or sets the message version
        /// </summary>
        [DataMember(Name = "MessageVersion")]
        public int MessageVersion { get; set; }

        /// <summary>
        /// Gets or sets the token id
        /// </summary>
        [DataMember(Name = "TokenID")]
        public long TokenID { get; set; }

        /// <summary>
        /// Gets or sets the application type
        /// </summary>
        [DataMember(Name = "ApplicationType")]
        public int ApplicationType { get; set; }

        /// <summary>
        /// Gets or sets the sequence number
        /// </summary>
        [DataMember(Name = "SequenceNumber")]
        public long SequenceNumber { get; set; }
    }
}
