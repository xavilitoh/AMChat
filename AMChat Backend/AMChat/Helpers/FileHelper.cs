using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace AMChat.Helpers
{
    public class FileHelper : IFileHelper
    {
        public string UploadPhoto(IFormFile file, string folder)
        {
            if (file != null)
            {
                var fileName = file.FileName;
                var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/{folder}");
                var myFile = Path.Combine(path, fileName);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var filestream = new FileStream(myFile, FileMode.Create))
                {
                    file.CopyTo(filestream);
                }
                return fileName;
            }
            else
            {
                return "noImage.png";
            }
        }

        public byte[] GetBytesFromUrl(string url)
        {
            byte[] b;
            HttpWebRequest myReq = (HttpWebRequest)WebRequest.Create(url);
            WebResponse myResp = myReq.GetResponse();

            Stream stream = myResp.GetResponseStream();
            //int i;
            using (BinaryReader br = new BinaryReader(stream))
            {
                //i = (int)(stream.Length);
                b = br.ReadBytes(500000);
                br.Close();
            }
            myResp.Close();
            return b;
        }

        public string WriteBytesToFile(string fileName, byte[] content)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/images/Clientes");
            var myFile = Path.Combine(path, fileName);

            FileStream fs = new FileStream(myFile, FileMode.Create);
            BinaryWriter w = new BinaryWriter(fs);

            try
            {
                w.Write(content);
            }
            finally
            {
                fs.Close();
                w.Close();
            }

            return fileName;

        }
    }
}
