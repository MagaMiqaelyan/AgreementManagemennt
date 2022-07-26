using AgreementManagementTask.DataBase;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgreementManagementTask.Services
{
    public class AgreementService : ServiceBase
    {
        public AgreementService(AgreementDbContext agreementDbContext) : base(agreementDbContext)
        {

        }

        public override void Update()
        {

        }
    }

    public abstract class ServiceBase : IService
    {
        protected AgreementDbContext AgreementDbContext { get; }
        public ServiceBase(AgreementDbContext agreementDbContext)
        {
            AgreementDbContext = agreementDbContext;
        }
        public abstract void Update();

        public void Add()
        {
            AgreementDbContext.ProductGroups.Add(new Models.ProductGroup()
            {
                GroupCode = 2456,
                GroupDescription = "sdfdsfsdf",
                Active = true,
            });
            AgreementDbContext.SaveChanges();
        }

        public void Remove()
        {

        }

    }

    public interface IService
    {
        void Add();
        void Remove();
        void Update();
    }
}
