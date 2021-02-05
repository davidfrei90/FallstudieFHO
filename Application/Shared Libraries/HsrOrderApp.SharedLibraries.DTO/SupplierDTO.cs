#region

using System.Collections.Generic;
using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO
{
    [DataContract]
    public class SupplierDTO : DTOParentObject
    {
        private IList<AddressDTO> _addresses;
        private string _name;
        private int _accountNumber;
        private int _creditRating;
        private bool _preferredSupplierFlag;
        private bool _activeFlag;
        private string _purchasingWebServiceURL;

        public SupplierDTO()
        {
            this.Name = string.Empty;
            this.AccountNumber = default(int);
            this.CreditRating = default(int);
            this.PreferredSupplierFlag = default(bool);
            this.ActiveFlag = default(bool);
            this.PurchasingWebServiceURL = string.Empty;
            this.Addresses = new List<AddressDTO>();
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string Name
        {
            get { return _name; }
            set
            {
                if (value != _name)
                {
                    this._name = value;
                    OnPropertyChanged(() => Name);
                }
            }
        }

        [DataMember]
        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Ignore)]
        public int AccountNumber
        {
            get { return _accountNumber; }
            set
            {
                if (value != _accountNumber)
                {
                    this._accountNumber = value;
                    OnPropertyChanged(() => AccountNumber);
                }
            }
        }

        [DataMember]
        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Ignore)]
        public int CreditRating
        {
            get { return _creditRating; }
            set
            {
                if (value != _creditRating)
                {
                    this._creditRating = value;
                    OnPropertyChanged(() => CreditRating);
                }
            }
        }

        [DataMember]
        public bool ActiveFlag
        {
            get { return _activeFlag; }
            set
            {
                if (value != _activeFlag)
                {
                    this._activeFlag = value;
                    OnPropertyChanged(() => ActiveFlag);
                }
            }
        }

        [DataMember]
        public bool PreferredSupplierFlag
        {
            get { return _preferredSupplierFlag; }
            set
            {
                if (value != _preferredSupplierFlag)
                {
                    this._preferredSupplierFlag = value;
                    OnPropertyChanged(() => PreferredSupplierFlag);
                }
            }
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string PurchasingWebServiceURL
        {
            get { return _purchasingWebServiceURL; }
            set
            {
                if (value != _purchasingWebServiceURL)
                {
                    this._purchasingWebServiceURL = value;
                    OnPropertyChanged(() => PurchasingWebServiceURL);
                }
            }
        }

        [DataMember]
        [ObjectCollectionValidator(typeof (AddressDTO))]
        public IList<AddressDTO> Addresses
        {
            get { return _addresses; }
            set
            {
                if (value != _addresses)
                {
                    this._addresses = value;
                    OnPropertyChanged(() => Addresses);
                }
            }
        }
    }
}