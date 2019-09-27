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
    public class ProfileResult : ProfileBase
    {
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
        /// Gets or sets the client type
        /// </summary>
        [DataMember(Name = "ClientType")]
        public string ClientType { get; set; }

        /// <summary>
        /// Gets or sets the client status
        /// </summary>
        [DataMember(Name = "ClientStatus")]
        public int ClientStatus { get; set; }

        /// <summary>
        /// Gets or sets the context
        /// </summary>
        [DataMember(Name = "Context")]
        public int Context { get; set; }

        /// <summary>
        /// Gets or sets included in auto square off blocked
        /// </summary>
        [DataMember(Name = "IncludeInAutoSquareoffBlocked")]
        public bool IncludeInAutoSquareoffBlocked { get; set; }

        /// <summary>
        /// Gets or sets if client pro
        /// </summary>
        [DataMember(Name = "IsProClient")]
        public bool IsProClient { get; set; }

        /// <summary>
        /// Gets or sets the office address
        /// </summary>
        [DataMember(Name = "OfficeAddress")]
        public string OfficeAddress { get; set; }

        /// <summary>
        /// Gets or sets the mobile number
        /// </summary>
        [DataMember(Name = "MobileNo")]
        public string MobileNo { get; set; }

        /// <summary>
        /// Gets or sets the group name
        /// </summary>
        [DataMember(Name = "GroupName")]
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the client bank details
        /// </summary>
        [DataMember(Name = "ClientBankInfoList")]
        public List<ClientBankInfoList> ClientBankInfoList { get; set; }
        
        /// <summary>
        /// Gets or sets the dealers credential
        /// </summary>
        [DataMember(Name = "DealerCredential")]
        public DealerCredential DealerCredential { get; set; }

        /// <summary>
        /// Gets or sets the client exchange details list
        /// </summary>
        [DataMember(Name = "ClientExchangeDetailsList")]
        public Dictionary<string, ClientExchangeDetails> ClientExchangeDetailsList { get; set; }

        /// <summary>
        /// Gets or sets the dealer
        /// </summary>
        [DataMember(Name = "Dealer")]
        public Dealer Dealer { get; set; }

        /// <summary>
        /// Gets or sets if converted to investor client
        /// </summary>
        [DataMember(Name = "IsConvertToInvestorClient")]
        public bool IsConvertToInvestorClient { get; set; }

        

    }

    [DataContract]
    public class Dealer : ProfileBase
    {

        /// <summary>
        /// Gets or sets the dealer id
        /// </summary>
        [DataMember(Name = "DealerId")]
        public string DealerId { get; set; }

        /// <summary>
        /// Gets or sets the dealer name
        /// </summary>
        [DataMember(Name = "DealerName")]
        public string DealerName { get; set; }

        /// <summary>
        /// Gets or sets the pin code
        /// </summary>
        [DataMember(Name = "Pincode")]
        public string Pincode { get; set; }

        /// <summary>
        /// Gets or sets privilege created by 
        /// </summary>
        [DataMember(Name = "CreatedPrivilegeBy")]
        public int CreatedPrivilegeBy { get; set; }

        /// <summary>
        /// Gets or sets the count
        /// </summary>
        [DataMember(Name = "Count")]
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the dealer exchange details list
        /// </summary>
        [DataMember(Name = "DealerExchangeDetailsList")]
        public List<DealerExchangeDetails> DealerExchangeDetailsList { get; set; }

        /// <summary>
        /// Gets or sets if included in auto square off block
        /// </summary>
        [DataMember(Name = "IncludeInAutoSquareoffBlock")]
        public bool IncludeInAutoSquareoffBlock { get; set; }

        /// <summary>
        /// Gets or sets the mobile number
        /// </summary>
        [DataMember(Name = "MobileNumber")]
        public string MobileNumber { get; set; }

        /// <summary>
        /// Gets or sets if pro order is allowed
        /// </summary>
        [DataMember(Name = "AllowedProOrder")]
        public bool AllowedProOrder { get; set; }

        /// <summary>
        /// Gets or sets the pro client id
        /// </summary>
        [DataMember(Name = "ProClientId")]
        public string ProClientId { get; set; }

        /// <summary>
        /// Gets or sets the dealer credential
        /// </summary>
        [DataMember(Name = "DealerCredentialObj")]
        public DealerCredential DealerCredentialObj { get; set; }
    }

    


    [DataContract]
    public class DealerExchangeDetails
    {
        /// <summary>
        /// Gets or sets the dealer id
        /// </summary>
        [DataMember(Name = "DealerId")]
        public string DealerId { get; set; }

        /// <summary>
        /// Gets or sets the exchange segment number
        /// </summary>
        [DataMember(Name = "ExchangeSegNumber")]
        public int ExchangeSegNumber { get; set; }

        /// <summary>
        /// Gets or sets if enabled or not
        /// </summary>
        [DataMember(Name = "Enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the expiry date
        /// </summary>
        [DataMember(Name = "ExpiryDate")]
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Gets or sets the exchange user id
        /// </summary>
        [DataMember(Name = "ExchangeUserId")]
        public int ExchangeUserId { get; set; }

        /// <summary>
        /// Gets or sets the terminal info
        /// </summary>
        [DataMember(Name = "TerminalInfo")]
        public string TerminalInfo { get; set; }

        /// <summary>
        /// Gets or sets if investor client
        /// </summary>
        [DataMember(Name = "IsInvertorClient")]
        public bool IsInvertorClient { get; set; }


    }

    [DataContract]
    public class ClientDealerMapping
    {
        /// <summary>
        /// Gets or sets the dealer id
        /// </summary>
        [DataMember(Name = "DealerId")]
        public string DealerId { get; set; }

        /// <summary>
        /// Gets or sets the client id
        /// </summary>
        [DataMember(Name = "ClientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the creators id
        /// </summary>
        [DataMember(Name = "CreatedBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the creation date
        /// </summary>
        [DataMember(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

    }

    [DataContract]
    public class ClientExchangeDetails
    {
        /// <summary>
        /// Gets or sets the client id
        /// </summary>
        [DataMember(Name = "ClientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the exchange segment number
        /// </summary>
        [DataMember(Name = "ExchangeSegNumber")]
        public int ExchangeSegNumber { get; set; }

        /// <summary>
        /// Gets or sets if enabled
        /// </summary>
        [DataMember(Name = "Enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets if participant code
        /// </summary>
        [DataMember(Name = "ParticipantCode")]
        public string ParticipantCode { get; set; }
    }

    [DataContract]
    public class ClientBankInfoList
    {

    }

    [DataContract]
    public class DealerCredential
    {
        /// <summary>
        /// Gets or sets the dealer id
        /// </summary>
        [DataMember(Name = "DealerId")]
        public string DealerId { get; set; }

        /// <summary>
        /// Gets or sets the mac id
        /// </summary>
        [DataMember(Name = "MacId")]
        public string MacId { get; set; }

        /// <summary>
        /// Gets or sets the server verification image id
        /// </summary>
        [DataMember(Name = "ServerVerficationImageId")]
        public int ServerVerficationImageId { get; set; }

        /// <summary>
        /// Gets or sets the login password
        /// </summary>
        [DataMember(Name = "LoginPassword")]
        public string LoginPassword { get; set; }

        /// <summary>
        /// Gets or sets the login password
        /// </summary>
        [DataMember(Name = "TransactionPassword")]
        public string TransactionPassword { get; set; }

        /// <summary>
        /// Gets or sets if enabed
        /// </summary>
        [DataMember(Name = "Enabled")]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the enabled date
        /// </summary>
        [DataMember(Name = "EnabledOn")]
        public DateTime EnabledOn { get; set; }

        /// <summary>
        /// Gets or sets the disabled reason
        /// </summary>
        [DataMember(Name = "ReasonDisabled")]
        public int ReasonDisabled { get; set; }

        /// <summary>
        /// Gets or sets the disabled on date
        /// </summary>
        [DataMember(Name = "DisabledOn")]
        public DateTime DisabledOn { get; set; }

        /// <summary>
        /// Gets or sets the number of invalid attempts
        /// </summary>
        [DataMember(Name = "InvalidAttempts")]
        public int InvalidAttempts { get; set; }

        /// <summary>
        /// Gets or sets the invalid 2FA attempts
        /// </summary>
        [DataMember(Name = "Invalid2FAAttempts")]
        public int Invalid2FAAttempts { get; set; }

        /// <summary>
        /// Gets or sets the 2FA changed date
        /// </summary>
        [DataMember(Name = "Level2FAChangedOn")]
        public DateTime Level2FAChangedOn { get; set; }

        /// <summary>
        /// Gets or sets the transaction password change date
        /// </summary>
        [DataMember(Name = "TransactionPasswordChangedOn")]
        public DateTime TransactionPasswordChangedOn { get; set; }

        /// <summary>
        /// Gets or sets the login password change date
        /// </summary>
        [DataMember(Name = "LoginPasswordChangedOn")]
        public DateTime LoginPasswordChangedOn { get; set; }

        /// <summary>
        /// Gets or sets the last successfull login date
        /// </summary>
        [DataMember(Name = "LastSuccessfulLoginOn")]
        public DateTime LastSuccessfulLoginOn { get; set; }

        /// <summary>
        /// Gets or sets the last update date
        /// </summary>
        [DataMember(Name = "LastUpdatedOn")]
        public DateTime LastUpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets the days of warning
        /// </summary>
        [DataMember(Name = "DaysOfWarning")]
        public int DaysOfWarning { get; set; }

        /// <summary>
        /// Gets or sets the client id
        /// </summary>
        [DataMember(Name = "ClientId")]
        public string ClientId { get; set; }


        /// <summary>
        /// Gets or sets the regen login password
        /// </summary>
        [DataMember(Name = "ReGenLoginPassword")]
        public bool ReGenLoginPassword { get; set; }

        /// <summary>
        /// Gets or sets the regen transaction password
        /// </summary>
        [DataMember(Name = "ReGenTransPassword")]
        public bool ReGenTransPassword { get; set; }

        /// <summary>
        /// Gets or sets if 2FA is reset
        /// </summary>
        [DataMember(Name = "Reset2FA")]
        public bool Reset2FA { get; set; }

        /// <summary>
        /// Gets or sets the date of birth
        /// </summary>
        [DataMember(Name = "DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the user access type
        /// </summary>
        [DataMember(Name = "UserAccessType")]
        public int UserAccessType { get; set; }

        /// <summary>
        /// Gets or sets the privilage
        /// </summary>
        [DataMember(Name = "Privilege")]
        public int Privilege { get; set; }

        /// <summary>
        /// Gets or sets the role name
        /// </summary>
        [DataMember(Name = "RoleName")]
        public string RoleName { get; set; }

        /// <summary>
        /// Gets or sets the client overview
        /// </summary>
        [DataMember(Name = "ClientOverview")]
        public bool ClientOverview { get; set; }

        /// <summary>
        /// Gets or sets the max scrip per session
        /// </summary>
        [DataMember(Name = "MaxScripPerSession")]
        public int MaxScripPerSession { get; set; }

        /// <summary>
        /// Gets or sets the ip address
        /// </summary>
        [DataMember(Name = "IPAddress")]
        public string IPAddress { get; set; }



    }   
}
