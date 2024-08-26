using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models
{
    public class DocumentTypeLog
    {
        [Key]
        public int DocTypeLogId { get; set; }
        public DateTime DocTypeLogDate { get; set; } = DateTime.Now;
        public string DocTypeLogActivity { get; set; }
        public string DocTypeLogEmail { get; set; }
        public string DocTypeOptionId { get; set; }
        public string CurrentDocTypeOption { get; set; }
        public string NewDocTypeOption { get; set; }
    }
}
