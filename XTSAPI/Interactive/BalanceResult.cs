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
    public class BalanceResult
    {
        /// <summary>
        /// Gets or sets the balance list
        /// </summary>
        [DataMember(Name = "BalanceList")]
        public List<BalanceList> BalanceList { get; set; }
    }


    [DataContract]
    public class BalanceList
    {
        /// <summary>
        /// Gets or sets the limit header
        /// </summary>
        [DataMember(Name = "limitHeader")]
        public string limitHeader { get; set; }

        /// <summary>
        /// Gets or sets the limit object
        /// </summary>
        [DataMember(Name = "limitObject")]
        public LimitObject limitObject { get; set; }
    }

    [DataContract]
    public class LimitObject
    {
        /// <summary>
        /// Gets or sets the RMS sub limits
        /// </summary>
        [DataMember(Name = "RMSSubLimits")]
        public RMSSubLimits RMSSubLimits { get; set; }

        /// <summary>
        /// Gets or sets the margin available
        /// </summary>
        [DataMember(Name = "marginAvailable")]
        public MarginAvailable marginAvailable { get; set; }

        /// <summary>
        /// Gets or sets the margin utilized
        /// </summary>
        [DataMember(Name = "marginUtilized")]
        public MarginUtilized marginUtilized { get; set; }

        /// <summary>
        /// Gets or sets the limits assigned
        /// </summary>
        [DataMember(Name = "limitsAssigned")]
        public LimitsAssigned limitsAssigned { get; set; }
        
        /// <summary>
        /// Gets or sets the accont id
        /// </summary>
        [DataMember(Name = "AccountID")]
        public string AccountID { get; set; }
    }

    [DataContract]
    public class RMSSubLimits
    {
        /// <summary>
        /// Gets or sets the cash available
        /// </summary>
        [DataMember(Name = "cashAvailable")]
        public double cashAvailable { get; set; }

        /// <summary>
        /// Gets or sets the collateral
        /// </summary>
        [DataMember(Name = "collateral")]
        public double collateral { get; set; }

        /// <summary>
        /// Gets or sets the margin utilized
        /// </summary>
        [DataMember(Name = "marginUtilized")]
        public double marginUtilized { get; set; }

        /// <summary>
        /// Gets or sets the net margin available
        /// </summary>
        [DataMember(Name = "netMarginAvailable")]
        public double netMarginAvailable { get; set; }

        /// <summary>
        /// Gets or sets the mark to market (MTM) value
        /// </summary>
        [DataMember(Name = "MTM")]
        public double MTM { get; set; }

        /// <summary>
        /// Gets or sets the Unrealized mark to market (MTM) value
        /// </summary>
        [DataMember(Name = "UnrealizedMTM")]
        public double UnrealizedMTM { get; set; }

        /// <summary>
        /// Gets or sets the realized mark to market (MTM) value
        /// </summary>
        [DataMember(Name = "RealizedMTM")]
        public double RealizedMTM { get; set; }

    }

    [DataContract]
    public class MarginAvailable
    {
        /// <summary>
        /// Gets or sets the cash margin available
        /// </summary>
        [DataMember(Name = "CashMarginAvailable")]
        public double CashMarginAvailable { get; set; }

        /// <summary>
        /// Gets or sets the adhoc margin
        /// </summary>
        [DataMember(Name = "AdhocMargin")]
        public double AdhocMargin { get; set; }

        /// <summary>
        /// Gets or sets the notional cash
        /// </summary>
        [DataMember(Name = "NotinalCash")]
        public double NotinalCash { get; set; }

        /// <summary>
        /// Gets or sets the pay in amount
        /// </summary>
        [DataMember(Name = "PayInAmount")]
        public double PayInAmount { get; set; }

        /// <summary>
        /// Gets or sets the pay out amount
        /// </summary>
        [DataMember(Name = "PayOutAmount")]
        public double PayOutAmount { get; set; }

        /// <summary>
        /// Gets or sets the delivery sell benefit
        /// </summary>
        [DataMember(Name = "CNCSellBenifit")]
        public double CNCSellBenifit { get; set; }

        /// <summary>
        /// Gets or sets the direct collateral
        /// </summary>
        [DataMember(Name = "DirectCollateral")]
        public double DirectCollateral { get; set; }

        /// <summary>
        /// Gets or sets the holding collateral
        /// </summary>
        [DataMember(Name = "HoldingCollateral")]
        public double HoldingCollateral { get; set; }

        /// <summary>
        /// Gets or sets the client branch adhoc
        /// </summary>
        [DataMember(Name = "ClientBranchAdhoc")]
        public double ClientBranchAdhoc { get; set; }

        /// <summary>
        /// Gets or sets the sell option premium
        /// </summary>
        [DataMember(Name = "SellOptionsPremium")]
        public double SellOptionsPremium { get; set; }

        /// <summary>
        /// Gets or sets the total branch adhoc
        /// </summary>
        [DataMember(Name = "TotalBranchAdhoc")]
        public double TotalBranchAdhoc { get; set; }

        /// <summary>
        /// Gets or sets the adhoc FnO margin
        /// </summary>
        [DataMember(Name = "AdhocFOMargin")]
        public double AdhocFOMargin { get; set; }

        /// <summary>
        /// Gets or sets the adhoc currency margin
        /// </summary>
        [DataMember(Name = "AdhocCurrencyMargin")]
        public double AdhocCurrencyMargin { get; set; }

        /// <summary>
        /// Gets or sets the adhoc commodity margin
        /// </summary>
        [DataMember(Name = "AdhocCommodityMargin")]
        public double AdhocCommodityMargin { get; set; }


    }

    [DataContract]
    public class MarginUtilized
    {
        /// <summary>
        /// Gets or sets the gross exposure margin present
        /// </summary>
        [DataMember(Name = "GrossExposureMarginPresent")]
        public double GrossExposureMarginPresent { get; set; }

        /// <summary>
        /// Gets or sets the buy exposure margin present
        /// </summary>
        [DataMember(Name = "BuyExposureMarginPresent")]
        public double BuyExposureMarginPresent { get; set; }

        /// <summary>
        /// Gets or sets the sell exposre margin present
        /// </summary>
        [DataMember(Name = "SellExposureMarginPresent")]
        public double SellExposureMarginPresent { get; set; }

        /// <summary>
        /// Gets or sets the var margin present
        /// </summary>
        [DataMember(Name = "VarELMarginPresent")]
        public double VarELMarginPresent { get; set; }

        /// <summary>
        /// Gets or sets the scrip basket margin present
        /// </summary>
        [DataMember(Name = "ScripBasketMarginPresent")]
        public double ScripBasketMarginPresent { get; set; }

        /// <summary>
        /// Gets or sets the gross exposure limit present
        /// </summary>
        [DataMember(Name = "GrossExposureLimitPresent")]
        public double GrossExposureLimitPresent { get; set; }

        /// <summary>
        /// Gets or sets the buy exposure limit present
        /// </summary>
        [DataMember(Name = "BuyExposureLimitPresent")]
        public double BuyExposureLimitPresent { get; set; }

        /// <summary>
        /// Gets or sets the sell exposure limit present
        /// </summary>
        [DataMember(Name = "SellExposureLimitPresent")]
        public double SellExposureLimitPresent { get; set; }

        /// <summary>
        /// Gets or sets the delivery limit used
        /// </summary>
        [DataMember(Name = "CNCLimitUsed")]
        public double CNCLimitUsed { get; set; }

        /// <summary>
        /// Gets or sets the delivery amount used
        /// </summary>
        [DataMember(Name = "CNCAmountUsed")]
        public double CNCAmountUsed { get; set; }

        /// <summary>
        /// Gets or sets the margin used
        /// </summary>
        [DataMember(Name = "MarginUsed")]
        public double MarginUsed { get; set; }

        /// <summary>
        /// Gets or sets the limit used
        /// </summary>
        [DataMember(Name = "LimitUsed")]
        public double LimitUsed { get; set; }

        /// <summary>
        /// Gets or sets the total span margin
        /// </summary>
        [DataMember(Name = "TotalSpanMargin")]
        public double TotalSpanMargin { get; set; }

        /// <summary>
        /// Gets or sets the exposure margin present
        /// </summary>
        [DataMember(Name = "ExposureMarginPresent")]
        public double ExposureMarginPresent { get; set; }

    }

    [DataContract]
    public class LimitsAssigned
    {
        /// <summary>
        /// Gets or sets the delivery limit
        /// </summary>
        [DataMember(Name = "CNCLimit")]
        public double CNCLimit { get; set; }

        /// <summary>
        /// Gets or sets the turnover limit present
        /// </summary>
        [DataMember(Name = "TurnoverLimitPresent")]
        public double TurnoverLimitPresent { get; set; }

        /// <summary>
        /// Gets or sets the present mark to market loss limit
        /// </summary>
        [DataMember(Name = "MTMLossLimitPresent")]
        public double MTMLossLimitPresent { get; set; }

        /// <summary>
        /// Gets or sets the buy exposure limit
        /// </summary>
        [DataMember(Name = "BuyExposureLimit")]
        public double BuyExposureLimit { get; set; }

        /// <summary>
        /// Gets or sets the sell exposure limit
        /// </summary>
        [DataMember(Name = "SellExposureLimit")]
        public double SellExposureLimit { get; set; }

        /// <summary>
        /// Gets or sets the gross exposure limit
        /// </summary>
        [DataMember(Name = "GrossExposureLimit")]
        public double GrossExposureLimit { get; set; }

        /// <summary>
        /// Gets or sets the gross exposure derivative limit
        /// </summary>
        [DataMember(Name = "GrossExposureDerivativesLimit")]
        public double GrossExposureDerivativesLimit { get; set; }

        /// <summary>
        /// Gets or sets the buy exposure futures limit
        /// </summary>
        [DataMember(Name = "BuyExposureFuturesLimit")]
        public double BuyExposureFuturesLimit { get; set; }

        /// <summary>
        /// Gets or sets the buy exposure options limit
        /// </summary>
        [DataMember(Name = "BuyExposureOptionsLimit")]
        public double BuyExposureOptionsLimit { get; set; }

        /// <summary>
        /// Gets or sets the sell exposure options limit
        /// </summary>
        [DataMember(Name = "SellExposureOptionsLimit")]
        public double SellExposureOptionsLimit { get; set; }

        /// <summary>
        /// Gets or sets the sell exposure futures limit
        /// </summary>
        [DataMember(Name = "SellExposureFuturesLimit")]
        public double SellExposureFuturesLimit { get; set; }

    }

}
