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
    public abstract class ProfileBase
    {
        /// <summary>
        /// Gets or sets the branch id
        /// </summary>
        [DataMember(Name = "BranchId")]
        public string BranchId { get; set; }

        /// <summary>
        /// Gets or sets the email id
        /// </summary>
        [DataMember(Name = "EmailId")]
        public string EmailId { get; set; }

        /// <summary>
        /// Gets or sets the Permanent Account Number (PAN)
        /// </summary>
        [DataMember(Name = "PAN")]
        public string PAN { get; set; }

        /// <summary>
        /// Gets or sets if position can be auto squared off
        /// </summary>
        [DataMember(Name = "IncludeInAutoSquareoff")]
        public bool IncludeInAutoSquareoff { get; set; }

        /// <summary>
        /// Gets or sets the assigned order types
        /// <see cref="OrderType"/>
        /// </summary>
        [DataMember(Name = "OrderTypesAssigned")]
        public int OrderTypesAssigned { get; set; }

        /// <summary>
        /// Gets or sets the assigned product types
        /// <see cref="ProductType"/>
        /// </summary>
        [DataMember(Name = "ProductsAssigned")]
        public int ProductsAssigned { get; set; }

        /// <summary>
        /// Gets or sets the residential address
        /// </summary>
        [DataMember(Name = "ResidentialAddress")]
        public string ResidentialAddress { get; set; }

        /// <summary>
        /// Gets or sets the last update time
        /// </summary>
        [DataMember(Name = "LastUpdatedOn")]
        public DateTime LastUpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets the last update by
        /// </summary>
        [DataMember(Name = "LastUpdatedBy")]
        public string LastUpdatedBy { get; set; }


        /// <summary>
        /// Gets or sets when the account creation date
        /// </summary>
        [DataMember(Name = "CreatedOn")]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets who created the account
        /// </summary>
        [DataMember(Name = "CreatedBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets if client is investor
        /// </summary>
        [DataMember(Name = "IsInvestorClient")]
        public string IsInvestorClient { get; set; }

        /// <summary>
        /// Gets or sets the client dealer mapping list
        /// </summary>
        [DataMember(Name = "ClientDealerMappingList")]
        public List<ClientDealerMapping> ClientDealerMappingList { get; set; }
    }
}
