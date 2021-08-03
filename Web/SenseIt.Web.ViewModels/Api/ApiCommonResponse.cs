namespace SenseIt.Web.ViewModels.Api
{
    public class ApiCommonResponse<T>
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }
    }
}
