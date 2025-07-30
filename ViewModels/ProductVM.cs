using firs_dot_net_project.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace firs_dot_net_project.ViewModels
{
    public class ProductVM
    {
        [ValidateNever]
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public Product Product { get; set; }
    }
}
