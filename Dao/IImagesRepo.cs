using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public interface IImagesRepo
    {
        string GetImage(string name, string shirtNum);
        void SetImage(string selectedImagePath);
    }
}
