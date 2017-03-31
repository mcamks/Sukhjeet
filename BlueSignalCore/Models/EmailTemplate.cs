using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSignalCore.Models
{
    public partial class EmailTemplate : BaseEntity
    {
        [Key]
        public int EmailTempelateID { get; set; }

        public Nullable<int> EmailType { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        //public int CreatedBy { get; set; }
        //public System.DateTime CreatedDate { get; set; }
        //public Nullable<int> ModifiedBy { get; set; }
        //public Nullable<System.DateTime> ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<int> DeletedBy { get; set; }
        public Nullable<System.DateTime> DeletedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}
