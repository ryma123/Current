using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DataAccess.Models
{
    [Table("KPI")]
  public  class KPI
        
    {
        [Key]
        public int Id { get; set; }
        public Product Product { get; set; }
        public Release Release { get; set; }
    }
}
