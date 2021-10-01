
namespace store.dal.DataConnections
{
    public interface IDatabaseBuilder
    {
        void BuildDataBase();
        Database Database { get; }
    }
}
