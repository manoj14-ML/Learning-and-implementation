using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Services;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;


namespace Test_webapplication
{
    public partial class CaptureCam : System.Web.UI.Page
    {
      public  static string servermappath="";
      public string strFormat = "dd-MM-yy-hh-mm-ss";
      public string Imagefolderpath = "~/Captures/";
        protected void Page_Load(object sender, EventArgs e)
        {
            servermappath = Server.MapPath(Imagefolderpath);

            if (!this.IsPostBack)
            {
                if (Request.InputStream.Length > 0)
                {
                    using (StreamReader myreader = new
                       StreamReader(Request.InputStream))
                    {
                        string contentType = Request.ContentType;
                        string hexString = Server.UrlEncode(myreader.ReadToEnd());
                        string imageName = DateTime.Now.ToString(strFormat);


                        string imagePath = string.Format("~/Captures/{0}.png", imageName);
                        

                    }
                }
            }
            if (IsPostBack && fileImageUpload.PostedFile != null)
            {
                if (fileImageUpload.PostedFile.FileName.Length > 0)
                {
                    System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath(string.Format
                            (Imagefolderpath)));
                    foreach (FileInfo file in di.GetFiles())
                    {
                        if (file.Length > 0)
                            file.Delete();
                    }


                    string myimageName = DateTime.Now.ToString
                           (strFormat) + Path.GetExtension(fileImageUpload.FileName).ToLower();
                    string ext = Path.GetExtension(fileImageUpload.FileName).ToLower();
                    string folderPath = Server.MapPath(Imagefolderpath);
                    servermappath = folderPath;
                    //Check whether Directory (Folder) exists.
                    if (!Directory.Exists(folderPath))
                    {
                        //If Directory (Folder) does not exists Create it.
                        Directory.CreateDirectory(folderPath);
                    }
                    if (fileImageUpload.HasFile)
                    {
                        if (fileImageUpload.PostedFile.ContentLength < 2097152)
                        {

                            if (ext == ".jpg" || ext == ".jpeg" || ext == ".gif" || ext == ".png")
                            {
                                //Save the File to the Directory (Folder).
                                fileImageUpload.SaveAs(folderPath + myimageName);

                                //Display the Picture in Image control.
                                panCrop.Visible = true;
                                srcUpload.ImageUrl = Imagefolderpath + myimageName;
                                //string filename = folderPath + myimageName;
                                srcUpload.Dispose();
                                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(fileImageUpload.PostedFile.InputStream);

                                Save(bmpPostedImage, 300, 300, 300, folderPath + myimageName);


                            }
                            else
                            {
                                lblText.Text = "Other file types(jpeg,gif,png) not allowed";
                            }
                        }
                        else
                        {
                            lblText.Text = "File size should not exceed more than 2 MB.";

                        }
                    }


                }
            }
        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="image"></param>
       /// <param name="maxWidth"></param>
       /// <param name="maxHeight"></param>
       /// <param name="quality"></param>
       /// <param name="filePath"></param>
        public void Save(Bitmap image, int maxWidth, int maxHeight, int quality, string filePath)
        {
            // Get the image's original width and height
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            // To preserve the aspect ratio
            float ratioX = (float)maxWidth / (float)originalWidth;
            float ratioY = (float)maxHeight / (float)originalHeight;
            float ratio = Math.Min(ratioX, ratioY);

            // New width and height based on aspect ratio
            int newWidth = (int)(originalWidth * ratio);
            int newHeight = (int)(originalHeight * ratio);

            // Convert other formats (including CMYK) to RGB.
            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            // Draws the image in the specified size with quality mode set to HighQuality
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            // Get an ImageCodecInfo object that represents the JPEG codec.
            ImageCodecInfo imageCodecInfo = this.GetEncoderInfo(ImageFormat.Jpeg);

            // Create an Encoder object for the Quality parameter.
            Encoder encoder = Encoder.Quality;

            // Create an EncoderParameters object. 
            EncoderParameters encoderParameters = new EncoderParameters(1);

            // Save the image as a JPEG file with quality level.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;
            newImage.Save(filePath, imageCodecInfo, encoderParameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param> 
        /// <param name="e"></param>
        protected void ExportToImage(object sender, EventArgs e)
        {
            string base64 = Request.Form[hfImageData.UniqueID].Split(',')[1];
            byte[] bytes = Convert.FromBase64String(base64);
           
            System.IO.DirectoryInfo di = new DirectoryInfo(servermappath);
            foreach (FileInfo file in di.GetFiles())
            {
                if (file.Length > 0)
                    file.Delete();
            }
            string filename = DateTime.Now.ToString
                          (strFormat) + ".png";
            string fileNameWitPath = servermappath + filename;

            using (FileStream fs = new FileStream(fileNameWitPath, FileMode.Create, FileAccess.ReadWrite))
            {

                using (BinaryWriter bw = new BinaryWriter(fs))
                {

                    byte[] data = bytes;

                    bw.Write(data);
                    bw.Flush();
                    bw.Close();
                    bw.Dispose();
                   
                   
                }
                
                //fs.Flush();
                fs.Dispose();
                fs.Close();
           }
            panCrop.Visible = true;
            srcUpload.ImageUrl = Imagefolderpath + filename;            
            srcUpload.DataBind();
            srcUpload.Dispose();
            
        }
      

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //take  the file from capture folder and save that to database 
            System.IO.DirectoryInfo di = new DirectoryInfo(Server.MapPath(string.Format
                           (Imagefolderpath)));
            foreach (FileInfo file in di.GetFiles())
            {
                if (file.Length > 0)
                {

                    string fileName = file.Name;
                    string filePath = Path.Combine(Server.MapPath(Imagefolderpath), fileName);
                    //string cropFileName = "";
                    //string cropFilePath = "";
                    System.Drawing.Image orgImg = System.Drawing.Image.FromFile(filePath);
                    Rectangle CropArea = new Rectangle(Convert.ToInt32(X.Value), Convert.ToInt32(Y.Value), Convert.ToInt32(W.Value), Convert.ToInt32(H.Value));
                    try
                    {
                        Bitmap bitMap = new Bitmap(CropArea.Width, CropArea.Height);
                        using (Graphics g = Graphics.FromImage(bitMap))
                        {
                            g.DrawImage(orgImg, new Rectangle(0, 0, bitMap.Width, bitMap.Height), CropArea, GraphicsUnit.Pixel);
                        }
                        //cropFileName = "crop_" + fileName;
                        //cropFilePath = Path.Combine(Server.MapPath("~/Captures"), cropFileName);
                        orgImg.Dispose();

                        if (file.Length > 0)
                            file.Delete();
                        bitMap.Save(filePath);
                        srcUpload.ImageUrl = Imagefolderpath + fileName;
                        byte[] imageArray = System.IO.File.ReadAllBytes(filePath);
                        string base64ImageRepresentation = Convert.ToBase64String(imageArray); 
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }



            //after save,delete the file from folder completely

          

        }





    }

}