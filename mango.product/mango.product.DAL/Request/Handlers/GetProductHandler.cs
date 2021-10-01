using Dapper;
using mango.product.DAL.Models;
using mango.product.DAL.Request.QueryModels;
using MediatR;
using store.dal.DataConnections;




namespace mango.product.DAL.Request.Handlers
{
    class GetProductHandler : IRequestHandler<GetProduct, Product>
    {

        private readonly IDatabaseBuilder _sqlDatabaseBuilder;

        public GetProductHandler(IDatabaseBuilder sqlDatabaseBuilder)
        {
            this._sqlDatabaseBuilder = sqlDatabaseBuilder;
        }
        public async Task<Product> Handle(GetProduct request, CancellationToken cancellationToken)
        {
            var product = new List<Product>();

            using (Database database = DatabaseFactory.CreateDatabase(_sqlDatabaseBuilder))
            {

                string sql = $"SELECT  [CLIENTE_NICKNAME] ClienteNickname" +
                                $"    ,[DESCRIPCION] Descripcion" +
                                $"  FROM [CLIENTES]" +
                                $" WHERE CLIENTE_NICKNAME ={request.ProductId}";
                database.Connection.Open();

                product = (List<Product>)(await database.Connection.QueryAsync<Product>(sql));


            }
            return product.FirstOrDefault();
        }

    }


}
