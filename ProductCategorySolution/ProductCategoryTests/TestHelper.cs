using Microsoft.Extensions.Configuration;

namespace ProductCategoryTests
{
    internal static class TestHelper
    {
        public static IConfiguration Configuration
            => new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
    }
}
