using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models
{
    /*public class SiteLocation
    {
        [Key]
        public int SiteId { get; set; }
        public string SiteName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedByEmail { get; set; }

        *//*public virtual ICollection<Department> Departments { get; set; }*/
    /*public List<Department> Departments { get; set; }*//*
}

public class Department
{
    [Key]
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    *//*public int SiteDepartmentId { get; set; }*//*
    public SiteLocation SiteDeptId { get; set; }
    public string SiteDeptName { get; set; }
    *//*public string SiteName { get; set; }*//*
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public string? CreatedByEmail { get; set; }
}*/


    public class Division
    {
        [Key]
        public int DivisionId { get; set; }
        public string DivisionName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedByEmail { get; set; }

        /*public virtual ICollection<Department> Departments { get; set; }*/
    }

    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public int DivisionId { get; set; }
        /*public string SiteName { get; set; }*/
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? CreatedByEmail { get; set; }

        /*[ForeignKey("SiteId")]
        public virtual SiteLocation Site { get; set; }*/
    }

}
