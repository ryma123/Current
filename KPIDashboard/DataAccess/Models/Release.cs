using System;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DataAccess.Models
{
    [Table("kpiRelease")]
   public class Release
    {

        [Key]
        public int Id { get; set; }
    

        public string ReleaseName { get; set; }
        public virtual Product Product { get; set; }
    }
}
