using System.Text.Json.Serialization;

namespace MiniProjet.Projectmanagement.Contracts.Contracts.Responses;

public class GetEmployeeResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("age")]
    public int Age { get; set; }

    [JsonPropertyName("departementName")]
    public string DepartementName { get; set; }
}
