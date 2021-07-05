using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AR_IS.ViewModelQuery
{
    public class AccountVMQ
    {
    }
    public class FirstlevelVMQ
    {
       
        public int Id { get; set; }
        public int AccountNo { get; set; }
        public int Headid { get; set; }
        public string AccountTitle { get; set; }
        public string Head_Name { get; set; }
        public int Comid { get; set; }
    }
    public class SecondLevelVMQ
    {
        public int Id { get; set; }
        public int Headid { get; set; }
        public int SubHeadid { get; set; }
        public int AccountNo { get; set; }
        public string AccountTitle { get; set; }
        public int Comid { get; set; }
        public string Head_Name { get; set; }
        public string AccountName { get; set; }


    }
    public class ThirdLevelVMQ
    {
       
        public int Id { get; set; }
        public int AccountNo { get; set; }
        public int HeadId { get; set; }
        public int FirstLevelId { get; set; }
        public int SecondLevelId { get; set; }
        public string AccountTitle { get; set; }
        public string AccountType { get; set; }
        public decimal Cr { get; set; }
        public decimal Dr { get; set; }
        public int Comid { get; set; }
        public string FirstAccountTitle { get; set; }
        public string SecondAccountTitle { get; set; }
        public string Head_Name { get; set; }
        

    }
    public class MonthlyExpenseVMQ
    {
        public decimal Expenses { get; set; }
        public string AccountType { get; set; }
        public int SecondLevelId { get; set; }
        public decimal SumDr { get; set; }
        public int AccountNo { get; set; }
        public string AccountTitle { get; set; }

    }
    public class TranscationVMQ
    {
        public int Id { get; set; }
        public int Transid { get; set; }
        public string TransDes { get; set; }
        public string TransDate { get; set; }
        public int AccountNo { get; set; }
        public decimal Dr { get; set; }
        public decimal Cr { get; set; }
        public int Invid { get; set; }
        public string Vtype { get; set; }
        public string cle_date { get; set; }
        public string check_no { get; set; }
        public string Bank { get; set; }
        public string BankDes { get; set; }
        public string Remarks { get; set; }
        public string BankAcc { get; set; }
        public int V_No { get; set; }
        public int Comid { get; set; }
        public string AccountTitle { get; set; }
        public string AccountType { get; set; }

        

    }
    
    public class LedgerQuery
    {
        public int HeadId { get; set; }
        public int FirstLevelId { get; set; }
        public int SecondLevelId { get; set; }
        public decimal Dr { get; set; }
        public decimal Cr { get; set; }
        public int V_No { get; set; }
        public string BankDes { get; set; }
        public string BankAcc { get; set; }
        public string Bank { get; set; }
        public string check_no { get; set; }
        public string cle_date { get; set; }
        public string Vtype { get; set; }
        public string TransDate { get; set; }
        public string TransDes { get; set; }
        public string AccountTitle { get; set; }
        public string AccountType { get; set; }
        public int Transid { get; set; }
        public int AccountNo { get; set; }

    }
    public class TrailVMQ
    {
        public int AccountNo { get; set; }
        public string AccountTitle { get; set; }
        public decimal Dr { get; set; }
        public decimal Cr { get; set; }
        public decimal ccr { get; set; }
        public decimal cdr { get; set; }
        public int HeadId { get; set; }
    }
    public class PayRecVMQ
    {
        public int AccountNo { get; set; }
        public string AccountName { get; set; }
        public decimal Payable { get; set; }
        public decimal Receivable { get; set; }
    }
}