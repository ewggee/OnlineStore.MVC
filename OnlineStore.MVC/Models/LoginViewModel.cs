using Microsoft.AspNetCore.Authentication;

namespace OnlineStore.MVC.Models
{
    public sealed class LoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public IEnumerable<AuthenticationScheme> Shemes { get; set; }
    }
}
