using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstFullStackApp;

[ApiController]
[Route("[controller]")]
public class UserController(UserDbContext context) : ControllerBase
{
    #region User Info From ID
    [HttpGet, Route("[controller]/[action]/{id:int}")]
    public async Task<IActionResult> GetUserInfo(int id)
    { 
        User? user = await context.Users.FindAsync(id);
        return user != null ? Ok(user) : NotFound();
    }
        
    [HttpPost]
    public async Task<IActionResult> PostUserInfo([FromBody] User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return Ok(user);
    }
    #endregion
    [HttpGet, Route("[controller]/[action]/{userID:int}")]
    public async Task<IActionResult> GetUserExperiences(int userID)
    {
        List<WorkExperience> experiences = await context.WorkExperiences.Where(we => we.UserID == userID).ToListAsync();
        return Ok(experiences);
    }
        
    [HttpPost, Route("[controller]/[action]/{userID:int}")]
    public async Task<IActionResult> PostWorkExperiences(int userID, [FromBody] WorkExperienceDTO workExperience)
    {
        User? user = await context.Users.FindAsync(userID);
        if (user == null) return NotFound();
        return Ok(new WorkExperience(workExperience, user));
    }
}

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<WorkExperience> WorkExperiences { get; set; } = default!;
        
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<WorkExperience>()
            .HasOne(we => we.User) // A WorkExperience (we) has one User
            .WithMany(u => u.WorkExperiences) // That User (u) has many WorkExperiences
            .HasForeignKey(we => we.UserID) // The link is via the UserId field
            .OnDelete(DeleteBehavior.Cascade); // If a User is deleted, their experiences are also deleted.

        modelBuilder.Entity<User>().HasData(new User { ID = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@default.com", WorkExperiences = new List<WorkExperience>() });

    }
        
}