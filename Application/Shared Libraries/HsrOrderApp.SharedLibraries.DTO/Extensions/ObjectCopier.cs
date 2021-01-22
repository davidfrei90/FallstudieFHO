using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using HsrOrderApp.SharedLibraries.DTO.Base;

namespace HsrOrderApp.SharedLibraries.DTO
{
    public static class ObjectCopier
    {
        public static T Clone<T>(this T source) where T : DTOBase
        {
            var dcs = new DataContractSerializer(typeof (T));
            using (var ms = new MemoryStream())
            {
                dcs.WriteObject(ms, source);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                return (T) dcs.ReadObject(ms);
            }
        }
    }
}
