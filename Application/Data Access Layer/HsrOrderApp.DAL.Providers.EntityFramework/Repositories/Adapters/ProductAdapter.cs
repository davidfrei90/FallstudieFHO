#region

using System.Data.Objects.DataClasses;
using HsrOrderApp.SharedLibraries.SharedEnums;

#endregion

namespace HsrOrderApp.DAL.Providers.EntityFramework.Repositories.Adapters
{
    internal static class ProductAdapter
    {
        internal static BL.DomainModel.Product AdaptProduct(EntityReference<Product> p)
        {
            if (p.Value == null)
                return null;
            return AdaptProduct(p.Value);
        }

        internal static BL.DomainModel.Product AdaptProduct(Product c)
        {
            BL.DomainModel.Product product = new BL.DomainModel.Product()
                                                 {
                                                     ProductId = c.ProductId,
                                                     Category = c.Category,
                                                     Name = c.Name,
                                                     ListUnitPrice = c.ListUnitPrice,
                                                     QuantityPerUnit = c.QuantityPerUnit,
                                                     UnitsOnStock = c.UnitsOnStock,
                                                     Version = c.Version.ToUlong(),
                                                     ProductNumber = c.ProductNumber,
                                                 };
            return product;
        }
    }
}