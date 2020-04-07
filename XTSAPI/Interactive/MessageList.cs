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

namespace XTSAPI.Interactive
{
    [DataContract]
    public class MessageList
    {
        [DataMember(Name = "messageList")]
        public List<ExchangeMessage> messageList { get; set; }

    }

    [DataContract]
    public class ExchangeMessage
    {
        [DataMember(Name = "ExchangeSegment")]
        public int ExchangeSegment { get; set; }

        [DataMember(Name = "ExchangeTimeStamp")]
        public long ExchangeTimeStamp { get; set; }

        [DataMember(Name = "BroadcastMessage")]
        public string BroadcastMessage { get; set; }

        [DataMember(Name = "SequenceId")]
        public long SequenceId { get; set; }

        [DataMember(Name = "MessageCode")]
        public int MessageCode { get; set; }

        [DataMember(Name = "MessageVersion")]
        public int MessageVersion { get; set; }

        [DataMember(Name = "TokenID")]
        public int TokenID { get; set; }

        [DataMember(Name = "ApplicationType")]
        public int ApplicationType { get; set; }

        [DataMember(Name = "SequenceNumber")]
        public long SequenceNumber { get; set; }

    }
}
