#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO
{
    [DataContract]
    public class AddressDTO : DTOVersionObject
    {
        private string _addressLine1;
        private string _addressLine2;
        private string _city;
        private string _email;
        private string _phone;
        private string _postalCode;

        public AddressDTO()
        {
            this.AddressLine1 = string.Empty;
            this.AddressLine2 = string.Empty;
            this.PostalCode = string.Empty;
            this.City = string.Empty;
            this.Phone = string.Empty;
            this.Email = string.Empty;
        }


        [DataMember]
        [StringLengthValidator(1, 50)]
        public string AddressLine1
        {
            get { return _addressLine1; }
            set
            {
                if (value != _addressLine1)
                {
                    this._addressLine1 = value;
                    OnPropertyChanged(() => AddressLine1);
                }
            }
        }

        [DataMember]
        public string AddressLine2
        {
            get { return _addressLine2; }
            set
            {
                if (value != _addressLine2)
                {
                    this._addressLine2 = value;
                    OnPropertyChanged(() => AddressLine2);
                }
            }
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string PostalCode
        {
            get { return _postalCode; }
            set
            {
                if (value != _postalCode)
                {
                    this._postalCode = value;
                    OnPropertyChanged(() => PostalCode);
                }
            }
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string City
        {
            get { return _city; }
            set
            {
                if (value != _city)
                {
                    this._city = value;
                    OnPropertyChanged(() => City);
                }
            }
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string Phone
        {
            get { return _phone; }
            set
            {
                if (value != _phone)
                {
                    this._phone = value;
                    OnPropertyChanged(() => Phone);
                }
            }
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string Email
        {
            get { return _email; }
            set
            {
                if (value != _email)
                {
                    this._email = value;
                    OnPropertyChanged(() => Email);
                }
            }
        }
    }
}