using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace PSWebApp
{
    public class ProcessCSV : Penface
    {
        public String WriteBannerJournalCSV(String filename, String filePath, List<Penface.BannerJournal> bjList)
        {
            String success = "success";

            var csv = new StringBuilder();


            //Suggestion made by KyleMit
            var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                "ACCT_CD", "CHECK_DT", "DEPTID", "DEPT_DESCR", "EMPLID", "NAME", "XNS_DESCR", "T_AMOUNT", "AMT", "DRCR");
            csv.AppendLine(newLine);

            List<BannerJournal>.Enumerator bjListEnum = bjList.GetEnumerator();
            List<BannerJournal>.Enumerator tempBJListEnum = bjListEnum;

            while (bjListEnum.MoveNext())
            {
                BannerJournal bj = bjListEnum.Current;
                newLine = "";
                newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                       bj.acctCD, bj.dte, bj.deptId, bj.deptDescr.Replace(",", " "), bj.emplId,
                       bj.name, bj.xnsDescr.Replace(",", " "), bj.t_amt, bj.amt, bj.drcr);
                csv.AppendLine(newLine);
            }
            //after your loop
            filePath = filePath + filename;
            File.WriteAllText(filePath, csv.ToString());
            return success;
        }
        public String writePenfaceFinanceSpreadsheetCSV(String filename, String filePath, String payEndDt, List<PenfaceMySQL.Employee> employeeList)
        {
            String success = "success";

            var csv = new StringBuilder();

            Util util = new Util();
            Double total_amt = 0;
            Int32 cnt = 0;

            //Suggestion made by KyleMit

            var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13}",
                "NAME", "REC_TYPE", "KEY_FLD1", "KEY_FLD2", "FIN_ELMT", "CODE", "BASIS", "EFF_DATE",
                "CATEGORY", "VALUE", "CEASE_DATE", "REVIEW_CODE", "DUE_DT", "COMNO");
            csv.AppendLine(newLine);
            
            String[] word = payEndDt.Split('/');
            String month = word[0];
            String day = word[1];
            String year = word[2];
            String concateDate = util.adjString(Int32.Parse(day)) + util.adjString(Int32.Parse(month)) + year;

            DateTime now = DateTime.Now;
            String ddMMyyyyHHmm = util.adjString(now.Day) + util.adjString(now.Month) + now.Year + util.adjString(now.Hour) + util.adjString(now.Minute);

            newLine = string.Format("{0},{1},{2},{3},{4}", " ", "payroll", filename, concateDate, ddMMyyyyHHmm);
            csv.AppendLine(newLine);

            List<Employee>.Enumerator empListEnum = employeeList.GetEnumerator();
            List<Employee>.Enumerator tempEmpListEnum = empListEnum;

            /*
            while (empListEnum.MoveNext())
            {
                /*
                Employee employee = empListEnum.Current;
                newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                    employee.name, "FIV", employee.emplid, "", "C", "ERS", "", employee.pay_end_dt, "ERS", employee.employer_cont);
                csv.AppendLine(newLine);
                cnt++;
                
            }*/
            //    empListEnum = tempEmpListEnum;
            while (empListEnum.MoveNext())
            {
                Employee employee = empListEnum.Current;

                newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                   employee.name, "FIV", employee.emplid, "", "C", "ERS", "", employee.pay_end_dt, "ERS", employee.employer_cont);
                csv.AppendLine(newLine);
                cnt++;

                newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                    employee.name, "FIV", employee.emplid, "", "C", "BAS", "", employee.pay_end_dt, "EMP", employee.employee_cont);
                csv.AppendLine(newLine);
                total_amt = employee.employee_cont + total_amt;
                cnt++;

                newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                   employee.name, "FIV", employee.emplid, "", "C", "AVC", "", employee.pay_end_dt, "EMP", employee.avc_cont);
                csv.AppendLine(newLine);
                total_amt = employee.avc_cont + total_amt;
                cnt++;
            }
            /*
            empListEnum = tempEmpListEnum;
            while (empListEnum.MoveNext())
            {
                /*
                Employee employee = empListEnum.Current;
                newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                    employee.name, "FIV", employee.emplid, "", "C", "AVC", "", employee.pay_end_dt, "EMP", employee.avc_cont);
                csv.AppendLine(newLine);
                total_amt = employee.avc_cont + total_amt;
                cnt++;
            }*/
            //csv.AppendLine("");
            newLine = string.Format("{0},{1},{2}", "FIVTOT", cnt, total_amt);
            csv.AppendLine(newLine);

            filePath = filePath + "penfacefinancedata.csv";
            File.WriteAllText(filePath, csv.ToString());

            return success;
        }
        public List<PenfaceFinanceData> LoadCSV(String filename)
        {
            int cnt = 0;
            StreamReader reader = new StreamReader(File.OpenRead(@filename));
            String[] word = null;
            List<PenfaceFinanceData> list = new List<PenfaceFinanceData>();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                cnt++;
                PenfaceFinanceData pfd = new PenfaceFinanceData();
                if (cnt > 1)
                {

                    if (cnt == 2)
                    {
                        word = line.Split(',');
                        pfd.rec_type = word[1];
                        pfd.key_fld1 = word[2];
                        pfd.key_fld2 = word[3];
                        pfd.fin_elmt = word[4];
                    } else
                    {
                        word = line.Split(',');
                        
                            pfd.name = word[0];
                            pfd.rec_type = word[1];
                            pfd.key_fld1 = word[2];
                            pfd.fin_elmt = word[4];
                            pfd.code = word[5];
                            pfd.eff_date = word[7];
                            pfd.category = word[8];
                            pfd.value = word[9];

                    }
                    list.Add(pfd);
                }
                
            }
                return list;
        }
        public String outputFSSUData(Object lst,String filePath, String filename)
        {
            String success = "success";

            XmlSerializer ser = new XmlSerializer(lst.GetType(), "http://schemas.penface.com");

            XmlDocument xd = null;

            using (MemoryStream memStm = new MemoryStream())
            {
                ser.Serialize(memStm, lst);

                memStm.Position = 0;

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreWhitespace = true;

                using (var xtr = XmlReader.Create(memStm, settings))
                {
                    xd = new XmlDocument();
                    xd.Load(xtr);
                }
            }
            XmlNode root = xd.DocumentElement;
            XmlNode fssu;
            IEnumerator ienum = root.GetEnumerator();

            List<FSSU> fssuList = new List<FSSU>();
            while (ienum.MoveNext())
            {
                FSSU obj = new FSSU();
                fssu = (XmlNode)ienum.Current;
                for (int n = 0; n < fssu.ChildNodes.Count; n++)
                {
                    String name = fssu.ChildNodes[n].Name;
                    if (name.Equals("benefitPlan"))
                    {
                        obj.beneefitPlan = fssu.ChildNodes[n].InnerText;
                    }
                    else if (name.Equals("dedClass"))
                    {
                        obj.dedClass = fssu.ChildNodes[n].InnerText;
                    }
                    else if (name.Equals("dedCur"))
                    {
                        obj.dedCur = fssu.ChildNodes[n].InnerText;
                    }
                    else if (name.Equals("emplid"))
                    {
                        obj.emplid = fssu.ChildNodes[n].InnerText;
                    }
                    else if (name.Equals("employerCont"))
                    {
                        obj.employerCont = fssu.ChildNodes[n].InnerText;
                    }
                    else if (name.Equals("firstname"))
                    {
                        obj.firstname = fssu.ChildNodes[n].InnerText;
                    }
                    else if (name.Equals("lastname"))
                    {
                        obj.lastname = fssu.ChildNodes[n].InnerText;
                    }
                    else if (name.Equals("nationalID"))
                    {
                        obj.nationalID = fssu.ChildNodes[n].InnerText;
                    }
                    else if (name.Equals("payEndDt"))
                    {
                        obj.payEndDt = fssu.ChildNodes[n].InnerText;
                    }
                }
                fssuList.Add(obj);
            }

            var csv = new StringBuilder();
            String newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", 
                "BENEFIT_PLAN", "EMPLID", "FIRSTNAME", "LASTNAME", "DED_CLASS",
                "DED_CUR", "EMPLOYER_CONT", "NATIONAL_ID");
            csv.AppendLine(newLine);

            List<FSSU>.Enumerator fssuListEnum = fssuList.GetEnumerator();

            while (fssuListEnum.MoveNext())
            {
               FSSU fssuObj = fssuListEnum.Current;
                newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                    fssuObj.beneefitPlan, fssuObj.emplid, fssuObj.firstname,
                    fssuObj.lastname, fssuObj.dedClass, fssuObj.dedCur,
                    fssuObj.employerCont, fssuObj.nationalID);
                csv.AppendLine(newLine);
            }

            filePath = filePath + filename;
            File.WriteAllText(filePath, csv.ToString());
            return success;
            
           			

        }
    }
}