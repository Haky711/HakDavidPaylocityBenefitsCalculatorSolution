namespace Api.Exceptions
{
    public class NotPossibleToInsertException : Exception
    {
        public NotPossibleToInsertException(string message) : base(message) { }
    }
}
