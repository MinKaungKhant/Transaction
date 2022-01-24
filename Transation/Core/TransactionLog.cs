using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class TransactionLog
    {
        public string filename { get; set; }
        public string process { get; set; }
        public DateTime time { get; set; }
    }
}
