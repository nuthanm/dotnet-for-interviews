using Demo_On_UploadLargeFiles.DTO;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

namespace Demo_On_UploadLargeFiles.Services;

public class FileService : IFileService
{
    private const string UploadDirectory = "FilesUploaded";
    private readonly List<string> _allowedExtensions = [".zip", ".bin", ".png", ".jpg"];

    public async Task<FileUploadSummary> UploadFileAsync(Stream bodyStream, string contentType)
    {
        var filesCount = 0;
        long totalSizeInBytes = 0;


        var boundry = GetBoundry(MediaTypeHeaderValue.Parse(contentType));

        // Read multiple files from stream using Multipart reader
        var multiPartReader = new MultipartReader(boundry, bodyStream);
        var section = await multiPartReader.ReadNextSectionAsync();

        var filePaths = new List<string>();
        var notUploadedFiles = new List<string>();

        while (section is not null)
        {
            var fileSection = section.AsFileSection();
            if (fileSection is not null)
            {
                var result = await SaveFileAsync(fileSection, filePaths, notUploadedFiles);

                if (result > 0)
                {
                    totalSizeInBytes += result;
                    filesCount++;
                }
            }

            section = await multiPartReader.ReadNextSectionAsync();
        }

        return new FileUploadSummary
        {
            TotalFilesUploaded = filesCount,
            TotalSizeUploaded = ConvertSizeIntoString(totalSizeInBytes),
            FilePaths = filePaths,
            NotUploadedFiles = notUploadedFiles
        };
    }

    private string ConvertSizeIntoString(long totalSizeInBytes)
    {
        var fileSize = new decimal(totalSizeInBytes);
        var kilobyte = new decimal(1024);
        var megabyte = new decimal(1024 * 1024);
        var gigabyte = new decimal(1024 * 1024 * 1024);

        return fileSize switch
        {
            _ when fileSize < kilobyte => "Less than 1KB",
            _ when fileSize < megabyte =>
                $"{Math.Round(fileSize / kilobyte, fileSize < 10 * kilobyte ? 2 : 1, MidpointRounding.AwayFromZero): ##, ###.##}KB",
            _ when fileSize < gigabyte =>
            $"{Math.Round(fileSize / megabyte, fileSize < 10 * megabyte ? 2 : 1, MidpointRounding.AwayFromZero): ##, ###.##}MB",
            _ when fileSize >= gigabyte => $"{Math.Round(fileSize / gigabyte, fileSize < 10 * megabyte ? 2 : 1, MidpointRounding.AwayFromZero): ##, ###.##}GB",
            _ => "n/a"
        };
    }

    private async Task<long> SaveFileAsync(FileMultipartSection fileSection,
        List<string> filePaths, List<string> notUploadedFiles)
    {
        var extension = Path.GetExtension(fileSection.FileName);
        if (!_allowedExtensions.Contains(extension))
        {
            notUploadedFiles.Add(fileSection.FileName);
            return 0;
        }

        Directory.CreateDirectory(UploadDirectory);

        var filePath = Path.Combine(UploadDirectory, fileSection.FileName);

        await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, 1024);
        await fileSection.FileStream.CopyToAsync(stream);
        filePaths.Add(GetFullFilePath(fileSection));

        return fileSection.FileStream.Length;

    }

    private string GetFullFilePath(FileMultipartSection fileSection)
    {
        return !string.IsNullOrEmpty(fileSection.FileName) ?
            Path.Combine(Directory.GetCurrentDirectory(), UploadDirectory, fileSection.FileName) :
            string.Empty;
    }

    private string GetBoundry(MediaTypeHeaderValue mediaTypeHeaderValue)
    {
        // Extract the boundry value
        var boundry = HeaderUtilities.RemoveQuotes(mediaTypeHeaderValue.Boundary).Value;

        if (string.IsNullOrWhiteSpace(boundry))
            throw new InvalidDataException("Missing content-type boundary");

        return boundry;
    }
}
