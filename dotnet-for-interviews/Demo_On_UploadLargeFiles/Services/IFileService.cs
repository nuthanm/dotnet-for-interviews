using Demo_On_UploadLargeFiles.DTO;

namespace Demo_On_UploadLargeFiles.Services
{
    public interface IFileService
    {
        Task<FileUploadSummary> UploadFileAsync(Stream bodyStream, string contentType);
    }
}
