namespace Chat.API.SignalR
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/ChatHub");
            });
        }
    }
}
