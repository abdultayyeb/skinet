using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dto;
using AutoMapper;
using Core.Entities;

namespace API.Helper
{
    public class ProductURLResolver : IValueResolver<Product, ProductOutputDto, string>
    {
        private readonly IConfiguration _config;

        public ProductURLResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, ProductOutputDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $@"{_config["ApiURL"]}{source.PictureUrl}";
            }
            return null;
        }
    }
}