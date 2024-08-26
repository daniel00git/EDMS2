using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class ContentType
    {
        [Key]
        public int ContentTypeId { get; set; }
        public string ContentTypeValue { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string CreatedByEmail { get; set; }
    }
}
