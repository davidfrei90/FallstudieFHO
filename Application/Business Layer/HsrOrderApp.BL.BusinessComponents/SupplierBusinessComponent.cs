#region

using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.DAL.Data.Repositories;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.BL.BusinessComponents
{
    public class SupplierBusinessComponent
    {
        private ISupplierRepository rep;

        public SupplierBusinessComponent()
        {
        }

        public SupplierBusinessComponent(ISupplierRepository unitOfWork)
        {
            this.rep = unitOfWork;
        }

        public ISupplierRepository Repository
        {
            get { return this.rep; }
            set { this.rep = value; }
        }

        public Supplier GetSupplierById(int supplierId)
        {
            Supplier supplier = rep.GetById(supplierId);
            return supplier;
        }

        public IQueryable<Supplier> GetSuppliersByCriteria(SupplierSearchType searchType, string city, string supplierName)
        {
            IQueryable<Supplier> suppliers = new List<Supplier>().AsQueryable();

            switch (searchType)
            {
                case SupplierSearchType.None:
                    suppliers = rep.GetAll();
                    break;
                case SupplierSearchType.ByCity:
                    suppliers = rep.GetAll().Where(cu => cu.Addresses.Any(a => a.City == city));
                    break;
                case SupplierSearchType.ByName:
                    suppliers = rep.GetAll().Where(cu => cu.Name == supplierName);
                    break;
            }
            return suppliers;
        }

        public int StoreSupplier(Supplier supplier, IEnumerable<ChangeItem> changeItems)
        {
            int supplierId = default(int);
            using (TransactionScope transaction = new TransactionScope())
            {
                supplierId = rep.SaveSupplier(supplier);
                foreach (ChangeItem change in changeItems)
                {
                    if (change.Object is Address)
                    {
                        Address address = (Address) change.Object;
                        switch (change.ChangeType)
                        {
                            case ChangeType.ChildInsert:
                            case ChangeType.ChildUpate:
                                rep.SaveAddress(address, supplier);
                                break;
                            case ChangeType.ChildDelete:
                                rep.DeleteAddress(address.AddressId);
                                break;
                        }
                    }
                }
                transaction.Complete();
            }

            return supplierId;
        }

        public void DeleteSupplier(int supplierId)
        {
            rep.DeleteSupplier(supplierId);
        }
    }
}