namespace _03.FootballBetting
{
    using Data;
    using Microsoft.EntityFrameworkCore;

    public class Startup
    {
        public static void Main(string[] args)
        {
            using (var db = new FootballBettingDbContext())
            {
                db.Database.Migrate();
            }
        }
    }
}
