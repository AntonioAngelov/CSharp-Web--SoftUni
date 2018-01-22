namespace _04.ManyToManyRelation
{
    public class Startup
    {
        public static void Main(string[] args)
        {
            InitializeDb();

        }

        private static void InitializeDb()
        {
            using (var context = new StudentsDbContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}
