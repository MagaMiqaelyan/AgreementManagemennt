using AgreementManagementTask.Models;
using AgreementManagementTask.Services;
using AgreementManagementTask.ViewModels;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Linq;

namespace AgreementManagementTask.Controllers
{
    public class AgreementController : Controller
    {
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly IService<Agreement> _agreementService;
        private readonly IService<Product> _productService;
        private readonly IService<ProductGroup> _productGroupService;

        public AgreementController(UserManager<IdentityUser<int>> userManager,
            IService<Agreement> agreementService,
            IService<Product> productService,
            IService<ProductGroup> productGroupService)
        {
            _userManager = userManager;
            _agreementService = agreementService;
            _productGroupService = productGroupService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoadData()
        {
            return Json(new
            {
                data = _agreementService.GetAll(includedTypes: new IncludedType[] { IncludedType.ProductGroup, IncludedType.Product, IncludedType.User })
            });
        }

        [HttpPost]
        public IActionResult Delete(int? id)
        {
            if (id == null) Json(new { success = false, message = "Not Deleted !!!" });
            var deletedItem = _agreementService.Get(id.Value);
            if (deletedItem == null)
                return Json(new { success = false, message = "Something Went Wrong !!!" });

            _agreementService.Remove(deletedItem);
            _agreementService.SaveDataBaseChanges();
            return Json(new { success = true, message = "Delete Successful" });
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var productGroups = _productGroupService.GetAll();
            var products = _productService.GetAll();

            var agreementViewModel = new AgreementViewModel()
            {
                Agreement = new Agreement(),
                ProductGroups = new SelectList(productGroups, nameof(ProductGroup.Id), nameof(ProductGroup.GroupCode)),
                Products = new SelectList(products, nameof(Product.Id), nameof(Product.ProductNumber)),
            };

            if (id != null)
            {
                agreementViewModel.ModelId = id.Value;
                agreementViewModel.Agreement = _agreementService.FirstOrDefault(x => x.Id == id, includedTypes: new IncludedType[] { IncludedType.ProductGroup, IncludedType.Product, IncludedType.User });
            }
            return View(agreementViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Agreement agreement)
        {
            if (!ModelState.IsValid) return View(agreement);

            if (agreement.Id == 0)
            {
                agreement.UserId = int.Parse(_userManager.GetUserId(User));
                agreement.Product = null;
                _agreementService.Add(agreement);
            }
            else
            {
                agreement.Product = null;
                _agreementService.Update(agreement);
            }
            _agreementService.SaveDataBaseChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
