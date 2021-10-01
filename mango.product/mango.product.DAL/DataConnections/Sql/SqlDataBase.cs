namespace store.dal.DataConnections.Sql
{
    public class SqlDataBase : Database
    {


        public override void Dispose()
        {

            foreach (var _command in _commands)
                _command.Dispose();


            _connection.Dispose();
        }
    }
}
