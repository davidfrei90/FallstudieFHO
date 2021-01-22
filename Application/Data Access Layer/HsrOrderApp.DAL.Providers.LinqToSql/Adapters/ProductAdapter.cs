#region

using System.Data.Linq;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.DAL.Providers.LinqToSql.Adapters
{
    internal static class ProductAdapter
    {
        internal static BL.DomainModel.Product AdaptProduct(Product c)
        {
            BL.DomainModel.Product product = new BL.DomainModel.Product()
                                                 {
                                                     ProductId = c.ProductId,
                                                     ProductNumber = c.ProductNumber,
                                                     Category = c.Category,
                                                     Name = c.Name,
                                                     ListUnitPrice = c.ListUnitPrice,
                                                     QuantityPerUnit = c.QuantityPerUnit,
                                                     UnitsOnStock = c.UnitsOnStock,
                                                     Version = c.Version.ToUlong(),
                                                 };
            return product;
        }
    }
}