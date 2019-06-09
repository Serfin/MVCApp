namespace MVCApp.Common.Exceptions
{
    public class InvalidPaginationArgument : System.Exception
    {
        public InvalidPaginationArgument() { }
        public InvalidPaginationArgument(string message) : base(message) { }
        public InvalidPaginationArgument(string message, System.Exception innerException) : base(message, innerException) { }
    }
}