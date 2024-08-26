using System.ComponentModel.DataAnnotations;
using WebApplication2.Models;
 

namespace WebApplication2.Models
{
    public class Metadata2
    {
        [Key]
        public int Id { get; set; }

        public string DocumentTypeOptionValue { get; set; }
        /*public List<SelectListItem> DocumentTypeOptionValue { get; set; }*/


        public string DocumentDescription { get; set; }

    }
}