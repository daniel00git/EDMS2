using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class UploadDocument
    {
        [Key]
        public Guid DocumentId { get; set; }
        public string DocumentName { get; set; }
        public byte[] DocumentContent { get; set; }
        public DateTime UploadDate { get; set; } // New property for upload date
        public string? CreatedBy { get; set; } // New property for created by
        public string? CreatedByEmail { get; set; } // New property for created by email
        public string OriginalExtension { get; set; }
    }
}
