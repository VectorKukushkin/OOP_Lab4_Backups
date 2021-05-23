using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Lab4_Backups
{
    class FileInfo
    {
        public FileInfo(string path)
        {
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
            Exist = fileInfo.Exists;
            if (Exist)
            {
                Size = fileInfo.Length;
                Time = fileInfo.LastWriteTime;
            }
        }

        public readonly bool Exist;
        public readonly long Size;
        public readonly DateTime Time;
    }
}
