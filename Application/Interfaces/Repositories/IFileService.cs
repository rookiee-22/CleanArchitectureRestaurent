using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories;

public interface IFileService
{
    Task<string> UploadAsync(
         IFormFile file,
         string folderName);
}
