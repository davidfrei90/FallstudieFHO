#region

using System.Collections.Generic;
using System.Linq;
using HsrOrderApp.BL.DomainModel;
using HsrOrderApp.BL.DTOAdapters.Helper;
using HsrOrderApp.SharedLibraries.DTO;

#endregion

namespace HsrOrderApp.BL.DtoAdapters
{
    public class AddressAdapter
    {
        #region AddressToDTO

        public static IList<AddressDTO> AddressToDtos(IQueryable<Address> addresses)
        {
            IQueryable<AddressDTO> addressDTOs = from a in addresses
                                                 select new AddressDTO()
                                                            {
                                                                Id = a.AddressId,
                                                                AddressLine1 = a.AddressLine1,
                                                                AddressLine2 = a.AddressLine2,
                                                                PostalCode = a.PostalCode,
                                                                City = a.City,
                                                                Phone = a.Phone,
                                                                Email = a.Email,
                                                                Version = a.Version
                                                            };
            return addressDTOs.ToList();
        }

        #endregion

        #region DTOToAddress

        public static Address DtoToAddress(AddressDTO dto)
        {
            Address address = new Address
                                  {
                                      AddressId = dto.Id,
                                      AddressLine1 = dto.AddressLine1,
                                      AddressLine2 = dto.AddressLine2,
                                      PostalCode = dto.PostalCode,
                                      City = dto.City,
                                      Phone = dto.Phone,
                                      Email = dto.Email,
                                      Version = dto.Version
                                  };

            ValidationHelper.Validate(address);
            return address;
        }

        #endregion
    }
}