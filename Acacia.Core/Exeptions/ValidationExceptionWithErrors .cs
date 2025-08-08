namespace Acacia.Core.Exeptions
{
    public class ValidationExceptionWithErrors : Exception
    {
        public Dictionary<string, List<string>> Errors { get; }

        public ValidationExceptionWithErrors(string message, Dictionary<string, List<string>> errors)
            : base(message)
        {
            Errors = errors;
        }
    }
}
