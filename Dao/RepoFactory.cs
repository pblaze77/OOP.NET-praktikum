using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dao
{
    public static class RepoFactory
    {
        public static IImagesRepo GetImagesRepo() => new ImagesRepo();
        public static IFilesRepo GetRepo() => new TextRepo();
        public static IWebRepo GetWebRepo() => new WebRepo();
       
    }
}
