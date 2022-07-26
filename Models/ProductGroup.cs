using System.ComponentModel;

namespace AgreementManagementTask.Models
{
    public class ProductGroup
    {
        public int Id { get; set; }
        public string GroupDescription { get; set; }
        public int GroupCode { get; set; }
        public bool Active { get; set; }
    }
}
