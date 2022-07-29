using AgreementManagementTask.Models;

using Microsoft.AspNetCore.Mvc.Rendering;

namespace AgreementManagementTask.ViewModels
{
    public class AgreementViewModel
    {
        public int ModelId { get; set; }
        public Agreement Agreement { get; set; }
        public SelectList Products { get; set; }
        public SelectList ProductGroups { get; set; }

    }
}
