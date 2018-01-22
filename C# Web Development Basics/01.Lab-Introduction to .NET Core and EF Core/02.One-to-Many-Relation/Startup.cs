namespace _02.One_to_Many_Relation
{
    using System;

    public class Startup
    {
        public static void Main(string[] args)
        {
            InitializeDb();

        }

        private static void InitializeDb()
        {
            using (var context = new MyDBContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }
    }
}
