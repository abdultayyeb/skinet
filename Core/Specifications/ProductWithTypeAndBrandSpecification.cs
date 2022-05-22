using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Dto;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithTypeAndBrandSpecification : BaseSpecification<Product>
    {
        public ProductWithTypeAndBrandSpecification(ProductInputDto input)
        :base(x=> 
            (string.IsNullOrEmpty(input.Search) || x.Name.ToLower().Contains(input.Search)) &&
            (!input.BrandId.HasValue|| x.ProductBrandId == input.BrandId)
            &&
            (!input.TypeId.HasValue|| x.ProductTypeId == input.TypeId)
        )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
           
            AddOrderBy(x=>x.Name);
            ApplyPagination(input.PageSize*(input.PageIndex -1) ,input.PageSize);
            if(!string.IsNullOrEmpty(input.Sort)){
                switch (input.Sort)
                {
                    case  "priceAsc":
                    AddOrderBy(x=>x.Price);                    
                    break;
                    case  "priceDesc":
                    AddOrderByDesc(x=>x.Price);                    
                    break;
                    case "nameDesc":
                    AddOrderByDesc(x=>x.Name);
                    break;
                    default:
                    AddOrderBy(x=>x.Name);
                    break;
                }
            }

    

        }

        public ProductWithTypeAndBrandSpecification(int id) : base(x=>x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x=> x.Name);
        }
    }
}