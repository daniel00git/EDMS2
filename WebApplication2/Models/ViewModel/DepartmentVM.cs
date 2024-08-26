using Microsoft.Graph.Drives.Item.Items.Item.Workbook.Worksheets.Item.Charts.ItemWithName;
using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models.ViewModel
{
    public class DepartmentVM
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int SiteDepartmentId { get; set; }
        public string SiteDepartmentName { get; set; }
    }
}
