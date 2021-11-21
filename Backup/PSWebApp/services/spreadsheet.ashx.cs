using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PSWebApp.services
{
    /// <summary>
    /// Summary description for spreadsheet
    /// </summary>
    public class spreadsheet : IHttpHandler
    {

    

        public void ProcessRequest(HttpContext context)
        {

            context.Response.ContentType = " text/plain";
            String msg = "No File Available";
            
            context.Response.Write(msg);

            String filename = "";

            Boolean imageAdded = false;

            try
            {
                
                //write your handler implementation here.
                if (context.Request.Files.Count <= 0)
                {
                    context.Response.Write("No file uploaded");
                }
                else
                {
                    for (int i = 0; i <= context.Request.Files.Count; ++i)
                    {
                        if (i >= 1)
                        {

                            HttpPostedFile file = context.Request.Files[i - 1];

                            string extension = System.IO.Path.GetExtension(file.FileName.ToString());

                            filename = System.IO.Path.GetFileName(file.FileName.ToString());

                            if ((extension.Equals(".xlsx")))
                            {

                                String xlsxFile = context.Server.MapPath("~/" + "payroll/penface/finance data/" + file.FileName);

                                file.SaveAs(xlsxFile);
                                file.InputStream.Close();
                                
                                msg = "Success";
                                
                            }
                            else
                            {
                                if (!msg.Equals("Success"))
                                 msg = "Upload Failure, File Extension should be .xlsx";
                            }

                            context.Response.Clear();
                            context.Response.Write(msg);

                        }

                        if (imageAdded)
                            break;

                    }


                }
            }
            catch (Exception e)
            {
                context.Response.Clear();
                context.Response.Write("Error Message = " + e.Message);
                Console.WriteLine("{0} Exception caught.", e);
            }


             
          
               
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}