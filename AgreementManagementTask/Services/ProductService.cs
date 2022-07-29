using AgreementManagementTask.DataBase;
using AgreementManagementTask.Models;

using System.Linq;

namespace AgreementManagementTask.Services
{
    public class ProductService : ServiceBase<Product>
    {
        public ProductService(AgreementDbContext agreementDbContext) : base(agreementDbContext)
        {
        }

        public override void Update(Product product)
        {
            var oldPoroduct = AgreementDbContext.Products.FirstOrDefault(s => s.Id == product.Id);
            if (oldPoroduct == null) return;
            oldPoroduct.Price = product.Price;
            oldPoroduct.ProductDescription = product.ProductDescription;
            oldPoroduct.Active = product.Active;
            oldPoroduct.ProductGroupId = product.ProductGroupId;
            SaveDataBaseChanges();
        }
    }
}
