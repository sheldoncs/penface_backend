using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSWebApp
{
    public class Util
    {
        PenfaceMySQL penface;
        public void setPenface(PenfaceMySQL p)
        {
            penface = p;
        }
        public String getNewFile(Boolean previousFile, String fileType, String dteStr)
        {
            String Str_SeqNo = "01";
            Boolean Found = true;
            Int32 SeqNo = 0;
            Int32 SeqNo_Length = 0;
            String filename = "";
            DateTime ss;
            String mth = "";
            String yr = "";

            while (Found)
            {

                ss = Convert.ToDateTime(dteStr);
                mth = adjString(ss.Month);
                yr = adjString(ss.Year);
                yr = yr.Substring(2, 2);

                filename = "UCH" + mth.Trim() + yr.Trim() + Str_SeqNo + fileType;

                Found = penface.getPenfaceFileTracker(filename);

                if (Found)
                    SeqNo = SeqNo + 1;

                SeqNo_Length = SeqNo.ToString().Length;
                if (SeqNo_Length == 1)
                    Str_SeqNo = "0" + SeqNo.ToString();
                else
                    Str_SeqNo = SeqNo.ToString();

            }

            if (previousFile)
            {
                SeqNo = SeqNo - 1;
                Str_SeqNo = adjString(SeqNo);
                filename = "UCH" + mth.Trim() + yr.Trim() + Str_SeqNo + fileType;
            }

            return filename;

        }
        public String adjString(Int32 val)
        {
            String strV;

            if (val < 10)
                strV = "0" + val.ToString();
            else
                strV = val.ToString();

            return strV;

        }
    }
}