using Dapper;
using mango.product.DAL.Models;
using mango.product.DAL.Request.QueryModels;
using MediatR;
using store.dal.DataConnections;

namespace mango.product.DAL.Request.Handlers
{
    internal class GetProductsByNameHandler : IRequestHandler<GetProductsByName, List<Product>>
    {

        private readonly IDatabaseBuilder _sqlDatabaseBuilder;

        public GetProductsByNameHandler(IDatabaseBuilder sqlDatabaseBuilder)
        {
            this._sqlDatabaseBuilder = sqlDatabaseBuilder;
        }

        public async Task<List<Product>> Handle(GetProductsByName request, CancellationToken cancellationToken)
        {
            var products = new List<Product>();

            using (Database database = DatabaseFactory.CreateDatabase(_sqlDatabaseBuilder))
            {

                List<string> names = new List<string> { request.Name };

                string sql = $"  SELECT [ProductId] " +
                              $"       ,[Name] " +
                              $"       ,[Price] " +
                              $"       ,[Description] " +
                              $"       ,[CategoryName] " +
                              $"       ,[ImageUrl] " +
                              $"  FROM [Products] " +
                              $" WHERE Name=@names";


                database.Connection.Open();

                products = (List<Product>)(await database.Connection.QueryAsync<Product>(sql, new { names }));


            }
            return products;
        }
    }
}
