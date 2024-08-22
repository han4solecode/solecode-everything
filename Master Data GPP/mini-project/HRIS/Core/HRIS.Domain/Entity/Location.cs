using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRIS.Domain.Entity
{
    [Table("locations")]
    public partial class Location
    {
        [Key]
        [Column("locationid")]
        public int Locationid { get; set; }

        [Column("address")]
        [StringLength(255)]
        public string Address { get; set; } = null!;

        [Column("deptno")]
        public int? Deptno { get; set; }

        [ForeignKey("Deptno")]
        [InverseProperty("Locations")]
        public virtual Department? DeptnoNavigation { get; set; }
    }
}