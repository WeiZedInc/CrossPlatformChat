namespace CrossPlatformChat.Utils
{
    public static class ServiceHelper // Dependency Injection helper
    {
        public static TService GetService<TService>() => Current.GetService<TService>();

        public static IServiceProvider Current => IPlatformApplication.Current.Services;
    }
}