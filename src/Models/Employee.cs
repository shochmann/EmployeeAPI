using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EmployeeAPI.Models
{
    public class Employee
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [JsonPropertyName("firstName")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [JsonPropertyName("lastName")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [JsonPropertyName("email")]
        public required string Email { get; set; }
    }
}
