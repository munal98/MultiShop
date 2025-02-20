namespace MultiShop.Basket.LoginServices
{
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
        }

        public string GetUserId
        {
            get
            {
                var subClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("sub");
                return subClaim?.Value ?? string.Empty; // null ise boş bir string döner
            }
        }
    }
}
