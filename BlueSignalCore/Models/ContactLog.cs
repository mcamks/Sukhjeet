using System;
using System.ComponentModel.DataAnnotations;

namespace BlueSignalCore.Models
{
    public partial class ContactLog : BaseEntity
    {
        [Key]
        public long ContactLogId { get; set; }
        public string ContactType { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string LogFrom { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        //public Nullable<long> ModifiedBy { get; set; }
        //public Nullable<long> ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }
        public long? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
        public long? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
