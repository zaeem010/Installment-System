using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AR_IS.Models;
using AR_IS.ViewModelQuery;

namespace AR_IS.ViewModel
{
    public class VoucherVM
    {
        public TranscationDetail TranscationDetail { get; set; }
        public IEnumerable<TranscationVMQ> TranscationList { get; set; }
        public TranscationVMQ Transcation { get; set; }
        public IEnumerable<ThirdLevel> Accounts { get; set; }
        public IEnumerable<ThirdLevel> Banks { get; set; }
        public Setting Setting { get; set; }
        public string wordinnum { get; set; }
        public string BankName { get; set; }
        //Monthly Expenses
        public IEnumerable<MonthlyExpenseVMQ> MonthlyExpense { get; set; }
        public string Month { get; set; }
        public decimal Profit { get; set; }
    }
}