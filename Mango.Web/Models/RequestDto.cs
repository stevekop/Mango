using Mango.Web.Utils;
using static Mango.Web.Utils.SD;

namespace Mango.Web.Models
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; } = string.Empty;
        public object  Data { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public ContentType ContentType { get; set; } = SD.ContentType.Json;
    }
}
