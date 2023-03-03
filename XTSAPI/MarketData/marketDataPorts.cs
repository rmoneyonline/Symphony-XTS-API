/*
    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF 
    MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
    FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION 
    WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/

namespace XTSAPI.MarketData
{
    /*
     "8001":"TopGainersPriceChange",
     "8002":"TopLosersPriceChange",
     "8003":"TopGainerPercentageChange",
     "8004":"TopLoserPercentageChang",
     "8005":"TopGainerVolume",
     "8006":"TopLoserVolume",
     "8007":"TopGainerPrice",
     "8008":"TopLoserPrice",
     "8009":"TopVolumeGainer",
     "8010":"UpperCircuitBreaker",
     "8011":"LowerCircuitBreaker",
     "8012":"NearUpperCircuitBreaker",
     "8013":"NearLowerCircuitBreaker",
     "8014":"IntradayFall",
     "8015":"IntradayRecovery",
     "8016":"OpenEqualHigh",
     "8017":"OpenEqualLow",
     "8018":"ResistanceBreachLevelZero",
     "8019":"ResistanceBreachLevelOne",
     "8020":"ResistanceBreachLevelTwo",
     "8021":"ResistanceBreachLevelThree",
     "8022":"SupportBreachLevelOne",
     "8023":"SupportBreachLevelTwo",
     "8024":"SupportBreachLevelThree",
     "8025":"Near52WHigh",
     "8026":"Near52WLow",
     "8027":"DayHighBreach",
     "8028":"DayLowBreach",
     "8029":"WeekHighBreach",
     "8030":"WeekLowBreach",
     "8031":"MonthHighBreach",
     "8032":"MonthLowBreach",
     "8033":"TwoMonthsHighBreach",
     "8034":"TwoMonthsLowBreach",
     "8035":"ThreeMonthsHighBreach",
     "8036":"ThreeMonthsLowBreach",
     "8037":"FiftyTwoWeekHighBreach",
     "8038":"FiftyTwoWeekLowBreach",
     "8039":"TwoWeekHighBreach",
     "8040":"TwoWeekLowBreach",
     "8041":"RiseInPrice",
     "8042":"FallInPrice",
     "8043":"PotentialVolumeShocker",
     "8044":"WeekUnusualVolumeShocke",
     "8045":"MonthUnusualVolumeShocker",
     "8046":"LongSpreadNearChange",
     "8047":"ShortSpreadNearChange",
     "8048":"LongSpreadNextChange",
     "8049":"ShortSpreadNextChange",
     "8050":"LongSpreadFarChange",
     "8051":"ShortSpreadFarChange",
     "8052":"HighBuildUp",
     "8053":"LowBuildUp",
     "touchlineEvent":1501,
     "marketDepthEvent":1502,
     "topGainerLosserEvent":1503,
     "indexDataEvent":1504,
     "candleDataEvent":1505,
     "generalMessageBroadcastEvent":1506,
     "exchangeTradingStatusEvent":1507,
     "openInterestEvent":1510,
     "instrumentSubscriptionInfo":5505,
     "marketDepthEvent100":5018,
     "scannerDataEvent":8000,
     "topGainersPriceChangeEvent":8001,
     "topLosersPriceChangeEvent":8002,
     "topGainerPercentageChangeEvent":8003,
     "topLoserPercentageChangeEvent":8004,
     "topGainerVolumeEvent":8005,
     "topLoserVolumeEvent":8006,
     "topGainerPriceEvent":8007,
     "topLoserPriceEvent":8008,
     "topVolumeGainerEvent":8009,
     "upperCircuitBreakerEvent":8010,
     "lowerCircuitBreakerEvent":8011,
     "nearUpperCircuitBreakerEvent":8012,
     "nearLowerCircuitBreakerEvent":8013,
     "intradayFallEvent":8014,
     "intradayRecoveryEvent":8015,
     "openEqualHighEvent":8016,
     "openEqualLowEvent":8017,
     "resistanceBreachLevelZeroEvent":8018,
     "resistanceBreachLevelOneEvent":8019,
     "resistanceBreachLevelTwoEvent":8020,
     "resistanceBreachLevelThreeEvent":8021,
     "supportBreachLevelOneEvent":8022,
     "supportBreachLevelTwoEvent":8023,
     "supportBreachLevelThreeEvent":8024,
     "near52WHighEvent":8025,
     "near52WLowEvent":8026,
     "dayHighBreachEvent":8027,
     "dayLowBreachEvent":8028,
     "weekHighBreachEvent":8029,
     "weekLowBreachEvent":8030,
     "monthHighBreachEvent":8031,
     "monthLowBreachEvent":8032,
     "twoMonthsHighBreachEvent":8033,
     "twoMonthsLowBreachEvent":8034,
     "threeMonthsHighBreachEvent":8035,
     "threeMonthsLowBreachEvent":8036,
     "fiftyTwoWeekHighBreachEvent":8037,
     "fiftyTwoWeekLowBreachEvent":8038,
     "twoWeekHighBreachEvent":8039,
     "twoWeekLowBreachEvent":8040,
     "riseEvent":8041,
     "fallEvent":8042,
     "potentialVolumeShockerEvent":8043,
     "weekUnusualVolumeShockerEvent":8044,
     "monthUnusualVolumeShockerEvent":8045,
     "longSpreadNearChangeEvent":8046,
     "shortSpreadNearChangeEvent":8047,
     "longSpreadNextChangeEvent":8048,
     "shortSpreadNextChangeEvent":8049,
     "longSpreadFarChangeEvent":8050,
     "shortSpreadFarChangeEvent":8051,
     "highBuildUpEvent":8052,
     "lowBuildUpEvent":8053} 
    */

    public enum MarketDataPorts
    {
        /// <summary>
        /// Instrument change property event
        /// </summary>
        instrumentPropertyChangeEvent = 1105,
        /// <summary>
        /// Level I events
        /// </summary>
        touchlineEvent = 1501,
        /// <summary>
        /// Level II events
        /// </summary>
        marketDepthEvent = 1502,
        /// <summary>
        /// Top gainers and loser event
        /// </summary>
        topGainerLosserEvent = 1503,
        /// <summary>
        /// Index event
        /// </summary>
        indexDataEvent = 1504,
        /// <summary>
        /// Candle event
        /// </summary>
        candleDataEvent = 1505,
        /// <summary>
        /// General message broadcast event
        /// </summary>
        generalMessageBroadcastEvent = 1506,
        /// <summary>
        /// Exchange trading status event
        /// </summary>
        exchangeTradingStatusEvent = 1507,
        /// <summary>
        /// Open interest event
        /// </summary>
        openInterestEvent = 1510,
        /// <summary>
        /// Instrument subscription info
        /// </summary>
        instrumentSubscriptionInfo = 5505,
        /// <summary>
        /// Level III 30 depth picture
        /// </summary>
        marketDepthEvent30 = 5014,
        /// <summary>
        /// Level III 100 depth picture
        /// </summary>
        marketDepthEvent100 = 5018
        

    }
}
