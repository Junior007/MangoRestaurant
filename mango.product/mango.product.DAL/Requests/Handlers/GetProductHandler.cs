using Dapper;
using mango.product.DAL.Models;
using mango.product.DAL.Requests.QueryModels;
using MediatR;
using store.dal.DataConnections;




namespace mango.product.DAL.Requests.Handlers
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



                string sql = $"  SELECT [ProductId] " +
                              $"       ,[Name] " +
                              $"       ,[Price] " +
                              $"       ,[Description] " +
                              $"       ,[CategoryName] " +
                              $"       ,[ImageUrl] " +
                              $"       ,[RowVersion] " +
                              $"  FROM [Products] " +
                              $" WHERE ProductId={request.ProductId}";


                database.Connection.Open();

                product = (List<Product>)(await database.Connection.QueryAsync<Product>(sql));


            }
            return product.FirstOrDefault();
        }

    }


}
