#region

using System;
using System.Runtime.Serialization;

#endregion

namespace HsrOrderApp.SharedLibraries.DTO.Faults
{
    public enum FaultType
    {
        General,
        Security,
        Concurrency,
        Validation
    }

    [DataContract]
    public class ServiceFault
    {
        private FaultType faultType;
        private Guid id;
        private string message;

        [DataMember]
        public string MessageText
        {
            get { return message; }
            set { message = value; }
        }

        [DataMember]
        public Guid Id
        {
            get { return id; }
            set { id = value; }
        }

        [DataMember]
        public FaultType FaultType
        {
            get { return this.faultType; }
            set { this.faultType = value; }
        }

        public string FaultTypeAsString
        {
            set
            {
                this.faultType = FaultType.General;

                if (value != null && value != string.Empty)
                {
                    try
                    {
                        this.faultType = (FaultType) Enum.Parse(typeof (FaultType), value);
                    }
                    catch
                    {
                    }
                }
            }
        }
    }
}