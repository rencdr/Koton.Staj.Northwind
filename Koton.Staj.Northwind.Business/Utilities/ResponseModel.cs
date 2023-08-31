

namespace Koton.Staj.Northwind.Business.Utilities
{
    public class ResponseModel
    {
        //success bool
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }

}
