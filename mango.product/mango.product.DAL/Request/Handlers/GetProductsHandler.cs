using Dapper;
using mango.product.DAL.Models;
using mango.product.DAL.Request.QueryModels;
using MediatR;
using store.dal.DataConnections;

namespace mango.product.DAL.Request.Handlers
{
    class GetProductsHandler : IRequestHandler<GetProducts, List<Product>>
    {

        private readonly IDatabaseBuilder _sqlDatabaseBuilder;

        public GetProductsHandler(IDatabaseBuilder sqlDatabaseBuilder)
        {
            this._sqlDatabaseBuilder = sqlDatabaseBuilder;
        }

        public async Task<List<Product>> Handle(GetProducts request, CancellationToken cancellationToken)
        {
            var products = new List<Product>();

            using (Database database = DatabaseFactory.CreateDatabase(_sqlDatabaseBuilder))
            {

                string sql = $" SELECT [ProductId] " +
                              $"      ,[Name] " +
                              $"      ,[Price] " +
                              $"      ,[Description] " +
                              $"      ,[CategoryName] " +
                              $"      ,[ImageUrl] " +
                              $" FROM [Products] ";

                database.Connection.Open();

                products = (List<Product>)(await database.Connection.QueryAsync<Product>(sql));


            }
            return products;
        }
    }


}
