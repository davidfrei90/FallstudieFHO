using HsrOrderApp.SharedLibraries.DTO;
using HsrOrderApp.SharedLibraries.SharedEnums;
using HsrOrderApp.UI.Mvc.Controllers.Base;
using HsrOrderApp.UI.Mvc.Helpers;
using HsrOrderApp.UI.Mvc.Models;
using HsrOrderApp.UI.Mvc.Resources;
using System;
using System.Linq;
using System.Web.Mvc;

namespace HsrOrderApp.UI.Mvc.Controllers
{
    [CustomAuthorize(RequiredPermissions = new UserPermission[] { UserPermission.ADMIN })]
    public class UserController : HsrOrderAppController
    {
        // GET: User
        public ActionResult Index()
        {
            var vm = GetViewModelFromTempData<UserListViewModel>() ?? new UserListViewModel();
            vm.DisplayName = Strings.UserViewModel_DisplayName;
            vm.Items = Service.GetAllUsers().ToList();
            vm.SelectedItem = vm.Items.FirstOrDefault();

            // Finish Action
            StoreViewModelToTempData(vm);
            return View(vm);
        }

        // GET: User/Details/5
        public ActionResult Details(int id)
        {
            UserDTO item = Service.GetUserById(id);
            return DisplayDetails(item);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            var vm = GetViewModelFromTempData<UserViewModel>();
            bool needsRefresh = NeedsRefresh(vm, default(int));

            if (needsRefresh)
            {
                RemoveViewModelFromTempData<UserViewModel>();
                RemoveViewModelFromTempData<RoleViewModel>(typeof(RoleController).FullName);
            }

            return DisplayDetails(vm?.Model ?? new UserDTO());
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserViewModel vmChanged, string redirectButton)
        {
            var vm = GetViewModelFromTempData<UserViewModel>() ?? new UserViewModel(new UserDTO(), null, true);
            vm.DisplayName = Strings.UserViewModel_DisplayName;
            vm.ApplyFormAttributes(vmChanged.Model);

            return StoreEntity(vm, redirectButton);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = GetViewModelFromTempData<UserViewModel>();
            bool needsRefresh = NeedsRefresh(vm, id);

            if (needsRefresh)
            {
                RemoveViewModelFromTempData<UserViewModel>();
                RemoveViewModelFromTempData<RoleViewModel>(typeof(RoleController).FullName);

                UserDTO item = Service.GetUserById(id);
                return DisplayDetails(item);
            }

            return DisplayDetails(vm.Model);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(UserViewModel vmChanged, string redirectButton)
        {
            var vm = GetViewModelFromTempData<UserViewModel>();

            vm.ApplyFormAttributes(vmChanged.Model);

            return StoreEntity(vm, redirectButton);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Service.DeleteUser(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex);
            }
            return RedirectToAction("Index");
        }

        protected ActionResult DisplayDetails(UserDTO item)
        {
            var vm = GetViewModelFromTempData<UserViewModel>() ?? new UserViewModel(item, null, false);
            vm.DisplayName = Strings.UserViewModel_DisplayName;
            vm.Model = item;
            vm.IsNew = item.Id <= 0;
            vm.Customers = vm.Customers ?? Service.GetAllCustomers().ToList();

            // Refreshes the RoleViewModel in UserViewModel
            RefreshRoleViewModel(vm, item);

            // Marks child entity changes in UserViewModel
            MarkRoleChanges(vm);

            // Finish Action
            StoreViewModelToTempData(vm);
            StoreViewModelToTempData(vm.Roles, typeof(RoleController).FullName);
            return View(vm);
        }

        private void RefreshRoleViewModel(UserViewModel vm, UserDTO item)
        {
            var vmRole = GetViewModelFromTempData<RoleViewModel>(typeof(RoleController).FullName);

            if (vmRole != null && vmRole.LatestControllerAction != ControllerAction.None)
            {
                vm.Roles = vmRole;
            }
            else
            {
                vm.Roles = new RoleViewModel(item.Roles.ToList());
            }

            vm.Roles.IsReadOnly = CurrentActionName == "Details";
            vm.Roles.ReturnController = CurrentControllerName;
            vm.Roles.ReturnAction = CurrentActionName;
            vm.Roles.ReturnId = CurrentParameterId;
        }

        private void MarkRoleChanges(UserViewModel vm)
        {
            switch (vm.Roles.LatestControllerAction)
            {
                case ControllerAction.Create:
                    foreach (var ins in vm.Roles.ItemsToInsert)
                        vm.Model.MarkChildForInsertion(ins);
                    break;
                case ControllerAction.Edit:
                    if (!string.IsNullOrEmpty(ReferrerParameterId))
                        vm.Model.MarkChildForUpdate(vm.Roles.SelectedItem);
                    break;
                case ControllerAction.Delete:
                    foreach (var del in vm.Roles.ItemsToDelete)
                        vm.Model.MarkChildForDeletion(del);
                    break;
            }
        }

        protected ActionResult StoreEntity(UserViewModel vm, string redirectButton)
        {
            bool persist = string.IsNullOrEmpty(redirectButton);
            try
            {
                if (ModelState.IsValid && persist)
                {
                    Service.StoreUser(vm.Model);

                    // Finish Action and go back to Index
                    RemoveViewModelFromTempData<UserViewModel>();
                    RemoveViewModelFromTempData<RoleViewModel>(typeof(RoleController).FullName);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex);
            }

            // Finish Action without saving
            StoreViewModelToTempData(vm);
            StoreViewModelToTempData(vm.Roles, typeof(RoleController).FullName);

            if (persist)
                return View(vm);
            else
                return Redirect(redirectButton);
        }

        private bool NeedsRefresh(UserViewModel vm, int id)
        {
            // True when Id changed
            if (vm?.Model?.Id != id) return true;

            // True when coming from other Controller
            if (ReferrerControllerName == "User" && ReferrerActionName != "Index") return false;
            if (ReferrerControllerName == "Role") return false;
            return true;
        }
    }
}
