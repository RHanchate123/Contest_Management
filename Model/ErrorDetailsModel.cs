using System.Text.Json;

namespace Contest_Management.API.Models
{
    public class ErrorDetailsModel
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; } = null;
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
