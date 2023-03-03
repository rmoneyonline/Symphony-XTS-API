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
    public class HoldingsResult
    {
        /// <summary>
        /// Gets or sets the client id
        /// </summary>
        [DataMember(Name = "ClientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the RMS holding list
        /// </summary>
        [DataMember(Name = "RMSHoldingList")]
        public RMSHoldingList[] RMSHoldingList { get; set; }
    }

    [DataContract]
    public class RMSHoldingList
    {
        /// <summary>
        /// Gets or sets the rms holding id
        /// </summary>
        [DataMember(Name = "RMSHoldingId")]
        public int RMSHoldingId { get; set; }

        /// <summary>
        /// Gets or sets the client id
        /// </summary>
        [DataMember(Name = "ClientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client name
        /// </summary>
        [DataMember(Name = "ClientName")]
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the exchange instrument id
        /// </summary>
        [DataMember(Name = "ExchangeNSEInstrumentId")]
        public long ExchangeNSEInstrumentId { get; set; }

        /// <summary>
        /// Gets or sets the exchange bse instrument id
        /// </summary>
        [DataMember(Name = "ExchangeBSEInstrumentId")]
        public long ExchangeBSEInstrumentId { get; set; }

        /// <summary>
        /// Gets or sets the exchange mse instrument id
        /// </summary>
        [DataMember(Name = "ExchangeMSEIInstrumentId")]
        public long ExchangeMSEIInstrumentId { get; set; }

        /// <summary>
        /// Gets or sets the holding type
        /// </summary>
        [DataMember(Name = "HoldingType")]
        public string HoldingType { get; set; }

        /// <summary>
        /// Gets or sets the holding quantity
        /// </summary>
        [DataMember(Name = "HoldingQuantity")]
        public double HoldingQuantity { get; set; }

        /// <summary>
        /// Gets or sets the valuation type
        /// </summary>
        [DataMember(Name = "ValuationType")]
        public double ValuationType { get; set; }

        /// <summary>
        /// Gets or sets the haircut
        /// </summary>
        [DataMember(Name = "Haircut")]
        public double Haircut { get; set; }

        /// <summary>
        /// Gets or sets the collateral quantity
        /// </summary>
        [DataMember(Name = "CollateralQuantity")]
        public double CollateralQuantity { get; set; }

        /// <summary>
        /// Gets or sets the target product
        /// </summary>
        [DataMember(Name = "TargetProduct")]
        public int TargetProduct { get; set; }

        /// <summary>
        /// Gets or sets who create the holding
        /// </summary>
        [DataMember(Name = "CreatedBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the last update by
        /// </summary>
        [DataMember(Name = "LastUpdatedBy")]
        public string LastUpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the creation time
        /// </summary>
        [DataMember(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the last update time
        /// </summary>
        [DataMember(Name = "LastUpdatedOn")]
        public DateTime LastUpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets the used quantity
        /// </summary>
        [DataMember(Name = "UsedQuantity")]
        public double UsedQuantity { get; set; }

        /// <summary>
        /// Gets or sets the BuyAvgPrice added by me
        /// </summary>
        [DataMember(Name = "BuyAvgPrice")]
        public int BuyAvgPrice { get; set; }

        /// <summary>
        /// Gets or sets the IsBuyAvgPriceProvided added by me 
        /// </summary>
        [DataMember(Name = "IsBuyAvgPriceProvided ")]
        public bool IsBuyAvgPriceProvided { get; set; }

        /// <summary>
        /// Gets or sets the exchange segment
        /// </summary>
        [DataMember(Name = "exchangeSegment")]
        public string exchangeSegment { get; set; }
    }

}
