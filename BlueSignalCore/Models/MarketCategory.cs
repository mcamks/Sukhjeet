﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueSignalCore.Models
{
    public class MarketCategory : BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public string CategoryName { get; set; }
        public long SortOrder { get; set; }
        public Nullable<bool> IsActive { get; set; }
    }
}
