using System.Linq;

namespace SportsStoreOnCore2.Models
{
    public interface IProductRepository
    {
        IQueryable<Product> Products { get; }
    }
}
