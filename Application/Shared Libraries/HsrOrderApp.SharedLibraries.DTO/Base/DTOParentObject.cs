#region

using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.ChangeSet;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Base
{
    [DataContract]
    public class DTOParentObject : DTOVersionObject
    {
        private List<ChangeItem> _changeSet = new List<ChangeItem>();

        public DTOParentObject()
        {
        }

        [DataMember]
        public List<ChangeItem> Changes
        {
            get
            {
                if (this._changeSet == null)
                    _changeSet = new List<ChangeItem>();
                return this._changeSet;
            }
        }


        public void MarkChildForInsertion<T>(T child) where T : DTOVersionObject
        {
            if (Changes.FirstOrDefault(c => c.Object == child && c.ChangeType == ChangeType.ChildInsert) == null)
                Changes.Add(new ChangeItem(child, ChangeType.ChildInsert));
        }

        public void MarkChildForUpdate(DTOVersionObject child)
        {
            //if (Changes.FirstOrDefault(c => c.Object == child && (c.ChangeType == ChangeType.ChildUpate || c.ChangeType == ChangeType.ChildInsert)) == null)
            ChangeItem oldObject = Changes.FirstOrDefault(c => c.Object.Id == child.Id && (c.ChangeType == ChangeType.ChildUpate || c.ChangeType == ChangeType.ChildInsert));
            if (oldObject != null)
                Changes.Remove(oldObject);
            Changes.Add(new ChangeItem(child, ChangeType.ChildUpate));
        }

        public void MarkChildForDeletion(DTOVersionObject child)
        {
            if (Changes.FirstOrDefault(c => c.Object == child && c.ChangeType == ChangeType.ChildDelete) == null)
            {
                Changes.Add(new ChangeItem(child, ChangeType.ChildDelete));
                Changes.RemoveAll(c => c.Object == child && c.ChangeType != ChangeType.ChildDelete);
            }
        }
    }
}