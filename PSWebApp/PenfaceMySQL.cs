using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace PSWebApp
{
    public class PenfaceMySQL : Penface
    {
       

        public void insertIntoPenfacePersonal(List<Personal> penfacePersonal, String filename, List<PenfaceJoiners> penfaceJoiner, String dteStr)
        {

            mySQLconnectSQL();
            SQL sql = new SQL();
            insertPenfacePersonalHeader(sql, filename, dteStr);
            insertPenfacePersonalDetails(penfacePersonal, penfaceJoiner);
            InsertPenfacePersonalFooter(getPenfacePersonalMySQL());

        }
        private void InsertPenfacePersonalFooter(Int32 cnt)
        {
            SQL sql = new SQL();

            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.InsertPenfacePersonalFooter(), conn))
                {
                    command.Parameters.Add(new MySqlParameter("0", "PERTOT"));
                    command.Parameters.Add(new MySqlParameter("1", cnt - 1));

                    command.ExecuteNonQuery();

                    command.Dispose();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }
            conn.Close();
        }
        public String WritePenfaceLeaversFile(String filename)
        {
            mySQLconnectSQL();
            SQL sql = new SQL();
            String status = "Success";

            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.selectPenfaceLeaveDataSQL(), conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filename, true))
                            {
                                while (reader.Read())
                                {
                                    file.Write((reader.IsDBNull(reader.GetOrdinal("REC_TYPE")) ? "" : reader.GetString("REC_TYPE")) + ","
                                                            + (reader.IsDBNull(reader.GetOrdinal("KEY_FLD1")) ? "" : reader.GetString("KEY_FLD1")) + ","
                                                            + (reader.IsDBNull(reader.GetOrdinal("KEY_FLD2")) ? "" : reader.GetString("KEY_FLD2")) + ","
                                                            + (reader.IsDBNull(reader.GetOrdinal("J_L_IND")) ? "" : reader.GetString("J_L_IND")) + ","
                                                            + (reader.IsDBNull(reader.GetOrdinal("SEX")) ? "" : reader.GetString("SEX")) + ","
                                                            + (reader.IsDBNull(reader.GetOrdinal("TITLE")) ? "" : reader.GetString("TITLE")) + ","
                                                            + (reader.IsDBNull(reader.GetOrdinal("SURNAME")) ? "" : reader.GetString("SURNAME")) + ","
                                                            + (reader.IsDBNull(reader.GetOrdinal("FORENAMES")) ? "" : reader.GetString("FORENAMES")) + ","
                                                            + (reader.IsDBNull(reader.GetOrdinal("BIRTH_DT")) ? "" : reader.GetString("BIRTH_DT")) + ","
                                                            + (reader.IsDBNull(reader.GetOrdinal("BIRTH_DT")) ? "" : reader.GetString("BIRTH_DT")) + ","
                                                            + (reader.IsDBNull(reader.GetOrdinal("BIRTH_DT_VERIFIED")) ? "" : reader.GetString("BIRTH_DT_VERIFIED"))
                                                            + (reader.IsDBNull(reader.GetOrdinal("NI_NO")) ? "" : reader.GetString("NI_NO"))
                                                            + (reader.IsDBNull(reader.GetOrdinal("NORM_RTR_DT")) ? "" : reader.GetString("NORM_RTR_DT"))
                                                            + (reader.IsDBNull(reader.GetOrdinal("PAYNO")) ? "" : reader.GetString("PAYNO"))
                                                            + (reader.IsDBNull(reader.GetOrdinal("OTHER_REF")) ? "" : reader.GetString("OTHER_REF"))
                                                            + (reader.IsDBNull(reader.GetOrdinal("LEAVE_DT")) ? "" : reader.GetString("LEAVE_DT"))
                                                            + (reader.IsDBNull(reader.GetOrdinal("MARITAL")) ? "" : reader.GetString("MARITAL")));

                                    file.WriteLine();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                status = "Writing to File Failed";
                Console.WriteLine("{0} Exception caught.", e);
                status = "Writing to File Failed " + e.Message;
            }

            return status;
        }
        public String WritePenfaceIndicatorFile(String filename)
        {
            mySQLconnectSQL();
            SQL sql = new SQL();
            String status = "Success";

            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.getPenfaceIndicatorSQL(), conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filename, true))
                            {
                                while (reader.Read())
                                {
                                    file.Write((reader.IsDBNull(reader.GetOrdinal("REC_TYPE")) ? "" : reader.GetString("REC_TYPE")) + ","
                                                            + (reader.IsDBNull(reader.GetOrdinal("KEY_FLD1")) ? "" : reader.GetString("KEY_FLD1")) + ","
                                                            + (reader.IsDBNull(reader.GetOrdinal("REF_CODE")) ? "" : reader.GetString("REF_CODE")) + ","
                                                            + (reader.IsDBNull(reader.GetOrdinal("EFFECTIVE_DT")) ? "" : reader.GetString("EFFECTIVE_DT")) + ","
                                                            + (reader.IsDBNull(reader.GetOrdinal("INDICATOR")) ? "" : reader.GetString("INDICATOR")));
                                    file.WriteLine();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                status = "Writing to File Failed";
                Console.WriteLine("{0} Exception caught.", e);
                status = "Writing to File Failed " + e.Message;
            }

            return status;
        }
        public String WritePenfaceServiceDetailsFile(String filename){

            mySQLconnectSQL();
            SQL sql = new SQL();
            String status = "failure";
            try
            {
               
                using (MySqlCommand command = new MySqlCommand(sql.getPenfaceServiceSQL(), conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filename, true))
                            {
                                while (reader.Read())
                                {
                                    status = "success";
                                    file.Write((reader.IsDBNull(reader.GetOrdinal("REC_TYPE")) ? "" : reader.GetString("REC_TYPE")) + ","
                                                             + (reader.IsDBNull(reader.GetOrdinal("KEY_FLD1")) ? "" : reader.GetString("KEY_FLD1")) + ","
                                                             + (reader.IsDBNull(reader.GetOrdinal("KEY_FLD2")) ? "" : reader.GetString("KEY_FLD2")) + ","
                                                             + (reader.IsDBNull(reader.GetOrdinal("ACT_IND")) ? "" : reader.GetString("ACT_IND")) + ","
                                                             + (reader.IsDBNull(reader.GetOrdinal("START_DT")) ? "" : reader.GetString("START_DT")) + ","
                                                             + (reader.IsDBNull(reader.GetOrdinal("END_DT")) ? "" : reader.GetString("END_DT")) + ","
                                                             + (reader.IsDBNull(reader.GetOrdinal("FUND")) ? "" : reader.GetString("FUND")) + ","
                                                             + (reader.IsDBNull(reader.GetOrdinal("SCHEME")) ? "" : reader.GetString("SCHEME")) + ","
                                                             + (reader.IsDBNull(reader.GetOrdinal("COMPANY")) ? "" : reader.GetString("COMPANY")) + ","
                                                             + (reader.IsDBNull(reader.GetOrdinal("LOCATION")) ? "" : reader.GetString("LOCATION")) + ","
                                                             + (reader.IsDBNull(reader.GetOrdinal("WRK_CAT")) ? "" : reader.GetString("WRK_CAT")) + ","
                                                             + (reader.IsDBNull(reader.GetOrdinal("PART_TM_HRS")) ? "" : reader.GetString("PART_TM_HRS")) + ","
                                                             + (reader.IsDBNull(reader.GetOrdinal("FLEX_RATE")) ? "" : reader.GetString("FLEX_RATE")));
                                    file.WriteLine();

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                status = "Writing to File Failed";
                Console.WriteLine("{0} Exception caught.", e);
                status = "Writing to File Failed " + e.Message;
            }

            return status;
        }
        public String WritePenfaceFinanceData(String eff_dte, out String txtFile, String path)
        {
            mySQLconnectSQL();
            SQL sql = new SQL();
            String status = "Success";
            String filename = getFinanceDataFileName();
            txtFile = filename;

            Int32 cnt = 1;
            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.selectPenfaceFinanceSQL(), conn))
                {
                    
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                       
                        if (reader.HasRows)
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(path + filename+".txt", true))
                            {
                                while (reader.Read())
                                {
                                    file.Write((reader.IsDBNull(reader.GetOrdinal("REC_TYPE")) ? "" : reader.GetString("REC_TYPE")) + ","
                                      + (reader.IsDBNull(reader.GetOrdinal("KEY_FLD1")) ? "" : reader.GetString("KEY_FLD1")) + ","
                                      + (reader.IsDBNull(reader.GetOrdinal("KEY_FLD2")) ? "" : reader.GetString("KEY_FLD2")) + ","
                                      + (reader.IsDBNull(reader.GetOrdinal("FIN_ELMT")) ? "" : reader.GetString("FIN_ELMT")) + ","
                                      + (reader.IsDBNull(reader.GetOrdinal("CODE")) ? "" : reader.GetString("CODE")) + ","
                                      + (reader.IsDBNull(reader.GetOrdinal("EFF_DATE")) || reader.GetString("EFF_DATE") =="" ? "" : eff_dte) + ","
                                      + (reader.IsDBNull(reader.GetOrdinal("CATEGORY")) ? "" : reader.GetString("CATEGORY")) + ","
                                      + (reader.IsDBNull(reader.GetOrdinal("VALUE")) ? "" : reader.GetString("VALUE")));

                                    file.WriteLine();
                                }
                                
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                status = "Writing to File Failed";
                Console.WriteLine("{0} Exception caught.", e);
                status = "Writing to File Failed " + e.Message;
            }

            return status;


        }
        public Boolean isAuthenticated(String username, String password)
        {
            SQL sql = new SQL();
            mySQLconnectSQL();
            string sqlstmt = string.Format(sql.getLoginSQL(), username, password);
            try
            {
                using (MySqlCommand command = new MySqlCommand(sqlstmt, conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Exception caught.", e);       
            }
            return false;
        }
        public List<Penface.PenfaceLeaverObj> getPenfaceLeaverMySQL()
        {
            mySQLconnectSQL();
            SQL sql = new SQL();

            List<Penface.PenfaceLeaverObj> list = new List<Penface.PenfaceLeaverObj>();
            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.getPenfaceLeavers(), conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Penface.PenfaceLeaverObj obj = new Penface.PenfaceLeaverObj();
                                obj.emplId = reader.GetString("emplid");
                                list.Add(obj);
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
        public String WritePenfaceFinanceRatesFile(String filename)
        {
            mySQLconnectSQL();
            SQL sql = new SQL();
            String status = "Success";
            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.getFinanceRatesSQL(), conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filename, true))
                            {
                                while (reader.Read())
                                {

                                   
                                    file.Write((reader.IsDBNull(reader.GetOrdinal("REC_TYPE")) ? "" : reader.GetString("REC_TYPE")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("KEY_FLD1")) ? "" : reader.GetString("KEY_FLD1")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("KEY_FLD2")) ? "" : reader.GetString("KEY_FLD2")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("RATE_CLASS")) ? "" : reader.GetString("RATE_CLASS")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("CONT_TYPE")) ? "" : reader.GetString("CONT_TYPE")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("EFF_DATE")) ? "" : reader.GetString("EFF_DATE")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("NI")) ? "" : reader.GetString("NI")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("RATE_TYPE")) ? "" : reader.GetString("RATE_TYPE")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("VALUE")) ? "" : reader.GetString("VALUE")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("SOURCE")) ? " " : reader.GetString("SOURCE")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("REFERENCE")) ? "" : reader.GetString("REFERENCE")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("MAT_DATE")) ? "" : reader.GetString("MAT_DATE")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("A_Y_SCHEME")) ? "" : reader.GetString("A_Y_SCHEME")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("A_Y_PERIOD")) ? "" : reader.GetString("A_Y_PERIOD")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("A_Y_BASIS")) ? "" : reader.GetString("A_Y_BASIS")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("UNIT_RT")) ? "" : reader.GetString("UNIT_RT")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("ACCUM")) ? "" : reader.GetString("ACCUM")));

                                    file.WriteLine();
                                   
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                status = "Writing to File Failed";
                Console.WriteLine("{0} Exception caught.", e);
                status = "Writing to File Failed " + e.Message;
            }

            return status;
        }
        private String getFinanceDataFileName()
        {
            SQL sql = new SQL();
            String filename = "";
            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.getFinanceDataFileNameSQL(), conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                filename = reader.GetString("KEY_FLD1");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
               
                Console.WriteLine("{0} Exception caught.", e);
                
            }

            return filename;
        }
        public String WritePenfacePersonalJoinerFile(String filename)
        {
            mySQLconnectSQL();
            SQL sql = new SQL();
            String status = "Success";
            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.getPenfacePersonalSQL(), conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@filename))
                            {
                               while (reader.Read())
                               {
                                   
                                   file.Write((reader.IsDBNull(reader.GetOrdinal("REC_TYPE")) ? "" : reader.GetString("REC_TYPE")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("KEY_FLD1")) ? "" : reader.GetString("KEY_FLD1")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("KEY_FLD2")) ? "" : reader.GetString("KEY_FLD2")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("J_L_IND")) ? "" : reader.GetString("J_L_IND")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("SEX")) ? "" : reader.GetString("SEX")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("TITLE")) ? "" : reader.GetString("TITLE")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("SURNAME")) ? "" : reader.GetString("SURNAME")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("FORENAMES")) ? "" : reader.GetString("FORENAMES")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("BIRTH_DT")) ? "" : reader.GetString("BIRTH_DT")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("BIRTH_DT_VERIFIED")) ? " " : reader.GetString("BIRTH_DT_VERIFIED")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("NI_NO")) ? "" : reader.GetString("NI_NO")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("NORM_RTR_DT")) ? "" : reader.GetString("NORM_RTR_DT")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("PAYNO")) ? "" : reader.GetString("PAYNO")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("OTHER_REF")) ? "" : reader.GetString("OTHER_REF")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("DATE_LEAVE")) ? "" : reader.GetString("DATE_LEAVE")) + ","
                                       + (reader.IsDBNull(reader.GetOrdinal("MARITAL")) ? "" : reader.GetString("MARITAL")));
                                   file.WriteLine();
                                   
                               }
                            }
                            
                        }
                    }
                }
            }
            catch (Exception e)
            {
                status = "Writing to File Failed";
                Console.WriteLine("{0} Exception caught.", e);
                status = "Writing to File Failed " + e.Message ;
            }

            this.insertIntoinsertPenfaceTracker(filename, sql);

            conn.Close();

            return status;

        }
        private String testIsNull(String v)
        {
            String val = "";
            if (v != null)
            {
                val = v;
            }
            else
            {
                val = "";
            }

            return val;
        }
        public List<PenfaceJoiners> getPenfaceJoiners()
        {
            mySQLconnectSQL();
            SQL sql = new SQL();
            List<PenfaceJoiners> list = new List<PenfaceJoiners>();

            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.getPenfaceJoiner(), conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PenfaceJoiners joiners = new PenfaceJoiners();
                                joiners.emplid = reader.GetString(0);
                                list.Add(joiners);
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
        public Int32 getMySQLPenfaceFinanceRates()
        {
            mySQLconnectSQL();
            SQL sql = new SQL();
            Int32 cnt = 0;
            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.getMySQLPenfaceFinanceRates(), conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cnt++;
                            }
                        }
                    }


                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }

            return cnt;
        }
        public void deleteFromPenfaceServiceScheme()
        {
            mySQLconnectSQL();
            SQL sql = new SQL();
            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.deleteFromPenfaceServiceScheme(), conn))
                {
                    command.ExecuteNonQuery();

                    command.Dispose();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }
        }
        public void InsertFooterPenfaceFinanceEmployerRates(Int32 recCount, Int32 total)
        {

             mySQLconnectSQL();
             SQL sql = new SQL();

             try
             {
                 using (MySqlCommand command = new MySqlCommand(sql.InsertFooterPenfaceFinanceEmployerRates(), conn))
                 {
                     command.Parameters.Add(new MySqlParameter("0", "FIRTOT"));
                     command.Parameters.Add(new MySqlParameter("1", recCount));
                     command.Parameters.Add(new MySqlParameter("2", total));

                     command.ExecuteNonQuery();

                     command.Dispose();
                 }
             }
             catch (Exception e)
             {

                 Console.WriteLine("{0} Exception caught.", e);

             }
        }
        private Int32 getPenfacePersonalMySQL()
        {
            SQL sql = new SQL();
            Int32 cnt = 0;
            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.getPenfacePersonalMySQL(), conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cnt = reader.GetInt32(0);
                            }
                        }
                    }


                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }

            return cnt;
        }
        public Int32 getPenfaceIndicatorRecords()
        {
            mySQLconnectSQL();
            SQL sql = new SQL();

            Int32 cnt = 0;
            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.getPenfaceIndicatorRecords(), conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cnt = reader.GetInt32(0);
                            }
                        }
                    }


                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }

            return cnt;
        }
        public Int32 getPenfaceServiceRecords()
        {
            mySQLconnectSQL();
            SQL sql = new SQL();
            Int32 cnt = 0;
            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.getPenfaceServiceRecords(), conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                cnt = reader.GetInt32(0);
                            }
                        }
                    }


                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }

            return cnt;
        }
        public void deleteFromPenfaceService()
        {
            mySQLconnectSQL();
            SQL sql = new SQL();

            try
            {

                MySqlCommand command = new MySqlCommand(sql.deleteFromPenfaceServiceScheme(), conn);

                command.ExecuteNonQuery();

                command.Dispose();

            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }
            conn.Close();
        }
        public void InsertPenfaceServiceScheme(Dictionary<string,Penface.PenfaceServiceScheme> serviceSchemeList, List<PenfaceJoiners> penfaceJoiner)
        {
           
            List<PenfaceJoiners>.Enumerator joinerEnumerator = penfaceJoiner.GetEnumerator();
            
            mySQLconnectSQL();
            SQL sql = new SQL();

          
                while (joinerEnumerator.MoveNext())
                {
                    PenfaceJoiners joiner = joinerEnumerator.Current;

                   

                    if (serviceSchemeList.ContainsKey(joiner.emplid))
                    {
                        try
                        {
                           
                           Penface.PenfaceServiceScheme serviceSchema = new Penface.PenfaceServiceScheme();

                           serviceSchemeList.TryGetValue(joiner.emplid, out serviceSchema);


                           using (MySqlCommand command = new MySqlCommand(sql.InsertIntoPenfaceService(), conn))
                           {
                               command.Parameters.Add(new MySqlParameter("0", "SER"));
                               command.Parameters.Add(new MySqlParameter("1", serviceSchema.emplid));
                               command.Parameters.Add(new MySqlParameter("2", "S"));
                               command.Parameters.Add(new MySqlParameter("3", serviceSchema.start_dt));
                               command.Parameters.Add(new MySqlParameter("4", "UWI"));
                               command.Parameters.Add(new MySqlParameter("5", "FSSU"));
                               command.Parameters.Add(new MySqlParameter("6", "000"));

                               command.ExecuteNonQuery();

                           }
                            
                        } catch (Exception e)
                        {

                                Console.WriteLine("{0} Exception caught.", e);

                        }
                             

                    }
                    
                }

                conn.Close();
                
        }
        public void InsertPenfaceServiceCompany(Dictionary<string,Penface.PenfaceServiceScheme> serviceCompany, List<PenfaceJoiners> penfaceJoiner)
        {

            List<PenfaceJoiners>.Enumerator joinerEnumerator = penfaceJoiner.GetEnumerator();

            mySQLconnectSQL();
            SQL sql = new SQL();

               
                while (joinerEnumerator.MoveNext())
                {
                    PenfaceJoiners joiner = joinerEnumerator.Current;

                    if (serviceCompany.ContainsKey(joiner.emplid))
                    {
                        try
                        {
                            Penface.PenfaceServiceScheme scheme = new Penface.PenfaceServiceScheme();

                            serviceCompany.TryGetValue(joiner.emplid, out scheme);

                            MySqlCommand command = new MySqlCommand(sql.InsertPenfaceServiceCompany(), conn);
                            command.Parameters.Add(new MySqlParameter("0", "SER"));
                            command.Parameters.Add(new MySqlParameter("1", scheme.emplid));
                            command.Parameters.Add(new MySqlParameter("2", "S"));
                            command.Parameters.Add(new MySqlParameter("3", scheme.start_dt));
                            command.Parameters.Add(new MySqlParameter("4", "UWI"));
                            command.Parameters.Add(new MySqlParameter("5", "CAVE"));
                            command.Parameters.Add(new MySqlParameter("6", "000"));

                            command.ExecuteNonQuery();

                            command.Dispose();

                        }
                        catch (Exception e)
                        {

                            Console.WriteLine("{0} Exception caught.", e);

                        }
                        

                    }

                }

                conn.Close();

        }
        public void InsertPenfaceServiceFooter(Int32 recordCnt)
        {
            mySQLconnectSQL();
            SQL sql = new SQL();
            try
            {
                MySqlCommand command = new MySqlCommand(sql.InsertPenfaceServiceFooter(), conn);
                command.Parameters.Add(new MySqlParameter("0", "SERTOT"));
                command.Parameters.Add(new MySqlParameter("1", recordCnt));

                command.ExecuteNonQuery();

                command.Dispose();

            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }
            conn.Close();

        }
        public void deletePenfaceIndicators()
        {
            mySQLconnectSQL();
            SQL sql = new SQL();

            try
            {

                MySqlCommand command = new MySqlCommand(sql.deletePenfaceIndicators(), conn);


                command.ExecuteNonQuery();

                command.Dispose();



            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }
            conn.Close();
        }
        public void InsertPenfaceIndicator(Dictionary<string,PenfaceIndicators> penfaceIndicatorList, List<PenfaceJoiners> penfaceJoiner)
        {
            mySQLconnectSQL();
            SQL sql = new SQL();
            
           
             List<PenfaceJoiners>.Enumerator penfaceJoinerEnum =  penfaceJoiner.GetEnumerator();

             
             while (penfaceJoinerEnum.MoveNext())
             {
                 PenfaceJoiners indicator = penfaceJoinerEnum.Current;



                 if (penfaceIndicatorList.ContainsKey(indicator.emplid))
                 {

                     try
                     {
                         using (MySqlCommand command = new MySqlCommand(sql.insertPenaceIndicators(), conn)){
                             
                             PenfaceIndicators pi = new PenfaceIndicators();
                             penfaceIndicatorList.TryGetValue(indicator.emplid, out pi);

                             command.Parameters.Add(new MySqlParameter("0", "IND"));
                             command.Parameters.Add(new MySqlParameter("1", indicator.emplid));
                             command.Parameters.Add(new MySqlParameter("2", "NIN"));
                             command.Parameters.Add(new MySqlParameter("3", pi.national_id));

                             command.ExecuteNonQuery();

                             command.Dispose();
                         
                         }
                     }
                     catch (Exception e)
                     {

                         Console.WriteLine("{0} Exception caught.", e);

                     }
                    

                 }
             }
             conn.Close();
        }
        public void InsertPenfaceIndicatorFooter(Int32 recCount){

            mySQLconnectSQL();
            SQL sql = new SQL();

            try
            {

                using (MySqlCommand command = new MySqlCommand(sql.insertpenfaceIndicatorFooter(), conn))
                {
                    command.Parameters.Add(new MySqlParameter("0", "INDTOT"));
                    command.Parameters.Add(new MySqlParameter("1", recCount));

                    command.ExecuteNonQuery();

                    command.Dispose();
                }


            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }
            conn.Close();
        
        }
        public void deleteFromPenfacePersonal()
        {
            mySQLconnectSQL();
            SQL sql = new SQL();

            try
            {

                MySqlCommand command = new MySqlCommand(sql.deleteFromPenfacePersonal(), conn);


                command.ExecuteNonQuery();

                command.Dispose();



            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }
            conn.Close();
        }
        public void deleteJoinerPenfaceFinanceRates()
        {
            mySQLconnectSQL();
            SQL sql = new SQL();

            try
            {

                MySqlCommand command = new MySqlCommand(sql.deleteJoinerPenfaceFinanceRates(), conn);
               

                command.ExecuteNonQuery();

                command.Dispose();



            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }
            conn.Close();
        }

        public void insertIntoinsertPenfaceTracker(String filename, SQL sql)
        {
            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.insertPenfaceTrackerSQL(), conn))
                {
                    DateTime now = DateTime.Now;
                    command.Parameters.Add(new MySqlParameter("0",filename));
                    command.Parameters.Add(new MySqlParameter("1", now));

                    command.ExecuteNonQuery();

                    command.Dispose();

                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }
        }
        public void deletePenfaceFinance()
        {
            mySQLconnectSQL();
            SQL sql = new SQL();
            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.deletePenfaceFinanceSQL(), conn))
                {
                    command.ExecuteNonQuery();

                    command.Dispose();
                }

            }catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }
        }
        public String insertPenfaceFinance(List<PenfaceFinanceData> List)
        {
            String str = "";
            SQL sql = new SQL();
            String status = "Success";
            Int32 cnt = 0;
            PenfaceFinanceData pfdTemp = new PenfaceFinanceData(); 
           
            List<PenfaceFinanceData>.Enumerator personalEnum = List.GetEnumerator();
            try
            {
                while (personalEnum.MoveNext())
                {
                    cnt++;
                    if (cnt == 688)
                    {
                        Console.Write("");
                    }
                    PenfaceFinanceData pfd = personalEnum.Current;
                    pfdTemp = pfd;
                    /**
                     * String sqlstmt = "INSERT INTO payroll.`PENFACE FINANCE` (REC_TYPE,KEY_FLD1,KEY_FLD2,FIN_ELMT,CODE,EFF_DATE,CATEGORY,VALUE) VALUES (@0,@1,@2,@3,@4,@5,@6,@7)";
                     * */

                    using (MySqlCommand command = new MySqlCommand(sql.insertPenfaceFinanceSQL(), conn))
                    {
                        
                        command.Parameters.Add(new MySqlParameter("0", pfd.rec_type));
                        command.Parameters.Add(new MySqlParameter("1", pfd.key_fld1));
                        command.Parameters.Add(new MySqlParameter("2", pfd.key_fld2 == null ? "" : pfd.key_fld2));
                        command.Parameters.Add(new MySqlParameter("3", pfd.fin_elmt));
                        command.Parameters.Add(new MySqlParameter("4", pfd.code));
                        command.Parameters.Add(new MySqlParameter("5", pfd.eff_date));
                        command.Parameters.Add(new MySqlParameter("6", pfd.category));
                        command.Parameters.Add(new MySqlParameter("7", pfd.value));
                        str = pfd.key_fld1;
                        command.ExecuteNonQuery();

                        command.Dispose();



                    }
                }
            }
            catch (Exception e)
            {
                status = "Failure";
                Console.WriteLine("{0} Exception caught." + str, e);

            }

            return status;
        }
        private void insertPenfacePersonalHeader(SQL sql, String filename, String dteStr)
        {
            
            Util util = new Util();

            util.setPenface(this);
            
            try
            {

                using (MySqlCommand command = new MySqlCommand(sql.insertIntoPenfacePersonalHeader(), conn))
                {
                    command.Parameters.Add(new MySqlParameter("0", ","));
                    command.Parameters.Add(new MySqlParameter("1", "payroll"));

                    filename = filename.Replace("PER", "JNR");
                    command.Parameters.Add(new MySqlParameter("2", filename));

                    DateTime myDate = DateTime.Parse(dteStr);

                    String ddMMyyyy = util.adjString(myDate.Day) + util.adjString(myDate.Month) + myDate.Year;
                    command.Parameters.Add(new MySqlParameter("3", ddMMyyyy));

                    DateTime now = DateTime.Now;

                    String ddMMyyyyHHmm = util.adjString(now.Day) + util.adjString(now.Month) + now.Year + util.adjString(now.Hour) + util.adjString(now.Minute);
                    command.Parameters.Add(new MySqlParameter("4", ddMMyyyyHHmm));


                    command.ExecuteNonQuery();

                    command.Dispose();
                }


            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }

        }

        public Boolean getPenfaceFileTracker(String filename)
        {
            mySQLconnectSQL();
            SQL sql = new SQL();
            Boolean found = false;
            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.getPenfaceFileTracker(), conn))
                {
                    command.Parameters.Add(new MySqlParameter("0", filename));
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                if (reader.IsDBNull(0))
                                    found = false;
                                else
                                    found = true;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }

            return found;
        }
        private void insertPenfacePersonalDetails(List<Personal> penfacePersonal, List<PenfaceJoiners> penfaceJoiner)
        {
            SQL sql = new SQL();

            List<Personal> penfacePersonalList = penfacePersonal;
            List<PenfaceJoiners> penfaceJoinerList = penfaceJoiner;

            List<Personal>.Enumerator personalList = penfacePersonalList.GetEnumerator();


            while (personalList.MoveNext())
            {
                Personal personal = personalList.Current;

                List<PenfaceJoiners>.Enumerator joinerList = penfaceJoinerList.GetEnumerator();
                while (joinerList.MoveNext())
                {
                    PenfaceJoiners joiner = joinerList.Current;

                    if (joiner.emplid == "20005816")
                        Console.WriteLine("{0} Exception caught.", joiner.emplid);

                    if (joiner.emplid == personal.payno)
                    {
                        try
                        {

                            using (MySqlCommand command = new MySqlCommand(sql.insertIntoPenfacePersonal(), conn))
                            {
                                command.Parameters.Add(new MySqlParameter("0", "PER"));
                                command.Parameters.Add(new MySqlParameter("1", joiner.emplid));
                                command.Parameters.Add(new MySqlParameter("2", ""));
                                command.Parameters.Add(new MySqlParameter("3", "J"));
                                command.Parameters.Add(new MySqlParameter("4", personal.sex));
                                command.Parameters.Add(new MySqlParameter("5", personal.title));
                                command.Parameters.Add(new MySqlParameter("6", personal.surname));
                                command.Parameters.Add(new MySqlParameter("7", personal.forenames));
                                command.Parameters.Add(new MySqlParameter("8", personal.birth_dt));
                                command.Parameters.Add(new MySqlParameter("9", "Y"));
                                command.Parameters.Add(new MySqlParameter("10", ""));
                                command.Parameters.Add(new MySqlParameter("11", personal.norm_rtr_dt));
                                command.Parameters.Add(new MySqlParameter("12", personal.payno));
                                command.Parameters.Add(new MySqlParameter("13", ""));
                                command.Parameters.Add(new MySqlParameter("14", ""));
                                command.Parameters.Add(new MySqlParameter("15", personal.marital));

                                command.ExecuteNonQuery();

                                command.Dispose();

                            }

                        }
                        catch (Exception e)
                        {

                            Console.WriteLine("{0} Exception caught.", e);

                        }
                    }

                }

            }


        }
        public void SetupJoinerPenfaceFinanceEmployerRates(List<Penface.FinancialData> fdList, List<PenfaceJoiners> penfaceJoiner, ref Int32 total)
        {
            List<FinancialData>.Enumerator financialDataList = fdList.GetEnumerator();

            mySQLconnectSQL();
            SQL sql = new SQL();

            while (financialDataList.MoveNext())
            {

                FinancialData financialData = financialDataList.Current;

                List<PenfaceJoiners>.Enumerator joinerList = penfaceJoiner.GetEnumerator();

                while (joinerList.MoveNext())
                {
                    PenfaceJoiners joiners = joinerList.Current;

                    if (financialData.emplid == joiners.emplid)
                    {
                        try
                        {
                            using (MySqlCommand command = new MySqlCommand(sql.SetupJoinerPenfaceFinanceEmployerRates(), conn))
                            {
                                command.Parameters.Add(new MySqlParameter("0", "FIR"));
                                command.Parameters.Add(new MySqlParameter("1", financialData.emplid));
                                command.Parameters.Add(new MySqlParameter("2", "S"));
                                command.Parameters.Add(new MySqlParameter("3", "ERS"));
                                command.Parameters.Add(new MySqlParameter("4", financialData.effDate));
                                command.Parameters.Add(new MySqlParameter("5", "PC"));
                                command.Parameters.Add(new MySqlParameter("6", financialData.pct));

                                total = total + financialData.pct;

                                command.ExecuteNonQuery();

                                command.Dispose();

                            }
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine("{0} Exception caught.", e);

                        }

                    }

                }

            }
        }

        public void setupJoinerPenfaceFinanceBasicRates(List<FinancialData> fdList, List<PenfaceJoiners> penfaceJoiner, ref Int32 total)
        {
            List<FinancialData>.Enumerator financialDataList = fdList.GetEnumerator();

            mySQLconnectSQL();
            SQL sql = new SQL();

            while (financialDataList.MoveNext())
            {

                FinancialData financialData = financialDataList.Current;

                List<PenfaceJoiners>.Enumerator joinerList = penfaceJoiner.GetEnumerator();

                while (joinerList.MoveNext())
                {
                    PenfaceJoiners joiners = joinerList.Current;

                    if (financialData.emplid == joiners.emplid)
                    {
                        try
                        {
                            using (MySqlCommand command = new MySqlCommand(sql.SetupJoinerPenfaceFinanceBasicRates(), conn))
                            {
                                command.Parameters.Add(new MySqlParameter("0", "FIR"));
                                command.Parameters.Add(new MySqlParameter("1", financialData.emplid));
                                command.Parameters.Add(new MySqlParameter("2", "S"));
                                command.Parameters.Add(new MySqlParameter("3", "BAS"));
                                command.Parameters.Add(new MySqlParameter("4", financialData.effDate));
                                command.Parameters.Add(new MySqlParameter("5", "PC"));
                                command.Parameters.Add(new MySqlParameter("6", financialData.pct));

                                total = total + financialData.pct;

                                command.ExecuteNonQuery();

                                command.Dispose();

                            }
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine("{0} Exception caught.", e);

                        } 

                    }

                }

            }


        }
        public List<PenfaceLeavers> getPenfaceLeavers()
        {
            mySQLconnectSQL();
            SQL sql = new SQL();
            List<PenfaceLeavers> list = new List<PenfaceLeavers>();

            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.getPenfaceLeaverSQL(), conn))
                {

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                PenfaceLeavers pl = new PenfaceLeavers();
                                pl.emplid = reader.GetString(0);
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
        public void deletePenfaceLeaveData()
        {
            mySQLconnectSQL();
            SQL sql = new SQL();
            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.deleteFromLeaverData(), conn))
                {
                    command.ExecuteNonQuery();

                    command.Dispose();
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }
            
        }
        public string insertIntoPenfaceData(List<PSWebApp.Penface.PenfaceLeaverOracle> leaverListOracle, List<PenfaceLeavers> leaverListMySQL)
        {
            mySQLconnectSQL();
            SQL sql = new SQL();
            String success = "success";
            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.insertPenfaceLeaveData(), conn))
                {

                    List<PenfaceLeavers>.Enumerator leaverList = leaverListMySQL.GetEnumerator();

                    while (leaverList.MoveNext())
                    {
                        PenfaceLeavers leavers = leaverList.Current;

                        List<PenfaceLeaverOracle>.Enumerator oracleLeaverList = leaverListOracle.GetEnumerator();

                        while (oracleLeaverList.MoveNext())
                        {
                            PenfaceLeaverOracle leaverOracle = oracleLeaverList.Current;


                            if (leaverOracle.emplid.Equals(leavers.emplid))
                            {

                                command.Parameters.Add(new MySqlParameter("0", "PER"));
                                command.Parameters.Add(new MySqlParameter("1", leaverOracle.emplid));
                                command.Parameters.Add(new MySqlParameter("2", "L"));
                                command.Parameters.Add(new MySqlParameter("3", leaverOracle.emplid));
                                command.Parameters.Add(new MySqlParameter("4", leaverOracle.leaverDT));

                                command.ExecuteNonQuery();

                                command.Dispose();

                            }
                            else
                            {
                                success = "No Leavers Found";
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                success = "Failure";
                Console.WriteLine("{0} Exception caught.", e);

            }

            return success;
        }
        
        /*Public Function WritePenfaceLeaversFile(ByVal dv As DataView, ByVal fileNamePath As String, _
                                            ByVal Session As HttpSessionState, ByVal trackerDS As SqlDataSource, _
                                                   ByVal filename As String) As String

        Dim strDestinationFile As String = fileNamePath
        Dim tw As TextWriter
        Dim msg As String

        Try
            tw = New StreamWriter(strDestinationFile)

            For cnt As Integer = 0 To dv.Count - 1

                tw.Write(dv(cnt)("REC_TYPE") & "," & dv(cnt)("KEY_FLD1") & "," & dv(cnt)("KEY_FLD2") & "," _
                                         & dv(cnt)("J_L_IND") & "," & dv(cnt)("SEX") & "," & dv(cnt)("TITLE") & "," _
                                         & dv(cnt)("SURNAME") & "," & dv(cnt)("FORENAMES") & "," & dv(cnt)("BIRTH_DT") & "," & dv(cnt)("BIRTH_DT_VERIFIED") & "," _
                                         & dv(cnt)("NI_NO") & "," & dv(cnt)("NORM_RTR_DT") & "," & dv(cnt)("PAYNO") & "," _
                                         & dv(cnt)("OTHER_REF") & "," & dv(cnt)("LEAVE_DT") & "," & dv(cnt)("MARITAL"))
                tw.WriteLine()
            Next

            Session("filename") = filename

            '* Update Penface Tracker File *
            Session("transdt") = Now()
            trackerDS.Insert()

            msg = "success"
            tw.Close()

        Catch ex As Exception

            msg = ex.Message()
            If msg.IndexOf("Duplicate") > 0 Then
                msg = "Duplicate Filename " & Session("filename")
            End If
            tw.Close()

        End Try

        tw.Close()

        WritePenfaceLeaversFile = msg

    End Function*/

        public String returnFileName()
        {
            mySQLconnectSQL();
            SQL sql = new SQL();
            String filename = "";

            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.returnFileNameSQL(), conn))
                {

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                filename = reader.GetString(0);
                            }
                        }
                    }
                }

            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }

            return filename;

        }
        public void SetupJoinerPenfaceFinanceAVCRates(List<FinancialData> fdList, List<PenfaceJoiners> penfaceJoiner, ref Int32 total)
        {
            List<FinancialData>.Enumerator financialDataList = fdList.GetEnumerator();

            mySQLconnectSQL();
            SQL sql = new SQL();

            while (financialDataList.MoveNext())
            {
                FinancialData financialData = financialDataList.Current;

                List<PenfaceJoiners>.Enumerator joinerList = penfaceJoiner.GetEnumerator();

                while (joinerList.MoveNext())
                {
                    PenfaceJoiners joiners = joinerList.Current;

                    if (financialData.emplid == joiners.emplid)
                    {
                        try
                        {
                            using (MySqlCommand command = new MySqlCommand(sql.SetupJoinerPenfaceFinanceAVCRates(), conn))
                            {

                                command.Parameters.Add(new MySqlParameter("0", "FIR"));
                                command.Parameters.Add(new MySqlParameter("1", financialData.emplid));
                                command.Parameters.Add(new MySqlParameter("2", "V"));
                                command.Parameters.Add(new MySqlParameter("3", "AVC"));
                                command.Parameters.Add(new MySqlParameter("4", financialData.effDate));
                                command.Parameters.Add(new MySqlParameter("5", "FS"));
                                command.Parameters.Add(new MySqlParameter("6", financialData.pct));

                                total = total + financialData.pct;

                                command.ExecuteNonQuery();

                                command.Dispose();

                            }
                        }
                        catch (Exception e)
                        {

                            Console.WriteLine("{0} Exception caught.", e);

                        }

                    }


                }

            }

        }
        public List<ButtonMenu> getButtonMenu()
        {
            List<ButtonMenu> buttonMenuList = new List<ButtonMenu>();

            mySQLconnectSQL();
            SQL sql = new SQL();

            try
            {
                using (MySqlCommand command = new MySqlCommand(sql.getButtonMenu(), conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ButtonMenu buttonMenu = new ButtonMenu();
                                buttonMenu.path = reader.GetString(0);
                                buttonMenu.title = reader.GetString(1);
                                buttonMenuList.Add(buttonMenu);

                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

                Console.WriteLine("{0} Exception caught.", e);

            }

            return buttonMenuList;
        }
        private void mySQLconnectSQL()
        {

            if (conn == null)
            {
                conn = new MySqlConnection();


                conn.ConnectionString = "SERVER=" + "OWL1" + ";" + "DATABASE=" +
                    "payroll" + ";" + "port=3305;" + "UID=" + "admin" + ";" + "PASSWORD=" + "kentish" + ";";


                conn.Open();
            }
            else
            {
                if (!conn.State.ToString().Equals("Open"))
                {

                    conn = new MySqlConnection();


                    conn.ConnectionString = "SERVER=" + "OWL1" + ";" + "DATABASE=" +
                        "payroll" + ";" + "port=3305;" + "UID=" + "admin" + ";" + "PASSWORD=" + "kentish" + ";";


                    conn.Open();

                }
            }


        }


        
        
    }
}


