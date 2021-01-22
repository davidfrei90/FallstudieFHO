using HsrOrderApp.BL.DomainModel.HelperObjects;
using HsrOrderApp.SharedLibraries.SharedEnums;

namespace HsrOrderApp.BL.BusinessComponents
{
    public class ChangeItem
    {
        public ChangeItem(ChangeType type, DomainObject obj)
        {
            this.Object = obj;
            this.ChangeType = type;
        }

        public DomainObject Object { get; set; }
        public ChangeType ChangeType { get; set; }
    }
}