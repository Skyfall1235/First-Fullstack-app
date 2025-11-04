using System.ComponentModel.DataAnnotations;

namespace FirstFullStackApp
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? ProfilePicture { get; set; }
        public required string Email { get; set; }
        public string? GithubLink { get; set; }
        public string? LinkedInLink { get; set; }
        public string? Bio { get; set; }
        public required ICollection<WorkExperience> WorkExperiences { get; set; } = new List<WorkExperience>();
    }
}
