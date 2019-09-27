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

namespace XTSAPI.Interactive
{
    public class InteractiveEventArgs : EventArgs
    {
        public InteractiveEventArgs(InteractiveMessageType interactiveMessageType, object data)
        {
            this.Data = data;
            this.InteractiveMessageType = interactiveMessageType;
        }

        /// <summary>
        /// Gets the event data
        /// </summary>
        public object Data { get; private set; }

        /// <summary>
        /// Gets the interactive message type
        /// <see cref="InteractiveMessageType"/>
        /// </summary>
        public InteractiveMessageType InteractiveMessageType { get; private set; }
    }
}
