using ProductCategory.Service.Models;

namespace ProductCategory.Service.Auth
{
    public interface ITokenService
    {
        string CreateToken(TokenRequestDto password);
    }
}
