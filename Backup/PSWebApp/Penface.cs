using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using System.Data.OracleClient;

namespace PSWebApp
{

    public class Penface 
    {
        protected String payEndDt;

        protected MySqlConnection conn;

        protected OracleConnection con;

        public class Personal
        {
            public string payno { get; set; }
            public string sex { get; set; }
            public string title { get; set; }
            public string surname { get; set; }
            public string empl_rcd { get; set; }
            public string plan_type { get; set; }
            public string benefit_nbr { get; set; }
            public string effdt { get; set; }
            public string birth_dt { get; set; }
            public string norm_rtr_dt { get; set; }
            public string forenames { get; set; }
            public string marital { get; set; }
        }
        public class BannerJournal
        {
            public string acctCD { get; set; }
            public string deptDescr { get; set; }
            public string xnsDescr { get; set; }
            public double amt { get; set; }
            public double t_amt { get; set; }
            public string dte { get; set; }
            public string drcr { get; set; }
            public string deptId { get; set; }
            public string emplId { get; set; }
            public string name { get; set; }
        }

        public class FinancialData
        {
            public string emplid { get; set; }
            public string benefitPlan { get; set; }
            public string effDate { get; set; }
            public Int32 pct { get; set; }
        }
        public class Employee
        {

            public string emplid { get; set; }
            public string name { get; set; }
            public double employer_cont { get; set; }
            public double employer_cont_orig { get; set; }
            public double avc_cont { get; set; }
            public string pay_end_dt { get; set; }
            public double employee_cont { get; set; }

        }
        public class PenfaceServiceScheme
        {
            public string emplid { get; set; }
            public string start_dt { get; set; }
        }

        public class PenfaceJoiners
        {
            public string emplid { get; set; }
        }
        public class PenfaceLeavers
        {
            public string emplid { get; set; }
        }
        public class ButtonMenu
        {
            public string path { get; set; }
            public string title { get; set; }
        }
        public class PenfaceIndicators
        {
            public String emplid;
            public String national_id;
        }
        public void setPayEndDt(String strDte){

            payEndDt = strDte;

        }
        public class PenfaceFinanceData
        {
            public string name { get; set; }
            public string rec_type { get; set; }
            public string key_fld1 { get; set; }
            public string key_fld2 { get; set; }
            public string fin_elmt { get; set; }
            public string code { get; set; }
            public string eff_date { get; set; }
            public string category { get; set; }
            public string value { get; set; }
        }
        public class PenfaceLeaverOracle
        {
            public String emplid { get; set; }
            public String leaverDT { get; set; }
        }
        public class PenfaceLeaverObj
        {
            public String id { get; set; }
            public String emplId { get; set; }
        }

        
    
    }
    
}