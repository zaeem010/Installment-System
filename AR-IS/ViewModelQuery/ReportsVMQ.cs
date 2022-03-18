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

    

    public class VehiclePurchaseReportVMQ
    {
        public int Invid { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public decimal NetAmount { get; set; }
        public decimal CargoCharges { get; set; }
        public decimal Total { get; set; }
        public int AccountNo { get; set; }
    }
    public class VehiclePurchaseSummaryVMQ
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
    public class PurchaseSummaryVMQ
    {
        public string Vtype { get; set; }
        public int Invid { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string ItemName { get; set; }
        public int Itemid { get; set; }
        public decimal Qty { get; set; }
        public decimal ItemUnit { get; set; }
        public decimal CTN { get; set; }
        public int AccountNo { get; set; }
        public decimal NetTotal { get; set; }



    }
    public class VehicleSaleReportVMQ
    {
        public int Invid { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public decimal TotalRate { get; set; }
        public decimal NetTotal { get; set; }
        public decimal Interests { get; set; }
        public decimal Discount { get; set; }
       
    }
    public class VehicleSaleSummaryVMQ
    {
       
        public string Date { get; set; }
        public string Name { get; set; }
        public int AccountNo { get; set; }
        public int Invid { get; set; }
        public string KeyNo { get; set; }
        public string Color { get; set; }
        public string Remarks { get; set; }
        public string VehicleName { get; set; }
        public string ModelNo { get; set; }
        public string ChassiNo { get; set; }
        public string EngineNo { get; set; }
        public decimal NetTotal { get; set; }
       
    }
    public class PurchaseReportVMQ
    {
        public int Invid { get; set; }
        public string Date { get; set; }
        public string Vtype { get; set; }
        public string Name { get; set; }
        public decimal NetAmount { get; set; }
        public decimal CargoCharges { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }


    }
    public class PurchaseReportDetailVMQ
    {
        public int Invid { get; set; }
        public int Itemid { get; set; }
        public string ItemName { get; set; }
        public decimal CP { get; set; }
        public decimal Qty { get; set; }
        public decimal NetTotal { get; set; }
        public string Date { get; set; }
        public string SrNo { get; set; }
        public string Vtype { get; set; }
        public int Comid { get; set; }
        public decimal ItemUnit { get; set; }
        public decimal CTN { get; set; }
        public int AccountNo { get; set; }

    }
    public class SaleReportVMQ
    {
        public int Invid { get; set; }
        public string Date { get; set; }
        public string Vtype { get; set; }
        public string Name { get; set; }
        public decimal NetAmount { get; set; }
        public decimal CargoCharges { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Total { get; set; }
    }

    public class SaleReportDetailVMQ
    {
        public int Invid { get; set; }
        public int Itemid { get; set; }
        public string ItemName { get; set; }
        public decimal CP { get; set; }
        public decimal Qty { get; set; }
        public decimal NetTotal { get; set; }
        public string Date { get; set; }
       
        public string Vtype { get; set; }
        public int Comid { get; set; }
        public decimal ItemUnit { get; set; }
        public decimal CTN { get; set; }
        public int AccountNo { get; set; }

    }
    public class SaleSummaryVMQ
    {
        public string Vtype { get; set; }
        public int Invid { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string ItemName { get; set; }
        public int Itemid { get; set; }
        public decimal Qty { get; set; }
        public decimal ItemUnit { get; set; }
        public decimal CTN { get; set; }
        public int AccountNo { get; set; }
        public decimal NetTotal { get; set; }
    }

    public class InventoryVMQ
    {
        public int Id { get; set; }
        public string Iname { get; set; }
        public int Openingstock { get; set; }
        public int Itemunit { get; set; }
        public int PurQty { get; set; }
        public int PurCTN { get; set; }
        public int SaleQty { get; set; }
        public int SaleCTN { get; set; }
    }
    public class OpeningVMQ
    {
        public int Id { get; set; }
        public string Iname { get; set; }
        public int Opening { get; set; }
       
    }
}