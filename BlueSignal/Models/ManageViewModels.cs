using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using BlueSignalCore.Dto;

namespace BlueSignal.Models
{
    public class IndexViewModel
    {
        public bool HasPassword { get; set; }
        public IList<UserLoginInfo> Logins { get; set; }
        public string PhoneNumber { get; set; }
        public bool TwoFactor { get; set; }
        public bool BrowserRemembered { get; set; }
    }

    public class ManageLoginsViewModel
    {
        public IList<UserLoginInfo> CurrentLogins { get; set; }
        public IList<AuthenticationDescription> OtherLogins { get; set; }
    }

    public class FactorViewModel
    {
        public string Purpose { get; set; }
    }

    public class SetPasswordViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class AddPhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string Number { get; set; }
    }

    public class VerifyPhoneNumberViewModel
    {
        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }

    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
    }

    public class MarketDataViewModel
    {
        public string MarketDataDaily { get; set; }
        public string MarketDataWeekly { get; set; }
        public string MarketDataMonthly { get; set; }
        public string MarketDataQuartely { get; set; }
        public string MarketDataYearly { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ClosingPrice { get; set; }
        public string Exchange { get; set; }
        public string SymbolName { get; set; }

        public string AverageVolumn { get; set; }
        public string SymbolNameData { get; set; }

        public List<ChartDataModel> ChartData { get; set; }

        public string LastTradingDay { get; set; }

        public IEnumerable<MarketDataDto> MarketLists { get; set; }
        public IEnumerable<SelectItem> Categories { get; set; }
    }

    public class Data
    {
        public List<resultsData> results { get; set; }
    }

    public class resultsData
    {
        public string symbol { get; set; }
        public DateTime timestamp { get; set; }
        public DateTime tradingDay { get; set; }
        public string open { get; set; }
        public string high { get; set; }
        public string low { get; set; }
        public string close { get; set; }
        public string volume { get; set; }
        public string openInterest { get; set; }
    }

    public class ChartDataModel
    {
        public string symbol { get; set; }
        public DateTime tradingDay { get; set; }
        public string TradingDayTimeStamp { get; set; }
        public decimal? open { get; set; }
        public decimal? high { get; set; }
        public decimal? low { get; set; }
        public decimal? close { get; set; }
        public string volume { get; set; }
        public string openInterest { get; set; }
    }
}