using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.ViewModel

{
    public class AddMetadata
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
        /* public IFormFile DocumentFile { get; set; }*/
        public string DocumentDescription { get; set; }
        public string? DocumentComment { get; set; }
        public IFormFile DocumentFile { get; set; }
        public string OriginalExtension { get; set; }

    }
}
