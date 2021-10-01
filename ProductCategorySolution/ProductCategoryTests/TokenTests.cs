using FluentAssertions;
using Moq;
using ProductCategory.Service;
using ProductCategory.Service.Auth;
using ProductCategory.Service.Models;
using System;
using Xunit;

namespace ProductCategoryTests
{
    public class TokenTests
    {
        private readonly ITokenService _tokenService;

        public TokenTests() => _tokenService = new TokenService(TestHelper.Configuration);

        /// <summary>
        /// Covering null request case.
        /// </summary>
        [Fact]
        public void CreateToken_ArgumentNullException()
        {
            var exception = Record.Exception(() => _tokenService.CreateToken(It.IsAny<TokenRequestDto>()));
            exception.Should().BeOfType(typeof(ArgumentNullException));
        }

        /// <summary>
        /// Covering wrong password case.
        /// </summary>
        [Fact]
        public void CreateToken_UnauthorizedAccessException()
        {
            var exception = Record.Exception(() => _tokenService.CreateToken(new TokenRequestDto
            {
                Password = "dummy_passwrod"
            }));

            exception.Should().BeOfType(typeof(UnauthorizedAccessException));
        }

        /// <summary>
        /// Covering successful case.
        /// </summary>
        [Fact]
        public void CreateToken_Token()
        {
            string token = _tokenService.CreateToken(new TokenRequestDto
            {
                Password = TestHelper.Configuration.GetSection(TokenConstants.TokenPassword).Value
            });

            token.Should().NotBeNull();
        }
    }
}
