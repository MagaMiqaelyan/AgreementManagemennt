using AgreementManagementTask.DataBase;
using AgreementManagementTask.Models;

using System.Linq;

namespace AgreementManagementTask.Services
{
    public class ProductGroupService : ServiceBase<ProductGroup>
    {
        public ProductGroupService(AgreementDbContext agreementDbContext) : base(agreementDbContext)
        {
        }

        public override void Update(ProductGroup productGroup)
        {
            var oldProductGroup = AgreementDbContext.ProductGroups.FirstOrDefault(s => s.Id == productGroup.Id);
            if (oldProductGroup == null) return;
            oldProductGroup.Active = productGroup.Active;
            oldProductGroup.GroupCode = productGroup.GroupCode;
            oldProductGroup.GroupDescription = productGroup.GroupDescription;
            AgreementDbContext.SaveChanges();
        }
    }
}
