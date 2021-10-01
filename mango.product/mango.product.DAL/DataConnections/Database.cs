using System;
using System.Collections.Generic;
using System.Data.Common;

namespace store.dal.DataConnections
{
    public abstract class Database:IDisposable
    {
        private protected DbConnection _connection = null;

        private protected List<DbCommand> _commands = new List<DbCommand>();


        public DbConnection Connection
        {
            get
            {
                return _connection;
            }
            set
            {
                _connection = value;
            }
        }

        public DbCommand Command
        {
            get
            {
                DbCommand _command = _connection.CreateCommand();
                _commands.Add(_command);
                return _command;
            }

        }
        public abstract void Dispose();


    }
}
