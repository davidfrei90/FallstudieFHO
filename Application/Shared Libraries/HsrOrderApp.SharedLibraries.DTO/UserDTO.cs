#region

using System.Collections.Generic;
using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO
{
    [KnownType(typeof (RoleDTO))]
    public class UserDTO : DTOParentObject
    {
        private int _customerId;
        private string _customerName;
        private string _password;
        private IList<RoleDTO> _roles;
        private string _userName;

        public UserDTO()
        {
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.CustomerId = default(int);
            this.CustomerName = string.Empty;
            this.Roles = new List<RoleDTO>();
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (value != _userName)
                {
                    this._userName = value;
                    OnPropertyChanged(() => UserName);
                }
            }
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string Password
        {
            get { return _password; }
            set
            {
                if (value != _password)
                {
                    this._password = value;
                    OnPropertyChanged(() => Password);
                }
            }
        }

        [DataMember]
        public int CustomerId
        {
            get { return _customerId; }
            set
            {
                if (value != _customerId)
                {
                    this._customerId = value;
                    OnPropertyChanged(() => CustomerId);
                }
            }
        }

        [DataMember]
        public string CustomerName
        {
            get { return _customerName; }
            set
            {
                if (value != _customerName)
                {
                    this._customerName = value;
                    OnPropertyChanged(() => CustomerName);
                }
            }
        }

        [DataMember]
        [ObjectCollectionValidator(typeof (RoleDTO))]
        public IList<RoleDTO> Roles
        {
            get { return _roles; }
            set
            {
                if (value != _roles)
                {
                    this._roles = value;
                    OnPropertyChanged(() => Roles);
                }
            }
        }
    }
}