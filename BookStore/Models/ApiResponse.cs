namespace BookStore.Models
{
    public class ApiResponse
    {
        /// <summary>
        /// data coming from api
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// list of api error
        /// </summary>
        public object? Error { get; set; }
        /// <summary>
        ///  api status code 200=success , 400 = faild
        /// </summary>
        public string StatusCode { get; set; }

    }
}
