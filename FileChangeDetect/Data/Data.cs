using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using ServiceStack.Text;
using System.Text;
using System.Collections;
using FileChangeDetect.Comparer;

namespace FileChangeDetect.Data
{
    public static class Data
    {
        public const string JSON = "JSON";
        public const string JSONAtr = ".json";
        public static List<FileObj> GetFileSpecsFromJson(string path)
        {
            try
            {
                List<FileObj> files = new List<FileObj>();

                if (File.Exists(path))
                {
                    using (StreamReader reader = new StreamReader(path))
                    {
                        files = JsonSerializer.DeserializeFromReader<List<FileObj>>(reader);
                    }
                }
                return files;
            }
            catch (Exception ex)
            {

                throw new Exception("GetFileSpecsFromJson - specs loading issue occured", ex);
            }
        }
        public static bool WritteJsonData(string path, List<FileObj> files)
        {
            try
            {
                EnsureDirectoryExists(path);

                using (StreamWriter writer = new StreamWriter(path))
                {
                    JsonSerializer.SerializeToWriter(files, writer);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("WritteJsonData - specs writting issue occured", ex);
            }

        }
        private static void EnsureDirectoryExists(string filePath)
        {
            try
            {
                FileInfo fi = new FileInfo(filePath);
                if (!fi.Directory.Exists)
                {
                    System.IO.Directory.CreateDirectory(fi.DirectoryName);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("EnsureDirectoryExists - Directory creation/access issue occured", ex);
            }
        }

        public static List<FileObj> GetAvaillableFiles(string folderPath)
        {
            try
            {
                List<FileObj> files = new List<FileObj>();

                DirectoryInfo fileDir = new DirectoryInfo(folderPath);

                FileInfo[] Files = fileDir.GetFiles("*");

                foreach (FileInfo file in Files)
                {
                    FileObj fobj = new FileObj();
                    fobj.FilePath = file.FullName;
                    fobj.CreationDate = file.CreationTime;
                    fobj.ModificationDate = file.LastWriteTime;
                    fobj.Size = file.Length;
                    fobj.Version = 1;

                    files.Add(fobj);
                }

                return files;
            }
            catch (Exception ex)
            {
                throw new Exception("GetAvaillableFiles - Getting availlable file issue occured", ex);
            }
        }

        public static List<FileObj> GetDiffenceList(List<FileObj> filesSpecs, List<FileObj> filesFound)
        {
            try
            {
                List<FileObj> differenceListFiles = filesSpecs.Except(filesFound, new MissingComp()).ToList();
                return differenceListFiles;
            }
            catch (Exception ex)
            {
                throw new Exception("GetDiffenceList - issue occured during getting difference files", ex);
            }


        }
        public static List<FileObj> GetDeletedFiles(List<FileObj> savedspecs, List<FileObj> differenceList)
        {
            try
            {
                List<FileObj> deletedFiles = new List<FileObj>();
                deletedFiles = differenceList.Where(s => !savedspecs.Any(p => p.FilePath == s.FilePath)).ToList();

                return deletedFiles;
            }
            catch (Exception ex)
            {
                throw new Exception("GetDeletedFiles - issue occured during getting list of deleted files", ex);
            }
        }

        public static List<FileObj> GetUpdatedFoundList(List<FileObj> filesFound, List<FileObj> modifiedFiles)
        {
            try
            {
                List<FileObj> updatedFilesFoundList = new List<FileObj>();
                modifiedFiles.ForEach(x =>
                {
                    var itemToUpdate = filesFound.FirstOrDefault(p => p.FilePath == x.FilePath);
                    if (itemToUpdate != null)
                        filesFound[filesFound.IndexOf(itemToUpdate)].Version = x.Version;
                });
                return filesFound;
            }
            catch (Exception ex)
            {
                throw new Exception("GetUpdatedFoundList - issue occured during generating updated file list", ex);
            }

        }
        public static List<FileObj> GetModifiedFiles(List<FileObj> differenceList, List<FileObj> deletedItems)
        {
            try
            {
                List<FileObj> modifiedItems = new List<FileObj>();

                foreach (var item in differenceList.Except(deletedItems, new MissingComp()).ToList())
                {
                    item.Version++;
                    modifiedItems.Add(item);
                }

                return modifiedItems;
            }
            catch (Exception ex)
            {
                throw new Exception("GetModifiedFiles - issue occured during getting modified file list", ex);
            }

        }

        public static string GetPathHashName(string path)
        {
            try
            {
                using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
                {
                    byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(path);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    StringBuilder sb = new System.Text.StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        sb.Append(hashBytes[i].ToString("X2"));
                    }
                    return sb.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("GetPathHashName - issue occured during generating directory hash", ex);
            }
        }
    }
}