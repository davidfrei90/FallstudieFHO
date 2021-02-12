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
    public class SupplierController : HsrOrderAppController
    {
        // GET: Supplier
        public ActionResult Index()
        {
            var vm = GetViewModelFromTempData<SupplierListViewModel>() ?? new SupplierListViewModel();
            vm.DisplayName = Strings.SupplierViewModel_DisplayName;
            vm.Items = Service.GetAllSuppliers().ToList();
            vm.SelectedItem = vm.Items.FirstOrDefault();

            // Finish Action
            StoreViewModelToTempData(vm);
            return View(vm);
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            SupplierDTO item = Service.GetSupplierById(id);
            return DisplayDetails(item);
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            var vm = GetViewModelFromTempData<SupplierViewModel>();
            bool needsRefresh = NeedsRefresh(vm, default(int));

            if (needsRefresh)
            {
                RemoveViewModelFromTempData<SupplierViewModel>();
                RemoveViewModelFromTempData<AddressViewModel>(typeof(AddressController).FullName);
            }

            return DisplayDetails(vm?.Model ?? new SupplierDTO());
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(SupplierViewModel vmChanged, string redirectButton)
        {
            var vm = GetViewModelFromTempData<SupplierViewModel>() ?? new SupplierViewModel(new SupplierDTO(), null, true);
            vm.DisplayName = Strings.SupplierViewModel_DisplayName;
            vm.ApplyFormAttributes(vmChanged.Model);

            return StoreEntity(vm, redirectButton);
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = GetViewModelFromTempData<SupplierViewModel>();
            bool needsRefresh = NeedsRefresh(vm, id);

            if (needsRefresh)
            {
                RemoveViewModelFromTempData<SupplierViewModel>();
                RemoveViewModelFromTempData<AddressViewModel>(typeof(AddressController).FullName);

                SupplierDTO item = Service.GetSupplierById(id);
                return DisplayDetails(item);
            }

            return DisplayDetails(vm.Model);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(SupplierViewModel vmChanged, string redirectButton)
        {
            var vm = GetViewModelFromTempData<SupplierViewModel>();

            vm.ApplyFormAttributes(vmChanged.Model);

            return StoreEntity(vm, redirectButton);
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Service.DeleteSupplier(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex);
            }
            return RedirectToAction("Index");
        }

        protected ActionResult DisplayDetails(SupplierDTO item)
        {
            var vm = GetViewModelFromTempData<SupplierViewModel>() ?? new SupplierViewModel(item, null, false);
            vm.DisplayName = Strings.SupplierViewModel_DisplayName;
            vm.Model = item;
            vm.IsNew = item.Id <= 0;

            // Refreshes the AddressViewModel in SupplierViewModel
            RefreshAddressViewModel(vm, item);

            // Marks child entity changes in SupplierViewModel
            MarkAddressChanges(vm);

            // Finish Action
            StoreViewModelToTempData(vm);
            StoreViewModelToTempData(vm.Addresses, typeof(AddressController).FullName);
            return View(vm);
        }

        private void RefreshAddressViewModel(SupplierViewModel vm, SupplierDTO item)
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

        private void MarkAddressChanges(SupplierViewModel vm)
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

        protected ActionResult StoreEntity(SupplierViewModel vm, string redirectButton)
        {
            bool persist = string.IsNullOrEmpty(redirectButton);
            try
            {
                if (ModelState.IsValid && persist)
                {
                    Service.StoreSupplier(vm.Model);

                    // Finish Action and go back to Index
                    RemoveViewModelFromTempData<SupplierViewModel>();
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

        private bool NeedsRefresh(SupplierViewModel vm, int id)
        {
            // True when Id changed
            if (vm?.Model?.Id != id) return true;

            // True when coming from other Controller
            if (ReferrerControllerName == "Supplier" && ReferrerActionName != "Index") return false;
            if (ReferrerControllerName == "Address") return false;
            return true;
        }
    }
}
