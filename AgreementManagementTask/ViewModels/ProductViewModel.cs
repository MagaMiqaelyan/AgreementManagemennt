using AgreementManagementTask.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgreementManagementTask.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }
        public SelectList ProductGroups { get; set; }
    }
}
