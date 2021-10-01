
namespace store.dal.DataConnections
{
    class DatabaseFactory
    {
        public static Database CreateDatabase(IDatabaseBuilder builder)
        {
            builder.BuildDataBase();
            return  builder.Database;
        }

    }
}
