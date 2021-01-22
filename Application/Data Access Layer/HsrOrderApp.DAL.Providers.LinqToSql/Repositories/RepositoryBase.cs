namespace HsrOrderApp.DAL.Providers.LinqToSql.Repositories
{
    public abstract class RepositoryBase
    {
        protected HsrOrderAppDataContext db;

        public RepositoryBase(HsrOrderAppDataContext db)
        {
            this.db = db;
        }

        public RepositoryBase(string connectionString)
        {
            this.db = new HsrOrderAppDataContext(connectionString);
        }
    }
}