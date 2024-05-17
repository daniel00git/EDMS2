/*using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;
public class MetadataForm
{
  public int Id { get; set; }

  [Required]
  [StringLength(100)]
  public string DocumentName { get; set; }

  [Required]
  [StringLength(100)]
  public string DocumentType { get; set; }

  [Required]
  [StringLength(100)]
  public string DocumentFormat { get; set; }

  [StringLength(500)]
  public string Message { get; set; }

  public string FilePath { get; set; }
}
*/
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
  public class MetadataForm
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Document Name is required")]
    [StringLength(100)]
    public string DocumentName { get; set; }

    [Required(ErrorMessage = "Document Type is required")]
    [StringLength(100)]
    public string DocumentType { get; set; }

    [Required(ErrorMessage = "Document Format is required")]
    [StringLength(100)]
    public string DocumentFormat { get; set; }

    [StringLength(500)]
    public string Message { get; set; }
    public string FilePath { get; set; }

    public byte[] FileContent { get; set; }
  }
}
