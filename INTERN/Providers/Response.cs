using static System.Runtime.InteropServices.JavaScript.JSType;

namespace INTERN.Providers
{
    public class Response
    {
        public bool Success { get; set; }
        public ResponseProduct data { get; set; }
        public string Message { get; set; }
        public Response() {
        this.Success = false;
        this.data = new ResponseProduct();
        }
        public Response(bool success, ResponseProduct RProduct, string error) {
        this.Success = success;
        this.data = RProduct; 
        this.Message = error;

        }
    }
}
