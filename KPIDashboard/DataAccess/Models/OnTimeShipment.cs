using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    class OnTimeShipment : KPI
    {

        public int PlannedUseCases { get; set; }
        public int ActualUseCases { get; set; }
    }
}
