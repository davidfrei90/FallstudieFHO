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
    public class ProductController : HsrOrderAppController
    {
        // GET: Product
        public ActionResult Index()
        {
            var vm = new ProductListViewModel();
            vm.DisplayName = Strings.ProductViewModel_DisplayName;
            vm.Items = Service.GetAllProducts().ToList();
            vm.SelectedItem = vm.Items.FirstOrDefault();

            // Finish Action
            return View(vm);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            ProductDTO item = Service.GetProductById(id);
            return DisplayDetails(item);
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            ProductDTO item = new ProductDTO();
            return DisplayDetails(item);
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductViewModel vmChanged)
        {
            ProductViewModel vm = DisplayDetails(vmChanged);
            return StoreEntity(vm);
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            ProductDTO item = Service.GetProductById(id);
            return DisplayDetails(item);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductViewModel vmChanged)
        {
            ProductViewModel vm = DisplayDetails(vmChanged);
            return StoreEntity(vm);
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Service.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex);
            }
            return RedirectToAction("Index");
        }

        protected ProductViewModel DisplayDetails(ProductViewModel vmChanged)
        {
            var vm = new ProductViewModel();
            vm.DisplayName = Strings.ProductViewModel_DisplayName;
            vm.Model = vmChanged.Model;

            return vm;
        }
        protected ActionResult DisplayDetails(ProductDTO item)
        {
            var vm = GetViewModelFromTempData<ProductViewModel>() ?? new ProductViewModel();
            vm.DisplayName = Strings.ProductViewModel_DisplayName;
            vm.Model = item;

            // Finish Action
            StoreViewModelToTempData(vm);
            return View(vm);
        }

        protected ActionResult StoreEntity(ProductViewModel vm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Service.StoreProduct(vm.Model);

                    // Finish Action and go back to Index
                    StoreViewModelToTempData(vm);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex);
            }

            // Finish Action without saving
            StoreViewModelToTempData(vm);
            return View(vm);
        }
    }
}
