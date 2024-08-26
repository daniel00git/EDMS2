using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class Metadata
    {
        [Key]
        public Guid DocumentId { get; set; }
        public string DocumentName { get; set; }

        /*public string SelectedSiteName { get; set; }
        public string SelectedDepartmentName { get; set; }
        public string SelectedDocumentName { get; set; }*/

        public string SiteName { get; set; }
        public string DepartmentName { get; set; }
        public string MainFolderName { get; set; }
        public string SubFolderName { get; set; }
        public string SubDocumentName { get; set; }

        public string DocumentType { get; set; }
        public string DocumentFormat { get; set; }
        public string DocumentDescription { get; set; }
        public string? DocumentComment { get; set; }
        public byte[] DocumentContent { get; set; }
        public string Status { get; set; }
        public DateTime UploadDate { get; set; } // New property for upload date
        public string? CreatedBy { get; set; } // New property for created by
        public string? CreatedByEmail { get; set; } // New property for created by email
        public DateTime ApprovedDate { get; set; } // New property for approved date
        public string? ApprovedByEmail { get; set; } // New property for approved by
        public string OriginalExtension { get; set; }
    }
}
