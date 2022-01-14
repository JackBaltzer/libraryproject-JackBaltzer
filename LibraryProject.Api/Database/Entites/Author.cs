using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryProject.Api.Database.Entites
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(32)")]
        public string MiddleName { get; set; }

        [Column(TypeName = "smallint")]
        public int BirthYear { get; set; }

        [Column(TypeName = "smallint")]
        public int? YearOfDeath { get; set; }
    }
}
