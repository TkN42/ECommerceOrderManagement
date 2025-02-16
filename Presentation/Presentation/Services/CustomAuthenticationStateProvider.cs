using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Presentation.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public CustomAuthenticationStateProvider(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        // AuthenticationState'yi almak için bu metodu kullanıyoruz.
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            // SignInManager üzerinden kullanıcıyı alıyoruz
            var user = await _userManager.GetUserAsync(_signInManager.Context.User);

            // Eğer kullanıcı varsa, kimlik doğrulaması yapılmışsa, ClaimsPrincipal ile döndür
            if (user != null)
            {
                var claimsPrincipal = await GetClaimsPrincipalAsync(user);
                return new AuthenticationState(claimsPrincipal);
            }

            // Kullanıcı doğrulaması yapılmamışsa boş bir ClaimsPrincipal döndür
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        // Kullanıcının kimlik bilgilerini oluşturuyoruz.
        private async Task<ClaimsPrincipal> GetClaimsPrincipalAsync(IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                // Gerekirse ek claims (roller, izinler vb.) buraya eklenebilir
            };

            var identity = new ClaimsIdentity(claims, "apiauth");
            return new ClaimsPrincipal(identity);
        }

        // Kullanıcı girişini bildiren metod
        public async Task NotifyUserAuthentication(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null)
            {
                var claimsPrincipal = await GetClaimsPrincipalAsync(user);
                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            }
        }

        // Kullanıcı çıkışını bildiren metod
        public Task NotifyUserLogout()
        {
            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            return Task.CompletedTask;
        }
    }
}
