using Base.Domain.Entities.Aggregates.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.Domain.Contracts.Repositories
{
    public interface IProductRepository
    {
        Task AddAsync(Product product);
        IQueryable<Product> Query();
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);

    }
}
