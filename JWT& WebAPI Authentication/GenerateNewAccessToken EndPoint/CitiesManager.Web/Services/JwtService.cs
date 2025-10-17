using CitiesManager.Web.DTO;
using CitiesManager.Web.Identity;
using CitiesManager.Web.ServiceContracts;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CitiesManager.Web.Services
{
	public class JwtService : IJwtService
	{
		private readonly IConfiguration _configuration;
		public JwtService(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public AuthenticationResponse CreateJwtToken(ApplicationUser user)
		{
			#region PayLoad
			//			2 - you have to prepare the claims that are needed to be added in the payload
			//--> claim represnnts a particular value much like fields or details of the particular user

			//lets make the payload
			DateTime expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:EXPIRATION_MINUTES"]));
			Claim[] claims = new Claim[]
			{
			//1-SUB
			new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),//the sub value indicated the user identity

			//2-Jti -->the unique id for the token
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

			//3-Iat-->represents the date and time of token generation
			new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),

			//4-optionally --> NameIdentifier 
			//notpresentin jwt but oprionally we are adding , its beusedasidentity,butwe already 
			//made theuserid aboveasidentity, solets add the email thistime
			new Claim(ClaimTypes.NameIdentifier, user.Email),

			//5-optionally --> person name
			new Claim(ClaimTypes.Name, user.PersonName),

			new Claim(ClaimTypes.Email, user.Email)
			};
			#endregion

			#region Secretkey
			//SymmetricSecurityKey -->now we have to generate the secret key
			SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

			//SigningCredentials-->assign the algorithm we wanttouse
			SigningCredentials signingCredentialss = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			//then we have to make a token generator -->create token based on all these informations we had
			JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
				_configuration["Jwt:Issuer"],
				_configuration["Jwt:Audience"],
				claims, 
				expires:expiration,
				signingCredentials:signingCredentialss			
				);

			//but still token cant begenerated from JwtSecurityToken only
			//we have to make JwtSecurityTokenHandler 

			JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
			string token=handler.WriteToken(jwtSecurityToken);
			#endregion

			return new AuthenticationResponse() {Token = token, Email=user.Email,
				Expiration=expiration,
				PersonName= user.PersonName,
				RefreshToken=GenerateRefreshToken(),
				RefreshTokenExpirationDateTime= DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["RefreshToken:EXPIRATION_MINUTES"]))
			};
		}

		public ClaimsPrincipal? GetPrincipalFromJwtToken(string? token)
		{

			var validationparameters = new TokenValidationParameters()
			{
				ValidateAudience = true,
				ValidAudience = _configuration["Jwt:Audience"],
				ValidateIssuer = true,
				ValidIssuer= _configuration["Jwt:Issuer"],
				ValidateIssuerSigningKey= true,
				IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
				ValidateLifetime = false //-->because token here is already expired
			};

			var tokenhandler = new JwtSecurityTokenHandler();
			ClaimsPrincipal? principal= tokenhandler.ValidateToken(token, validationparameters, out SecurityToken securitytoken);
			if (securitytoken is not JwtSecurityToken jwtsecuritytoken ||
				!jwtsecuritytoken.Header.Alg.Equals( SecurityAlgorithms.HmacSha256,StringComparison.InvariantCultureIgnoreCase))
			{
				throw new SecurityTokenException("Invalid Token");
			};
			return principal;
		}

		private string GenerateRefreshToken()
		{
			byte[] bytes = new byte[64];

			var randomnumbergenerator=RandomNumberGenerator.Create();

			randomnumbergenerator.GetBytes(bytes); //it now fills this 
			//emptyarray withthese randomnumbers,it generatesand add,generatesandadd
			return Convert.ToBase64String(bytes); //the base64 string of the
			//random number is Our RefreshToken

		}
	}
}
