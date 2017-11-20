using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace DataAccess.Models
{
    [Table("joinReleaeseKPI")]
  public class JoinReleaseKpi
    {
        [Key]
        public int Id { get; set; }
        public Release Release { get; set; }
        public KPI kpi { get; set; }

    }
}
