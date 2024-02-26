using API.Helpers;
using API.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinarySettings> config)
        {
            Account account = new Account(
                "dx4urayif",
                "685624613773267",
                "RZF7Bh7cX8uafydwSZldT_KDVq0");

            _cloudinary = new Cloudinary(account);
        }
        public async  Task<ImageUploadResult> AddPhotoAsync(IFormFile file) 
        {
            var ImageUploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill"),
                    Folder = "da-net7"
                };
                ImageUploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return ImageUploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            return await _cloudinary.DestroyAsync(deleteParams);
        }
    }
}