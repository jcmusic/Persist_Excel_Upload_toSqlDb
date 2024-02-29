using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "datetime2(7)")]
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column(TypeName = "nvarchar(10)")]
        public string OrderNumber { get; set; } = "";

        [Required]
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; } = "";

        [Column(TypeName = "nvarchar(15)")]
        public string Phone { get; set; } = "";

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Total { get; set; }

        public bool? Shipped { get; set; }

        public DateTime Created { get; set; } = DateTime.UtcNow;

        public int? Priority { get; set; }

    }
}
