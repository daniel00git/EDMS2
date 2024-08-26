/*using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<MetadataForm> MetadataForms { get; set; }
  }
}
*/

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)

    {
    }

    public DbSet<Metadata> MetadataForms { get; set; }

    /* public DbSet<LogFile> LogFile { get; set; }*/

    // Database name: Document Type
    public DbSet<DocumentTypeOption> DocumentType { get; set; }

        public DbSet<DocumentTypeLog> DocTypeLog { get; set; }

        public DbSet<DocumentFolder> DocFolder { get; set; }

        public DbSet<ContentType> ContentTypes { get; set; }

        public DbSet<Division> Divisions { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<UploadDocument> UploadDocuments { get; set; }
        public DbSet<LetterOfAppointment> LetterOfAppointments { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .HasOne(o => o.Site)
                .WithMany(c => c.Departments)
                .HasForeignKey(o => o.SiteId);
        }*/


    }
}
