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
	}
}
