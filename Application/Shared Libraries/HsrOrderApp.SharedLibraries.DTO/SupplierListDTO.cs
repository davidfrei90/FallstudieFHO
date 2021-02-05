#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO
{
    [DataContract]
    public class SupplierListDTO : DTOBase
    {
        public SupplierListDTO()
        {
            this.Name = string.Empty;
            this.AccountNumber = default(int);
            this.CreditRating = default(int);
            this.PreferredSupplierFlag = default(bool);
            this.ActiveFlag = default(bool);
            this.PurchasingWebServiceURL = string.Empty;
        }
        [DataMember]
        [StringLengthValidator(1, 50)]
        public string Name { get; set; }

        [DataMember]
        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Ignore)]
        public int AccountNumber { get; set; }

        [DataMember]
        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Ignore)]
        public int CreditRating { get; set; }

        [DataMember]
        public bool PreferredSupplierFlag { get; set; }

        [DataMember]
        public bool ActiveFlag { get; set; }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string PurchasingWebServiceURL { get; set; }
    }
}