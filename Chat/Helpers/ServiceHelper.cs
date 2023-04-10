namespace CrossPlatformChat.Helpers
{
    public static class ServiceHelper // Dependency Injection helper
    {
        public static TService Get<TService>() => Current.GetService<TService>();

        public static IServiceProvider Current => IPlatformApplication.Current.Services;
    }
}