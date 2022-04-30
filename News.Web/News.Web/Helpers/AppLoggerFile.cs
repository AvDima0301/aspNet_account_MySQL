namespace News.Web.Helpers
{
    public static class AppLoggerFile
    {
        public static void UseLoggerFile(this WebApplication app)
        {
            using(var scope = app.Services.CreateScope())
            {
                var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                var fileLog = Path.Combine(path, "log-{Date}.txt");
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetService<ILoggerFactory>();
                loggerFactory.AddFile(fileLog);
            }
        }
    }
}
