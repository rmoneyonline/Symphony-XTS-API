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

namespace XTSAPI.MarketData
{
    [DataContract]
    public class SearchByIdResult : SearchBase
    {
        /// <summary>
        /// Gets or sets the agm
        /// </summary>
        [DataMember(Name = "AGM")]
        public bool AGM { get; set; }

        /// <summary>
        /// Gets or sets all or none
        /// </summary>
        [DataMember(Name = "AllOrNone")]
        public bool AllOrNone { get; set; }

        /// <summary>
        /// Gets or sets the bonus
        /// </summary>
        [DataMember(Name = "Bonus")]
        public bool Bonus { get; set; }

        /// <summary>
        /// Gets or sets the dividend
        /// </summary>
        [DataMember(Name = "Dividend")]
        public bool Dividend { get; set; }

        /// <summary>
        /// Gets or sets the egm
        /// </summary>
        [DataMember(Name = "EGM")]
        public bool EGM { get; set; }

        /// <summary>
        /// Gets or sets the auction details
        /// </summary>
        [DataMember(Name = "AuctionDetailInfo")]
        public AuctionDetailInfo AuctionDetailInfo { get; set; }

        /// <summary>
        /// Gets or sets the el margin
        /// </summary>
        [DataMember(Name = "ELMargin")]
        public int ELMargin { get; set; }

        /// <summary>
        /// Gets or sets the interest
        /// </summary>
        [DataMember(Name = "Interest")]
        public bool Interest { get; set; }

        /// <summary>
        /// Gets or sets the isin
        /// </summary>
        [DataMember(Name = "ISIN")]
        public string ISIN { get; set; }

        /// <summary>
        /// Gets or sets the minimum fill
        /// </summary>
        [DataMember(Name = "MinimumFill")]
        public bool MinimumFill { get; set; }

        /// <summary>
        /// Gets or sets the rights
        /// </summary>
        [DataMember(Name = "Rights")]
        public bool Rights { get; set; }

        /// <summary>
        /// Gets or sets the var margin
        /// </summary>
        [DataMember(Name = "VaRMargin")]
        public double VaRMargin { get; set; }

        /// <summary>
        /// Gets or sets the issued capital
        /// </summary>
        [DataMember(Name = "IssuedCapital")]
        public long IssuedCapital { get; set; }

        /// <summary>
        /// Gets or sets the board lot quantity
        /// </summary>
        [DataMember(Name = "BoardLotQuantity")]
        public int BoardLotQuantity { get; set; }

        /// <summary>
        /// Gets or sets the face value
        /// </summary>
        [DataMember(Name = "FaceValue")]
        public int FaceValue { get; set; }

        /// <summary>
        /// Gets or sets the spread
        /// </summary>
        [DataMember(Name = "Spread")]
        public int Spread { get; set; }

        /// <summary>
        /// Gets or sets the call auction flag
        /// </summary>
        [DataMember(Name = "CallAuction1Flag")]
        public bool CallAuction1Flag { get; set; }

        /// <summary>
        /// Gets or sets the gsm indicator
        /// </summary>
        [DataMember(Name = "GSMIndicator")]
        public int GSMIndicator { get; set; }

        /// <summary>
        /// Gets or sets the auction number
        /// </summary>
        [DataMember(Name = "AuctionNumber")]
        public int AuctionNumber { get; set; }

        /// <summary>
        /// Gets or sets the minimum quantity
        /// </summary>
        [DataMember(Name = "MinimumQty")]
        public int MinimumQty { get; set; }

        /// <summary>
        /// Gets or sets the quantity multiplier
        /// </summary>
        [DataMember(Name = "QuantityMutliplier")]
        public int QuantityMutliplier { get; set; }

        /// <summary>
        /// Gets or sets the price numerator
        /// </summary>
        [DataMember(Name = "PriceNumerator")]
        public int PriceNumerator { get; set; }

        /// <summary>
        /// Gets or sets the price demoninator
        /// </summary>
        [DataMember(Name = "PriceDenominator")]
        public int PriceDenominator { get; set; }

        /// <summary>
        /// Gets or sets the symbol type
        /// </summary>
        [DataMember(Name = "SymbolType")]
        public int SymbolType { get; set; }

        /// <summary>
        /// Gets or sets the cfi code
        /// </summary>
        [DataMember(Name = "CfiCode")]
        public string CfiCode { get; set; }

        /// <summary>
        /// Gets or sets the status
        /// </summary>
        [DataMember(Name = "Status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the ticker per point
        /// </summary>
        [DataMember(Name = "TicksPerPoint")]
        public int TicksPerPoint { get; set; }

        /// <summary>
        /// Gets or sets is implied market
        /// </summary>
        [DataMember(Name = "IsImpliedMarket")]
        public bool IsImpliedMarket { get; set; }

        /// <summary>
        /// Gets or sets if tradeable
        /// </summary>
        [DataMember(Name = "IsTradeable")]
        public bool IsTradeable { get; set; }

        /// <summary>
        /// Gets or sets the max trade volume
        /// </summary>
        [DataMember(Name = "MaxTradeVolume")]
        public int MaxTradeVolume { get; set; }

        /// <summary>
        /// Gets or sets the decimal displace
        /// </summary>
        [DataMember(Name = "DecimalDisplace")]
        public int DecimalDisplace { get; set; }

        /// <summary>
        /// Gets or sets the extended market properties
        /// </summary>
        [DataMember(Name = "ExtendedMarketProperties")]
        public Dictionary<string, ExtendedMarketProperties> ExtendedMarketProperties { get; set; }

        /// <summary>
        /// Gets or sets the market type status eligibility
        /// </summary>
        [DataMember(Name = "MarketTypeStatusEligibility")]
        public Dictionary<string, MarketTypeStatusEligibility> MarketTypeStatusEligibility { get; set; }

        /// <summary>
        /// Gets or sets the display name with series
        /// </summary>
        [DataMember(Name = "DisplayNameWithSeries")]
        public string DisplayNameWithSeries { get; set; }

        /// <summary>
        /// Gets or sets the display name with series and exchange
        /// </summary>
        [DataMember(Name = "DisplayNameWithSeriesAndExchange")]
        public string DisplayNameWithSeriesAndExchange { get; set; }

        /// <summary>
        /// Gets or sets the display name with exchange
        /// </summary>
        [DataMember(Name = "DisplayNameWithExchange")]
        public string DisplayNameWithExchange { get; set; }

        

        /// <summary>
        /// Gets or sets the last update time
        /// <see cref="Globals.ToDateTime(long)"/>
        /// </summary>
        [DataMember(Name = "LastUpdateTime")]
        public int LastUpdateTime { get; set; }

        /// <summary>
        /// Gets or sets the yearly high
        /// </summary>
        [DataMember(Name = "FiftyTwoWeekHigh")]
        public double FiftyTwoWeekHigh { get; set; }

        /// <summary>
        /// Gets or sets the yearly low
        /// </summary>
        [DataMember(Name = "FiftyTwoWeekLow")]
        public double FiftyTwoWeekLow { get; set; }

        /// <summary>
        /// Gets or sets the bhavcopy
        /// </summary>
        [DataMember(Name = "Bhavcopy")]
        public Bhavcopy Bhavcopy { get; set; }

        /// <summary>
        /// Gets or sets the additial per expiry margin percentage
        /// </summary>
        [DataMember(Name = "AdditionalPreExpiryMarginPerc")]
        public int AdditionalPreExpiryMarginPerc { get; set; }

        /// <summary>
        /// Gets or sets the additional margin percent long
        /// </summary>
        [DataMember(Name = "AdditionalMarginPercLong")]
        public int AdditionalMarginPercLong { get; set; }

        /// <summary>
        /// Gets or sets the additional margin percent short
        /// </summary>
        [DataMember(Name = "AdditionalMarginPercShort")]
        public int AdditionalMarginPercShort { get; set; }

        /// <summary>
        /// Gets or sets the delivery margin percent
        /// </summary>
        [DataMember(Name = "DeliveryMarginPerc")]
        public int DeliveryMarginPerc { get; set; }

        /// <summary>
        /// Gets or sets the special margin percent buy
        /// </summary>
        [DataMember(Name = "SpecialMarginPercBuy")]
        public int SpecialMarginPercBuy { get; set; }

        /// <summary>
        /// Gets or sets the special margin percent sell
        /// </summary>
        [DataMember(Name = "SpecialMarginPercSell")]
        public int SpecialMarginPercSell { get; set; }

        /// <summary>
        /// Gets or sets the tender margin
        /// </summary>
        [DataMember(Name = "TenderMargin")]
        public int TenderMargin { get; set; }

        /// <summary>
        /// Gets or sets the EML long margin
        /// </summary>
        [DataMember(Name = "ELMLongMargin")]
        public int ELMLongMargin { get; set; }

        /// <summary>
        /// Gets or sets the ELM short margin
        /// </summary>
        [DataMember(Name = "ELMShortMargin")]
        public int ELMShortMargin { get; set; }

        /// <summary>
        /// Gets or sets the initial margin percent
        /// </summary>
        [DataMember(Name = "InitialMarginPerc")]
        public int InitialMarginPerc { get; set; }

        /// <summary>
        /// Gets or sets the exposure margin percent
        /// </summary>
        [DataMember(Name = "ExposureMarginPerc")]
        public int ExposureMarginPerc { get; set; }

        /// <summary>
        /// Gets or sets the call auction indicator
        /// </summary>
        [DataMember(Name = "CallAuctionIndicator")]
        public int CallAuctionIndicator { get; set; }

        /// <summary>
        /// Gets or sets the market type
        /// </summary>
        [DataMember(Name = "MarketType")]
        public int MarketType { get; set; }

        /// <summary>
        /// Gets or sets the current eligible market type
        /// </summary>
        [DataMember(Name = "CurrentEligibleMarketType")]
        public int CurrentEligibleMarketType { get; set; }

        /// <summary>
        /// Gets or sets the instrument lazy loader
        /// </summary>
        [DataMember(Name = "InstrumentLazyLoader")]
        public int InstrumentLazyLoader { get; set; }



    }

    [DataContract]
    public class AuctionDetailInfo
    {
        /// <summary>
        /// Gets or sets the auction number
        /// </summary>
        [DataMember(Name = "AuctionNumber")]
        public int AuctionNumber { get; set; }

        /// <summary>
        /// Gets or sets the auction status
        /// </summary>
        [DataMember(Name = "AuctionStatus")]
        public int AuctionStatus { get; set; }

        /// <summary>
        /// Gets or sets the initiator type
        /// </summary>
        [DataMember(Name = "InitiatorType")]
        public int InitiatorType { get; set; }

        /// <summary>
        /// Gets or sets the settlement period
        /// </summary>
        [DataMember(Name = "SettlementPeriod")]
        public int SettlementPeriod { get; set; }

        /// <summary>
        /// Gets or sets the total buy quantity
        /// </summary>
        [DataMember(Name = "TotalBuyQty")]
        public int TotalBuyQty { get; set; }

        /// <summary>
        /// Gets or sets the total sell quantity
        /// </summary>
        [DataMember(Name = "TotalSellQty")]
        public int TotalSellQty { get; set; }

        /// <summary>
        /// Gets or sets the auction quantity
        /// </summary>
        [DataMember(Name = "AuctionQty")]
        public int AuctionQty { get; set; }

        /// <summary>
        /// Gets or sets the auction price
        /// </summary>
        [DataMember(Name = "AuctionPrice")]
        public int AuctionPrice { get; set; }

        /// <summary>
        /// Gets or sets the best buy price
        /// </summary>
        [DataMember(Name = "BestBuyPrice")]
        public int BestBuyPrice { get; set; }

        /// <summary>
        /// Gets or sets the best sell price
        /// </summary>
        [DataMember(Name = "BestSellPrice")]
        public int BestSellPrice { get; set; }


    }

    


    [DataContract]
    public class ExtendedMarketProperties
    {
        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [DataMember(Name = "Name")]
        public Name1 Name { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        [DataMember(Name = "Value")]
        public string Value { get; set; }
    }

    [DataContract]
    public class Name1
    {
        /// <summary>
        /// Get or sets the name
        /// </summary>
        [DataMember(Name = "Name")]
        public string Name { get; set; }
    }


    [DataContract]
    public class MarketTypeStatusEligibility
    {
        /// <summary>
        /// Gets or sets the market type
        /// </summary>
        [DataMember(Name = "MarketType")]
        public int MarketType { get; set; }

        /// <summary>
        /// Gets or sets the eligible
        /// </summary>
        [DataMember(Name = "Eligibile")]
        public bool Eligibile { get; set; }

        /// <summary>
        /// Gets or sets the trading status
        /// </summary>
        [DataMember(Name = "TradingStatus")]
        public int TradingStatus { get; set; }
    }

    
    [DataContract]
    public class Bhavcopy
    {
        /// <summary>
        /// Gets or sets the open
        /// </summary>
        [DataMember(Name = "Open")]
        public double Open { get; set; }

        /// <summary>
        /// Gets or sets the high
        /// </summary>
        [DataMember(Name = "High")]
        public double High { get; set; }

        /// <summary>
        /// Gets or sets the low
        /// </summary>
        [DataMember(Name = "Low")]
        public double Low { get; set; }

        /// <summary>
        /// Gets or sets the close
        /// </summary>
        [DataMember(Name = "Close")]
        public double Close { get; set; }

        /// <summary>
        /// Gets or sets the total traded quantity
        /// </summary>
        [DataMember(Name = "TotTrdQty")]
        public long TotTrdQty { get; set; }

        /// <summary>
        /// Gets or sets hte total traded value
        /// </summary>
        [DataMember(Name = "TotTrdVal")]
        public double TotTrdVal { get; set; }

        /// <summary>
        /// Gets or sets the timestamp
        /// </summary>
        [DataMember(Name = "TimeStamp")]
        public string TimeStamp { get; set; }

        /// <summary>
        /// Gets or sets the total trades
        /// </summary>
        [DataMember(Name = "TotalTrades")]
        public long TotalTrades { get; set; }

        /// <summary>
        /// Gets or sets the open interest
        /// </summary>
        [DataMember(Name = "OpenInterest")]
        public long OpenInterest { get; set; }

        /// <summary>
        /// Gets or sets the settlement price
        /// </summary>
        [DataMember(Name = "SettlementPrice")]
        public double SettlementPrice { get; set; }


    }
}
