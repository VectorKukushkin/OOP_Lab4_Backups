using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Lab4_Backups
{
    class RestorePoint
    {
        public RestorePoint(bool full, HashSet<string> files, bool isArchive = false)
        {
            IsFull = full;
            Time = DateTime.Now;
            foreach (string file in files)
            {
                Files.Add(file, new FileInfo(file));
            }
            IsArchive = isArchive;
        }

        public RestorePoint(HashSet<string> files, bool isArchive = false) : this(true, files, isArchive) { }

        public readonly bool IsFull;
        public readonly DateTime Time;

        public readonly bool IsArchive;

        public readonly Dictionary<string, FileInfo> Files = new Dictionary<string, FileInfo>();

        public long Size
        {
            get
            {
                long size = 0;
                foreach (FileInfo file in Files.Values)
                {
                    if (file.Exist)
                    {
                        size += file.Size;
                    }
                }
                if (IsArchive)
                {
                    size -= size / 5;
                }
                return size;
            }
        }
    }
}
