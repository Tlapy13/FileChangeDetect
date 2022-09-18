using FileChangeDetect.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileChangeDetect.Comparer
{
        public class MissingComp : IEqualityComparer<FileObj>
        {
       
        public bool Equals(FileObj x, FileObj y)
        {
            if (x is null || y is null)
                return false;

            return x.FilePath == y.FilePath;
        }

        public int GetHashCode(FileObj obj)
        {
            if (obj is null) return 0;

            int hashNumf = obj.FilePath == null ? 0 : string.Format("{0},{1},{2},{3},", 
                obj.FilePath, obj.CreationDate, obj.ModificationDate, obj.Size).GetHashCode();

            return hashNumf;
        }
    }


    }