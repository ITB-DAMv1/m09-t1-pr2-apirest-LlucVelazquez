using System.IdentityModel.Tokens.Jwt;

namespace ClientWebRP.Tools
{
    public class TokenHelper
    {
        public static bool IsTokenSession(string token)
        {
            return !string.IsNullOrEmpty(token) && !IsTokenExpired(token);
        }

        public static bool IsTokenExpired(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (string.IsNullOrWhiteSpace(token) || !handler.CanReadToken(token))
            {
                return true; // Treat as expired if token is invalid or not readable
            }

            var jwt = handler.ReadJwtToken(token);
            var expiration = jwt.ValidTo;
            return expiration < DateTime.UtcNow;
        }
        public static string GetTokenExpiration(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (string.IsNullOrWhiteSpace(token) || !handler.CanReadToken(token))
            {
                return "Invalid token";
            }
            var jwt = handler.ReadJwtToken(token);
            var expiration = jwt.ValidTo;
            return expiration.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static bool IsTokenAdmin(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (string.IsNullOrWhiteSpace(token) || !handler.CanReadToken(token))
            {
                return false; // Treat as not admin if token is invalid or not readable
            }
            var jwt = handler.ReadJwtToken(token);
            var roleClaim = jwt.Claims.FirstOrDefault(c => c.Type == "role");
            return roleClaim != null && roleClaim.Value == "Admin";
        }
        public static string GetUserName(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            if (string.IsNullOrWhiteSpace(token) || !handler.CanReadToken(token))
            {
                return "Invalid token";
            }
            var jwt = handler.ReadJwtToken(token);
            var nameClaim = jwt.Claims.FirstOrDefault(c => c.Type == "name");
            return nameClaim?.Value ?? "No name claim found";
        }
    }
}
