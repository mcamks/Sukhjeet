using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSignalCore.Models
{
   public class UserChartHistory :BaseEntity
    {
        [Key]
        public long UserChartHistoryId { get; set; }

        public string Symbol { get; set; }

        public string ChartType { get; set; }

        public long UserId { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }
    }
}
