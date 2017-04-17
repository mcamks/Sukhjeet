using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BlueSignalCore.Models
{
    public partial class MarketData : BaseEntity
    {
        [Key]
        public long Id { get; set; }

        public string ProductTypeID { get; set; }

        public string SymbolName { get; set; }

        public string SymbolCode { get; set; }

        public string QuantTradingType { get; set; }

        public decimal? Price { get; set; }

        public decimal? EntryPrice { get; set; }

        public DateTime? EntryDate { get; set; }

        public DateTime? ExitDate { get; set; }

        public string Result { get; set; }
        public long? CategoryId { get; set; }
        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; }

        public int CreatedBy { get; set; }


        public DateTime? ModifiedDate { get; set; }

        public int? ModifiedBy { get; set; }

    }




    public class WP_User
    {

        public WP_User()
        {
            bundles = new List<Models.bundles>();
        }
        public string ID { get; set; }
        public string user_login { get; set; }
        public string user_password { get; set; }
        public string user_nicename { get; set; }
        public string user_email { get; set; }
        public string user_registered { get; set; }
        public string display_name { get; set; }
        public string display_AdminKey { get; set; }

        public bool IsAlreadyRegisteredWithBSPortal { get; set; }

        //public List<WP_UserBundle> user_bundle { get; set; }

        public List<bundles> bundles { get; set; }
        /*
         * Commented by AJ on 21 March 2017
         * and made them dynamic according to the bundles, fetched according to the current loggedin user.
         */
        //public bool IsBluFactrol { get { return ((this.bundles.Any(x => (x.id == (int)WP_UserBundle.BLUFA) || x.id == (int)WP_UserBundle.BLUFM || IsAdminUser)) || IsAdminUser); } }
        //public bool IsBluNeutral { get { return ((this.bundles.Any(x => (x.id == (int)WP_UserBundle.BLUNA) || x.id == (int)WP_UserBundle.BLUNM)) || IsAdminUser); } }
        //public bool IsBluQuant { get { return ((this.bundles.Any(x => (x.id == (int)WP_UserBundle.BLUQA) || x.id == (int)WP_UserBundle.BLUQM)) || IsAdminUser); } }


        public bool IsBluFactrol { get { return (bundles.Any(x => x.name.ToLower().Trim().Contains("fractal")) || IsAdminUser); } }
        public bool IsBluNeutral { get { return (bundles.Any(x => x.name.ToLower().Trim().Contains("neural")) || IsAdminUser); } }
        public bool IsBluQuant { get { return (bundles.Any(x => x.name.ToLower().Trim().Contains("quant")) || IsAdminUser); } }
        public bool IsBluCombo { get { return ((IsBluNeutral && IsBluFactrol) || IsAdminUser); } }
        public bool IsAdminUser { get { return display_AdminKey.Contains("administrator"); } } //We will change logic once we get


        //public bool IsAdminUser { get { return true; } } //We will change logic once we get
        
    }


    public enum WP_UserBundle
    {
        BLUFM = 1,
        BLUFA = 2,
        BLUNM = 3,
        BLUNA = 4,
        BLUQM = 5,
        BLUQA = 6
    }

    public class Rootobject
    {
        public string response_code { get; set; }
        public string response_message { get; set; }
        public Response_Data response_data { get; set; }
    }

    public class Response_Data
    {
        public int member_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string is_complimentary { get; set; }
        public string registered { get; set; }
        public string cancellation_date { get; set; }
        public string last_logged_in { get; set; }
        public string last_updated { get; set; }
        public int days_as_member { get; set; }
        public string status { get; set; }
        public string status_name { get; set; }
        public string membership_level { get; set; }
        public string membership_level_name { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public object password { get; set; }
        public string phone { get; set; }
        public string billing_address { get; set; }
        public string billing_city { get; set; }
        public string billing_state { get; set; }
        public string billing_zip { get; set; }
        public string billing_country { get; set; }
        public string shipping_address { get; set; }
        public string shipping_city { get; set; }
        public string shipping_state { get; set; }
        public string shipping_zip { get; set; }
        public string shipping_country { get; set; }
        public List<bundles> bundles { get; set; }
        public Custom_Fields[] custom_fields { get; set; }
    }

    public class Custom_Fields
    {
        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }

    public class bundles
    {
        public int id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }

    public class FinanceInfo
    {
        public string Result { get; set; }
        public string Symbol { get; set; }
    }
}
