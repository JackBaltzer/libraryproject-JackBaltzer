using System.ComponentModel.DataAnnotations;

namespace LibraryProject.Api.Services
{
    public class AuthorRequest
    {
        [Required]
        [StringLength(32, ErrorMessage = "FirstName must not contain more than 32 chars")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "LastName must not contain more than 32 chars")]
        public string LastName { get; set; }

        [StringLength(32, ErrorMessage = "MiddleName must not contain more than 32 chars")]
        public string MiddleName { get; set; } = "";

        [Required]
        [Range(1, 2500, ErrorMessage = "Birthyear must be between 1 and 2500")]
        public int BirthYear { get; set; }

        [Range(1, 2500, ErrorMessage = "YearOfDeath must be between 1 and 2500")]
        public int? YearOfDeath { get; set; }
    }
}