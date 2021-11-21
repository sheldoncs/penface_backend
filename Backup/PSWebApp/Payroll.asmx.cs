using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data.MySqlClient;
using System.Data.OracleClient;
using System.Net.Mail;
using System.IO;

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
               
                 attachOutputFile("webmaster@cavehill.uwi.edu", email, "Penface Leaver File Attachment",
                             "Penface Leaver File Attachment", filename, tempPath);
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
                             "Penface Finance Rates File Attachment", TextFile, path);
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

        private void attachOutputFile(String from, String to, String subject, String msg, String filename, String path)
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
                    message.Subject = "Penface Finance Rates File Attachment";
                    message.From = new System.Net.Mail.MailAddress("sheldon.spencer@cavehill.uwi.edu");
                    message.Body = "Penface Finance Rates File Attachment";
                    message.IsBodyHtml = true;

                    disposition.CreationDate = System.IO.File.GetCreationTime(path + filename);
                    disposition.ModificationDate = System.IO.File.GetLastWriteTime(path + filename);
                    disposition.ReadDate = System.IO.File.GetLastAccessTime(path + filename);
                    // Add the file attachment to this e-mail message.
                    message.Attachments.Add(data);

                    // Add your send code in here too
                    System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.cavehill.uwi.edu");
                    smtp.Send(message);

                }

            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);
            }




        }
        [WebMethod]
        public String ExecutePenfaceFinanceDataProcess()
        {
            PenfaceMySQL penfaceMySQL = new PenfaceMySQL();
            penfaceMySQL.deletePenfaceFinance();

            String TextFile = Server.MapPath("payroll\\penface\\finance data\\" + "Penfacefinancedata" + ".xlsx");

            ProcessXLS xls = new ProcessXLS();

            /*Get Data From Excel spread Sheet*/
            List<Penface.PenfaceFinanceData> list = xls.LoadXLS(TextFile);

            /*Insert Into 'PENFACE FINANCE' Table*/
            String status = penfaceMySQL.insertPenfaceFinance(list);

            if (status.Equals("Success")){
                status = "Finance Data Upload Completed Successfully!!";
            }
            return status;

        }

        [WebMethod]
        public String ExecuteBannerJournal(String dteStr, String email)
        {
            PenfaceOracle penfaceOracle = new PenfaceOracle();
            List<PSWebApp.Penface.BannerJournal> bjList = penfaceOracle.getBannerJournal(dteStr);
            String path =  Server.MapPath("payroll\\Banner Export\\");
            deleteAnyFile(path, "Banner_Journal.xls");

            ProcessXLS process = new ProcessXLS();
            String success = process.WriteBannerJournalXLS("Banner_Journal.xls", path, bjList);
            
            attachOutputFile("sheldon.spencer@cavehill.uwi.edu", email, "Penface Finance Details",
                           "Penface Finance Details", "Banner_Journal" + ".xls", path);

            return success;

        }
       
        [WebMethod]
        public String ExecutePenfaceFinanceSpreadsheetProcess(String dteStr)
        {
            PenfaceOracle penfaceOracle = new PenfaceOracle();
            PenfaceMySQL penfaceMySQL = new PenfaceMySQL();
            String filename = returnFileName("FIN");
            SQL sql = new SQL();
            String success = "success";
            String path = "";
            try
            {
                /*Insert Into Tracker*/
                penfaceMySQL.insertIntoinsertPenfaceTracker(filename, sql);

                path = Server.MapPath("payroll\\penface\\finance data\\");

                
                /*Delete Files in The Directory*/
                deleteFile(path);

                /*Get Penface Spread Sheet Data From Oracle*/
                List<PenfaceMySQL.Employee> employeeList = penfaceOracle.PenfaceFinanceSpreadsheetData(dteStr);

                ProcessXLS process = new ProcessXLS();

                /*Output Spreadsheet Data to XLSX file*/
                success = process.writePenfaceFinanceSpreadsheetXLS(filename, path, dteStr, employeeList);
              
            }
            catch (Exception e)
            {
                success = "failure " + success + e.Message + ", "+ path;
            }
            return success;

        }
        [WebMethod]
        public void getFSSUData(String dte)
        {
            PeopleSoft.PeopleSoftService ps = new PeopleSoft.PeopleSoftService();

           Object lst  =  ps.getFSSUEmployees(dte);
           ps.getFSSUEmployees(dte);
            /*
             dt.Columns.Add(New DataColumn("BENEFIT_PLAN", GetType(String)))
                 dt.Columns.Add(New DataColumn("EMPLID", GetType(String)))
        dt.Columns.Add(New DataColumn("FIRSTNAME", GetType(String)))
        dt.Columns.Add(New DataColumn("LASTNAME", GetType(String)))
        dt.Columns.Add(New DataColumn("DED_CLASS", GetType(String)))
        dt.Columns.Add(New DataColumn("DED_CUR", GetType(Double)))
        dt.Columns.Add(New DataColumn("EMPLOYER_CONT", GetType(Double)))
        dt.Columns.Add(New DataColumn("NATIONAL_ID", GetType(String)))
             * 
             * 
             */
        }
        [WebMethod]
        public String FinancialDataExport(String dteStr, String email)
        {
            PenfaceMySQL penfaceMySQL = new PenfaceMySQL();
            String filename = "";
            String path = Server.MapPath("payroll\\penface\\finance data\\");
            String success = penfaceMySQL.WritePenfaceFinanceData(dteStr, out filename, path);


            attachOutputFile("sheldon.spencer@cavehill.uwi.edu", email, "Penface Finance Details",
                            "Penface Finance Details", filename +".txt", path);

            return success;
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
