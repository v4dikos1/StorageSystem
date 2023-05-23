namespace Application.Common.Exceptions
{
    public class WrongConfirmationCodeException : Exception
    {
        public WrongConfirmationCodeException() : base("Incorrect confirmation code.")
        {
            
        }
    }
}
