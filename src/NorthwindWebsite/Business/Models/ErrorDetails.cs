using Newtonsoft.Json;

namespace NorthwindWebsite.Business.Models;

public class ErrorDetails
{
    [JsonProperty("status code")]
    public int StatusCode { get; set; }

    [JsonProperty("error message")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("additional information")]
    public string AdditionalInformation { get; set; } = "Address the logs for additional information";
}
