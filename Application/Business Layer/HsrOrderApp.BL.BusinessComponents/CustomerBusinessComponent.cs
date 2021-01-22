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
    public class CustomerBusinessComponent
    {
        private ICustomerRepository rep;

        public CustomerBusinessComponent()
        {
        }

        public CustomerBusinessComponent(ICustomerRepository unitOfWork)
        {
            this.rep = unitOfWork;
        }

        public ICustomerRepository Repository
        {
            get { return this.rep; }
            set { this.rep = value; }
        }

        public Customer GetCustomerById(int customerId)
        {
            Customer customer = rep.GetById(customerId);
            return customer;
        }

        public IQueryable<Customer> GetCustomersByCriteria(CustomerSearchType searchType, string city, string customerName)
        {
            IQueryable<Customer> customers = new List<Customer>().AsQueryable();

            switch (searchType)
            {
                case CustomerSearchType.None:
                    customers = rep.GetAll();
                    break;
                case CustomerSearchType.ByCity:
                    customers = rep.GetAll().Where(cu => cu.Addresses.Any(a => a.City == city));
                    break;
                case CustomerSearchType.ByName:
                    customers = rep.GetAll().Where(cu => cu.Name == customerName);
                    break;
            }
            return customers;
        }

        public int StoreCustomer(Customer customer, IEnumerable<ChangeItem> changeItems)
        {
            int customerId = default(int);
            using (TransactionScope transaction = new TransactionScope())
            {
                customerId = rep.SaveCustomer(customer);
                foreach (ChangeItem change in changeItems)
                {
                    if (change.Object is Address)
                    {
                        Address address = (Address) change.Object;
                        switch (change.ChangeType)
                        {
                            case ChangeType.ChildInsert:
                            case ChangeType.ChildUpate:
                                rep.SaveAddress(address, customer);
                                break;
                            case ChangeType.ChildDelete:
                                rep.DeleteAddress(address.AddressId);
                                break;
                        }
                    }
                }
                transaction.Complete();
            }

            return customerId;
        }

        public void DeleteCustomer(int customerId)
        {
            rep.DeleteCustomer(customerId);
        }
    }
}