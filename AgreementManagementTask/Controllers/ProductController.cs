using AgreementManagementTask.Models;
using AgreementManagementTask.Services;
using AgreementManagementTask.ViewModels;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Linq;

namespace AgreementManagementTask.Controllers
{
    public class ProductController : Controller
    {
        private readonly IService<Product> _productService;
        private readonly IService<ProductGroup> _productGroupService;

        public ProductController(IService<Product> productService, IService<ProductGroup> productGroupService)
        {
            _productGroupService = productGroupService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoadData()
        {
            return Json(new { data = _productService.GetAll(includedTypes: IncludedType.ProductGroup) });
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == null) Json(new { success = false, message = "Not Deleted !!!" });
            var deletedItem = _productService.Get(id.Value);
            if (deletedItem == null)
            {
                return Json(new { success = false, message = "Something Went Wrong !!!" });
            }

            _productService.Remove(deletedItem);
            _productService.SaveDataBaseChanges();
            return Json(new { success = true, message = "Delete Successful" });
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var productGroups = _productGroupService.GetAll();

            var productViewModel = new ProductViewModel()
            {
                Product = new Product(),
                ProductGroups = new SelectList(productGroups, nameof(ProductGroup.Id), nameof(ProductGroup.GroupCode)),

            };

            if (id != null)
                productViewModel.Product = _productService.FirstOrDefault(x => x.Id == id, IncludedType.ProductGroup);

            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Product product)
        {
            if (!ModelState.IsValid) return View(product);
            if (product.Id == 0)
                _productService.Add(product);
            else
                _productService.Update(product);

            _productService.SaveDataBaseChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
