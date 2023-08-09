namespace ECPMaster.DbContext
{
    public class DbInitializer
    {
        public static void Initialize(ECPDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}