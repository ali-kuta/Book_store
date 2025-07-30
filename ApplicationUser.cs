using Microsoft.AspNetCore.Identity;

namespace firs_dot_net_project
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        
        public string PhoneNumber { get; set; }
    }

}
