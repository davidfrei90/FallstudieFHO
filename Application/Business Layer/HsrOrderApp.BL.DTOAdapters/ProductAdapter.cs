#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DTOAdapters.Helper;
using HsrOrderApp.SharedLibraries.DTO;

#endregion

namespace HsrOrderApp.BL.DtoAdapters
{
    public class ProductAdapter
    {
        #region ProductsToDTO

        public static IList<ProductDTO> ProductsToDtos(IQueryable<Product> products)
        {
            IQueryable<ProductDTO> productDTOs = from p in products
                                                 select ProductToDto(p);
            return productDTOs.ToList();
        }

        public static ProductDTO ProductToDto(Product p)
        {
            ProductDTO dto = new ProductDTO()
                                 {
                                     Id = p.ProductId,
                                     ProductNumber = p.ProductNumber,
                                     Name = p.Name,
                                     Category = p.Category,
                                     QuantityPerUnit = p.QuantityPerUnit,
                                     ListUnitPrice = p.ListUnitPrice,
                                     UnitsOnStock = p.UnitsOnStock,
                                     Version = p.Version
                                 };

            return dto;
        }

        #endregion

        #region DTOToProduct

        public static Product DtoToProduct(ProductDTO dto)
        {
            Product product = new Product()
                                  {
                                      ProductId = dto.Id,
                                      ProductNumber = dto.ProductNumber,
                                      Name = dto.Name,
                                      Category = dto.Category,
                                      QuantityPerUnit = dto.QuantityPerUnit,
                                      ListUnitPrice = dto.ListUnitPrice,
                                      UnitsOnStock = dto.UnitsOnStock,
                                      Version = dto.Version
                                  };
            ValidationHelper.Validate(product);
            return product;
        }

        #endregion
    }
}