#region

using System.Runtime.Serialization;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Base
{
    [DataContract]
    public class DTOVersionObject : DTOBase
    {
        [DataMember]
        [RangeValidator(typeof (ulong), "0", RangeBoundaryType.Inclusive, null, RangeBoundaryType.Ignore)]
        public ulong Version { get; set; }
    }
}