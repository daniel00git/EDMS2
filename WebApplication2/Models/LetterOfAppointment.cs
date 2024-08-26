using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    public class LetterOfAppointment
    {
        [Key]
        public Guid LoaId { get; set; }
        public string LoaReference { get; set; }
        public string LoaProjectName { get; set; }
    }
}
