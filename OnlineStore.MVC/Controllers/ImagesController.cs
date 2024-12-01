using Microsoft.AspNetCore.Mvc;
using OnlineStore.Contracts.Images;
using OnlineStore.Core.Images.Services;

namespace OnlineStore.MVC.Controllers
{
    public class ImagesController : Controller
    {
        private readonly IImageService _imageService;

        public ImagesController(
            IImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpGet("images/{imageId}")]
        public async Task<IActionResult> GetImage([FromRoute] int imageId, CancellationToken cancellation)
        {
            var image = await _imageService.GetAsync(imageId, cancellation);

            return File(image.Data, image.ContentType);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImages(List<IFormFile> imageFiles, CancellationToken cancellation)
        {
            bool isImageContainsContent;
            bool isImageValidContentType;
            var result = new List<IFormFile>();
            foreach (var imageFile in imageFiles)
            {
                isImageContainsContent = imageFile.Length > 0;

                isImageValidContentType = 
                    imageFile.ContentType == "image/png" || 
                    imageFile.ContentType == "image/jpg" ||
                    imageFile.ContentType == "image/jpeg";

                if (!(isImageContainsContent && isImageValidContentType))
                    continue;

                result.Add(imageFile);
            }

            var imageUrls = await _imageService.SaveImagesAsync(result, cancellation);

            return Json(imageUrls);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteImage([FromBody] DeleteImageRequest deleteImageModel, CancellationToken cancellation)
        {
            var imageId = _imageService.ExtractImageId(deleteImageModel.imageUrl);

            await _imageService.DeleteAsync(imageId, cancellation);

            return Ok();
        }
    }
}
