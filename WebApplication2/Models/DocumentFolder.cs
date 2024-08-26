using System;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class DocumentFolder
    {
        [Key]
        public Guid FolderId { get; set; }
        public string FolderName { get; set; }
        public string ParentFolder { get; set; }
        public string FolderPath { get; set; }
        public string CreatedBy { get; set; }

        public string CreatedByEmail { get; set; }
    }
}
