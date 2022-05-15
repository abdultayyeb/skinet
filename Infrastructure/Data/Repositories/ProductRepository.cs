using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interface;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public StoreContext _context { get; set; }

        public ProductRepository(StoreContext context)
        {
            _context = context;

        }

        public async Task<IReadOnlyList<Product>> GetProductAsync()
        {
            return await _context.Products
            .Include(x => x.ProductType)
            .Include(x => x.ProductBrand)
            .ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
            .Include(x => x.ProductBrand)
            .Include(x => x.ProductType)
            .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ProductBrand> GetProductBrandsByIdAsync(int id)
        {
            return await _context.ProductBrands.FindAsync(id);
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<ProductType> GetProductTypesByIdAsync(int id)
        {
            return await _context.ProductTypes.FindAsync(id);
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}