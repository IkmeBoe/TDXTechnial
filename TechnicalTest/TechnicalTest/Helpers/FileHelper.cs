using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TechnicalTest.FileLoad;

namespace TechnicalTest.Helpers
{
    public class FileHelper
    {
        internal IEnumerable<string> GetFileLines(byte[] file)
        {
            return (Encoding.Default.GetString(
                file,
                0,
                file.Length - 1)).Split(new string[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None);
        }

        internal LoadedFile GetFile(string fullFileNameWithPath)
        {
            LoadedFile file = new LoadedFile
            {
                Filename = GetFileName(fullFileNameWithPath),
                FileBytes = File.ReadAllBytes(fullFileNameWithPath),
                FileExtension = fullFileNameWithPath.Split('.').Last()
            };


            return file;
        }

        internal string GetFileName(string fullFileNameWithPath)
        {
        int pos = fullFileNameWithPath.LastIndexOf("/") + 1;
            return fullFileNameWithPath.Substring(pos, fullFileNameWithPath.Length - pos);
        }
    }
}
