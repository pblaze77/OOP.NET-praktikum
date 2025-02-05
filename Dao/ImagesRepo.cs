using Dao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Dao
{
    internal class ImagesRepo : IImagesRepo
    {
        string imagesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Images");
        public string GetImage(string name, string shirtNum)
        {
            string imagePath = Path.Combine(imagesPath, $"{name}-{shirtNum}.png");
            return File.Exists(imagePath)
                ? imagePath
                : @"C:\Users\Paulo\Desktop\Paulo\Images\default.png";
        }
        public void SetImage(string selectedImagePath)
        {

            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
            }

            string fileName = Path.GetFileName(selectedImagePath);
            string destFilePath = Path.Combine(imagesPath, fileName);

            File.Copy(selectedImagePath, destFilePath, true);
        }
    }
}
