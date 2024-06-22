namespace INTERN.Providers
{
    public class ResponseTypeStatus
    {
        public bool Success { get; set; }
        public ResponseType data { get; set; }
        public ResponseTypeStatus()
        {
            this.Success = false;
            this.data = new ResponseType();
        }
        public ResponseTypeStatus(bool success, ResponseType RType)
        {
            this.Success = success;
            this.data = RType;
        }
    }
}
