using AgreementManagementTask.DataBase;
using AgreementManagementTask.Models;

using System.Linq;

namespace AgreementManagementTask.Services
{
    public class UserService : ServiceBase<User>
    {
        public UserService(AgreementDbContext agreementDbContext) : base(agreementDbContext)
        {

        }

        public override void Update(User user)
        {
            var oldUser = AgreementDbContext.AgreementUsers.FirstOrDefault(s => s.Id == user.Id);
            if (oldUser == null) return;
            oldUser.UserName = user.UserName;
            oldUser.Id = user.Id;
            //...
            AgreementDbContext.SaveChanges();
        }
    }
}
