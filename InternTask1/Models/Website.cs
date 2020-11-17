namespace InternTask1.Models
{
    public class Website
    {
        public string Url { get; private set; }
        public int StatusCode { get; private set; }

        public Website(string url, int statusCode)
        {
            Url = url;
            StatusCode = statusCode;
        }

        public override string ToString()
        {
            return $"{Url},{StatusCode}";
        }
    }
}
