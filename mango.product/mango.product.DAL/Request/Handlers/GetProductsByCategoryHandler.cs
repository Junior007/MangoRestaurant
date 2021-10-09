using Dapper;
using mango.product.DAL.Models;
using mango.product.DAL.Request.QueryModels;
using MediatR;
using store.dal.DataConnections;

namespace mango.product.DAL.Request.Handlers
{
    public class GetProductsByCategoryHandler : IRequestHandler<GetProductsByCategory, List<Product>>
    {

        private readonly IDatabaseBuilder _sqlDatabaseBuilder;

        public GetProductsByCategoryHandler(IDatabaseBuilder sqlDatabaseBuilder)
        {
            this._sqlDatabaseBuilder = sqlDatabaseBuilder;
        }

        public async Task<List<Product>> Handle(GetProductsByCategory request, CancellationToken cancellationToken)
        {
            var products = new List<Product>();

            using (Database database = DatabaseFactory.CreateDatabase(_sqlDatabaseBuilder))
            {

                List<string> categoryNames = new List<string> { request.CategoryName };

                string sql = $"  SELECT [ProductId] " +
                              $"       ,[Name] " +
                              $"       ,[Price] " +
                              $"       ,[Description] " +
                              $"       ,[CategoryName] " +
                              $"       ,[ImageUrl] " +
                              $"       ,[RowVersion] " +
                              $"  FROM [Products] " +
                              $" WHERE CategoryName=@categoryNames";


                database.Connection.Open();

                products = (List<Product>)(await database.Connection.QueryAsync<Product>(sql, new { categoryNames}));


            }
            return products;
        }
    }
}
