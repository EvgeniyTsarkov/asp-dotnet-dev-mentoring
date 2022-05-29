using Newtonsoft.Json;

namespace NorthwindWebsite.Business.Models;

public class ErrorDetails
{
    [JsonProperty("statusCode")]
    public int StatusCode { get; set; }

    [JsonProperty("errorMessage")]
    public string Message { get; set; } = string.Empty;

    [JsonProperty("additionalInformation")]
    public string AdditionalInformation { get; set; } = "Address the logs for additional information";
}
