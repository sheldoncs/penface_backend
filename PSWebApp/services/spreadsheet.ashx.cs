using System;
using System.Collections.Generic;
using System.IO;
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

                            if ((extension.Equals(".csv")))
                            {

                                String xlsxFile = context.Server.MapPath("~/" + "finance data/" + file.FileName);
                                String path = context.Server.MapPath("~/" + "finance data/");
                                deleteAnyFile(path, file.FileName);
                                file.SaveAs(xlsxFile);
                                file.InputStream.Close();
                                
                                msg = "Success";
                                
                            }
                            else
                            {
                                if (!msg.Equals("Success"))
                                 msg = "Upload Failure, File Extension should be .csv";
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
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}