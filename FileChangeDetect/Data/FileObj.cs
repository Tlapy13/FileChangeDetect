using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FileChangeDetect.Data
{
    public class FileObj
    {
        public string FilePath { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public long Size { get; set; }
        public int Version { get; set; }
    }
}