using System;
using System.Collections.Generic;
using System.Text;

namespace OOP_Lab4_Backups
{
    class Backup
    {
        public Backup(string[] files, bool isArchive = false)
        {
            for (int i = 0; i < files.Length; i++)
            {
                if (!Files.Contains(files[i]))
                {
                    Files.Add(files[i]);
                }
                else
                {
                    Console.Error.WriteLine("The input data contains the same files!");
                }
            }
            CreateBackup(true, isArchive);
        }

        public Backup(string path, bool isArchive = false) : this(System.IO.Directory.GetFiles(path), isArchive) { }

        public readonly HashSet<string> Files = new HashSet<string>();
        public readonly List<RestorePoint> RestorePoints = new List<RestorePoint>();

        private Limits.Limit limit;

        public Limits.Limit Limit
        {
            set
            {
                limit = value;
                DeletePastBackups();
            }
            get
            {
                return limit;
            }
        }

        public void AddFile(string file)
        {
            if (!Files.Contains(file))
            {
                Files.Add(file);
            }
            else
            {
                Console.Error.WriteLine("The backup already contains this file!");
            }
        }

        public void DeleteFile(string file)
        {
            if (Files.Contains(file))
            {
                Files.Remove(file);
            }
            else
            {
                Console.Error.WriteLine("The backup doesn't contain this file!");
            }
        }

        private FileInfo FindLastBackup(string file)
        {
            for (int i = RestorePoints.Count - 1; i >= 0; i--)
            {
                if (RestorePoints[i].Files.ContainsKey(file))
                {
                    return RestorePoints[i].Files[file];
                }
                else
                {
                    if (RestorePoints[i].IsFull)
                    {
                        return null;
                    }
                }
            }
            return null;
        }

        private bool IsBackupSame(FileInfo file1, FileInfo file2)
        {
            if (file1 != null && file2 != null)
            {
                if (file1.Time == file2.Time && file1.Size == file2.Size)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (file1 == file2)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        private bool IsBackupNeeded(string file)
        {
            if (IsBackupSame(new FileInfo(file), FindLastBackup(file)))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void CreateIncrementalBackup(bool isArchive = false)
        {
            HashSet<string> files = new HashSet<string>();
            foreach (string file in Files)
            {
                if (IsBackupNeeded(file))
                {
                    files.Add(file);
                }
            }
            RestorePoints.Add(new RestorePoint(false, files, isArchive));
        }

        public void CreateBackup(bool full = true, bool isArchive = false)
        {
            if (full)
            {
                RestorePoints.Add(new RestorePoint(true, Files, isArchive));
            }
            else
            {
                CreateIncrementalBackup(isArchive);
            }
            DeletePastBackups();
        }

        public int Count
        {
            get
            {
                return RestorePoints.Count;
            }
        }

        public DateTime FirstDate
        {
            get
            {
                if (RestorePoints.Count > 0)
                {
                    return RestorePoints[0].Time;
                }
                else
                {
                    return new DateTime();
                }
            }
        }

        public long Size
        {
            get
            {
                long size = 0;
                for (int i = 0; i < RestorePoints.Count; i++)
                {
                    size += RestorePoints[i].Size;
                }
                return size;
            }
        }

        public bool IsLimitAchieved()
        {
            if (Limit != null)
            {
                return Limit.Achieved(Count, FirstDate, Size);
            }
            else
            {
                return false;
            }
        }

        private int FindFirstFullBackup()
        {
            for (int i = 1; i < RestorePoints.Count; i++)
            {
                if (RestorePoints[i].IsFull)
                {
                    return i;
                }
            }
            return 0;
        }

        private void DeletePastBackups()
        {
            int number = FindFirstFullBackup();
            while (number > 0 && IsLimitAchieved())
            {
                RestorePoints.RemoveRange(0, number);
                number = FindFirstFullBackup();
            }
            if (IsLimitAchieved())
            {
                Console.Error.WriteLine("The limit has been achieved but it's impossible to delete more restore points!");
            }
        }
    }
}
