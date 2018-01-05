using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    class TestNotExecuted : KPI
    {

        public string Planned { get; set; }
        public string Executed { get; set; }
        public string NotExecutedPerdcentage { get; set; }
    }
}
