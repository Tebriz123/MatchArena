using MatchArena.Domain.Entities.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchArena.Application.Validators
{
    internal static class FileValidator
    {
        public static bool ValidateType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }
        public static bool ValidateSize(this IFormFile file, FileSize fileSize, int size)
        {
            switch (fileSize)
            {
                case FileSize.KB:
                    return file.Length < size * 1024;

                case FileSize.MB:
                    return file.Length < size * 1024 * 1024;

                case FileSize.GB:
                    return file.Length < size * 1024 * 1024 * 1024;

            }
            return false;
        }
    }
}
