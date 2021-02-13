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
            this.AccountNumber = default(int);
            this.Name = string.Empty;
            this.CreditRating = default(int);
            this.PreferredSupplierFlag = default(bool);
            this.ActiveFlag = default(bool);
            this.PurchasingWebServiceURL = string.Empty;
        }


        [DataMember]
        [StringLengthValidator(1, 50)]
        public string Name { get; set; }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string PurchasingWebServiceURL { get; set; }

        [DataMember]
        public bool ActiveFlag { get; set; }

        [DataMember]
        public bool PreferredSupplierFlag { get; set; }

        [DataMember]
        public int CreditRating { get; set; }

        [DataMember]
        public int AccountNumber { get; set; }

        public string FullName
        {
            get
            {
                if (Name == string.Empty)
                    return string.Empty;
                else
                    return Name;
            }
        }
    }
}