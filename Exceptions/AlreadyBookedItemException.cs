namespace Exceptions
{
    public class AlreadyBookedItemException : Exception
    {
        public AlreadyBookedItemException(string message)
            : base(message) { }
    }
}