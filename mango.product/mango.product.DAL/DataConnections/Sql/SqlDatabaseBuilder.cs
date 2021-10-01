using System.Data.SqlClient;

namespace store.dal.DataConnections.Sql
{
    public class SqlDatabaseBuilder : IDatabaseBuilder 
    {
        private Database _database;
        private string _connectionString;

        public SqlDatabaseBuilder(string connectionString)
        {
            _connectionString = connectionString;
            _database = new SqlDataBase();
        }
        public void BuildDataBase() { 
            _database.Connection = new SqlConnection(_connectionString);
        }

        public Database Database
        {
            get { return _database; }
        }

    }
}
