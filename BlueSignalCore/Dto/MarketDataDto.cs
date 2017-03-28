using AutoMapper;
using BlueSignalCommon;
using BlueSignalCore.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueSignalCore.Dto
{
    [NotMapped]
    public class MarketDataDto : MarketData
    {
        public string ProductName { get; set; }
        public string StrEntryDate
        {
            get
            {
                return EntryDate.GetShortDateString();
            }
        }
        public string StrExitDate
        {
            get
            {
                return ExitDate.GetShortDateString();
            }
        }

        public string Category { get; set; }

        public string StrEntryTimeValue
        {
            get
            {
                return EntryDate.Get12HourTimeString();
            }
        }

        public string StrEntryTime24Value
        {
            get
            {
                return EntryDate.Get24HourTimeString();
            }
        }

        public string Time { get; set; }

        public string StrCreatedTimeValue
        {
            get
            {
                return CreatedDate.Get12HourTimeString();
            }
        }
    }
}


