#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO
{
    [DataContract]
    public class CustomerListDTO : DTOBase
    {
        public CustomerListDTO()
        {
            this.Name = string.Empty;
            this.FirstName = string.Empty;
            this.NumberOfTotalOrders = default(int);
            this.NumberOfOpenOrders = default(int);
        }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string Name { get; set; }

        [DataMember]
        [StringLengthValidator(1, 50)]
        public string FirstName { get; set; }

        [DataMember]
        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Ignore)]
        public int NumberOfTotalOrders { get; set; }

        [DataMember]
        [RangeValidator(0, RangeBoundaryType.Inclusive, int.MaxValue, RangeBoundaryType.Ignore)]
        public int NumberOfOpenOrders { get; set; }

        public string FullName
        {
            get
            {
                if (Name == string.Empty && FirstName == string.Empty)
                    return string.Empty;
                if (FirstName == string.Empty)
                    return Name;
                else
                    return Name + ", " + FirstName;
            }
        }
    }
}