using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSignalCore.Dto
{
    public class SelectItem
    {
        public string Text { get; set; }
        public string Value { get; set; }
        public long SortOrder { get; set; }
        public string Selected { get; set; }
    }
}
