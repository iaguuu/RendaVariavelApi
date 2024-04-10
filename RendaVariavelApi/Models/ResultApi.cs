using Newtonsoft.Json;

namespace RendaVariavelApi.Models
{
    public class Result<T>
    {
        public string? title { get; set; }
        public int? status { get; set; }
        public string? detail { get; set; }
        public T? result { get; set; }
    }
}
