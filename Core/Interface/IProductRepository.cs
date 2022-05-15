using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interface
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);

        Task<IReadOnlyList<Product>> GetProductAsync();

        Task<ProductBrand> GetProductBrandsByIdAsync(int id);

        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        
        Task<ProductType> GetProductTypesByIdAsync(int id);

        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
    }
}