using CustomModelBinder.ModelBinderCustomClass;
namespace CustomModelBinder
{
    public class Program
    {
        public static void Main(string[] args)
        {
			var builder = WebApplication.CreateBuilder(args);
			builder.Services.AddControllers();
			builder.Services.AddControllers().AddXmlSerializerFormatters();

			builder.Services.AddControllers(options=>options
			.ModelBinderProviders.Insert(0, new ModelBinderProvider()));
			//why we in insert we gave him 0?
			//we here are making our provider is the first one the compiler will use
			//because by default the default model binder will be used
			//so that the Custom provider sometimes will not be used if you didnt do this 0
			var app = builder.Build();


			app.UseStaticFiles();
			app.UseRouting();
			app.MapControllers();

			app.Run();
		}
    }
}
