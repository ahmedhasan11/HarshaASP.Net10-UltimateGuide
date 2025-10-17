namespace CitiesManager.Web.DTO
{
	public class AuthenticationResponse
	{
		//here wewillput the propertes that youwill return as response after the token 
		//have been generated
		public string? PersonName { get; set; }
		public string? Email { get; set; }
		public string? Token { get; set; }
		public DateTime Expiration { get; set; }

		public string? RefreshToken { get; set; }
		public DateTime RefreshTokenExpirationDateTime { get; set; }
	}
}
