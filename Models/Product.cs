using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgreementManagementTask.Models
{
    public class Product
    {
        public int Id { get; set; }
        public int ProductGroupId { get; set; }
        [ForeignKey("ProductGroupId")]
        public ProductGroup ProductGroup { get; set; }
        public string ProductDescription { get; set; }
        public int ProductNumber { get; set; }
        public float Price { get; set; }
        public bool Active { get; set; }
        public ICollection<Agreement> Agreements { get; set; }

    }
}
