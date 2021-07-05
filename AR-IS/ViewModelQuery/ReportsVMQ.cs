using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModelQuery
{
    public class ReportsVMQ
    {
    }
    public class DeadlistVMQ
    {
        public int Id { get; set; }
        public int AccountNo { get; set; }
        public int Comid { get; set; }
        public string Date { get; set; }
        public int Invid { get; set; }
        public decimal PerMonthAmount { get; set; }
        public decimal Discounts { get; set; }
        public string Status { get; set; }
        public decimal ReceivedAmount { get; set; }
        public int InsId { get; set; }
        public string VehicleName { get; set; }
        public string EngineNo { get; set; }
        public string KeyNo { get; set; }
        public string CNIC { get; set; }
        public string Name { get; set; }
        public string Phone1 { get; set; }
        public string Email { get; set; }
        public string Month { get; set; }
        public string InstallmentMonths { get; set; }
    }
    public class VehicleProfitVMQ
    {
        
        public string VehicleName { get; set; }
        public string EngineNo { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Profit { get; set; }
        public string Date { get; set; }
    }

    public class FinalProfitVMQ
    {
        public decimal TotalProfit { get; set; }
        public decimal TotalSalePrice { get; set; }
        public decimal TotalCostPrice { get; set; }
    }

    public class PurchaseReportVMQ
    {
        public int Invid { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public decimal NetAmount { get; set; }
        public decimal AdditionalCharges { get; set; }
        public decimal Total { get; set; }
        public decimal TTotal { get; set; }
        public decimal TAdditionalCharges { get; set; }
        public decimal TNetAmount { get; set; }
    }
    public class PurchaseSummaryVMQ
    {
        public int Invid { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public decimal WithGSTTotal { get; set; }
        public string VehicleName { get; set; }
        public string EngineNo { get; set; }
        public decimal TTotal { get; set; }
        public decimal TAdditionalCharges { get; set; }
        public decimal TNetAmount { get; set; }
    }

    public class SaleReportVMQ
    {
        public int Invid { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string VehicleName { get; set; }
        public string EngineNo { get; set; }
        public decimal TotalRate { get; set; }
        public decimal NetTotal { get; set; }
        public decimal Interests { get; set; }
        public decimal Discount { get; set; }
        public decimal TRate { get; set; }
        public decimal TInterests { get; set; }
        public decimal TDiscount { get; set; }
        public decimal GrandNetTotal { get; set; }
    }
    public class SaleSummaryVMQ
    {
        public int Invid { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
       
        public string VehicleName { get; set; }
        public string EngineNo { get; set; }
        public decimal NetTotal { get; set; }
        public decimal GrandNetTotal { get; set; }

    }
}