using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModelQuery
{
    
    
    public class PurchaseWTCVMQ
    {
        public int Id { get; set; }
        public int Invid { get; set; }
        public int AccountNo { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
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
    public class PurchaseWVehicleVMQ
    {
        public int Id { get; set; }
        public int Invid { get; set; }
        public int AccountNo { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Date { get; set; }
        public decimal CargoCharges { get; set; }
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
    public class PrintPurchaseVehicleVMQ
    {
        public int Id { get; set; }
        public int Invid { get; set; }
        public int AccountNo { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Date { get; set; }
        public decimal CargoCharges { get; set; }
        public decimal NetAmount { get; set; }
        public decimal GrandTotal { get; set; }
        public decimal Total { get; set; }
        public decimal Ptotal { get; set; }
        public decimal BTotal { get; set; }
        public string Vtype { get; set; }
        public int Comid { get; set; }
        public string Name { get; set; }
    }
    
}