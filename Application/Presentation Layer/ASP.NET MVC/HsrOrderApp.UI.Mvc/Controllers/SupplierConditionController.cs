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
    [CustomAuthorize(RequiredPermissions = new UserPermission[] { UserPermission.ADMIN, UserPermission.STAFF })]
    public class SupplierConditionController : HsrOrderAppController
    {
        // GET: Order
        public ActionResult Index()
        {
            var vm = GetViewModelFromTempData<SupplierConditionListViewModel>() ?? new SupplierConditionListViewModel();
            vm.DisplayName = Strings.SupplierConditionViewModel_DisplayName;
            vm.Items = Service.GetAllSupplierConditions().ToList();
            vm.SelectedItem = vm.Items.FirstOrDefault();

            // Finish Action
            StoreViewModelToTempData(vm);
            return View(vm);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            SupplierConditionDTO item = Service.GetSupplierConditionById(id);
            return DisplayDetails(item);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            var vm = GetViewModelFromTempData<SupplierConditionViewModel>();
            bool needsRefresh = NeedsRefresh(vm, default(int));

            if (needsRefresh)
            {
                RemoveViewModelFromTempData<SupplierConditionViewModel>();
                //RemoveViewModelFromTempData<SupplierConditionDetailViewModel>(typeof(SupplierConditionDetailController).FullName);
            }

            return DisplayDetails(vm?.Model ?? new SupplierConditionDTO() );
        }

        // POST: SupplierCondition/Create
        [HttpPost]
        public ActionResult Create(SupplierConditionViewModel vmChanged, string redirectButton)
        {
            var vm = GetViewModelFromTempData<SupplierConditionViewModel>() ?? new SupplierConditionViewModel(new SupplierConditionDTO(), true);
            vm.DisplayName = Strings.SupplierConditionViewModel_DisplayName;
            vm.ApplyFormAttributes(vmChanged.Model);

            return StoreEntity(vm, redirectButton);
        }

        // GET: SupplierCondition/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = GetViewModelFromTempData<SupplierConditionViewModel>();
            bool needsRefresh = NeedsRefresh(vm, id);

            if (needsRefresh)
            {
                RemoveViewModelFromTempData<SupplierConditionViewModel>();
                //RemoveViewModelFromTempData<SupplierConditionDetailViewModel>(typeof(SupplierConditionDetailController).FullName);

                SupplierConditionDTO item = Service.GetSupplierConditionById(id);
                return DisplayDetails(item);
            }

            return DisplayDetails(vm.Model);
        }

        // POST: SupplierCondition/Edit/5
        [HttpPost]
        public ActionResult Edit(SupplierConditionViewModel vmChanged, string redirectButton)
        {
            var vm = GetViewModelFromTempData<SupplierConditionViewModel>();

            vm.ApplyFormAttributes(vmChanged.Model);

            return StoreEntity(vm, redirectButton);
        }

        // GET: SupplierCondition/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Service.DeleteSupplierCondition(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex);
            }
            return RedirectToAction("Index");
        }

        protected ActionResult DisplayDetails(SupplierConditionDTO item)
        {
            var vm = GetViewModelFromTempData<SupplierConditionViewModel>() ?? new SupplierConditionViewModel(item, false);
            vm.DisplayName = Strings.SupplierConditionViewModel_DisplayName;
            vm.Model = item;
            vm.IsNew = item.Id <= 0;
            vm.Suppliers = vm.Suppliers ?? Service.GetAllSuppliers().ToList();
            //vm.OrderStates = Enum.GetNames(typeof(OrderStatus)).ToList();

            // Refreshes the OrderDetailViewModel in OrderViewModel
            //RefreshSupplierConditionDetailViewModel(vm, item);

            // Marks child entity changes in OrderViewModel
            //MarkSupplierConditionDetailChanges(vm);

            // Finish Action
            StoreViewModelToTempData(vm);
            //StoreViewModelToTempData(vm.OrderDetails, typeof(OrderDetailController).FullName);
            return View(vm);
        }

        private void RefreshOrderDetailViewModel(OrderViewModel vm, OrderDTO item)
        {
            var vmOrderDetail = GetViewModelFromTempData<OrderDetailViewModel>(typeof(OrderDetailController).FullName);

            if (vmOrderDetail != null && vmOrderDetail.LatestControllerAction != ControllerAction.None)
            {
                vm.OrderDetails = vmOrderDetail;
            }
            else
            {
                vm.OrderDetails = new OrderDetailViewModel(item.Details.ToList());
            }

            vm.OrderDetails.IsReadOnly = CurrentActionName == "Details";
            vm.OrderDetails.ReturnController = CurrentControllerName;
            vm.OrderDetails.ReturnAction = CurrentActionName;
            vm.OrderDetails.ReturnId = CurrentParameterId;
        }

        private void MarkOrderDetailChanges(OrderViewModel vm)
        {
            switch (vm.OrderDetails.LatestControllerAction)
            {
                case ControllerAction.Create:
                    foreach (var ins in vm.OrderDetails.Items.Where(i => i.Id <= 0))
                        vm.Model.MarkChildForInsertion(ins);
                    break;
                case ControllerAction.Edit:
                    if (!string.IsNullOrEmpty(ReferrerParameterId))
                        vm.Model.MarkChildForUpdate(vm.OrderDetails.SelectedItem);
                    break;
                case ControllerAction.Delete:
                    foreach (var del in vm.OrderDetails.ItemsToDelete)
                        vm.Model.MarkChildForDeletion(del);
                    break;
            }
        }

        protected ActionResult StoreEntity(SupplierConditionViewModel vm, string redirectButton)
        {
            bool persist = string.IsNullOrEmpty(redirectButton);
            try
            {
                if (ModelState.IsValid && persist)
                {
                    Service.StoreSupplierCondition(vm.Model);

                    // Finish Action and go back to Index
                    RemoveViewModelFromTempData<SupplierConditionViewModel>();
                    //RemoveViewModelFromTempData<OrderDetailViewModel>(typeof(OrderDetailController).FullName);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex);
            }

            // Finish Action without saving
            StoreViewModelToTempData(vm);
            //StoreViewModelToTempData(vm.OrderDetails, typeof(OrderDetailController).FullName);

            if (persist)
                return View(vm);
            else
                return Redirect(redirectButton);
        }

        private bool NeedsRefresh(SupplierConditionViewModel vm, int id)
        {
            // True when Id changed
            if (vm?.Model?.Id != id) return true;

            // True when coming from other Controller
            if (ReferrerControllerName == "Order" && ReferrerActionName != "Index") return false;
            if (ReferrerControllerName == "OrderDetail") return false;
            return true;
        }
    }
}
