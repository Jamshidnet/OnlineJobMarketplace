namespace OnlineJobMarketplace.Application.Common.Exceptions
{
    public  class AlreadyExsistsException : Exception
    {
        public AlreadyExsistsException(string message) : base(message)
        {
            
        }
    }
}
