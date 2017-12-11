using Microsoft.Practices.EnterpriseLibrary.Data;

namespace LinJas.Manager
{
    public class BaseBlogManager
    {
        private static Database _database;
        protected static Database Database
        {
            get
            {
                if (_database == null)
                {
                    var factory = new DatabaseProviderFactory();
                    _database = factory.Create("XingfaGroup");
                }
                return _database;
            }
        }
    }
}
