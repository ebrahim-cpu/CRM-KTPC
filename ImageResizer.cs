using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace CRM
{
    public class ImageResizer
    {
        public static bool IsImageWidthGreaterThanOrEqualTo(Image imej, int lebarMinima)
        {
            return imej.Width >= lebarMinima;
        }
        public static void ResizeDanSimapanImej(string namaFailAsal, string lorongGambarAsal, string lorongFolderOutput, string dariMana)
        {
            string extenxions = Path.GetExtension(lorongGambarAsal).ToLower();
            using (Image gambarAsal = Image.FromFile(lorongGambarAsal))
            {
                if (IsImageWidthGreaterThanOrEqualTo(gambarAsal, 640))
                {
                    int lebarBaru = 640;
                    int tinggiBaru = (int)(((float)lebarBaru / gambarAsal.Width) * gambarAsal.Height);
                    using (Image gambarBersaizBaru = new Bitmap(lebarBaru, tinggiBaru))
                    using (Graphics geraphics = Graphics.FromImage(gambarBersaizBaru))
                    {
                        geraphics.DrawImage(gambarAsal, 0, 0, lebarBaru, tinggiBaru);
                        //string newFileName = $"{DateTime.Now:yyyyMMddHHmmss}_resized{extenxions}";
                        string newFileName = $"{dariMana}_{DateTime.Now:yyyyMMddHHmmss}_{namaFailAsal}{extenxions}";
                        string newFilePath = Path.Combine(lorongFolderOutput, newFileName);
                        gambarBersaizBaru.Save(newFilePath, gambarAsal.RawFormat);
                    }
                }
            }
        }
    }
}