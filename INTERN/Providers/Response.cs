namespace INTERN.Providers
{
    public class Response
    {
        public bool Success { get; set; }
        public ResponseProduct data { get; set; }   
        public Response() {
        this.Success = false;
        this.data = new ResponseProduct();
        }
        public Response(bool success, ResponseProduct RProduct) {
        this.Success = success;
        this.data = RProduct;
        }
    }
}
