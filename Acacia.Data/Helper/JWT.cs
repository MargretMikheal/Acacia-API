namespace Acacia.Data.Helper
{
    public class JWT
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double AccessTokenDurationInDays { get; set; }
        public double RefreshTokenDurationInDays { get; set; }

        public bool validateAudience { get; set; }
        public bool validateIssure { get; set; }
        public bool validateLifeTime { get; set; }
        public bool validateIssureSignInKey { get; set; }
    }
}
