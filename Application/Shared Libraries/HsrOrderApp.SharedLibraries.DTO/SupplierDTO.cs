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
        private int _accountnumber;
        private string _name;
        private int _creditrating;
            private bool _preferredsupplierflag;
                private bool _activeflag;
                    private string _purchasingwebserviceurl;



        public SupplierDTO()
        {
            this.AccountNumber = default(int);
            this.Name = string.Empty;
            this.CreditRating = default(int);
            this.PreferredSupplierFlag = default(bool);
            this.ActiveFlag = default(bool);
            this.PurchasingWebServiceURL = string.Empty;
            this.Addresses = new List<AddressDTO>();
        }



        [DataMember]
        public bool ActiveFlag
        {
            get { return _activeflag; }
            set
            {
                if (value != _activeflag)
                {
                    this._activeflag = value;
                    OnPropertyChanged(() => ActiveFlag);
                }
            }
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string PurchasingWebServiceURL
        {
            get { return _purchasingwebserviceurl; }
            set
            {
                if (value != _purchasingwebserviceurl)
                {
                    this._purchasingwebserviceurl = value;
                    OnPropertyChanged(() => PurchasingWebServiceURL);
                }
            }
        }

        [DataMember]
        public bool PreferredSupplierFlag
        {
            get { return _preferredsupplierflag; }
            set
            {
                if (value != _preferredsupplierflag)
                {
                    this._preferredsupplierflag = value;
                    OnPropertyChanged(() => PreferredSupplierFlag);
                }
            }
        }

        [DataMember]
        public int CreditRating {
            get
            {
                return _creditrating;
            }
            set
            {
                if (value != _creditrating)
                {
                    this._creditrating = value;
                    OnPropertyChanged(() => CreditRating);
                }
            }
        }

        [DataMember]
        public int AccountNumber
        {
            get { return _accountnumber; }
            set
            {
                if (value != _accountnumber)
                {
                    this._accountnumber = value;
                    OnPropertyChanged(() => AccountNumber);
                }
            }
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