using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productSpecParams)
        : base(x =>
                (string.IsNullOrEmpty(productSpecParams.Search) || x.Name.ToLower().Contains(productSpecParams.Search))
                &&
                (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId)
                &&
                (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId)
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(p => p.Name);
            ApplyPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1),
            productSpecParams.PageSize);

            switch (productSpecParams.Sort)
            {
                case "nameDesc":
                    AddOrderByDesccending(p => p.Name);
                    break;
                case "priceDesc":
                    AddOrderByDesccending(p => p.Price);
                    break;
                case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                default:
                    AddOrderBy(p => p.Name);
                    break;
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id)
            : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
 
    }
}