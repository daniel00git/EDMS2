/*Model for document type options in drop down menu*/

using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class DocumentTypeOption
    {
        [Key]
        public int DocumentTypeOptionId { get; set; }
        public string DocumentTypeOptionValue { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedByEmail { get; set; }
    }
}
