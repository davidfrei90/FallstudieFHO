#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.ChangeSet
{
    [DataContract]
    public class ChangeItem
    {
        public ChangeItem(DTOVersionObject obj, ChangeType type)
        {
            this.Object = obj;
            this.ChangeType = type;
        }

        [DataMember]
        public DTOVersionObject Object { get; set; }

        [DataMember]
        public ChangeType ChangeType { get; set; }
    }
}