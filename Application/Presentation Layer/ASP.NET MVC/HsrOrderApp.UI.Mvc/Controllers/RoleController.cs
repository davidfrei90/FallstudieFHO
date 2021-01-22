using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.UI.Mvc.Controllers.Base;
using HsrOrderApp.UI.Mvc.Helpers;
using HsrOrderApp.UI.Mvc.Models;
using HsrOrderApp.UI.Mvc.Resources;
using HsrOrderApp.UI.PresentationLogic.Helpers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HsrOrderApp.UI.Mvc.Controllers
{
    [CustomAuthorize(RequiredPermissions = new UserPermission[] { UserPermission.ADMIN })]
    public class RoleController : HsrOrderAppController
    {
        // GET: Role/Details/5
        public ActionResult Details(int id)
        {
            var vm = GetViewModelFromTempData<RoleViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            RoleDTO item = vm.Items.First(i => i.Id == id);
            return DisplayDetails(item);
        }

        // GET: Role/Create
        public ActionResult Create()
        {
            RoleDTO item = new RoleDTO();
            return DisplayDetails(item);
        }

        // POST: Role/Create
        [HttpPost]
        public ActionResult Create(RoleViewModel vmChanged)
        {
            var vm = GetViewModelFromTempData<RoleViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            vm.DisplayName = Strings.RoleDetailView_Title;
            vm.SelectedItem = vmChanged.SelectedItem;
            vm.LatestControllerAction = ControllerAction.Create;

            return StoreEntity(vm);
        }

        // GET: Role/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = GetViewModelFromTempData<RoleViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            RoleDTO item = vm.Items.First(i => i.Id == id);
            return DisplayDetails(item);
        }

        // POST: Role/Edit/5
        [HttpPost]
        public ActionResult Edit(RoleViewModel vmChanged)
        {
            var vm = GetViewModelFromTempData<RoleViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            vm.DisplayName = Strings.RoleDetailView_Title;
            vm.LatestControllerAction = ControllerAction.Edit;
            vm.ApplyFormAttributes(vmChanged.SelectedItem);

            return StoreEntity(vm);
        }

        // GET: Role/Delete/5
        public ActionResult Delete(int id)
        {
            var vm = GetViewModelFromTempData<RoleViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            vm.DisplayName = Strings.RoleDetailView_Title;
            vm.SelectedItem = vm.Items.Single(i => i.Id == id);
            vm.LatestControllerAction = ControllerAction.Delete;

            return StoreEntity(vm);
        }

        protected ActionResult DisplayDetails(RoleDTO item)
        {
            var vm = GetViewModelFromTempData<RoleViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            vm.DisplayName = Strings.RoleDetailView_Title;
            vm.SelectedItem = item;

            var vmUser = GetViewModelFromTempData<UserViewModel>(typeof(UserController).FullName);
            vm.Roles = vm.Roles ?? Service
                    .GetAllRoles()
                    .Where(r1 => !vmUser.Roles.Items.Exists(r2 => r2.RoleName == r1.RoleName))
                    .ToList();

            // Finish Action
            StoreViewModelToTempData(vm);
            TempData.Keep();
            return View(vm);
        }

        protected ActionResult StoreEntity(RoleViewModel vm)
        {
            try
            {
                CalculateRoleValues(vm);

                if (ModelState.IsValid)
                {
                    switch (vm.LatestControllerAction)
                    {
                        case ControllerAction.Create:
                        case ControllerAction.Edit:
                            if (!vm.Items.Exists(i => i.Id == vm.SelectedItem.Id))
                            {
                                vm.Items.Add(vm.SelectedItem);
                                vm.ItemsToInsert.Add(vm.SelectedItem);
                            }
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
        private void CalculateRoleValues(RoleViewModel vm)
        {
            if (vm.SelectedItem == null) return;

            var role = vm.Roles.Single(i => i.Id == vm.SelectedItem.Id);
            vm.SelectedItem.RoleName = role.RoleName;
        }

    }
}
