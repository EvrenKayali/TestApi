﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.Business.DTO
{
    public class CashierBranchDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int BranchId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Creator { get; set; }
    }
}
