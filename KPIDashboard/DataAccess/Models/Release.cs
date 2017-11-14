using System;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DataAccess.Models
{
    [Table("Release")]
   public class Release
    {

        [Key]
        public string Releaseid { get; set; }
        public virtual Product Product { get; set; }
    }
}
