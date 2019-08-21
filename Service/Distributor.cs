using FilesDistributor.Logger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FilesDistributor
{
    class Distributor
    {
        #region Constants
        private const string EmptyPathMessage = "Path is empty. Please, try again with correct data.";

        private const string ImageFolderName = "Images";
        private const string TextDocsFolderName = "Text Documents";
        private const string OtherFolderName = "Other";
        private const string ArchivesFolderName = "Archives";
        private const string ExecutableFolderName = "Executable files";
        
        #endregion

        #region Private fields

        private static Dictionary<string, List<string>> _fileTypesDictionary =
            new Dictionary<string, List<string>>
        {
            { ImageFolderName, new List<string> { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".ico" } },
            { TextDocsFolderName, new List<string> { ".txt", ".doc", ".docx", ".exl", ".pdf" } },
            { ArchivesFolderName, new List<string> { ".7zip", ".zip", ".rar" } },
            { ExecutableFolderName, new List<string> { ".exe", ".bat", ".bin" } }
        };

        private readonly ILogger _logger;

        #endregion

        public Distributor(ILogger logger) => _logger = logger;

        public void DistributeFiles(string path)
        {
            string[] files = Array.Empty<string>();
            if (string.IsNullOrEmpty(path))
            {
                _logger.Error(EmptyPathMessage);
                return;
            }

            try
            {
                files = Directory.GetFiles(path);
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message);
                return;
            }

            SortFilesByFolders(files, path);   
        }

        private void MoveFile(string rootPath, string filename, string folder)
        {
            string destFileName = $"{rootPath}/{folder}/{filename}";
            string srcFileName = $"{rootPath}/{filename}";

            Directory.CreateDirectory($"{rootPath}/{folder}");
            try
            {
                File.Move(srcFileName, destFileName);
            }
            catch(IOException ex)
            {
                _logger.Error(ex.Message);
                return;
            }

            _logger.Success(Path.GetFileName(filename) + $"\t  OK.");
        }

        private void SortFilesByFolders(IEnumerable<string> files, string path)
        {
            bool isDistributed;
            foreach (var file in files)
            {
                isDistributed = false;
                string extension = Path.GetExtension(file);

                foreach (var fileType in _fileTypesDictionary)
                {
                    if (fileType.Value.Contains(extension, StringComparer.InvariantCultureIgnoreCase))
                    {
                        MoveFile(path, Path.GetFileName(file), fileType.Key);
                        isDistributed = true;
                        break;
                    }
                }

                if (!isDistributed)
                {
                    MoveFile(path, Path.GetFileName(file), OtherFolderName);
                }
            }
        }
    }
}
