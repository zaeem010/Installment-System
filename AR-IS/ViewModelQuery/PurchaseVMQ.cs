using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModelQuery
{
    
    public class PurchaseVMQ
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
        public decimal Ptotal { get; set; }
        public decimal BTotal { get; set; }
        public string Vtype { get; set; }
        public int Comid { get; set; }
        public string Name { get; set; }
    }
    public class PurchaseWTCVMQ
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
        public decimal Ptotal { get; set; }
        public decimal BTotal { get; set; }
        public string Vtype { get; set; }
        public int Comid { get; set; }
        public string Name { get; set; }
    }
    public class PurchaseWVehicleVMQ
    {
        public int Id { get; set; }
        public int Invid { get; set; }
        public int AccountNo { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Date { get; set; }
        public decimal AdditionalCharges { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Total { get; set; }
        public decimal Ptotal { get; set; }
        public decimal BTotal { get; set; }
        public string Vtype { get; set; }
        public int Comid { get; set; }
        public string Name { get; set; }
    }
    public class PrintPurchaseVMQ
    {
        public int Id { get; set; }
        public int Invid { get; set; }
        public int AccountNo { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Date { get; set; }
        public string CargoName { get; set; }
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
    public class PrintPurchaseVehicleVMQ
    {
        public int Id { get; set; }
        public int Invid { get; set; }
        public int AccountNo { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Date { get; set; }
        public decimal AdditionalCharges { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Total { get; set; }
        public decimal Ptotal { get; set; }
        public decimal BTotal { get; set; }
        public string Vtype { get; set; }
        public int Comid { get; set; }
        public string Name { get; set; }
    }
    public class PWIVMQ
    {
        
        public int Id { get; set; }
        public int Invid { get; set; }
        public string VehicleName { get; set; }
        public string ModelNo { get; set; }
        public string ChassiNo { get; set; }
        public string EngineNo { get; set; }
        public decimal Rate { get; set; }
        public decimal GST { get; set; }
        public string KeyNo { get; set; }
        public decimal WithoutGSTTotal { get; set; }
        public decimal WithGSTTotal { get; set; }
        public decimal Discount { get; set; }
        public string Color { get; set; }
        public string Remarks { get; set; }
        public string Date { get; set; }
        public string Vtype { get; set; }
        public int Comid { get; set; }
        public string NumberOfInstallment { get; set; }
        public string InstallmentDate { get; set; }
        public decimal AdvancePayment { get; set; }
        public int AccountNo { get; set; }
        public decimal BalanceTotal { get; set; }
        public string Name { get; set; }
    }
}