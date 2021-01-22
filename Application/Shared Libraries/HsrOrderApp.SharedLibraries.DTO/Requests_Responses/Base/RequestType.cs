#region

using System.Runtime.Serialization;
using HsrOrderApp.SharedLibraries.DTO.Base;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Requests_Responses.Base
{
    [DataContract]
    public abstract class RequestType : DTOBase
    {
    }
}