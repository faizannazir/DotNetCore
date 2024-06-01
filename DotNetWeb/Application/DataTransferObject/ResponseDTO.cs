using Microsoft.AspNetCore.Mvc;

namespace DataTransferObject
{
    public class ResponseDTO
    {
        public dynamic Response { get; set; }
        public bool IsSuccessful { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Url{ get; set;}
        public string Token { get; set; }
        public PartialViewResult ViewResult { get; set; }
        public IList<string> Errors { get; set; }
    }
}
