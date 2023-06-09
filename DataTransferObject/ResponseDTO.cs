using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransferObject
{
    public class ResponseDTO
    {
        public dynamic Response { get; set; }
        public bool IsSuccessful { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Url{ get; set;}
        public Exception Error { get; set; }
    }
}
