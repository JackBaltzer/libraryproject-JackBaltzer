namespace LibraryProject.Api.DTOs
{
    public class AuthorResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int BirthYear { get; set; }
        public int? YearOfDeath { get; set; }
    }
}
