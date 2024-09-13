namespace TrelloManagmentSystem.Exceptions
{
    public class BusinessException :Exception
    {
        public  ErrorCode errorCode;

        public BusinessException(ErrorCode errorCode, string Message):base(Message)
        {
            this.errorCode = errorCode;
        }
    }
}
