

//namespace Koton.Staj.Northwind.Business.Utilities
//{
//    public class ResponseModel
//    {
//        public bool Success { get; set; } 
//        public string Message { get; set; }
//        public object Data { get; set; }
//    }

//}

namespace Koton.Staj.Northwind.Business.Utilities
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
}
