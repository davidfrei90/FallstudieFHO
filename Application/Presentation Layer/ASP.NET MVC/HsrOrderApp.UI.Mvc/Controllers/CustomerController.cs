using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.Mvc.Controllers.Base;
using HsrOrderApp.UI.Mvc.Helpers;
using HsrOrderApp.UI.Mvc.Models;
using HsrOrderApp.UI.Mvc.Resources;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HsrOrderApp.UI.Mvc.Controllers
{
    [CustomAuthorize(RequiredPermissions = new UserPermission[] { UserPermission.ADMIN, UserPermission.STAFF })]
    public class CustomerController : HsrOrderAppController
    {
        // GET: Customer
        public ActionResult Index()
        {
            var vm = GetViewModelFromTempData<CustomerListViewModel>() ?? new CustomerListViewModel();
            vm.DisplayName = Strings.CustomerViewModel_DisplayName;
            vm.Items = Service.GetAllCustomers().ToList();
            vm.SelectedItem = vm.Items.FirstOrDefault();

            // Finish Action
            StoreViewModelToTempData(vm);
            return View(vm);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            CustomerDTO item = Service.GetCustomerById(id);
            return DisplayDetails(item);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            var vm = GetViewModelFromTempData<CustomerViewModel>();
            bool needsRefresh = NeedsRefresh(vm, default(int));

            if (needsRefresh)
            {
                RemoveViewModelFromTempData<CustomerViewModel>();
                RemoveViewModelFromTempData<AddressViewModel>(typeof(AddressController).FullName);
            }

            return DisplayDetails(vm?.Model ?? new CustomerDTO());
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(CustomerViewModel vmChanged, string redirectButton)
        {
            var vm = GetViewModelFromTempData<CustomerViewModel>() ?? new CustomerViewModel(new CustomerDTO(), null, true);
            vm.DisplayName = Strings.CustomerViewModel_DisplayName;
            vm.ApplyFormAttributes(vmChanged.Model);

            return StoreEntity(vm, redirectButton);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = GetViewModelFromTempData<CustomerViewModel>();
            bool needsRefresh = NeedsRefresh(vm, id);

            if (needsRefresh)
            {
                RemoveViewModelFromTempData<CustomerViewModel>();
                RemoveViewModelFromTempData<AddressViewModel>(typeof(AddressController).FullName);

                CustomerDTO item = Service.GetCustomerById(id);
                return DisplayDetails(item);
            }

            return DisplayDetails(vm.Model);
        }

        // POST: Customer/Edit/5
        [HttpPost]
        public ActionResult Edit(CustomerViewModel vmChanged, string redirectButton)
        {
            var vm = GetViewModelFromTempData<CustomerViewModel>();

            vm.ApplyFormAttributes(vmChanged.Model);

            return StoreEntity(vm, redirectButton);
        }

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Service.DeleteCustomer(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex);
            }
            return RedirectToAction("Index");
        }

        protected ActionResult DisplayDetails(CustomerDTO item)
        {
            var vm = GetViewModelFromTempData<CustomerViewModel>() ?? new CustomerViewModel(item, null, false);
            vm.DisplayName = Strings.CustomerViewModel_DisplayName;
            vm.Model = item;
            vm.IsNew = item.Id <= 0;

            // Refreshes the AddressViewModel in CustomerViewModel
            RefreshAddressViewModel(vm, item);

            // Marks child entity changes in CustomerViewModel
            MarkAddressChanges(vm);

            // Finish Action
            StoreViewModelToTempData(vm);
            StoreViewModelToTempData(vm.Addresses, typeof(AddressController).FullName);
            return View(vm);
        }

        private void RefreshAddressViewModel(CustomerViewModel vm, CustomerDTO item)
        {
            var vmAddress = GetViewModelFromTempData<AddressViewModel>(typeof(AddressController).FullName);

            if (vmAddress != null && vmAddress.LatestAddressAction != ControllerAction.None)
            {
                vm.Addresses = vmAddress;
            }
            else
            {
                vm.Addresses = new AddressViewModel(item.Addresses.ToList());
            }

            vm.Addresses.IsReadOnly = CurrentActionName == "Details";
            vm.Addresses.ReturnController = CurrentControllerName;
            vm.Addresses.ReturnAction = CurrentActionName;
            vm.Addresses.ReturnId = CurrentParameterId;
        }

        private void MarkAddressChanges(CustomerViewModel vm)
        {
            switch (vm.Addresses.LatestAddressAction)
            {
                case ControllerAction.Create:
                    foreach (var ins in vm.Addresses.Items.Where(i => i.Id <= 0))
                        vm.Model.MarkChildForInsertion(ins);
                    break;
                case ControllerAction.Edit:
                    if (!string.IsNullOrEmpty(ReferrerParameterId))
                        vm.Model.MarkChildForUpdate(vm.Addresses.SelectedItem);
                    break;
                case ControllerAction.Delete:
                    foreach (var del in vm.Addresses.ItemsToDelete)
                        vm.Model.MarkChildForDeletion(del);
                    break;
            }
        }

        protected ActionResult StoreEntity(CustomerViewModel vm, string redirectButton)
        {
            bool persist = string.IsNullOrEmpty(redirectButton);
            try
            {
                if (ModelState.IsValid && persist)
                {
                    Service.StoreCustomer(vm.Model);

                    // Finish Action and go back to Index
                    RemoveViewModelFromTempData<CustomerViewModel>();
                    RemoveViewModelFromTempData<AddressViewModel>(typeof(AddressController).FullName);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex);
            }

            // Finish Action without saving
            StoreViewModelToTempData(vm);
            StoreViewModelToTempData(vm.Addresses, typeof(AddressController).FullName);

            if (persist)
                return View(vm);
            else
                return Redirect(redirectButton);
        }

        private bool NeedsRefresh(CustomerViewModel vm, int id)
        {
            // True when Id changed
            if (vm?.Model?.Id != id) return true;

            // True when coming from other Controller
            if (ReferrerControllerName == "Customer" && ReferrerActionName != "Index") return false;
            if (ReferrerControllerName == "Address") return false;
            return true;
        }
    }
}
