using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DHDCShop.Common.Util
{
    public static class UploadImage
    {
        public static string UploadOneImage(HttpPostedFileBase file, string path, string name)
        {
            if (file != null)
            {
                var ext = Path.GetExtension(file.FileName);
                string myfile = name + ext;
                // đường dẫn lưu vào database
                var url = path + myfile;

                //đường dẫn để lưu tạo file trên ổ cứng
                var path2 = Path.Combine(HttpContext.Current.Server.MapPath(path), myfile);
                file.SaveAs(path2);

                return url;
            }
            return null;
        }

        public static void DeleteImage(string path)
        {
            var del_path = HttpContext.Current.Server.MapPath(path);
            FileInfo file = new FileInfo(del_path);
            if (file.Exists)//check file exsit or not  
            {
                file.Delete();
            }
        }
    }
}
