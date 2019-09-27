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
    public class ErrorResult
    {
        /// <summary>
        /// Gets or sets the status
        /// </summary>
        [DataMember(Name = "status")]
        public int status { get; set; }

        /// <summary>
        /// Gets or sets the status text
        /// </summary>
        [DataMember(Name = "statusText")]
        public string statusText { get; set; }

        /// <summary>
        /// Gets or sets the errors
        /// </summary>
        [DataMember(Name = "errors")]
        public List<Error> errors { get; set; }
    }


    [DataContract]
    public class Error
    {
        /// <summary>
        /// Gets or sets the field
        /// </summary>
        [DataMember(Name = "field")]
        public List<string> field { get; set; }

        /// <summary>
        /// Gets or sets the location
        /// </summary>
        [DataMember(Name = "location")]
        public string location { get; set; }

        /// <summary>
        /// Gets or sets the messages
        /// </summary>
        [DataMember(Name = "messages")]
        public List<string> messages { get; set; }

        /// <summary>
        /// Gets or sets the types
        /// </summary>
        [DataMember(Name = "types")]
        public List<string> types { get; set; }

    }
}
