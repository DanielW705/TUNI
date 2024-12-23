using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;

namespace TUNIWEB.Utilities
{
    public static class CreateTemporalFiles
    {
        public static IHostingEnvironment _hostingEnvironment;
        public static string CreateTemporalyFile(Guid userId, string typeOfFile, string fileName, byte[] data)
        {

            if (_hostingEnvironment is null)
                throw new InvalidOperationException("HostingEnvironment no está configurado.");

            string path = Path.Combine(_hostingEnvironment.WebRootPath,"files", userId.ToString(), typeOfFile);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path = Path.Combine(path, fileName);

            File.WriteAllBytes(path, data);


            string relative_path = Path.Combine(@"\files", userId.ToString(), typeOfFile,fileName);
            
            
            return relative_path;
        }
    }
}
