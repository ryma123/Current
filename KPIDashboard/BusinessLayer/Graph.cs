using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
   public class Graph
    {
        public string productName;
        public string version;
        public List<string> percentages;
        public Graph(string productNamee, string versionn, List<string> percentagess)
        {
            productName = productNamee;
            version = versionn;
            percentages = percentagess;
        }
    }
}
