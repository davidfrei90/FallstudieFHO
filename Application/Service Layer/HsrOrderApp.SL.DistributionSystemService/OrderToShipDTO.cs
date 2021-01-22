using System.Runtime.Serialization;
namespace HsrOrderApp.SL.DistributionSystemService
{
    [DataContract]
    public class OrderToShipDTO
    {
        public OrderToShipDTO()
        {
            this.Id = -1;
        }

        [DataMember]
        public int Id { get; set; }
    }
}