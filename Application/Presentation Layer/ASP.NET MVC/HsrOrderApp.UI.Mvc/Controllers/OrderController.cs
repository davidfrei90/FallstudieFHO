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
    public class OrderController : HsrOrderAppController
    {
        // GET: Order
        public ActionResult Index()
        {
            var vm = GetViewModelFromTempData<OrderListViewModel>() ?? new OrderListViewModel();
            vm.DisplayName = Strings.OrderViewModel_DisplayName;
            vm.Items = Service.GetAllOrders().ToList();
            vm.SelectedItem = vm.Items.FirstOrDefault();

            // Finish Action
            StoreViewModelToTempData(vm);
            return View(vm);
        }

        // GET: Order/Details/5
        public ActionResult Details(int id)
        {
            OrderDTO item = Service.GetOrderById(id);
            return DisplayDetails(item);
        }

        // GET: Order/Create
        public ActionResult Create()
        {
            var vm = GetViewModelFromTempData<OrderViewModel>();
            bool needsRefresh = NeedsRefresh(vm, default(int));

            if (needsRefresh)
            {
                RemoveViewModelFromTempData<OrderViewModel>();
                RemoveViewModelFromTempData<OrderDetailViewModel>(typeof(OrderDetailController).FullName);
            }

            return DisplayDetails(vm?.Model ?? new OrderDTO() { OrderStatus = OrderStatus.Draft, OrderDate = DateTime.Now });
        }

        // POST: Order/Create
        [HttpPost]
        public ActionResult Create(OrderViewModel vmChanged, string redirectButton)
        {
            var vm = GetViewModelFromTempData<OrderViewModel>() ?? new OrderViewModel(new OrderDTO(), null, true);
            vm.DisplayName = Strings.OrderViewModel_DisplayName;
            vm.ApplyFormAttributes(vmChanged.Model);

            return StoreEntity(vm, redirectButton);
        }

        // GET: Order/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = GetViewModelFromTempData<OrderViewModel>();
            bool needsRefresh = NeedsRefresh(vm, id);

            if (needsRefresh)
            {
                RemoveViewModelFromTempData<OrderViewModel>();
                RemoveViewModelFromTempData<OrderDetailViewModel>(typeof(OrderDetailController).FullName);

                OrderDTO item = Service.GetOrderById(id);
                return DisplayDetails(item);
            }

            return DisplayDetails(vm.Model);
        }

        // POST: Order/Edit/5
        [HttpPost]
        public ActionResult Edit(OrderViewModel vmChanged, string redirectButton)
        {
            var vm = GetViewModelFromTempData<OrderViewModel>();

            vm.ApplyFormAttributes(vmChanged.Model);

            return StoreEntity(vm, redirectButton);
        }

        // GET: Order/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Service.DeleteOrder(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex);
            }
            return RedirectToAction("Index");
        }

        protected ActionResult DisplayDetails(OrderDTO item)
        {
            var vm = GetViewModelFromTempData<OrderViewModel>() ?? new OrderViewModel(item, null, false);
            vm.DisplayName = Strings.OrderViewModel_DisplayName;
            vm.Model = item;
            vm.IsNew = item.Id <= 0;
            vm.Customers = vm.Customers ?? Service.GetAllCustomers().ToList();
            vm.OrderStates = Enum.GetNames(typeof(OrderStatus)).ToList();

            // Refreshes the OrderDetailViewModel in OrderViewModel
            RefreshOrderDetailViewModel(vm, item);

            // Marks child entity changes in OrderViewModel
            MarkOrderDetailChanges(vm);

            // Finish Action
            StoreViewModelToTempData(vm);
            StoreViewModelToTempData(vm.OrderDetails, typeof(OrderDetailController).FullName);
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

        protected ActionResult StoreEntity(OrderViewModel vm, string redirectButton)
        {
            bool persist = string.IsNullOrEmpty(redirectButton);
            try
            {
                if (ModelState.IsValid && persist)
                {
                    Service.StoreOrder(vm.Model);

                    // Finish Action and go back to Index
                    RemoveViewModelFromTempData<OrderViewModel>();
                    RemoveViewModelFromTempData<OrderDetailViewModel>(typeof(OrderDetailController).FullName);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex);
            }

            // Finish Action without saving
            StoreViewModelToTempData(vm);
            StoreViewModelToTempData(vm.OrderDetails, typeof(OrderDetailController).FullName);

            if (persist)
                return View(vm);
            else
                return Redirect(redirectButton);
        }

        private bool NeedsRefresh(OrderViewModel vm, int id)
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
