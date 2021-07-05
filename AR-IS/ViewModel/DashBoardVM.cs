using AR_IS.ViewModelQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModel
{
    public class DashBoardVM
    {
        public int Suppliers { get; set; }
        public int Customers { get; set; }
        public int Products { get; set; }
        public int AvailableVehicle { get; set; }
        public int TodayPurchase { get; set; }
        public int TodaySale { get; set; }
        public int MonthPurchase { get; set; }
        public int MonthSale { get; set; }
        public IEnumerable<MonthlyVehichleSaleVMQ> MonthlyVehicleSale { get; set; }
        public SaleAnalysisVMQ SaleAnalysis { get; set; }

        public IEnumerable<MonthlyVehichlePurchaseVMQ> MonthlyVehichlePurchase { get; set; }
        public PurchaseAnalysisVMQ PurchaseAnalysis { get; set; }
        public Decimal DailyRecovery { get; set; }
        public Decimal MonthlyRecovery { get; set; }
        public Decimal DailyPayment { get; set; }
        public Decimal MonthlyPayment { get; set; }
        public Decimal DailyDiscount { get; set; }
        public Decimal MonthlyDiscount { get; set; }
        public IEnumerable<ReceivableVMQ> Receivable { get; set; }
        public IEnumerable<PayableVMQ> Payable { get; set; }
        public IEnumerable<CashBankVMQ> CashBank { get; set; }
        public decimal Sales { get; set; }
        public decimal Expenses { get; set; }
        public Decimal DailyExpense { get; set; }
        public Decimal MonthlyExpense { get; set; }

        public IEnumerable<PurchaseWVehicleVMQ> TopPurchase { get; set; }
        public IEnumerable<SWIVMQ> TopSale { get; set; }
    }
}