using AgreementManagementTask.DataBase;
using AgreementManagementTask.Models;

using System.Linq;

namespace AgreementManagementTask.Services
{
    public class AgreementService : ServiceBase<Agreement>
    {
        public AgreementService(AgreementDbContext agreementDbContext) : base(agreementDbContext)
        {

        }

        public override void Update(Agreement agreement)
        {
            var oldAgreement = AgreementDbContext.Agreements.FirstOrDefault(s => s.Id == agreement.Id);
            if (oldAgreement == null) return;
            oldAgreement.EffectiveDate = agreement.EffectiveDate;
            oldAgreement.ExpirationDate = agreement.ExpirationDate;
            oldAgreement.NewPrice = agreement.NewPrice;
            oldAgreement.ProductPrice = agreement.ProductPrice;
            oldAgreement.Product = agreement.Product;
            oldAgreement.ProductId = agreement.ProductId;
            oldAgreement.ProductGroup = agreement.ProductGroup;
            oldAgreement.ProductGroupId = agreement.ProductGroupId;
            SaveDataBaseChanges();
        }
    }

}
