using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Build.Evaluation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskMaster.Models;
namespace TaskMaster.Areas.Identity.Data;

public class TaskMasterDBContext : IdentityDbContext<IdentityUser>
{
    public TaskMasterDBContext(DbContextOptions<TaskMasterDBContext> options)
        : base(options)
    {
    }
    

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new TaskMasterUserEntityConfiguration());

    }


    public DbSet<TaskMaster.Models.Project>? Project { get; set; }


    public DbSet<TaskMaster.Models.Team>? Team { get; set; }

    public DbSet<TaskMaster.Models.Note>? Note { get; set; }
    
   

}

public class TaskMasterUserEntityConfiguration : IEntityTypeConfiguration<TaskMasterUser>
{
    public void Configure(EntityTypeBuilder<TaskMasterUser> builder)
    {
        builder.Property(u => u.firstName).HasMaxLength(255);
        builder.Property(u => u.lastName).HasMaxLength(255);
    }
}