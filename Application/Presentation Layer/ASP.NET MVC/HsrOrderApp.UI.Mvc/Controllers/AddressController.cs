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
    public class AddressController : HsrOrderAppController
    {
        // GET: Address/Details/5
        public ActionResult Details(int id)
        {
            var vm = GetViewModelFromTempData<AddressViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            AddressDTO item = vm.Items.First(i => i.Id == id);
            return DisplayDetails(item);
        }

        // GET: Address/Create
        public ActionResult Create()
        {
            AddressDTO item = new AddressDTO();
            return DisplayDetails(item);
        }

        // POST: Address/Create
        [HttpPost]
        public ActionResult Create(AddressViewModel vmChanged)
        {
            var vm = GetViewModelFromTempData<AddressViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            vm.DisplayName = Strings.AddressDetailViewModel_DisplayName;
            vm.SelectedItem = vmChanged.SelectedItem;
            vm.LatestAddressAction = ControllerAction.Create;

            return StoreEntity(vm);
        }

        // GET: Address/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = GetViewModelFromTempData<AddressViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            AddressDTO item = vm.Items.First(i => i.Id == id);
            return DisplayDetails(item);
        }

        // POST: Address/Edit/5
        [HttpPost]
        public ActionResult Edit(AddressViewModel vmChanged)
        {
            var vm = GetViewModelFromTempData<AddressViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            vm.DisplayName = Strings.AddressDetailViewModel_DisplayName;
            vm.LatestAddressAction = ControllerAction.Edit;
            vm.ApplyFormAttributes(vmChanged.SelectedItem);

            return StoreEntity(vm);
        }

        // GET: Address/Delete/5
        public ActionResult Delete(int id)
        {
            var vm = GetViewModelFromTempData<AddressViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            vm.DisplayName = Strings.AddressDetailViewModel_DisplayName;
            vm.SelectedItem = vm.Items.Single(i => i.Id == id);
            vm.LatestAddressAction = ControllerAction.Delete;

            return StoreEntity(vm);
        }

        protected ActionResult DisplayDetails(AddressDTO item)
        {
            var vm = GetViewModelFromTempData<AddressViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            vm.DisplayName = Strings.AddressDetailViewModel_DisplayName;
            vm.SelectedItem = item;

            // Finish Action
            StoreViewModelToTempData(vm);
            TempData.Keep();
            return View(vm);
        }

        protected ActionResult StoreEntity(AddressViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    switch(vm.LatestAddressAction)
                    {
                        case ControllerAction.Create:
                            vm.Items.Add(vm.SelectedItem);
                            break;
                        case ControllerAction.Edit:
                            int index = vm.Items.FindIndex(i => i.Id == vm.SelectedItem.Id);
                            vm.Items[index] = vm.SelectedItem;
                            break;
                        case ControllerAction.Delete:
                            vm.Items.Remove(vm.SelectedItem);
                            vm.ItemsToDelete.Add(vm.SelectedItem);
                            break;
                    }

                    // Finish Action and go back to Index
                    StoreViewModelToTempData(vm);
                    TempData.Keep();
                    return RedirectToAction(vm.ReturnAction, vm.ReturnController, new { id = vm.ReturnId });
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex);
            }

            // Finish Action without saving
            StoreViewModelToTempData(vm);
            TempData.Keep();
            return View(vm);
        }
    }
}
