using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.ViewModel

{
    public class AddDocument
    {
        [Key]
        public Guid DocumentId { get; set; }
        public string DocumentName { get; set; }
        public IFormFile[] DocumentFile { get; set; }
        public string OriginalExtension { get; set; }
    }
}
