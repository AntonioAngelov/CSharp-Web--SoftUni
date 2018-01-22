namespace _04.BankSystem
{
    using Core;
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class Startup
    {
        public static void Main(string[] args)
        {
            using (var context = new BankSystemDbContext())
            {
                context.Database.Migrate();

                var bankManager = new BankSystemManager(context);
                var engine = new Engine(bankManager);
                engine.Run();
            }
        }
    }
}
