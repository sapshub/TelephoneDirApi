namespace TelephoneDirApi.DTOs
{
    public class RefreshTokenDTO
    {
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
