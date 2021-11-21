using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices; 

namespace PSWebApp
{
    public class ProcessXLS : Penface
    {
        public List<PenfaceFinanceData> LoadXLS(String filename)
        {

            //Create COM Objects. Create a COM object for everything that is referenced
            Application xlApp = new Application();
            Workbook xlWorkbook = xlApp.Workbooks.Open(@filename);
            _Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Range xlRange = xlWorksheet.UsedRange;


            Int32 rowCount = xlRange.Rows.Count;
            Int32 colCount = xlRange.Columns.Count;


            List<PenfaceFinanceData> list = new List<PenfaceFinanceData>();

            for (int i = 1; i <= rowCount; i++)
            {
                PenfaceFinanceData pfd = new PenfaceFinanceData();
                if (i > 1)
                {

                    if (i == 2)
                    {
                        pfd.rec_type = xlRange.Cells[i, 2].Value2.ToString();
                        pfd.key_fld1 = xlRange.Cells[i, 3].Value2.ToString();
                        pfd.key_fld2 = xlRange.Cells[i, 4].Value2.ToString();
                        pfd.fin_elmt = xlRange.Cells[i, 5].Value2.ToString();
                       
                    }
                    if (i >= 3)
                    {
                        if (xlRange.Cells[i, 1].Value2 != null)
                          pfd.name = xlRange.Cells[i, 1].Value2.ToString();
                        if (xlRange.Cells[i, 2].Value2 != null)
                          pfd.rec_type = xlRange.Cells[i, 2].Value2.ToString();
                        if (xlRange.Cells[i, 3].Value2 != null)
                          pfd.key_fld1 = xlRange.Cells[i, 3].Value2.ToString();
                        if (xlRange.Cells[i, 5].Value2 != null)
                           pfd.fin_elmt = xlRange.Cells[i, 5].Value2.ToString();
                        if (xlRange.Cells[i, 6].Value2 != null)
                          pfd.code = xlRange.Cells[i, 6].Value2.ToString();
                        if (xlRange.Cells[i, 8].Value2 != null)
                          pfd.eff_date = xlRange.Cells[i, 8].Value2.ToString();
                        if (xlRange.Cells[i, 9].Value2 != null)
                          pfd.category = xlRange.Cells[i, 9].Value2.ToString();
                        if (xlRange.Cells[i, 10].Value2 != null)
                          pfd.value = xlRange.Cells[i, 10].Value2.ToString();

                       
                    }
                    list.Add(pfd);
                }
            }

            //cleanup
            GC.Collect();
            GC.WaitForPendingFinalizers();

            //release com objects to fully kill excel process from running in the background
            Marshal.ReleaseComObject(xlRange);
            Marshal.ReleaseComObject(xlWorksheet);

            //close and release
            xlWorkbook.Close();
            Marshal.ReleaseComObject(xlWorkbook);

            //quit and release
            xlApp.Quit();
            Marshal.ReleaseComObject(xlApp);


            return list;

        }
        public String WriteBannerJournalXLS(String filename, String path, List<Penface.BannerJournal> bjList)
        {
            Application oXL;
            _Workbook oWB;
            _Worksheet oSheet;
            Range oRng;
            Double total_amt = 0;
            String success = "success";
            object misvalue = System.Reflection.Missing.Value;

            //Start Excel and get Application object.
            oXL = new Microsoft.Office.Interop.Excel.Application();
            oXL.Visible = false;
            //true
            //Get a new workbook.
            oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
            oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

            //									

            
           
            try
            {
                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "ACCT_CD";
                oSheet.Cells[1, 2] = "CHECK_DT";
                oSheet.Cells[1, 3] = "DEPTID";
                oSheet.Cells[1, 4] = "DEPT_DESCR";
                oSheet.Cells[1, 5] = "EMPLID";
                oSheet.Cells[1, 6] = "NAME";
                oSheet.Cells[1, 7] = "XNS_DESCR";
                oSheet.Cells[1, 8] = "T_AMOUNT";
                oSheet.Cells[1, 9] = "AMT";
                oSheet.Cells[1, 10] = "DRCR";

                List<BannerJournal>.Enumerator bjListEnum = bjList.GetEnumerator();
                List<BannerJournal>.Enumerator tempBJListEnum = bjListEnum;
                Int32 i = 2;

                while (bjListEnum.MoveNext())
                {
                    BannerJournal bj = bjListEnum.Current;
                    oSheet.Cells[i, 1].Value = bj.acctCD;
                    oSheet.Cells[i, 2].Value = bj.dte;
                    oSheet.Cells[i, 3].Value = bj.deptId;
                    oSheet.Cells[i, 4].Value = bj.deptDescr;
                    oSheet.Cells[i, 5].Value = bj.emplId;
                    oSheet.Cells[i, 6].Value = bj.name;
                    oSheet.Cells[i, 7].Value = bj.xnsDescr;
                    oSheet.Cells[i, 8].Value = bj.t_amt;
                    oSheet.Cells[i, 9].Value = bj.amt;
                    oSheet.Cells[i, 10].Value = bj.drcr;
                    i++;
                }
                oXL.Visible = false;
                oXL.UserControl = false;
                oWB.SaveAs(path + filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB.Close();
               
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);
                success = "failure " + e.Message;
            }

            return success;
        }
        public String writePenfaceFinanceSpreadsheetXLS(String filename, String path, String payEndDt, List<PenfaceMySQL.Employee> employeeList)
        {
            Application oXL;
            _Workbook oWB;
            _Worksheet oSheet;
            Range oRng;
            Double total_amt = 0;
            String success = "success";
            object misvalue = System.Reflection.Missing.Value;

            Util util = new Util();

            
            try
            {
                //Start Excel and get Application object.
                oXL = new Microsoft.Office.Interop.Excel.Application();
                oXL.Visible = false;
                //true
                //Get a new workbook.
                oWB = (Microsoft.Office.Interop.Excel._Workbook)(oXL.Workbooks.Add(""));
                oSheet = (Microsoft.Office.Interop.Excel._Worksheet)oWB.ActiveSheet;

                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "NAME";
                oSheet.Cells[1, 2] = "REC_TYPE";
                oSheet.Cells[1, 3] = "KEY_FLD1";
                oSheet.Cells[1, 4] = "KEY_FLD2";
                oSheet.Cells[1, 5] = "FIN_ELMT";
                oSheet.Cells[1, 6] = "CODE";
                oSheet.Cells[1, 7] = "BASIS";
                oSheet.Cells[1, 8] = "EFF_DATE";
                oSheet.Cells[1, 9] = "CATEGORY";
                oSheet.Cells[1, 10] = "VALUE";
                oSheet.Cells[1, 11] = "CEASE_DATE";
                oSheet.Cells[1, 12] = "REVIEW_CODE";
                oSheet.Cells[1, 13] = "DUE_DT";
                oSheet.Cells[1, 13] = "COMNO";


                 oSheet.Cells[2, 2].Value = "payroll";
                 oSheet.Cells[2, 3].Value = filename;

                 //String month = payEndDt.Substring(0,2);
                 //String day = payEndDt.Substring(3,2);
                 //String year = payEndDt.Substring(6,4);
                String[] word = payEndDt.Split('/');
                String month = word[0];
                String day = word[1];
                String year = word[2];

                 oSheet.Cells[2, 4].Value = "'" +  day + month + year;
 
                 DateTime now = DateTime.Now;
                 String ddMMyyyyHHmm = util.adjString(now.Day) + util.adjString(now.Month) + now.Year + util.adjString(now.Hour) + util.adjString(now.Minute);

                 oSheet.Cells[2, 5].Value = "'" + ddMMyyyyHHmm;
 

                 List<Employee>.Enumerator empListEnum = employeeList.GetEnumerator();
                 List<Employee>.Enumerator tempEmpListEnum = empListEnum;
                 Int32 i = 3;
               
                 while (empListEnum.MoveNext())
                 {
                     Employee employee = empListEnum.Current;
                     oSheet.Cells[i, 1].Value = employee.name;
                     oSheet.Cells[i, 2].Value = "FIV";
                     oSheet.Cells[i, 3].Value = employee.emplid;
                     oSheet.Cells[i, 5].Value = "C";
                     oSheet.Cells[i, 6].Value = "ERS";
                     oSheet.Cells[i, 8].Value = employee.pay_end_dt;
                     oSheet.Cells[i, 9].Value = "ERS";
                     oSheet.Cells[i, 10].Value = employee.employer_cont;
                     i++;
                    
                }

                
                 empListEnum = tempEmpListEnum;
                 while (empListEnum.MoveNext())
                 {
                     Employee employee = empListEnum.Current;
                     oSheet.Cells[i, 1].Value = employee.name;
                     oSheet.Cells[i, 2].Value = "FIV";
                     oSheet.Cells[i, 3].Value = employee.emplid;
                     oSheet.Cells[i, 5].Value = "C";
                     oSheet.Cells[i, 6].Value = "BAS";
                     oSheet.Cells[i, 8].Value = employee.pay_end_dt;
                     oSheet.Cells[i, 9].Value = "EMP";
                     oSheet.Cells[i, 10].Value = employee.employee_cont;
                     total_amt = employee.employee_cont + total_amt;
                     
                     i++;
                 }

                 empListEnum = tempEmpListEnum;
                 while (empListEnum.MoveNext())
                 {
                     Employee employee = empListEnum.Current;
                     oSheet.Cells[i, 1].Value = employee.name;
                     oSheet.Cells[i, 2].Value = "FIV";
                     oSheet.Cells[i, 3].Value = employee.emplid;
                     oSheet.Cells[i, 5].Value = "C";
                     oSheet.Cells[i, 6].Value = "AVC";
                     oSheet.Cells[i, 8].Value = employee.pay_end_dt;
                     oSheet.Cells[i, 9].Value = "EMP";
                     oSheet.Cells[i, 10].Value = employee.avc_cont;
                     total_amt = employee.avc_cont + total_amt;

                     i++;
                 } 

                 oSheet.Cells[i, 1].Value = "FIVTOT";
                 oSheet.Cells[i, 2].Value = i-3;
                 oSheet.Cells[i, 3].Value = total_amt;

                oXL.Visible = false;
                oXL.UserControl = false;
                oWB.SaveAs(path+"penfacefinancedata", Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing,
                    false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                oWB.Close();
               
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);
                success = "failure " + e.Message;
            }

            return success;
        }
    }
}