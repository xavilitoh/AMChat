using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AMChat.Helpers
{
    public interface IFileHelper
    {
        string UploadPhoto(IFormFile file, string folder);

        byte[] GetBytesFromUrl(string url);

        string WriteBytesToFile(string fileName, byte[] content);
    }
}
