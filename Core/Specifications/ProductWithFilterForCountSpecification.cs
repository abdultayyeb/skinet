using Core.Dto;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>{
        public ProductWithFilterForCountSpecification(ProductInputDto input)
        :base(x=> 
            (!input.BrandId.HasValue|| x.ProductBrandId == input.BrandId)
            &&
            (!input.TypeId.HasValue|| x.ProductTypeId == input.TypeId)
        )
        {
            
        }
    }
}