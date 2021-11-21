using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data.MySqlClient;
using System.Data.OracleClient;
using System.Net.Mail;
using System.IO;
using System.Web.Script.Serialization;
using System.Xml;
using System.DirectoryServices;

namespace PSWebApp
{
    /// <summary>
    /// Summary description for Payroll
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Payroll : System.Web.Services.WebService
    {
        private const string Origin = "Origin";
        private const string AccessControlRequestMethod = "Access-Control-Request-Method";
        private const string AccessControlRequestHeaders = "Access-Control-Request-Headers";
        private const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
        private const string AccessControlAllowMethods = "Access-Control-Allow-Methods";
        private const string AccessControlAllowHeaders = "Access-Control-Allow-Headers";

        MySqlConnection conn;
        OracleConnection con;

        
        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod]
        public String ExecutPenfaceJoinerProcesses(String dteStr)
        {
            
            String success = "Success";
            Penface penface = new Penface();
            PenfaceOracle penfaceOracle = new PenfaceOracle();
            PenfaceMySQL penfaceMySQL = new PenfaceMySQL();
            Int32 total = 0;

            penface.setPayEndDt(dteStr);
            Util util = new Util();
            util.setPenface(penfaceMySQL);

            String filename = util.getNewFile(false, "JNR", dteStr);

             /**Setup Penface Personal Joiner Data**/

            try
            {

                List<Penface.Employee> contList = penfaceOracle.getEmployeeContInfo();
                List<Penface.Personal> penfacePersonal = penfaceOracle.getPenfacePersonal();
                List<Penface.PenfaceJoiners> penfaceJoiner = penfaceMySQL.getPenfaceJoiners();

                /*Reset Penface MySQL Tables*/
                penfaceMySQL.deleteFromPenfacePersonal();
                penfaceMySQL.deleteJoinerPenfaceFinanceRates();
                penfaceMySQL.deleteFromPenfaceService();
                penfaceMySQL.deleteFromPenfaceServiceScheme();
                penfaceMySQL.deletePenfaceIndicators();
                /*Reset Penface MySQL Tables*/

                penfaceMySQL.insertIntoPenfacePersonal(penfacePersonal, filename, penfaceJoiner, dteStr);


                /**Setup Penface Finance Basic Rates Data**/
                List<Penface.FinancialData> fdList = penfaceOracle.getPenfaceFinancialDataRates();
                penfaceMySQL.setupJoinerPenfaceFinanceBasicRates(fdList, penfaceJoiner, ref total);

                /**Setup Penface Finance AVC Rates Data**/
                List<Penface.FinancialData> penfaceAVCList = penfaceOracle.getJoinerPenfaceFinanceAVCRates();
                penfaceMySQL.SetupJoinerPenfaceFinanceAVCRates(penfaceAVCList, penfaceJoiner, ref total);

                /**Setup Penface Finance Employee Rates Data**/
                List<Penface.FinancialData> penfaceEmployerList = penfaceOracle.getPenfaceEmployerFinanceRates();
                penfaceMySQL.SetupJoinerPenfaceFinanceEmployerRates(penfaceEmployerList, penfaceJoiner, ref total);
                /**Insert Footer Penface Finance Rates Data**/
                penfaceMySQL.InsertFooterPenfaceFinanceEmployerRates(penfaceMySQL.getMySQLPenfaceFinanceRates(), total);

                /**Penface Service Scheme**/
                Dictionary<string,Penface.PenfaceServiceScheme> serviceSchemeList = penfaceOracle.getPenfaceServiceScheme();

                

                penfaceMySQL.InsertPenfaceServiceScheme(serviceSchemeList, penfaceJoiner);
                penfaceMySQL.InsertPenfaceServiceCompany(serviceSchemeList, penfaceJoiner);
                penfaceMySQL.InsertPenfaceServiceFooter(penfaceMySQL.getPenfaceServiceRecords());

                /**Setup Penface Indicators**/
                Dictionary<string,Penface.PenfaceIndicators> penfaceIndicatorList = penfaceOracle.getPenfaceIndicators();
                penfaceMySQL.InsertPenfaceIndicator(penfaceIndicatorList, penfaceJoiner);
                penfaceMySQL.InsertPenfaceIndicatorFooter(penfaceMySQL.getPenfaceIndicatorRecords());

            } catch (Exception e){

                success = "failure, message = " + e.Message;
                Console.WriteLine("{0} Exception caught.", e);

            }

            return success;
        }
        [WebMethod]
        public String validateCredintials(String username,String password)
        {
            String success = "";

            using (DirectoryEntry adsEntry = new DirectoryEntry("LDAP://OU=Staff,OU=Accounts,OU=Cave Hill,DC=cavehill,DC=uwi,DC=edu", "strAccountId", "strPassword"))
            {
                using (DirectorySearcher adsSearcher = new DirectorySearcher(adsEntry))
                {
                   
                    adsSearcher.Filter = "(sAMAccountName=" + username + ")";

                    try
                    {
                        SearchResult adsSearchResult = adsSearcher.FindOne();
                        

                       
                    }
                    catch (Exception ex)
                    {
                        // Failed to authenticate. Most likely it is caused by unknown user
                        // id or bad strPassword.
                        String strError = ex.Message;
                    }
                    finally
                    {
                        adsEntry.Close();
                    }

                }
            }
                return success;
        }
        [WebMethod]
        public String ExportPenfacePersonal()
        {
             String TextFile = Server.MapPath("payroll\\penface\\personal\\" + returnFileName("JNR") + ".txt");
             PenfaceMySQL penfaceMySQL = new PenfaceMySQL();
             String status = penfaceMySQL.WritePenfacePersonalJoinerFile(TextFile);
             return status;
        }
        [WebMethod]
        public String ExecutePenfaceLeaverProcesses(String startDate, String endDate, String payEndDt, String email)
        {

             DateTime stDate = DateTime.Parse(startDate);
             DateTime enDate = DateTime.Parse(endDate);

             PenfaceOracle penfaceOracle = new PenfaceOracle();
             PenfaceMySQL penfaceMySQL = new PenfaceMySQL();

             Util util = new Util();

             String filename = "";

             if (stDate < enDate)
             {
                 util.setPenface(penfaceMySQL);
                 
                 penfaceMySQL.deletePenfaceLeaveData();
                 List<PSWebApp.Penface.PenfaceLeaverOracle> leaverListOracle = penfaceOracle.getPenfaceLeaverOracle(startDate, endDate);
                 List<PSWebApp.Penface.PenfaceLeavers>  leaverListMySQL = penfaceMySQL.getPenfaceLeavers();

                 String success = penfaceMySQL.insertIntoPenfaceData(leaverListOracle, leaverListMySQL);


                 return success;
             }
             else if (stDate > enDate)
             {
                 return "Start Date Greater Than End Date";
             } else {
                 return "Check Date Formats.";
             }
        }
        [WebMethod]
        public String ExportPenfaceLeaver(String payEndDt, String email)
        {
            Util util = new Util();
            String filename = util.getNewFile(false, "LVR", payEndDt);
            String path = Server.MapPath("payroll\\penface\\leaver\\" + filename);
            String tempPath = Server.MapPath("payroll\\penface\\leaver\\");
            PenfaceMySQL penfaceMySQL = new PenfaceMySQL();

            String success = penfaceMySQL.WritePenfaceLeaversFile(path);

            if (success.Equals("success"))
            {
               String sentSuccess = "";
                 attachOutputFile("webmaster@cavehill.uwi.edu", email, "Penface Leaver File Attachment",
                             "Penface Leaver File Attachment", filename, tempPath, ref sentSuccess);
                success = sentSuccess;
            }

            return success;
        }
        [WebMethod]
        public List<Penface.PenfaceLeaverObj> getPenfaceLeavers()
        {
             PenfaceMySQL penfaceMySQL = new PenfaceMySQL();

             List <Penface.PenfaceLeaverObj> list = penfaceMySQL.getPenfaceLeaverMySQL();

             return list;
        }
        [WebMethod]
        public String ExportServiceDetails()
        {
            String TextFile = Server.MapPath("payroll\\penface\\personal\\" + returnFileName("JNR") + ".txt");
            PenfaceMySQL penfaceMySQL = new PenfaceMySQL();
            String status = penfaceMySQL.WritePenfaceServiceDetailsFile(TextFile);
            return status;
        }
        [WebMethod]
        public String ExportFinanceRates(String email)
        {
            String status = "Success";
            String TextFile = returnFileName("JNR") + ".txt";
            String path = Server.MapPath("payroll\\penface\\personal\\");
            PenfaceMySQL penfaceMySQL = new PenfaceMySQL();
            String rateStatus = penfaceMySQL.WritePenfaceFinanceRatesFile(path + TextFile);

            String indicatorStatus = penfaceMySQL.WritePenfaceIndicatorFile(path + TextFile);

            if ((!rateStatus.Equals("Success") || (!indicatorStatus.Equals("Success"))))
            {
                status = "Process Failure!!";
            }

            attachOutputFile("webmaster@cavehill.uwi.edu", email, "Penface Finance Rates File Attachment",
                             "Penface Finance Rates File Attachment", TextFile, path, ref status);
            return status;
        }
        private void sendMail(String from, String to, String subject, String msg, String filename)
        {
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();
            
            String[] word = to.Split(';');

            for (int i = 0; i < word.Length; i++)
                message.To.Add(word[i]);

            message.Subject = subject;
            message.From = new System.Net.Mail.MailAddress(from);
            message.Body = msg;
            message.IsBodyHtml = true;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.cavehill.uwi.edu");
            smtp.Send(message);

        }

        private void attachOutputFile(String from, String to, String subject, String msg, String filename, String path, ref String success)
        {

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

            try
            {
                using (Attachment data = new Attachment(path + filename, System.Net.Mime.MediaTypeNames.Application.Octet))
                {
                    // Add time stamp information for the file.             
                    System.Net.Mime.ContentDisposition disposition = data.ContentDisposition;

                    message.To.Add(to);
                    //message.To.Add(
                    message.Subject = subject;
                    message.From = new System.Net.Mail.MailAddress("sheldon.spencer@cavehill.uwi.edu");
                    message.Body = subject + " File Attachment";
                    message.IsBodyHtml = true;

                    disposition.CreationDate = System.IO.File.GetCreationTime(path + filename);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(path + filename);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(path + filename);
                    // Add the file attachment to this e-mail message.
                    message.Attachments.Add(data);

                    // Add your send code in here too
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.cavehill.uwi.edu");
                    smtp.Send(message);
                    success = "success";

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
                success = "failure";
            }




        }
        [WebMethod]
        public String ExecutePenfaceFinanceDataProcess()
        {
            PenfaceMySQL penfaceMySQL = new PenfaceMySQL();
            penfaceMySQL.deletePenfaceFinance();

            //String TextFile = Server.MapPath("payroll\\penface\\finance data\\" + "Penfacefinancedata" + ".xlsx");
            String TextFile = Server.MapPath("finance data\\" + "Penfacefinancedata" + ".csv");

             //ProcessXLS xls = new ProcessXLS();
            ProcessCSV csv = new ProcessCSV();
            /*Get Data From Excel spread Sheet*/
            //List<Penface.PenfaceFinanceData> list = xls.LoadXLS(TextFile);
            List<Penface.PenfaceFinanceData> list = csv.LoadCSV(TextFile);
            /*Insert Into 'PENFACE FINANCE' Table*/
            String status = penfaceMySQL.insertPenfaceFinance(list);

            if (status.Equals("Success")){
                status = "success";
            }
            return status;

        }

        [WebMethod]
        public String ExecuteBannerJournal(String dteStr, String email)
        {
            String success = "success";
            
            try
            {
                PenfaceOracle penfaceOracle = new PenfaceOracle();
                List<PSWebApp.Penface.BannerJournal> bjList = penfaceOracle.getBannerJournal(dteStr);
                String path = Server.MapPath("banner export\\");
                deleteAnyFile(path, "Banner_Journal.csv");



                /*
                 * ProcessXLS process = new ProcessXLS();
                 * success = process.WriteBannerJournalXLS("Banner_Journal.xlsx", path, bjList);
                */
                 ProcessCSV process = new ProcessCSV();
                success = process.WriteBannerJournalCSV("Banner_Journal.csv", path, bjList);
                attachOutputFile("sheldon.spencer@cavehill.uwi.edu", email, "Penface Finance Details",
                               "Penface Finance Details", "Banner_Journal" + ".csv", path, ref success);
                

            } catch (Exception e) {
                success = "failure " + e.Message;
                Console.WriteLine(success);
            }
            
            return success;

        }
       
        [WebMethod]
        public String emailPenfaceSheet(String email)
        {
            String path = Server.MapPath("finance data\\");
            String filename = "penfacefinancedata";
            String success = "";
            if (File.Exists(path + filename + ".csv"))
            {

                attachOutputFile("sheldon.spencer@cavehill.uwi.edu", email, "Penface Finance Details",
                               "Penface Finance Details", filename + ".csv", path, ref success);
            } else {
                success = "failure";
            }
            return success;
        }
        [WebMethod]
       
        public String ExecutePenfaceFinanceSpreadsheetProcess(String email, String dteStr)
        {
           
     
            PenfaceOracle penfaceOracle = new PenfaceOracle();

            Penface.Status status = new Penface.Status();
            
            
            PenfaceMySQL penfaceMySQL = new PenfaceMySQL();
            
            String filename = returnFileName("FIN");
            SQL sql = new SQL();
            String success = "success";
            String path = "";
            try
            {
                /*Insert Into Tracker*/
                penfaceMySQL.insertIntoinsertPenfaceTracker(filename, sql);

                //path = Server.MapPath("payroll\\penface\\finance data\\");
                path = Server.MapPath("finance data\\");
                //
                /*Delete Files in The Directory*/
                deleteFile(path);

                /*Get Penface Spread Sheet Data From Oracle*/
                List<PenfaceMySQL.Employee> employeeList = penfaceOracle.PenfaceFinanceSpreadsheetData(dteStr);



                /*Output Spreadsheet Data to XLSX file*/
                /*ProcessXLS process = new ProcessXLS();
                success = process.writePenfaceFinanceSpreadsheetXLS(filename, path, dteStr, employeeList);
                */

                ProcessCSV process = new ProcessCSV();
                success = process.writePenfaceFinanceSpreadsheetCSV(filename, path, dteStr, employeeList);
                attachOutputFile("sheldon.spencer@cavehill.uwi.edu", email, "Penface Finance Details",
                               "Penface Finance Spreadsheet", "penfacefinancedata" + ".csv", path, ref success);
            }
            catch (Exception e)
            {
                success = "failure " + success + e.Message + ", "+ path;
            }
            //status.success = success;
            //JavaScriptSerializer serializer = new JavaScriptSerializer();

            //Context.Response.Write(serializer.Serialize(status));
            return success;
        }
        [WebMethod]
        public String getFSSUData(String email, String dte)
        {
            PeopleSoft.PeopleSoftService ps = new PeopleSoft.PeopleSoftService();
            String success = "success";
            try { 
              Object lst = ps.getFSSUEmployees(dte);
              ProcessCSV process = new ProcessCSV();
              String path = Server.MapPath("fssu\\");
              deleteFile(path);
              process.outputFSSUData(lst, path,"FSSU.csv");

                attachOutputFile("sheldon.spencer@cavehill.uwi.edu", email, "FSSU",
                                "FSSU", "FSSU" + ".csv", path, ref success);

            }
            catch (Exception e) {
              success = "failure "+e.Message;
            }

            return success;
        }
        [WebMethod]
        public String FinancialDataExport(String dteStr, String email)
        {
            PenfaceMySQL penfaceMySQL = new PenfaceMySQL();
            String filename = "";
            String path = Server.MapPath("finance data\\");
            deleteAnyFile(path, "CH081801FIN.txt");
            String success = penfaceMySQL.WritePenfaceFinanceData(dteStr, out filename, path);


            attachOutputFile("sheldon.spencer@cavehill.uwi.edu", email, "Penface Finance Details",
                            "Penface Finance Details", filename +".txt", path, ref success);

            return success;
        }
        [WebMethod]
        public Boolean isAuthenticated(String username,String password)
        {
            PenfaceMySQL db = new PenfaceMySQL();
            Boolean isAuth = db.isAuthenticated(username, password);
            return isAuth;
        }
        [WebMethod]
        public List<Penface.ButtonMenu> getButtonMenu()
        {
            PenfaceMySQL penfaceMySQL = new PenfaceMySQL();
            List<Penface.ButtonMenu> list = penfaceMySQL.getButtonMenu();

            return list;
        }
        private String returnFileName(String fileType)
        {

            PenfaceMySQL penfaceMySQL = new PenfaceMySQL();
            String filename = penfaceMySQL.returnFileName();
            int seqNo_Len = filename.Length;
            filename = filename.Substring(1, seqNo_Len - 4);
            filename = filename + fileType;
            //SELECT MAX(FILE_NAME) AS CURR_FILE FROM `payroll`.`penface file track` WHERE FILE_NAME = ?

           

            return filename;
        }
        private void deleteAnyFile(String path, String filename)
        {
            System.IO.DirectoryInfo dir = new DirectoryInfo(@path);
            if (System.IO.File.Exists(path + filename))
            {
                foreach (FileInfo xlsFile in dir.GetFiles(@filename))
                {

                    xlsFile.Delete();
                }
            }
        }
        private void deleteFile(String path)
        {

            System.IO.DirectoryInfo dir = new DirectoryInfo(@path);

           
            if (System.IO.File.Exists(path + "penfacefinancedata.xlsx"))
            {
                foreach (FileInfo xlsFile in dir.GetFiles(@"penfacefinancedata.xlsx"))
                {

                    xlsFile.Delete();
                }
            }
        }

    }
}
