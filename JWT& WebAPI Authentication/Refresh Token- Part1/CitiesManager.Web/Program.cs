using CitiesManager.Web.DatabaseContext;
using CitiesManager.Web.Identity;
using CitiesManager.Web.ServiceContracts;
using CitiesManager.Web.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
	options.Filters.Add(new ProducesAttribute("application/json"));
	options.Filters.Add(new ConsumesAttribute("application/json"));
	var policy= new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
	options.Filters.Add(new AuthorizeFilter(policy));
	 //adding the builder.Services.AddAuthentication(.....) that wedid as a global filter for llcontroller
	 //insted ,youcn just dd the[Authorize] attribute for all your controller
	 //in account controller wemade [AllowAnonymous] so this will make the [Authorize] Disabled don't worry
});
builder.Services.AddDbContext<CitiesDbContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString"));
});
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options => 
{
	options.Password.RequiredLength = 5;
	options.Password.RequireUppercase = false;
	options.Password.RequireNonAlphanumeric = false;//symbols
	options.Password.RequireLowercase = true;
	options.Password.RequireDigit = false;
	//options.Password.RequiredUniqueChars = 3;
})
	.AddEntityFrameworkStores<CitiesDbContext>()
	.AddDefaultTokenProviders()
	.AddUserStore<UserStore<ApplicationUser, ApplicationRole, CitiesDbContext, Guid>>()
	.AddRoleStore<RoleStore<ApplicationRole, CitiesDbContext, Guid>>();

builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddEndpointsApiExplorer();

//Enable versioning in Web API controllers
builder.Services.AddApiVersioning(config =>
{
	config.ApiVersionReader = new UrlSegmentApiVersionReader(); //Reads version number from request url at "apiVersion" constraint

	//config.ApiVersionReader = new QueryStringApiVersionReader(); //Reads version number from request query string called "api-version". Eg: api-version=1.0

	//config.ApiVersionReader = new HeaderApiVersionReader("api-version"); //Reads version number from request header called "api-version". Eg: api-version: 1.0

	config.DefaultApiVersion = new ApiVersion(1, 0);
	config.AssumeDefaultVersionWhenUnspecified = true;
});
builder.Services.AddSwaggerGen(options => {
	options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "api.xml"));

	options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Cities Web API", Version = "1.0" });

	options.SwaggerDoc("v2", new Microsoft.OpenApi.Models.OpenApiInfo() { Title = "Cities Web API", Version = "2.0" });

}); //generates OpenAPI specification
builder.Services.AddVersionedApiExplorer(options => {
	options.GroupNameFormat = "'v'VVV"; //v1
	options.SubstituteApiVersionInUrl = true;
});



builder.Services.AddAuthentication(options => 
{
	options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}

).AddJwtBearer(options => 
{
	options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters() 
	{
		ValidateAudience = true,
		ValidAudience = builder.Configuration["Jwt:Audience"],
		ValidateIssuer = true,
		ValidIssuer = builder.Configuration["Jwt:Issuer"],
		ValidateLifetime=true,
		ValidateIssuerSigningKey = true,
		IssuerSigningKey=new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
	};
});
builder.Services.AddAuthorization();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
	options.SwaggerEndpoint("/swagger/v1/swagger.json", "1.0");
	options.SwaggerEndpoint("/swagger/v2/swagger.json", "2.0");
}); //creates swagger UI for testing all Web API endpoints / action methods
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
