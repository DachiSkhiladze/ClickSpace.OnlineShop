using ClickSpace.OnlineShop.BAL.Models;
using ClickSpace.OnlineShop.BAL.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClickSpace.OnlineShop.WebAPI.Controllers
{
    public class FileUploadController : ControllerBase
    {
        public static IWebHostEnvironment _webHostEnvironment;
        public static IProductPictureServices _productPictureServices;

        public FileUploadController(IWebHostEnvironment webHostEnvironment, IProductPictureServices productPictureServices)
        {
            _webHostEnvironment = webHostEnvironment;
            _productPictureServices = productPictureServices;
        }

        [Authorize(Roles = "Administrator")]
        [Route("PostPictures")]
        [HttpPost]
        public async Task<string> UploadProductPictures([FromForm] PictureModel fileUpload)
        {
            try
            {
                string path = _webHostEnvironment.WebRootPath + "\\uploads\\"; 
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                if (fileUpload.files.Length <= 0 || fileUpload.files.Length > 20)
                {
                    return "Pictures Are Too Many Or Too Few To Be Uploaded.";
                }
                foreach (var file in fileUpload.files)
                {
                    string name = _productPictureServices.GetNameForNewPicture(file.FileName);
                    using (FileStream fileStream = System.IO.File.Create(path + name))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    await _productPictureServices.InsertAsync(fileUpload.ProductId, name);    // Saving name and product id in the database
                }
                return "Upload Done.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [Route("GetPictureByName/{fileName}")]
        [HttpGet]
        public async Task<IActionResult> GetPictures([FromRoute] string fileName)
        {
            string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
            var filePath = path + fileName;
            if (System.IO.File.Exists(filePath))
            {
                byte[] b = System.IO.File.ReadAllBytes(filePath);
                return PhysicalFile(filePath, "image/png");
            }
            return NotFound();
        }

        [Authorize(Roles = "Administrator")]
        [Route("DeletePictureByName/{fileName}")]
        [HttpGet]
        public async Task<IActionResult> DeletePictureByName([FromRoute] string fileName)
        {
            await _productPictureServices.DeleteByNameAsync(fileName);
            return Ok();
        }

        [Route("GetPicturesNamesByID/{productId}")]
        [HttpGet]
        public IEnumerable<string> GetPicturesNamesByID([FromRoute] long productId)
        {
            string path = _webHostEnvironment.WebRootPath + "\\uploads\\";
            var photos = _productPictureServices.GetPicturesByProductID(productId);


            foreach (var photo in photos)
            {
                yield return photo.PictureUrl;
            }
        }
    }
}
