using AR_IS.ViewModelQuery;
using AR_IS.Models;
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
        public Decimal TodayPurchase { get; set; }
        public Decimal TodaySale { get; set; }
        public Decimal MonthPurchase { get; set; }
        public Decimal MonthSale { get; set; }
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
        public IEnumerable<PurchaseWVehicleVMQ> TopVehiclePurchase { get; set; }
        public IEnumerable<SWIVMQ> TopVehicleSale { get; set; }
        public IEnumerable<PurchaseWTCVMQ> TopPartsPurchase { get; set; }
        public IEnumerable<SaleWTCVMQ> TopPartsSale{ get; set; }
        public IEnumerable<GeneralUser> Userlist { get; set; }
        public int Total { get; set; }
        public int Active { get; set; }
        public int InActive { get; set; }
    }
}