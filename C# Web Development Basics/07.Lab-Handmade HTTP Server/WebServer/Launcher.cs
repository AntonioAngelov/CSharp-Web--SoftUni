namespace WebServer
{
    using Application;
    using Server;
    using Server.Contracts;
    using Server.Routing;
    using Server.Routing.Contracts;

    public class Launcher : IRunnable
    {
        private WebServer webServer;

        public static void Main()
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var app = new MainApplication();
            app.InitializeDb();

            IAppRouteConfig routeConfig = new AppRouteConfig();
            app.Start(routeConfig);

            this.webServer = new WebServer(8230, routeConfig);
            this.webServer.Run();
        }
    }
}
