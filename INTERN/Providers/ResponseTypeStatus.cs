using Microsoft.AspNetCore.Mvc;

namespace INTERN.Providers
{
    public class ResponseTypeStatus
    {
        public bool Success { get; set; }
        public ResponseType data { get; set; }
        public string Message { get; set; }
        public ResponseTypeStatus()
        {
            this.Success = false;
            this.data = new ResponseType();
        }
        public ResponseTypeStatus(bool success, ResponseType RType,string error)
        {
            this.Success = success;
            this.data = RType;
            this.Message = error;
        }
    }
}
