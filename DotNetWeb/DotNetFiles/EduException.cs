using System.Net;


namespace Edu.Common.Utils
{
    [Serializable]
    public class EduException : Exception
    {
        public int ErrorCode { get; set; }
        public EduException() { }
        public EduException(string message, int errorCode  = (int)HttpStatusCode.BadRequest) : base(message)
        {
            this.ErrorCode = errorCode;
        }
    }
}
