namespace BackendApi.Models
{
    public class AuthorRequest
    {
        public string response_type { get; set; }
        public string client_id { get; set; }
        public string state { get; set; }
        public string redirect_uri { get; set; }
    }
}
