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
    [CustomAuthorize(RequiredPermissions = new UserPermission[] { UserPermission.ADMIN, UserPermission.STAFF })]
    public class OrderDetailController : HsrOrderAppController
    {
        // GET: OrderDetail/Details/5
        public ActionResult Details(int id)
        {
            var vm = GetViewModelFromTempData<OrderDetailViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            OrderDetailDTO item = vm.Items.First(i => i.Id == id);
            return DisplayDetails(item);
        }

        // GET: OrderDetail/Create
        public ActionResult Create()
        {
            OrderDetailDTO item = new OrderDetailDTO();
            return DisplayDetails(item);
        }

        // POST: OrderDetail/Create
        [HttpPost]
        public ActionResult Create(OrderDetailViewModel vmChanged)
        {
            var vm = GetViewModelFromTempData<OrderDetailViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            vm.DisplayName = Strings.OrderDetailDetailView_Title;
            vm.SelectedItem = vmChanged.SelectedItem;
            vm.LatestControllerAction = ControllerAction.Create;

            return StoreEntity(vm);
        }

        // GET: OrderDetail/Edit/5
        public ActionResult Edit(int id)
        {
            var vm = GetViewModelFromTempData<OrderDetailViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            OrderDetailDTO item = vm.Items.First(i => i.Id == id);
            return DisplayDetails(item);
        }

        // POST: OrderDetail/Edit/5
        [HttpPost]
        public ActionResult Edit(OrderDetailViewModel vmChanged)
        {
            var vm = GetViewModelFromTempData<OrderDetailViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            vm.DisplayName = Strings.OrderDetailDetailView_Title;
            vm.LatestControllerAction = ControllerAction.Edit;
            vm.ApplyFormAttributes(vmChanged.SelectedItem);

            return StoreEntity(vm);
        }

        // GET: OrderDetail/Delete/5
        public ActionResult Delete(int id)
        {
            var vm = GetViewModelFromTempData<OrderDetailViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            vm.DisplayName = Strings.OrderDetailDetailView_Title;
            vm.SelectedItem = vm.Items.Single(i => i.Id == id);
            vm.LatestControllerAction = ControllerAction.Delete;

            return StoreEntity(vm);
        }

        protected ActionResult DisplayDetails(OrderDetailDTO item)
        {
            var vm = GetViewModelFromTempData<OrderDetailViewModel>();
            if (vm == null) { return RedirectToReferrer(); }

            vm.DisplayName = Strings.OrderDetailDetailView_Title;
            vm.SelectedItem = item;
            vm.Products = vm.Products ?? Service.GetAllProducts().ToList();

            // Finish Action
            StoreViewModelToTempData(vm);
            TempData.Keep();
            return View(vm);
        }

        protected ActionResult StoreEntity(OrderDetailViewModel vm)
        {
            try
            {
                CalculateProductValues(vm);

                if (ModelState.IsValid)
                {
                    switch(vm.LatestControllerAction)
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

        private void CalculateProductValues(OrderDetailViewModel vm)
        {
            if (vm.SelectedItem == null) return;

            var product = vm.Products.Single(i => i.Id == vm.SelectedItem.ProductId);
            vm.SelectedItem.ProductName = product.Name;
            vm.SelectedItem.UnitPrice = product.ListUnitPrice;
            vm.EstimatedDeliveryTime = CalculateEstimatedDeliveryTime(product.Id);
        }

        private string CalculateEstimatedDeliveryTime(int productId)
        {
            int unitsAvailable = default(int);
            int deliveryTime = -1;
            if (productId > 0)
            {
                Service.GetEstimatedDeliveryTime(productId, out unitsAvailable, out deliveryTime);
            }
            if (unitsAvailable > 0 || deliveryTime == -1)
            {
                deliveryTime = ConfigurationHelper.DeliveryTimeFromStock;
            }
            if (deliveryTime > 1)
                return string.Format(Strings.DeliveryTimeFromStock_Plural, deliveryTime);
            else if (deliveryTime == 1)
                return Strings.DeliveryTimeFromStock_Singular;
            else
                return Strings.DeliveryTimeFromStock_Unknown;
        }
    }
}
