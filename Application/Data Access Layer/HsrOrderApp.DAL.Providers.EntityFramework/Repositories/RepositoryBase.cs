namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories
{
    public abstract class RepositoryBase
    {
        protected HsrOrderAppEntities db;

        public RepositoryBase()
        {
            this.db = new HsrOrderAppEntities();
        }

        public RepositoryBase(HsrOrderAppEntities db)
        {
            this.db = db;
        }

        public RepositoryBase(string connectionString)
        {
            this.db = new HsrOrderAppEntities(connectionString);
        }
    }
}