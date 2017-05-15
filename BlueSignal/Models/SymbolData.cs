using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlueSignal.Models
{
    public class SymbolData
    {
        public string symbol { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string    exch { get; set; }

        public string    exchdisp { get; set; }
        public string typeDisp { get; set; }

    }
}