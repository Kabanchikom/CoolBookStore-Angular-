namespace Web.Areas.Account.Options;

public class AuthOptions
{
    public class AccessTokenData
    {
        public int ExpTimeInMinutes { get; set; }
        public string SecretKey { get; set; }
    }
    
    public class RefreshTokenData
    {
        public int ExpTimeInDays { get; set; }
        public string SecretKey { get; set; }
    }
    
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public AccessTokenData AccessToken { get; set; }
    public RefreshTokenData RefreshToken { get; set; }
}