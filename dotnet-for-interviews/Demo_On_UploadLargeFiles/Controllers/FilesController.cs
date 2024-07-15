using Demo_On_UploadLargeFiles.Services;
using Demo_On_UploadLargeFiles.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Demo_On_UploadLargeFiles.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class FilesController : ControllerBase
{
    private readonly IFileService fileService;

    public FilesController(IFileService fileService)
    {
        this.fileService = fileService;
    }

    [HttpPost("upload-filestream-multipartreader")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
    [MultiPartFormData]
    [DisableFormValueModelBinding]
    [RequestSizeLimit(long.MaxValue)]
    public async Task<IActionResult> UploadAsync(IFormFile file)
    {
        var fileUploadSummary = await this.fileService.UploadFileAsync(Request.Body, Request.ContentType!);
        return CreatedAtAction(nameof(UploadAsync), fileUploadSummary);
    }

}
