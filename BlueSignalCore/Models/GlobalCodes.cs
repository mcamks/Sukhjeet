using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSignalCore.Models
{
   public class GlobalCodes : BaseEntity
    {
        [Key]
        public long GlobalCodeID { get; set; }

        public string GlobalCodeCategoryValue { get; set; }

        public string GlobalCodeValue { get; set; }

        public string GlobalCodeName { get; set; }

        public string Description { get; set; }

        public string ExternalValue1 { get; set; }

        public string ExternalValue2 { get; set; }

        public string ExternalValue3 { get; set; }

        public string ExternalValue4 { get; set; }

        public string ExternalValue5 { get; set; }

        public string ExternalValue6 { get; set; }

        public bool IsActive { get; set; }

        public int? SortOrder { get; set; }

        public long? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public long? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public bool? IsDeleted { get; set; }

        public long? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }



    }
}
