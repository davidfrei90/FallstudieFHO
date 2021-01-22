#region

using HsrOrderApp.BL.DomainModel.HelperObjects;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.BL.DomainModel
{
    public class Address : DomainObject
    {
        public Address()
        {
            this.AddressId = default(int);
            this.AddressLine1 = string.Empty;
            this.AddressLine2 = string.Empty;
            this.PostalCode = string.Empty;
            this.City = string.Empty;
            this.Phone = string.Empty;
            this.Email = string.Empty;
        }

        public int AddressId { get; set; }

        [StringLengthValidator(1, 60)]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        [StringLengthValidator(1, 15)]
        public string PostalCode { get; set; }

        [StringLengthValidator(1, 50)]
        public string City { get; set; }

        [StringLengthValidator(1, 50)]
        public string Phone { get; set; }

        [StringLengthValidator(1, 50)]
        public string Email { get; set; }
    }
}