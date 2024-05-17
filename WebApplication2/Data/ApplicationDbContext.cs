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

namespace WebApplication2.Data
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<WebApplication2.Models.MetadataForm> MetadataForms { get; set; }
  }
}
