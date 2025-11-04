using System.ComponentModel.DataAnnotations;

namespace FirstFullStackApp;

public class WorkExperience
{
    public WorkExperience(WorkExperienceDTO dto, User user)
    {
        Company = dto.Company;
        Position = dto.Position;
        Description = dto.Description;
        StartDate = dto.StartDate;
        EndDate = dto.EndDate;
        Image = dto.Image;
        User = user;
        UserID = user.ID;
    }
    [Key]
    public int ID { get; set; }
    public string? Company { get; set; }
    public string? Position { get; set; }

    public string Description { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Image { get; set; }

    public User User { get; set; }

    public int UserID { get; set; }
}

public struct WorkExperienceDTO(WorkExperience workExperience)
{
    public static explicit operator WorkExperienceDTO(WorkExperience workExperience) => new(workExperience)
    {
        Company = workExperience.Company,
        Position = workExperience.Position,
        Description = workExperience.Description,
        StartDate = workExperience.StartDate,
        EndDate = workExperience.EndDate,
        Image = workExperience.Image,
    };
    public string? Company { get; set; } = workExperience.Company;
    public string? Position { get; set; } = workExperience.Position;
    public string Description { get; set; } = workExperience.Description;
    public DateOnly StartDate { get; set; } = workExperience.StartDate;
    public DateOnly? EndDate { get; set; } = workExperience.EndDate;
    public string? Image { get; set; } = workExperience.Image;
}