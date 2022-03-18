using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModelQuery
{
    public class SaleVMQ
    {
        public int Id { get; set; }
        public int Invid { get; set; }
        public int AccountNo { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Date { get; set; }
        public string CargoName { get; set; }
        public decimal CargoCharges { get; set; }
        public decimal NetAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Total { get; set; }
        public decimal Rtotal { get; set; }
        public decimal BTotal { get; set; }
        public string Vtype { get; set; }
        public int Comid { get; set; }
        public string Name { get; set; }
    }
    public class SaleWTCVMQ
    {
        public int Id { get; set; }
        public int Invid { get; set; }
        public int AccountNo { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Date { get; set; }
        public string CargoName { get; set; }
        public decimal CargoCharges { get; set; }
        public decimal NetAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Total { get; set; }
        public decimal Rtotal { get; set; }
        public decimal BTotal { get; set; }
        public string Vtype { get; set; }
        public int Comid { get; set; }
        public string Name { get; set; }
    }
    public class PrintSaleVMQ
    {
        public int Id { get; set; }
        public int Invid { get; set; }
        public int AccountNo { get; set; }
        public string Address { get; set; }
        public string Phone1 { get; set; }
        public string Email { get; set; }
        public string Date { get; set; }
        public int CargoId { get; set; }
        public decimal CargoCharges { get; set; }
        public decimal NetAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Total { get; set; }
        public decimal Ptotal { get; set; }
        public decimal BTotal { get; set; }
        public string Vtype { get; set; }
        public int Comid { get; set; }
        public string Name { get; set; }
    }
    public class GetVehicleVMQ
    {
        public string VehicleName { get; set; }
        public string MergingId { get; set; }
        
    }
    public class SWIVMQ
    {
       
        public int Id { get; set; }
        public int AccountNo { get; set; }
        public int Invid { get; set; }
        public string Date { get; set; }
        public int Comid { get; set; }
        public string Vtype { get; set; }
        public string KeyNo { get; set; }
        public string Color { get; set; }
        public string Remarks { get; set; }
        public string VehicleName { get; set; }
        public string ModelNo { get; set; }
        public string ChassiNo { get; set; }
        public string EngineNo { get; set; }
        public int PlanId { get; set; }
        public decimal TotalRate { get; set; }
        public decimal Interests { get; set; }
        public decimal AdvancePayment { get; set; }
        public decimal Discount { get; set; }
        public decimal BalanceTotal { get; set; }
        public decimal NetTotal { get; set; }
        public string Name { get; set; }
        public decimal FullFinal { get; set; }
        public decimal arrears { get; set; }
        public string Phone1 { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CNIC { get; set; }
        public decimal RemainingBalance { get; set; }
        public decimal ReceivedTotal { get; set; }


    }
    public class ReceiptPrintVMQ
    {
        public int Id { get; set; }
        public int AccountNo { get; set; }
        public int Comid { get; set; }
        public string ReceivedDate { get; set; }
        public int Invid { get; set; }
        public decimal PerMonthAmount { get; set; }
        public decimal Discounts { get; set; }
        public string Status { get; set; }
        public decimal ReceivedAmount { get; set; }
        public int InsId { get; set; }
        public string VehicleName { get; set; }
        public string EngineNo { get; set; }
        public string KeyNo { get; set; }
        public string InstallmentMonths { get; set; }
        public string Name { get; set; }
    }

}