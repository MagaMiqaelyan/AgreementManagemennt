using AgreementManagementTask.Models;
using AgreementManagementTask.Services;

using Microsoft.AspNetCore.Mvc;

namespace AgreementManagementTask.Controllers
{
    public class ProductGroupController : Controller
    {
        private readonly IService<ProductGroup> _productGroupService;

        public ProductGroupController(IService<ProductGroup> productGroupService)
        {
            _productGroupService = productGroupService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoadData()
        {
            return Json(new { data = _productGroupService.GetAll() });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var deletedItem = _productGroupService.Get(id);
            if (deletedItem == null)
            {
                return Json(new { success = false, message = "Something Went Wrong !!!" });
            }

            _productGroupService.Remove(deletedItem);
            _productGroupService.SaveDataBaseChanges();
            return Json(new { success = true, message = "Delete Successful" });
        }

        public IActionResult Edit(int? id)
        {
            var productGroups = _productGroupService.GetAll();

            var productGroup = new ProductGroup();
            if (id != null)
                productGroup = _productGroupService.FirstOrDefault(x => x.Id == id);

            return View(productGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProductGroup productGroup)
        {
            if (!ModelState.IsValid) return View(productGroup);

            if (productGroup.Id == 0)
                _productGroupService.Add(productGroup);
            else
                _productGroupService.Update(productGroup);

            _productGroupService.SaveDataBaseChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
