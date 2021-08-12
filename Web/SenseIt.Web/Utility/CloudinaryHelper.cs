namespace SenseIt.Web.Utility
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;

    public class CloudinaryHelper
    {
        public static async Task<string> Upload(Cloudinary cloudinary, ICollection<IFormFile> files, string folderName)
        {
            var imageUrl = "";

            foreach (var file in files)
            {
                byte[] destinationImage;

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    destinationImage = memoryStream.ToArray();
                }

                using (var destinationStream = new MemoryStream(destinationImage))
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        Folder = folder,
                        File = new FileDescription(file.FileName, destinationStream),
                    };

                    var result = await cloudinary.UploadAsync(uploadParams);

                    imageUrl = result.Uri.AbsoluteUri;

                    ;
                }
            }

            return imageUrl;
        }
    }
}
