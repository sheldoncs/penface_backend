using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Web.Services;

namespace PSWebApp
{
    public class PenfaceOracle : Penface
    {
        
        protected PenfaceMySQL penSQL = new PenfaceMySQL();

        
        public void oraclePSConnect()
        {

            con = new OracleConnection();
            con.ConnectionString = "User Id=admin;Password=proxyfield;Data Source=HRPRD90";
            con.Open();
            Console.WriteLine("Connected to Oracle" + con.ServerVersion);


        }
        public List<BannerJournal> getBannerJournal(String dteStr)
        {
            oraclePSConnect();
            SQL sql = new SQL();
            List<BannerJournal> list = new List<BannerJournal>();

            try
            {
                using (OracleCommand command = new OracleCommand(sql.getBannerJournalSQL(), con))
                {
                    command.Parameters.Add(new OracleParameter("0", dteStr));
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                BannerJournal bj = new BannerJournal();
                                bj.acctCD = reader.GetString(0);
                                bj.emplId = reader.GetString(10);
                                bj.t_amt = reader.GetDouble(4);
                                bj.amt = reader.GetDouble(5);
                                bj.name = reader.GetString(7).Replace(",",";");
                                bj.dte = reader.GetDateTime(1).ToString().Substring(0,10);
                                bj.deptId = reader.GetString(2);
                                bj.drcr = reader.GetString(6);
                                bj.xnsDescr = reader.GetString(8);
                                bj.deptDescr = reader.GetString(9);
                                list.Add(bj);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }
            return list;

        }
        
        public List<FinancialData> getPenfaceFinancialDataRates()
        {
            oraclePSConnect();
            SQL sql = new SQL();
            List<FinancialData> list = new List<FinancialData>();

            try
            {
                using (OracleCommand command = new OracleCommand(sql.getPenfaceFinancialDataRates(), con))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                FinancialData fd = new FinancialData();

                                fd.emplid = reader.GetString(0);
                                fd.benefitPlan = reader.GetString(1);
                                fd.effDate = reader.GetString(2);
                                fd.pct = reader.GetInt32(3);
                                list.Add(fd);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }
            return list;
        }
        public  Dictionary<string,PenfaceServiceScheme> getPenfaceServiceScheme(){

             Dictionary<string, PenfaceServiceScheme> list = new Dictionary<string,PenfaceServiceScheme>();
            
           
            String filename = System.Web.HttpContext.Current.Server.MapPath("payroll\\penface\\personal\\" + "test" + ".txt");
            
            
            oraclePSConnect();
            SQL sql = new SQL();
            
            try
            {
                using (OracleCommand command = new OracleCommand(sql.getPenfaceServiceScheme(), con))
                {
                     using (OracleDataReader reader = command.ExecuteReader())
                    {
                          if (reader.HasRows)
                          {
                              using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filename))
                              {
                                  while (reader.Read())
                                  {
                                      PenfaceServiceScheme pss = new PenfaceServiceScheme();

                                      
                                      pss.emplid = reader.GetString(0);

                                      file.Write(reader.GetString(0));
                                      file.WriteLine();
                                      
                                      pss.start_dt = reader.GetString(1);
                                      if (!list.ContainsKey(pss.emplid))
                                        list.Add(pss.emplid,pss);
                                  }
                              }
                          }
                   }
                }
                
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }
            return list;

        }
        public List<FinancialData> getJoinerPenfaceFinanceAVCRates()
        {

            oraclePSConnect();
            SQL sql = new SQL();
            List<FinancialData> list = new List<FinancialData>();

            try
            {
                using (OracleCommand command = new OracleCommand(sql.getJoinerPenfaceFinanceAVCRates(), con))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                FinancialData fd = new FinancialData();

                                fd.emplid = reader.GetString(0);
                                fd.benefitPlan = reader.GetString(1);
                                fd.effDate = reader.GetString(2);
                                fd.pct = reader.GetInt32(3);
                                list.Add(fd);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }
            return list;

        }
        public Dictionary<string,PenfaceIndicators> getPenfaceIndicators()
        {

            oraclePSConnect();
            SQL sql = new SQL();
            Dictionary<string,PenfaceIndicators> list = new Dictionary<string,PenfaceIndicators>();

            try
            {
                using (OracleCommand command = new OracleCommand(sql.getPenfaceIndicators(), con))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PenfaceIndicators pi = new PenfaceIndicators();
                                pi.emplid = reader.GetString(0);
                                pi.national_id = reader.GetString(1);
                                list.Add(pi.emplid,pi);
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }
            return list;
        }
        public List<FinancialData> getPenfaceEmployerFinanceRates()
        {
            oraclePSConnect();
            SQL sql = new SQL();
            List<FinancialData> list = new List<FinancialData>();

            try
            {
                using (OracleCommand command = new OracleCommand(sql.getPenfaceEmployerFinanceRates(), con))
                {
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                FinancialData fd = new FinancialData();

                                fd.emplid = reader.GetString(0);
                                fd.benefitPlan = reader.GetString(1);
                                fd.effDate = reader.GetString(2);
                                fd.pct = reader.GetInt32(3);
                                list.Add(fd);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }
            return list;

        }
        public List<Employee> getEmployeeContInfo()
        {
            oraclePSConnect();
            List<Employee> list = new List<Employee>();
            SQL sql = new SQL();

            try
            {

                using (OracleCommand command = new OracleCommand(sql.getEmployeeContInfo(), con))
                {
                    DateTime thisDay = DateTime.Today;
                    String dte = thisDay.ToString().Substring(0, 9);
                    dte = dte.Trim();
                    dte = "4/30/2017";

                    command.Parameters.Add(new OracleParameter("0", dte));
                    command.Parameters.Add(new OracleParameter("1", dte));
                    command.Parameters.Add(new OracleParameter("2", dte));
                    command.Parameters.Add(new OracleParameter("3", dte));
                    command.Parameters.Add(new OracleParameter("4", dte));
                    command.Parameters.Add(new OracleParameter("5", dte));
                    command.Parameters.Add(new OracleParameter("6", dte));
                    command.Parameters.Add(new OracleParameter("7", dte));
                    command.Parameters.Add(new OracleParameter("8", dte));
                    command.Parameters.Add(new OracleParameter("9", dte));
                    command.Parameters.Add(new OracleParameter("10", dte));
                    command.Parameters.Add(new OracleParameter("11", dte));
                    command.Parameters.Add(new OracleParameter("12", dte));

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Employee employee = new Employee();
                                employee.emplid = reader.GetString(0);
                                employee.name = reader.GetString(1).Substring(1, reader.GetString(1).Length - 1);
                                employee.employer_cont = reader.GetDouble(2);
                                employee.employer_cont_orig = reader.GetDouble(3);
                                employee.avc_cont = reader.GetDouble(4);
                                employee.pay_end_dt = reader.GetDateTime(5).ToString().Substring(0, 9);
                                employee.employee_cont = reader.GetDouble(6);

                                list.Add(employee);

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }

            return list;



        }
        public List<Personal> getPenfacePersonal()
        {
            oraclePSConnect();
            List<Personal> list = new List<Personal>();
            SQL sql = new SQL();

            try
            {

                using (OracleCommand command = new OracleCommand(sql.getPenfacePersonal(), con))
                {

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Personal personal = new Personal();
                                personal.payno = reader.GetString(0);
                                personal.sex = reader.GetString(1);
                                personal.title = reader.GetString(2);
                                personal.surname = reader.GetString(3);
                                personal.empl_rcd = reader.GetInt32(4).ToString();
                                personal.plan_type = reader.GetString(5);
                                personal.benefit_nbr = reader.GetInt32(6).ToString();
                                personal.effdt = reader.GetDateTime(7).ToString().Substring(0, 9);
                                personal.forenames = reader.GetString(8);
                                personal.marital = reader.GetString(11);

                                list.Add(personal);

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }

            return list;

        }
        public List<PenfaceLeaverOracle> getPenfaceLeaverOracle(String startDt, String endDt)
        {
            SQL sql = new SQL();
            oraclePSConnect();

            List<PenfaceLeaverOracle> list = new List<PenfaceLeaverOracle>();
            try
            {
                using (OracleCommand command = new OracleCommand(sql.getPenfaceLeaverOracleSQL(), con))
                {
                    command.Parameters.Add(new OracleParameter("0", startDt));
                    command.Parameters.Add(new OracleParameter("1", endDt));
                   
                    
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                               
                                PenfaceLeaverOracle pl = new PenfaceLeaverOracle();
                                pl.emplid = reader.GetString(1);
                                pl.emplid = reader.GetString(14);
                                list.Add(pl);

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }

            return list;
        }

        public List<Employee> PenfaceFinanceSpreadsheetData(String dteStr)
        {
            oraclePSConnect();
            SQL sql = new SQL();

            List<Employee> list = new List<Employee>();

            try
            {
                using (OracleCommand command = new OracleCommand(sql.getPenfaceFinanceSpreadsheetSQL(), con))
                {
                    command.Parameters.Add(new OracleParameter("1", dteStr));
                    command.Parameters.Add(new OracleParameter("2", dteStr));
                    command.Parameters.Add(new OracleParameter("3", dteStr));
                    command.Parameters.Add(new OracleParameter("4", dteStr));
                    command.Parameters.Add(new OracleParameter("5", dteStr));
                    command.Parameters.Add(new OracleParameter("6", dteStr));
                    command.Parameters.Add(new OracleParameter("7", dteStr));
                    command.Parameters.Add(new OracleParameter("8", dteStr));
                    command.Parameters.Add(new OracleParameter("9", dteStr));
                    command.Parameters.Add(new OracleParameter("10", dteStr));
                    command.Parameters.Add(new OracleParameter("11", dteStr));
                    command.Parameters.Add(new OracleParameter("12", dteStr));
                    command.Parameters.Add(new OracleParameter("13", dteStr));

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Employee employee = new Employee();
                                
                                employee.emplid = reader.GetString(0);
                                employee.name = reader.GetString(1).Replace(",",";");
                                employee.employer_cont = reader.GetDouble(2);
                                employee.pay_end_dt = dteStr;
                                employee.employee_cont = reader.GetDouble(6);
                                employee.avc_cont = reader.GetDouble(4);

                                list.Add(employee);
                            }
                        }
                    }

                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }

            return list;
        }


       
    }
}