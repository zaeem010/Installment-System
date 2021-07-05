using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModelQuery
{
    public class DashBoardVMQ
    {
    }
    public class MonthlyVehichleSaleVMQ
    {
        public int Vehicle { get; set; }
        public string Month { get; set; }
    }
    public class SaleAnalysisVMQ
    {
        public decimal TotalAmount { get; set; }
        public decimal TotalReceived { get; set; }
        public decimal TotalBalance { get; set; }
    }
    public class PurchaseAnalysisVMQ
    {
        public decimal TotalAmount { get; set; }
        public decimal Totalpaid { get; set; }
        public decimal TotalBalance { get; set; }
    }
    public class MonthlyVehichlePurchaseVMQ
    {
        public int Vehicle { get; set; }
        public string Month { get; set; }
    }
    public class ReceivableVMQ
    {
        public string AccountTitle { get; set; }
        public decimal Balance { get; set; }
        
    }

    public class PayableVMQ
    {
        public string AccountTitle { get; set; }
        public decimal Balance { get; set; }
        
    }
    public class CashBankVMQ
    {
        public string AccountTitle { get; set; }
        public decimal Balance { get; set; }

    }
    
}