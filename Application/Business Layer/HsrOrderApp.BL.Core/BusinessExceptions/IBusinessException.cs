namespace HsrOrderApp.BL.Core.BusinessExceptions
{
    public interface IBusinessException
    {
        string ExceptionType { get; }
    }

    public class Consts
    {
        public const string Concurrency = "Concurrency";
        public const string Security = "Security";
        public const string Validation = "Validation";
    }
}