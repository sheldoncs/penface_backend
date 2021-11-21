using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSWebApp
{
    public class SQL
    {

        public String getEmployeeContInfo()
        {

            String sqlStmt = "select PS_DEDUCTION_BAL.EMPLID AS EMPLID, " +
            "CONCAT(CONCAT(SUBSTR(PS_PERSONAL_DATA.NAME, INSTR(PS_PERSONAL_DATA.NAME,',')+1, LENGTH(PS_PERSONAL_DATA.NAME)), ' '),SUBSTR(PS_PERSONAL_DATA.NAME, 0, INSTR(PS_PERSONAL_DATA.NAME,',')-1)) AS NAME, " +
       "PS_DEDUCTION_BAL.DED_MTD / 2 as EMPLOYER_CONT, " +
       "(select " +
              "PS_DEDUCTION_BAL2.DED_MTD / 2 AS  DED_CNV " +
         "from SYSADM.PS_DEDUCTION_BAL PS_DEDUCTION_BAL2 " +
        "where PS_DEDUCTION_BAL2.BALANCE_PERIOD = TO_CHAR(TO_NUMBER(TO_CHAR(TO_DATE(:0,'mm/dd/yyyy'),'MM'))) " +
              "and PS_DEDUCTION_BAL2.EMPLID = PS_DEDUCTION_BAL.EMPLID " +
              "and PS_DEDUCTION_BAL2.BALANCE_YEAR = To_Char(TO_DATE(:1,'mm/dd/yyyy'),'YYYY') " +
              "and PS_DEDUCTION_BAL2.DEDCD = '019' " +
              "and PS_DEDUCTION_BAL2.DED_CLASS = 'A' " +
              "and PS_DEDUCTION_BAL2.PLAN_TYPE = '80') as EMPLOYEE_CONT_ORIG, " +
       "DECODE((select " +
              "PS_DEDUCTION_BAL3.DED_MTD / 2 AS  DED_CNV " +
         "from SYSADM.PS_DEDUCTION_BAL PS_DEDUCTION_BAL3 " +
        "where PS_DEDUCTION_BAL3.BALANCE_PERIOD = TO_CHAR(TO_NUMBER(TO_CHAR(TO_DATE(:2,'mm/dd/yyyy'),'MM'))) " +
              "and PS_DEDUCTION_BAL3.EMPLID = PS_DEDUCTION_BAL.EMPLID " +
              "and PS_DEDUCTION_BAL3.BALANCE_YEAR = To_Char(TO_DATE(:3,'mm/dd/yyyy'),'YYYY') " +
              "and PS_DEDUCTION_BAL3.DEDCD = '019' " +
              "and PS_DEDUCTION_BAL3.DED_CLASS = 'A' " +
              "and PS_DEDUCTION_BAL3.PLAN_TYPE = '80'), (PS_DEDUCTION_BAL.DED_MTD / 2),PS_DEDUCTION_BAL.DED_MTD / 4,0) as AVC_CONT, " +
       "TO_DATE(:4,'mm/dd/yyyy') as PAY_END_DT, " +
       "DECODE((select " +
              "PS_DEDUCTION_BAL3.DED_MTD / 2 AS  DED_CNV " +
         "from SYSADM.PS_DEDUCTION_BAL PS_DEDUCTION_BAL3 " +
        "where PS_DEDUCTION_BAL3.BALANCE_PERIOD = TO_CHAR(TO_NUMBER(TO_CHAR(TO_DATE(:5,'mm/dd/yyyy'),'MM'))) " +
              "and PS_DEDUCTION_BAL3.EMPLID = PS_DEDUCTION_BAL.EMPLID " +
              "and PS_DEDUCTION_BAL3.BALANCE_YEAR = To_Char(TO_DATE(:6,'mm/dd/yyyy'),'YYYY') " +
              "and PS_DEDUCTION_BAL3.DEDCD = '019' " +
              "and PS_DEDUCTION_BAL3.DED_CLASS = 'A' " +
              "and PS_DEDUCTION_BAL3.PLAN_TYPE = '80'), (PS_DEDUCTION_BAL.DED_MTD / 2),PS_DEDUCTION_BAL.DED_MTD / 4,(select " +
              "PS_DEDUCTION_BAL4.DED_MTD / 2 AS  DED_CNV " +
         "from SYSADM.PS_DEDUCTION_BAL PS_DEDUCTION_BAL4 " +
         "where PS_DEDUCTION_BAL4.BALANCE_PERIOD = TO_CHAR(TO_NUMBER(TO_CHAR(TO_DATE(:7,'mm/dd/yyyy'),'MM'))) " +
              "and PS_DEDUCTION_BAL4.EMPLID = PS_DEDUCTION_BAL.EMPLID " +
              "and PS_DEDUCTION_BAL4.BALANCE_YEAR = To_Char(TO_DATE(:8,'mm/dd/yyyy'),'YYYY') " +
              "and PS_DEDUCTION_BAL4.DEDCD = '019' " +
              "and PS_DEDUCTION_BAL4.DED_CLASS = 'A' " +
              "and PS_DEDUCTION_BAL4.PLAN_TYPE = '80')) EMPLOYEE_CONT  " +
  "from SYSADM.PS_DEDUCTION_BAL PS_DEDUCTION_BAL , SYSADM.PS_PERSONAL_DATA PS_PERSONAL_DATA " +
 "where PS_DEDUCTION_BAL.DED_CLASS = 'N' " +
       "and PS_PERSONAL_DATA.EMPLID = PS_DEDUCTION_BAL.EMPLID " +
       "and PS_DEDUCTION_BAL.BALANCE_PERIOD = TO_CHAR(TO_NUMBER(TO_CHAR(TO_DATE(:9,'mm/dd/yyyy'),'MM'))) " +
       "and PS_DEDUCTION_BAL.BALANCE_YEAR = To_Char(TO_DATE(:10,'mm/dd/yyyy'),'YYYY') " +
       "and ( PS_DEDUCTION_BAL.EMPLID, PS_DEDUCTION_BAL.DED_CLASS, PS_DEDUCTION_BAL.DED_MTD )  = " +
       "( select PS_DEDUCTION_BAL1.EMPLID, " +
                "PS_DEDUCTION_BAL1.DED_CLASS, " +
                "PS_DEDUCTION_BAL1.DED_MTD " +
           "from SYSADM.PS_DEDUCTION_BAL PS_DEDUCTION_BAL1 " +
          "where PS_DEDUCTION_BAL1.BALANCE_PERIOD = TO_CHAR(TO_NUMBER(TO_CHAR(TO_DATE(:11,'mm/dd/yyyy'),'MM'))) " +
                "and PS_DEDUCTION_BAL1.BALANCE_YEAR = To_Char(TO_DATE(:12,'mm/dd/yyyy'),'YYYY') " +
                "and PS_DEDUCTION_BAL1.DEDCD = '019' " +
                "and PS_DEDUCTION_BAL1.PLAN_TYPE = '80' " +
                "and PS_DEDUCTION_BAL1.EMPLID = PS_DEDUCTION_BAL.EMPLID " +
                "and PS_DEDUCTION_BAL1.DED_CLASS = PS_DEDUCTION_BAL.DED_CLASS " +
                "and PS_DEDUCTION_BAL1.DED_MTD = PS_DEDUCTION_BAL.DED_MTD )  ORDER BY PS_DEDUCTION_BAL.EMPLID ";

            return sqlStmt;

        }
        public String getPenfacePersonal()
        {
            String sqlstmt = "select PS_PERSONAL_DATA.EMPLID AS PAYNO, " +
            "PS_PERSONAL_DATA.SEX AS SEX, " +
       "PS_PERSONAL_DATA.NAME_PREFIX AS TITLE, " +
       "PS_PERSONAL_DATA.LAST_NAME AS SURNAME, " +
       "PS_PENSION_PLAN.EMPL_RCD, " +
       "PS_PENSION_PLAN.PLAN_TYPE, " +
       "PS_PENSION_PLAN.BENEFIT_NBR, " +
       "PS_PENSION_PLAN.EFFDT, " +
       "NVL(PS_PERSONAL_DATA.MIDDLE_NAME,' ') AS FORENAMES, " +
       "to_char(PS_PERSONAL_DATA.BIRTHDATE,'ddmmyyyy') AS BIRTH_DT, " +
       "TO_CHAR((PS_PERSONAL_DATA.BIRTHDATE+1)+(65*365.244),'ddmmyyyy') AS NORM_RTR_DT, " +
       "DECODE(PS_PERSONAL_DATA.MAR_STATUS, " +
                                        "'E','N', " +
                                          "PS_PERSONAL_DATA.MAR_STATUS) as MARITAL " +
       "from SYSADM.PS_PENSION_PLAN PS_PENSION_PLAN, " +
       "SYSADM.PS_PERSONAL_DATA PS_PERSONAL_DATA " +
       "where ( PS_PERSONAL_DATA.EMPLID = PS_PENSION_PLAN.EMPLID ) " +
       "and ( PS_PENSION_PLAN.BENEFIT_PLAN = 'FSSU' " +
        "and ( PS_PENSION_PLAN.EMPLID, PS_PENSION_PLAN.EMPL_RCD, PS_PENSION_PLAN.PLAN_TYPE, PS_PENSION_PLAN.BENEFIT_NBR, PS_PENSION_PLAN.EFFDT )  = " +
         "( select PS_PENSION_PLAN1.EMPLID, " +
                  "PS_PENSION_PLAN1.EMPL_RCD, " +
                  "PS_PENSION_PLAN1.PLAN_TYPE, " +
                  "PS_PENSION_PLAN1.BENEFIT_NBR, " +
                  " Max( PS_PENSION_PLAN1.EFFDT ) as Max_EFFDT " +
             "from SYSADM.PS_PENSION_PLAN PS_PENSION_PLAN1 " +
            "where PS_PENSION_PLAN1.EMPLID = PS_PENSION_PLAN.EMPLID " +
                  "and PS_PENSION_PLAN1.EMPL_RCD = PS_PENSION_PLAN.EMPL_RCD " +
                  "and PS_PENSION_PLAN1.PLAN_TYPE = '80' " +
                  "and PS_PENSION_PLAN1.BENEFIT_NBR = '0' " +
                  "and PS_PENSION_PLAN1.EFFDT <= CURRENT_DATE " +
            "group by PS_PENSION_PLAN1.EMPLID, " +
                     "PS_PENSION_PLAN1.EMPL_RCD, " +
                     "PS_PENSION_PLAN1.PLAN_TYPE, " +
                     "PS_PENSION_PLAN1.BENEFIT_NBR ) ) ";

            return sqlstmt;
        }
        public String getPenfaceFinancialDataRates()
        {

             String sqlstmt = "select PS_PENSION_PLAN.EMPLID as EMPLID, " +
             "PS_PENSION_PLN_TBL.BENEFIT_PLAN, " +
             "TO_CHAR(PS_PENSION_PLAN.EFFDT,'ddmmyyyy') as E_DATE, " +
             "PS_PENSION_PLN_TBL.EE_PCT_UNDER_YMPE AS VALUE " +
             "from SYSADM.PS_PENSION_PLAN PS_PENSION_PLAN, " +
             "SYSADM.PS_PENSION_PLN_TBL PS_PENSION_PLN_TBL " +
             "where ( PS_PENSION_PLN_TBL.PLAN_TYPE = PS_PENSION_PLAN.PLAN_TYPE "+
             "and PS_PENSION_PLN_TBL.BENEFIT_PLAN = PS_PENSION_PLAN.BENEFIT_PLAN ) " +
             "and ( PS_PENSION_PLN_TBL.PLAN_TYPE = '80' " +
             "and PS_PENSION_PLN_TBL.BENEFIT_PLAN = 'FSSU' " +
             "and PS_PENSION_PLAN.EMPL_RCD = '0' " +
             "and ( PS_PENSION_PLAN.EMPLID, PS_PENSION_PLAN.EMPL_RCD, PS_PENSION_PLAN.PLAN_TYPE, PS_PENSION_PLAN.BENEFIT_NBR, PS_PENSION_PLAN.EFFDT )  =  " +
             "( select PS_PENSION_PLAN1.EMPLID, " +
                  "PS_PENSION_PLAN1.EMPL_RCD, "+
                  "PS_PENSION_PLAN1.PLAN_TYPE, " +
                  "PS_PENSION_PLAN1.BENEFIT_NBR, " +
                  "Max( PS_PENSION_PLAN1.EFFDT ) as Max_EFFDT " +
             "from SYSADM.PS_PENSION_PLAN PS_PENSION_PLAN1 " +
            "where PS_PENSION_PLAN1.EMPLID = PS_PENSION_PLAN.EMPLID " +
                  "and PS_PENSION_PLAN1.EMPL_RCD = PS_PENSION_PLAN.EMPL_RCD " +
                  "and PS_PENSION_PLAN1.PLAN_TYPE = '80' " +
                  "and PS_PENSION_PLAN1.BENEFIT_NBR = '0' "  +
                  "and PS_PENSION_PLAN1.EFFDT <= CURRENT_DATE " +
            "group by PS_PENSION_PLAN1.EMPLID, " +
                     "PS_PENSION_PLAN1.EMPL_RCD, " +
                     "PS_PENSION_PLAN1.PLAN_TYPE, "+
                     "PS_PENSION_PLAN1.BENEFIT_NBR ) )";

            return sqlstmt;

        }
        public String getJoinerPenfaceFinanceAVCRates()
        {
            String sqlstmt = "select PS_PENSION_PLAN.EMPLID as EMPLID, " +
       "PS_PENSION_PLN_TBL.BENEFIT_PLAN, " +
       "TO_CHAR(PS_PENSION_PLAN.EFFDT,'ddmmyyyy') as E_DATE, " +
       "PS_PENSION_PLAN.VOLUNTARY_PCT AS VALUE, " +
       "DECODE(PS_PENSION_PLAN.VOLUNTARY_PCT,null,'PC','FS') AS RATE_TYPE " +
  "from SYSADM.PS_PENSION_PLAN PS_PENSION_PLAN, " +
       "SYSADM.PS_PENSION_PLN_TBL PS_PENSION_PLN_TBL " +
 "where ( PS_PENSION_PLN_TBL.PLAN_TYPE = PS_PENSION_PLAN.PLAN_TYPE " +
         "and PS_PENSION_PLN_TBL.BENEFIT_PLAN = PS_PENSION_PLAN.BENEFIT_PLAN ) " +
   "and ( PS_PENSION_PLN_TBL.PLAN_TYPE = '80' " +
         "and PS_PENSION_PLN_TBL.BENEFIT_PLAN = 'FSSU' " +
         "and PS_PENSION_PLAN.EMPL_RCD = '0' " +
         "and PS_PENSION_PLAN.VOLUNTARY_PCT <>0 " +
         "and ( PS_PENSION_PLAN.EMPLID, PS_PENSION_PLAN.EMPL_RCD, PS_PENSION_PLAN.PLAN_TYPE, PS_PENSION_PLAN.BENEFIT_NBR, PS_PENSION_PLAN.EFFDT )  = " +
         "( select PS_PENSION_PLAN1.EMPLID, " +
                  "PS_PENSION_PLAN1.EMPL_RCD, " +
                  "PS_PENSION_PLAN1.PLAN_TYPE, " +
                  "PS_PENSION_PLAN1.BENEFIT_NBR, " +
                  "Max( PS_PENSION_PLAN1.EFFDT ) as Max_EFFDT " +
             "from SYSADM.PS_PENSION_PLAN PS_PENSION_PLAN1 " +
            "where PS_PENSION_PLAN1.EMPLID = PS_PENSION_PLAN.EMPLID " +
                  "and PS_PENSION_PLAN1.EMPL_RCD = PS_PENSION_PLAN.EMPL_RCD " +
                  "and PS_PENSION_PLAN1.PLAN_TYPE = '80' " +
                  "and PS_PENSION_PLAN1.BENEFIT_NBR = '0' " +
                  "and PS_PENSION_PLAN1.EFFDT <= CURRENT_DATE " +
            "group by PS_PENSION_PLAN1.EMPLID, " +
                     "PS_PENSION_PLAN1.EMPL_RCD, " +
                     "PS_PENSION_PLAN1.PLAN_TYPE, " +
                     "PS_PENSION_PLAN1.BENEFIT_NBR ) ) ";

            return sqlstmt;
        }
        public String getPenfaceEmployerFinanceRates()
        {

            String sqlstmt = "select PS_PENSION_PLAN.EMPLID EMPLID, " +
       "PS_PENSION_PLN_TBL.BENEFIT_PLAN, " +
       "TO_CHAR(PS_PENSION_PLAN.EFFDT,'ddmmyyyy') as E_DATE, " +
       "PS_PENSION_PLN_TBL.ER_PCT_UNDER_YMPE as VALUE " +
  "from SYSADM.PS_PENSION_PLAN PS_PENSION_PLAN, " +
       "SYSADM.PS_PENSION_PLN_TBL PS_PENSION_PLN_TBL " +
 "where ( PS_PENSION_PLN_TBL.PLAN_TYPE = PS_PENSION_PLAN.PLAN_TYPE " +
         "and PS_PENSION_PLN_TBL.BENEFIT_PLAN = PS_PENSION_PLAN.BENEFIT_PLAN ) " +
   "and ( PS_PENSION_PLN_TBL.PLAN_TYPE = '80' " +
         "and PS_PENSION_PLN_TBL.BENEFIT_PLAN = 'FSSU' " +
         "and PS_PENSION_PLAN.EMPL_RCD = '0' " +
         "and ( PS_PENSION_PLAN.EMPLID, PS_PENSION_PLAN.EMPL_RCD, PS_PENSION_PLAN.PLAN_TYPE, PS_PENSION_PLAN.BENEFIT_NBR, PS_PENSION_PLAN.EFFDT )  = " +
         "( select PS_PENSION_PLAN1.EMPLID, " +
                  "PS_PENSION_PLAN1.EMPL_RCD, " +
                  "PS_PENSION_PLAN1.PLAN_TYPE, " +
                  "PS_PENSION_PLAN1.BENEFIT_NBR, " +
                  "Max( PS_PENSION_PLAN1.EFFDT ) as Max_EFFDT " +
             "from SYSADM.PS_PENSION_PLAN PS_PENSION_PLAN1 " +
            "where PS_PENSION_PLAN1.EMPLID = PS_PENSION_PLAN.EMPLID " +
                  "and PS_PENSION_PLAN1.EMPL_RCD = PS_PENSION_PLAN.EMPL_RCD " +
                  "and PS_PENSION_PLAN1.PLAN_TYPE = '80' " +
                  "and PS_PENSION_PLAN1.BENEFIT_NBR = '0' " +
                  "and PS_PENSION_PLAN1.EFFDT <= CURRENT_DATE " +
            "group by PS_PENSION_PLAN1.EMPLID, " +
                     "PS_PENSION_PLAN1.EMPL_RCD, " +
                     "PS_PENSION_PLAN1.PLAN_TYPE, " +
                     "PS_PENSION_PLAN1.BENEFIT_NBR ) )";
            
            return sqlstmt;

        }
        public String getPenfaceServiceScheme()
        {
            string sqlstmt = "select PS_PENSION_PLAN.EMPLID as EMPLID, " +
             "TO_CHAR(PS_PENSION_PLAN.EFFDT,'ddmmyyyy') as START_DT " +
           "from SYSADM.PS_PENSION_PLAN PS_PENSION_PLAN " +
           "where ( PS_PENSION_PLAN.EMPLID, PS_PENSION_PLAN.EMPL_RCD, PS_PENSION_PLAN.PLAN_TYPE, PS_PENSION_PLAN.BENEFIT_NBR, PS_PENSION_PLAN.EFFDT )  = " +
           "( select PS_PENSION_PLAN1.EMPLID, " +
                "PS_PENSION_PLAN1.EMPL_RCD, " +
                "PS_PENSION_PLAN1.PLAN_TYPE, " +
                "PS_PENSION_PLAN1.BENEFIT_NBR, " +
                "Max( PS_PENSION_PLAN1.EFFDT ) as Max_EFFDT " +
           "from SYSADM.PS_PENSION_PLAN PS_PENSION_PLAN1 " +
          "where PS_PENSION_PLAN1.EMPLID = PS_PENSION_PLAN.EMPLID " +
                "and PS_PENSION_PLAN1.EMPL_RCD = PS_PENSION_PLAN.EMPL_RCD " +
                "and PS_PENSION_PLAN1.PLAN_TYPE = '80' " +
                "and PS_PENSION_PLAN1.BENEFIT_NBR = '0' " +
                "and PS_PENSION_PLAN1.EFFDT <= CURRENT_DATE " +
          "group by PS_PENSION_PLAN1.EMPLID, " +
                   "PS_PENSION_PLAN1.EMPL_RCD, " +
                   "PS_PENSION_PLAN1.PLAN_TYPE, " +
                   "PS_PENSION_PLAN1.BENEFIT_NBR )  order by PS_PENSION_PLAN.EMPLID";

            return sqlstmt;
        }
        public String getPenfaceIndicators()
        {
            string sqlstmt = "select PS_PERS_NID.EMPLID, " +
                "PS_PERS_NID.NATIONAL_ID AS NATIONAL_ID " +
                "from SYSADM.PS_PERS_NID PS_PERS_NID " +
                "where PS_PERS_NID.NATIONAL_ID <> ' ' " +
                "and PS_PERS_NID.COUNTRY = 'BRB' " +
                "and PS_PERS_NID.NATIONAL_ID_TYPE = 'NRN'";

                return sqlstmt;
        }
        public String deleteFromPenfaceServiceScheme(){

            String sqlstmt = "delete from payroll.`penface service`";

            return sqlstmt;
        }
        public String getMySQLPenfaceFinanceRates()
        {
            String sqlstmt = "SELECT id, REC_TYPE,NI,  KEY_FLD1, KEY_FLD2, RATE_CLASS, CONT_TYPE, EFF_DATE, RATE_TYPE, `VALUE`, SOURCE, REFERENCE, MAT_DATE, A_Y_SCHEME, A_Y_PERIOD, A_Y_BASIS, UNIT_RT, ACCUM FROM `penface finance rates` " +
                "WHERE CONT_TYPE is not null";
            return sqlstmt;
        }
        public String InsertFooterPenfaceFinanceEmployerRates()
        {

            String sqlstmt = "insert into payroll.`penface finance rates`(REC_TYPE,KEY_FLD1,KEY_FLD2) VALUES (@0,@1,@2)";
            return sqlstmt;

        }
        public String InsertIntoPenfaceService()
        {
            String sqlstmt = "insert into `penface service` (REC_TYPE,KEY_FLD1,ACT_IND,START_DT,FUND,SCHEME,WRK_CAT) VALUES (@0,@1,@2,@3,@4,@5,@6)";
            return sqlstmt; 
        }
        public String InsertPenfaceServiceCompany()
        {
            String sqlstmt = "insert into `penface service` (REC_TYPE,KEY_FLD1,ACT_IND,START_DT,COMPANY,LOCATION,WRK_CAT) VALUES (@0,@1,@2,@3,@4,@5,@6)";
            return sqlstmt; 
            
        }
        public String InsertPenfaceServiceFooter()
        {
            String sqlstmt  = "insert into `penface service` (REC_TYPE,KEY_FLD1) VALUES (@0,@1)";

            return sqlstmt;
        }
        public String getPenfaceServiceRecords()
        {
            String sqlstmt  = "SELECT count(*) FROM `penface service` p where act_ind is not null";
            return sqlstmt;
        }
        public String getPenfaceIndicatorRecords()
        {
            String sqlstmt = "SELECT count(*) FROM `penface indicators` p where INDICATOR is not null";
            return sqlstmt;
        }
        public String insertpenfaceIndicatorFooter()
        {
            String sqlstmt = "insert into payroll.`penface indicators` (REC_TYPE,KEY_FLD1) VALUES (@0,@1)";

            return sqlstmt;
        }
        public String SetupJoinerPenfaceFinanceAVCRates()
        {
            String sqlstmt = "insert into payroll.`penface finance rates` (REC_TYPE,KEY_FLD1,RATE_CLASS,CONT_TYPE,EFF_DATE,RATE_TYPE,VALUE) VALUES (@0,@1,@2,@3,@4,@5,@6)";
           
            return sqlstmt;
        }
        public String deletePenfaceIndicators()
        {
            String sqlstmt = "delete FROM `penface indicators`";
            return sqlstmt;
        }
        public String insertPenaceIndicators()
        {
            String sqlstmt = "insert into `penface indicators` (REC_TYPE,KEY_FLD1,REF_CODE,INDICATOR) VALUES (@0,@1,@2,@3)";
            return sqlstmt;
        }
        public String deleteJoinerPenfaceFinanceRates()
        {
            String sqlstmt = "delete from payroll.`penface finance rates`";

            return sqlstmt;
        }
        public String getPenfaceFileTracker()
        {

            String sqlstmt = "SELECT MAX(FILE_NAME) AS CURR_FILE FROM `payroll`.`penface file track` WHERE FILE_NAME = @0";

            return sqlstmt;
        }
        public String getPenfaceJoiner()
        {
            String sqlstmt = "SELECT * FROM payroll.`penface joiners`";

            return sqlstmt;
        }
        public String deleteFromPenfacePersonal(){

            String sqlstmt = "delete from payroll.`penface personal`";

            return sqlstmt;
        }
        public String insertIntoPenfacePersonal()
        {
          
            String sqlstmt = "insert into payroll.`penface personal`(REC_TYPE,KEY_FLD1,KEY_FLD2,J_L_IND,SEX,TITLE,SURNAME,FORENAMES,BIRTH_DT,BIRTH_DT_VERIFIED,NI_NO,NORM_RTR_DT,PAYNO,OTHER_REF,DATE_LEAVE,MARITAL) values(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15)";
            return sqlstmt;
        }
        public String insertIntoPenfacePersonalHeader()
        {
            String sqlstmt = "insert into payroll.`penface personal`(REC_TYPE,KEY_FLD1,KEY_FLD2,J_L_IND,SEX) values(@0,@1,@2,@3,@4)";
            return sqlstmt;
        }
        public String InsertPenfacePersonalFooter()
        {
            String sqlstmt = "insert into payroll.`penface personal`(REC_TYPE,KEY_FLD1) values(@0,@1)";
            return sqlstmt;
        }
        
        public String SetupJoinerPenfaceFinanceBasicRates()
        {
            String sqlstmt = "insert into payroll.`penface finance rates` (REC_TYPE,KEY_FLD1,RATE_CLASS,CONT_TYPE,EFF_DATE,RATE_TYPE,VALUE) VALUES (@0,@1,@2,@3,@4,@5,@6)";
            return sqlstmt;
        }
        public String SetupJoinerPenfaceFinanceEmployerRates()
        {
            String sqlstmt = "insert into payroll.`penface finance rates` (REC_TYPE,KEY_FLD1,RATE_CLASS,CONT_TYPE,EFF_DATE,RATE_TYPE,VALUE) VALUES (@0,@1,@2,@3,@4,@5,@6)";
            return sqlstmt;
        }
        public String getPenfacePersonalMySQL()
        {

            String sqlstmt = "select count(*) cnt from payroll.`penface personal`";

            return sqlstmt;
        }
        public String getButtonMenu()
        {
            String sqlstmt = "select path, title from payroll.`menubuttons`";

            return sqlstmt;
        }
        public String returnFileNameSQL()
        {
            String sqlstmt = "SELECT key_fld2 FROM payroll.`penface personal` where key_fld2 like '%UCH%'";

            return sqlstmt;
        }
        public String getPenfaceServiceSQL(){
            String sqlstmt = "select * from payroll.`penface service`";
            return sqlstmt;
        }
        public String getPenfacePersonalSQL()
        {
             String sqlstmt = "select * from  payroll.`penface personal`";
             return sqlstmt;
        }
        public String insertPenfaceTrackerSQL()
        {
            String sqlstmt = "insert into `payroll`.`penface file track` (FILE_NAME,TRANSMIT_DT) values (@0,@1)";
            return sqlstmt;
        }
        public String getFinanceRatesSQL()
        {
            String sqlstmt = "SELECT id, REC_TYPE,NI,  KEY_FLD1, KEY_FLD2, RATE_CLASS, CONT_TYPE, EFF_DATE, RATE_TYPE, `VALUE`, SOURCE, REFERENCE, MAT_DATE, A_Y_SCHEME, A_Y_PERIOD, A_Y_BASIS, UNIT_RT, ACCUM FROM payroll.`penface finance rates`";
            return sqlstmt;
        }
        public String getPenfaceIndicatorSQL()
        {
            String sqlstmt = "SELECT REC_TYPE, KEY_FLD1, REF_CODE, EFFECTIVE_DT, INDICATOR FROM payroll.`penface indicators`";
            return sqlstmt;
        }
        public String deletePenfaceFinanceSQL(){
            String sqlstmt = "DELETE FROM payroll.`PenFace Finance`";
            return sqlstmt;
        }
        public String insertPenfaceFinanceSQL()
        {
            String sqlstmt = "INSERT INTO payroll.`PENFACE FINANCE` (REC_TYPE,KEY_FLD1,KEY_FLD2,FIN_ELMT,CODE,EFF_DATE,CATEGORY,VALUE) VALUES (@0,@1,@2,@3,@4,@5,@6,@7)";
            return sqlstmt;
        }
        public String getPenfaceFinanceSpreadsheetSQL()
        {
            
            String sqlstmt = "select PS_DEDUCTION_BAL.EMPLID AS EMPLID, " 
           + "CONCAT(CONCAT(SUBSTR(PS_PERSONAL_DATA.NAME, INSTR(PS_PERSONAL_DATA.NAME,',')+1, LENGTH(PS_PERSONAL_DATA.NAME)), ' '),SUBSTR(PS_PERSONAL_DATA.NAME, 0, INSTR(PS_PERSONAL_DATA.NAME,',')-1)) AS NAME, "   
           + "PS_DEDUCTION_BAL.DED_MTD / 2 as EMPLOYER_CONT, " 
           + "(select " 
           + "PS_DEDUCTION_BAL2.DED_MTD / 2 AS  DED_CNV " 
           + "from SYSADM.PS_DEDUCTION_BAL PS_DEDUCTION_BAL2 " 
           + "where PS_DEDUCTION_BAL2.BALANCE_PERIOD = TO_CHAR(TO_NUMBER(TO_CHAR(TO_DATE(:1,'mm/dd/yyyy'),'MM'))) "
           + "and PS_DEDUCTION_BAL2.EMPLID = PS_DEDUCTION_BAL.EMPLID "
           + "and PS_DEDUCTION_BAL2.BALANCE_YEAR = To_Char(TO_DATE(:2,'mm/dd/yyyy'),'YYYY') "
           + "and PS_DEDUCTION_BAL2.DEDCD = '019' "
           + "and PS_DEDUCTION_BAL2.BALANCE_ID = 'CY' "
           + "and PS_DEDUCTION_BAL2.DED_CLASS = 'A' "
           + "and PS_DEDUCTION_BAL2.PLAN_TYPE = '80')  as EMPLOYEE_CONT_ORIG, "
           + "DECODE((select "
           + "PS_DEDUCTION_BAL3.DED_MTD / 2 AS  DED_CNV " 
           + "from SYSADM.PS_DEDUCTION_BAL PS_DEDUCTION_BAL3 " 
           + "where PS_DEDUCTION_BAL3.BALANCE_PERIOD = TO_CHAR(TO_NUMBER(TO_CHAR(TO_DATE(:3,'mm/dd/yyyy'),'MM'))) "
           + "and PS_DEDUCTION_BAL3.EMPLID = PS_DEDUCTION_BAL.EMPLID "
           + "and PS_DEDUCTION_BAL3.BALANCE_YEAR = To_Char(TO_DATE(:4,'mm/dd/yyyy'),'YYYY') " 
           + "and PS_DEDUCTION_BAL3.DEDCD = '019' "
           + "and PS_DEDUCTION_BAL3.BALANCE_ID = 'CY' "
           + "and PS_DEDUCTION_BAL3.DED_CLASS = 'A' " 
           + "and PS_DEDUCTION_BAL3.PLAN_TYPE = '80'), (PS_DEDUCTION_BAL.DED_MTD / 2),PS_DEDUCTION_BAL.DED_MTD / 4,0) as AVC_CONT, " 
           + "TO_DATE(:5,'mm/dd/yyyy') as PAY_END_DT, "
           + "DECODE((select "
           + "PS_DEDUCTION_BAL3.DED_MTD / 2 AS  DED_CNV " 
           + "from SYSADM.PS_DEDUCTION_BAL PS_DEDUCTION_BAL3 " 
           + "where PS_DEDUCTION_BAL3.BALANCE_PERIOD = TO_CHAR(TO_NUMBER(TO_CHAR(TO_DATE(:6,'mm/dd/yyyy'),'MM'))) "
           + "and PS_DEDUCTION_BAL3.EMPLID = PS_DEDUCTION_BAL.EMPLID " 
           + "and PS_DEDUCTION_BAL3.BALANCE_YEAR = To_Char(TO_DATE(:7,'mm/dd/yyyy'),'YYYY') "
           + "and PS_DEDUCTION_BAL3.DEDCD = '019' "
           + "and PS_DEDUCTION_BAL3.BALANCE_ID = 'CY' "
           + "and PS_DEDUCTION_BAL3.DED_CLASS = 'A' " 
           + "and PS_DEDUCTION_BAL3.PLAN_TYPE = '80'), (PS_DEDUCTION_BAL.DED_MTD / 2),PS_DEDUCTION_BAL.DED_MTD / 4,(select "
           + "PS_DEDUCTION_BAL4.DED_MTD / 2 AS  DED_CNV " 
           + "from SYSADM.PS_DEDUCTION_BAL PS_DEDUCTION_BAL4 "
           + "where PS_DEDUCTION_BAL4.BALANCE_PERIOD = TO_CHAR(TO_NUMBER(TO_CHAR(TO_DATE(:8,'mm/dd/yyyy'),'MM'))) "
           + "and PS_DEDUCTION_BAL4.EMPLID = PS_DEDUCTION_BAL.EMPLID "
           + "and PS_DEDUCTION_BAL4.BALANCE_YEAR = To_Char(TO_DATE(:9,'mm/dd/yyyy'),'YYYY') "
           + "and PS_DEDUCTION_BAL4.DEDCD = '019' "
           + "and PS_DEDUCTION_BAL4.BALANCE_ID = 'CY' "
           + "and PS_DEDUCTION_BAL4.DED_CLASS = 'A' "
           + "and PS_DEDUCTION_BAL4.PLAN_TYPE = '80')) as EMPLOYEE_CONT " 
           + "from SYSADM.PS_DEDUCTION_BAL PS_DEDUCTION_BAL , SYSADM.PS_PERSONAL_DATA PS_PERSONAL_DATA " 
           + "where PS_DEDUCTION_BAL.DED_CLASS = 'N' "
           + "and PS_DEDUCTION_BAL.BALANCE_ID = 'CY' "
           + "and PS_PERSONAL_DATA.EMPLID = PS_DEDUCTION_BAL.EMPLID "
           + "and PS_DEDUCTION_BAL.BALANCE_PERIOD = TO_CHAR(TO_NUMBER(TO_CHAR(TO_DATE(:10,'mm/dd/yyyy'),'MM'))) "
           + "and PS_DEDUCTION_BAL.BALANCE_YEAR = To_Char(TO_DATE(:11,'mm/dd/yyyy'),'YYYY') " 
           + "and ( PS_DEDUCTION_BAL.EMPLID, PS_DEDUCTION_BAL.DED_CLASS, PS_DEDUCTION_BAL.DED_MTD )  = " 
           + "( select PS_DEDUCTION_BAL1.EMPLID, "
           + "PS_DEDUCTION_BAL1.DED_CLASS, "
           + "PS_DEDUCTION_BAL1.DED_MTD "
           + "from SYSADM.PS_DEDUCTION_BAL PS_DEDUCTION_BAL1 "
           + "where PS_DEDUCTION_BAL1.BALANCE_PERIOD = TO_CHAR(TO_NUMBER(TO_CHAR(TO_DATE(:12,'mm/dd/yyyy'),'MM'))) "
           + "and PS_DEDUCTION_BAL1.BALANCE_YEAR = To_Char(TO_DATE(:13,'mm/dd/yyyy'),'YYYY') " 
           + "and PS_DEDUCTION_BAL1.DEDCD = '019' "
           + "and PS_DEDUCTION_BAL1.BALANCE_ID = 'CY' "
           + "and PS_DEDUCTION_BAL1.PLAN_TYPE = '80' "
           + "and PS_DEDUCTION_BAL1.EMPLID = PS_DEDUCTION_BAL.EMPLID "
           + "and PS_DEDUCTION_BAL1.DED_CLASS = PS_DEDUCTION_BAL.DED_CLASS "
           + "and PS_DEDUCTION_BAL1.DED_MTD = PS_DEDUCTION_BAL.DED_MTD )  ORDER BY PS_DEDUCTION_BAL.EMPLID ";

            return sqlstmt;
        }
        public String selectPenfaceFinanceSQL()
        {

            String sqlstmt = "select * from payroll.`PENFACE FINANCE`";
            return sqlstmt;
        }
        public String getFinanceDataFileNameSQL()
        {
            String sqlstmt = "select * from payroll.`PENFACE FINANCE` WHERE REC_TYPE = 'payroll'";
            return sqlstmt;
        }
        public String getTrackerFileSQL()
        {
            String sqlstmt = "SELECT MAX(FILE_NAME) AS CURR_FILE FROM `payroll`.`penface file track` WHERE FILE_NAME = @0";
            return sqlstmt;
        }
        public String getPenfaceLeaverOracleSQL()
        {
            String sqlstmt = "select 'PER' AS REC_TYPE, PS_JOB.EMPLID AS KEY_FLD1,'' AS KEY_FLD2, 'L' AS J_L_IND,'' AS SEX,'' AS TITLE, " +
                " '' AS SURNAME,'' AS FORENAMES,'' AS BIRTH_DT,'' AS BIRTH_DT_VERIFIED,'' AS NI_NO,'' AS NORM_RTR_DT,PS_JOB.EMPLID AS PAYNO,'' AS OTHER_REF, " +
                "TO_CHAR(PS_JOB.EFFDT,'ddmmyyyy') as LEAVE_DT ,'' AS MARITAL " +
                "from SYSADM.PS_JOB PS_JOB " +
                "where PS_JOB.EMPL_STATUS = 'T' " +
                "and PS_JOB.EMPL_RCD = '0' " +
                "and PS_JOB.EFFDT >= TO_DATE(:0,'MM/DD/YYYY') " +
                "and PS_JOB.EFFDT <= TO_DATE(:1,'MM/DD/YYYY')";
            return sqlstmt;
        }
        public String getBannerExportSQL()
        {

            String sqlstmt = "select PS_CAV_BANNER_XNS.ACCT_CD, " +
                               "TO_CHAR(PS_CAV_BANNER_XNS.CHECK_DT,'mm/dd/yyyy')  as CHECK_DATE, " +
                               "decode(PS_CAV_BANNER_XNS.DRCR,'D',PS_CAV_BANNER_XNS.AMOUNT,PS_CAV_BANNER_XNS.AMOUNT * -1) as AMT " +
                               "from SYSADM.PS_CAV_BANNER_XNS PS_CAV_BANNER_XNS " +
                               "where PS_CAV_BANNER_XNS.CHECK_DT = to_date(:0,'mm/dd/yyyy') "  +
                               "group by PS_CAV_BANNER_XNS.ACCT_CD, " +
                               "TO_CHAR(PS_CAV_BANNER_XNS.CHECK_DT,'mm/dd/yyyy'), " +
                               "decode(PS_CAV_BANNER_XNS.DRCR,'D',PS_CAV_BANNER_XNS.AMOUNT,PS_CAV_BANNER_XNS.AMOUNT * -1)";
            return sqlstmt;

        }
        public String getBannerJournalSQL()
        {
            String sqlstmt = "select PS_CAV_BANNER_XNS.ACCT_CD, PS_CAV_BANNER_XNS.CHECK_DT, PS_CAV_BANNER_XNS.DEPTID, " +
                             "PS_CAV_BANNER_XNS.EMPLID, PS_CAV_BANNER_XNS.AMOUNT AS T_AMOUNT, " +
                             "DECODE(PS_CAV_BANNER_XNS.DRCR,'D',PS_CAV_BANNER_XNS.AMOUNT,PS_CAV_BANNER_XNS.AMOUNT * -1) AS AMT," +
                             "PS_CAV_BANNER_XNS.DRCR,PS_PERSONAL_DATA.NAME, " +
                             "PS_CAV_BANNER_XNS.DESCR AS XNS_DESCR, PS_DEPT_TBL.DESCR,  PS_PERSONAL_DATA.EMPLID " +
                             "from SYSADM.PS_CAV_BANNER_XNS PS_CAV_BANNER_XNS, " +
                             "SYSADM.PS_PERSONAL_DATA PS_PERSONAL_DATA, " +
                             "SYSADM.PS_DEPT_TBL PS_DEPT_TBL  " +
                             "where ( PS_CAV_BANNER_XNS.EMPLID = PS_PERSONAL_DATA.EMPLID " +
                             "and PS_DEPT_TBL.DEPTID = PS_CAV_BANNER_XNS.DEPTID ) " +
                             "and ( PS_CAV_BANNER_XNS.CHECK_DT = TO_DATE(:0,'mm/dd/yyyy') " +
                             "and PS_CAV_BANNER_XNS.ACCT_CD is not null " +
                             "and PS_CAV_BANNER_XNS.ACCT_CD <> '.' " +
                             "and PS_CAV_BANNER_XNS.AMOUNT > 0 " +
                             "and length(PS_CAV_BANNER_XNS.ACCT_CD) <> '3' " +
                             "and PS_DEPT_TBL.EFF_STATUS = 'A' ) " +
                             "order by PS_CAV_BANNER_XNS.ACCT_CD ";
            return sqlstmt;
        }
        
        

        public String getPenfaceLeaverSQL()
        {
            String sqlstmt = "select * from `payroll`.`penface leavers`";
            return sqlstmt;

        }
        public String deleteFromLeaverData()
        {
            String sqlstmt = "delete from `payroll`.`penface leaver data`";

            return sqlstmt;
        }

        public String insertPenfaceLeaveData()
        {
            String sqlstmt = "insert into `penface leaver data` (REC_TYPE,KEY_FLD1,J_L_IND,PAYNO,LEAVE_DT) VALUES (@0,@1,@2,@3,@4)";

           

            return sqlstmt;
        }
        public String selectPenfaceLeaveDataSQL()
        {
            String sqlstmt = "select `penface leaver data` (REC_TYPE,KEY_FLD1,KEY_FLD2,J_L_IND,SEX,TITLE,SURNAME,FORENAMES,BIRTH_DT, " +
                             "BIRTH_DT_VERIFIED,NI_NO,NORM_RTR_DT,PAYNO,OTHER_REF,LEAVE_DT,MARITAL) VALUES " +
                             "(@0,@1,@2,@3,@4,@5,@6,@7,@8,@9,@10,@11,@12,@13,@14,@15)";


            
            return sqlstmt;
        }
        public String getLoginSQL()
        {
            String sqlstmt = "select * from `login` where " +
                             "username = '{0}' and password = '{1}'";

            return sqlstmt;
        }
        public string getPenfaceLeavers()
        {
            String sqlstmt = "SELECT * FROM `penface leavers`";

            return sqlstmt;

        }
    }
    

}