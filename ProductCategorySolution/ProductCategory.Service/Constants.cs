namespace ProductCategory.Service
{
    public struct TokenConstants
    {
        public const string TokenIssuer = "Jwt:Issuer";
        public const string TokenAudience = "Jwt:Audience";
        public const string TokenSigningKey = "Jwt:SigningKey";
        public const string TokenPassword = "Jwt:TokenPassword";
    }

    public struct DatabaseConstants
    {
        public const string DatabaseName = "MongoDb";
        public const string RedisEndpoint = "Endpoints:Redis";
    }

    public struct GeneralConstants
    {
        public const string HealthcheckEndpoint = "/hc";
    }

    public struct ErrorMessageConstants
    {
        public const string General = "Something went wrong, please try again later.";
        public const string BadRequest = "Your request has failed ! Please check your parameters.";
        public const string Unauthorized = "You are not allowed to access this resource.";
    }
}
