namespace NoteApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Configure Services
            builder.Services.AddMvc(option => option.EnableEndpointRouting = false);

            var app = builder.Build();

            //Configure (pipeline)
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
            }

            app.UseStaticFiles();

            app.UseMvc(routes => {
                routes.MapRoute("default", "{controller=Home}/{action=Index}");
            });

            app.Run();
        }
    }
}