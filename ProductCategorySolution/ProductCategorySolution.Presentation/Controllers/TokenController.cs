using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCategory.Service.Auth;
using ProductCategory.Service.Models;
using ProductCategorySolution.Presentation.Controllers.Base;

namespace ProductCategorySolution.Presentation.Controllers
{
    [AllowAnonymous]
    public class TokenController : HepsiController
    {
        private readonly ITokenService _tokenService;

        public TokenController(ITokenService tokenService) => _tokenService = tokenService;

        [HttpPost, Route("get-token")]
        public string CreateToken(TokenRequestDto tokenRequestDto) => _tokenService.CreateToken(tokenRequestDto);
    }
}
