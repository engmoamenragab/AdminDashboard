using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADB.BL.Helpers
{
    public static class FileUploader
    {
        public static string UplodFile(string folderName, IFormFile file)
        {
            try
            {
                // 1 => Get Directory
                // Directory.GetCurrentDirectory() => replace the folder path on the server
                string filePath = Directory.GetCurrentDirectory() + "/wwwroot/files/" + folderName;
                // 2 => Get File Name
                //string cvName = model.Cv.FileName;
                // Some browser send the file name with token to make the name unique
                // So to remove this token and get the file name separate
                // We use [Path.GetFileName(model.Cv.FileName)] => remove any extras added to the file name
                // To make the file name unique we will concat [Guid.NewGuid()] with the file name
                // [Guid.NewGuid()] => It is a unique Id contains 36 digit
                // We will concat it from the left side to avoid override the file extension
                string fileName = Guid.NewGuid() + Path.GetFileName(file.FileName);
                // 3 => Merge Path with File Name
                //string cvFinalPath = cvPath + cvName;
                // To avoid forget any character into the URL
                // We use [Path.Combine()] function
                // [Path.Combine()] => It makes concat between two URLs and put [/] between them if not exist
                string fileFinalPath = Path.Combine(filePath, fileName);
                // 4 => Save File As Streams [Data Overtime]
                using (var Stream = new FileStream(fileFinalPath, FileMode.Create))
                {
                    file.CopyTo(Stream);
                }
                return fileName;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
