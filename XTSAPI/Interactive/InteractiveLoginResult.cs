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
    public class InteractiveLoginResult : LoginResultBase
    {
        /// <summary>
        /// Gets or sets the trading privilages
        /// </summary>
        [DataMember(Name = "enums")]
        public Enums enums { get; set; }

        /// <summary>
        /// Gets or sets if investor client
        /// </summary>
        [DataMember(Name = "isInvestorClient")]
        public bool isInvestorClient { get; set; }

        /// <summary>
        /// Gets or sets the client codes
        /// </summary>
        [DataMember(Name = "clientCodes")]
        public List<string> clientCodes { get; set; }
    }

    [DataContract]
    public class Enums
    {
        /// <summary>
        /// Gets or sets the order source
        /// <see cref="OrderSource"/>
        /// </summary>
        [DataMember(Name = "orderSource")]
        public List<string> orderSource { get; set; }

        /// <summary>
        /// Gets or sets the socket events
        /// </summary>
        [DataMember(Name = "socketEvent")]
        public List<string> socketEvent { get; set; }

        /// <summary>
        /// Gets or setst the order side
        /// <see cref="OrderSide"/>
        /// </summary>
        [DataMember(Name = "orderSide")]
        public List<string> orderSide { get; set; }

        /// <summary>
        /// Gets or sets the position square off mode
        /// <see cref="PositionSquareOffMode"/>
        /// </summary>
        [DataMember(Name = "positionSqureOffMode")]
        public List<string> positionSqureOffMode { get; set; }

        /// <summary>
        /// Gets or sets the position square off quantity type
        /// <see cref="PositionSquareOffQuantityType"/>
        /// </summary>
        [DataMember(Name = "positionSquareOffQuantityType")]
        public List<string> positionSquareOffQuantityType { get; set; }

        /// <summary>
        /// Gets or sets the day or net
        /// <see cref="DayOrNet"/>
        /// </summary>
        [DataMember(Name = "dayOrNet")]
        public List<string> dayOrNet { get; set; }

        /// <summary>
        /// Gets or sets the exchange privilages
        /// </summary>
        [DataMember(Name = "exchangeInfo")]
        public Dictionary<string, ExchangeInfo> exchangeInfo { get; set; }

    }

    [DataContract]
    public class ExchangeInfo
    {
        /// <summary>
        /// Gets or sets the product types
        /// <see cref="ProductType"/>
        /// </summary>
        [DataMember(Name = "productType")]
        public List<string> productType { get; set; }

        /// <summary>
        /// Gets or sets the order types
        /// <see cref="OrderType"/>
        /// </summary>
        [DataMember(Name = "orderType")]
        public List<string> orderType { get; set; }

        /// <summary>
        /// Gets or sets the time in force / order duration
        /// <see cref="TimeInForce"/>
        /// </summary>
        [DataMember(Name = "timeInForce")]
        public List<string> timeInForce { get; set; }
    }
}
