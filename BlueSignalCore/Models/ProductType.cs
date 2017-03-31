using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSignalCore.Models
{
    public partial class ProductType
    {
        [Key]
        public long Id { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public bool IsActive { get; set; }
    }
}
