namespace EventSourcing.Core
{
    public interface IProductRepository
    {
        Domain.Product Get(int id);

        void Save(Domain.Product product);

        // TODO : faire une table de projections à la place
        //IEnumerable<Domain.Product> GetAll();
    }
}