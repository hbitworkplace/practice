using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace CityInfoApi.Controllers
{
    [ApiController]
    [Route("api/Files")]
    public class FileController:ControllerBase
    {
        private readonly FileExtensionContentTypeProvider _fileExtensionContentTypeProvider;

        public FileController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            _fileExtensionContentTypeProvider = fileExtensionContentTypeProvider
                ?? throw new System.ArgumentException(nameof(fileExtensionContentTypeProvider));
        }
        [HttpGet("{fileId}")]
        public ActionResult GetFile(string fileId)
        {
            //lookup for file
            var pahtoFile = "cprogramming_tutorial.pdf";
            //check if the file exits
            if(!System.IO.File.Exists(pahtoFile))
            {
                return NotFound();
            }
            if(!_fileExtensionContentTypeProvider.TryGetContentType(pahtoFile,out var contentType))
            {
                contentType = "application/octet-stream";
            }
            var bytes = System.IO.File.ReadAllBytes(pahtoFile);
            return File(bytes, contentType, Path.GetFileName(pahtoFile));
        }
    }
}
