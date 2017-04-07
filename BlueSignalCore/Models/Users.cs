using System;
using System.ComponentModel.DataAnnotations;

namespace BlueSignalCore.Models
{
    public class Users : BaseEntity
    {
        [Key]
        public long UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string PhoneNumber { get; set; }

        //public string Url { get; set; } = "http://dashboard.blusignals.com/login";

        public bool PhoneNumberConfirmed { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public string UserName { get; set; }

        public DateTime? DOB { get; set; }

        public DateTime? CreatedDate { get; set; }

        public long? CreatedBy { get; set; }

        public bool? IsDeleted { get; set; }

        public long? DeletedBy { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool? IsActive { get; set; }
    }

    //public partial class Users : BaseEntity
    //{
    //    public long UserID { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    public string Email { get; set; }
    //    public bool EmailConfirmed { get; set; }
    //    public string PasswordHash { get; set; }
    //    public string PhoneNumber { get; set; }
    //    public bool PhoneNumberConfirmed { get; set; }
    //    public bool LockoutEnabled { get; set; }
    //    public int AccessFailedCount { get; set; }
    //    public string UserName { get; set; }
    //    public Nullable<System.DateTime> DOB { get; set; }
    //    //public Nullable<System.DateTime> CreatedDate { get; set; }
    //    //public Nullable<long> CreatedBy { get; set; }
    //    public Nullable<bool> IsDeleted { get; set; }
    //    public Nullable<long> DeletedBy { get; set; }
    //    public Nullable<System.DateTime> DeletedDate { get; set; }
    //    //public Nullable<bool> IsActive { get; set; }
    //}
}
