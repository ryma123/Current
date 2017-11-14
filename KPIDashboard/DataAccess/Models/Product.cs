using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DataAccess.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public string ProductId { get; set; }
        public string Location { get; set; }
         public List<Release> releases;
    }
}
