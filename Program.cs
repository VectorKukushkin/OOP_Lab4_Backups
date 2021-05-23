using System;

namespace OOP_Lab4_Backups
{
    class Program
    {
        static void Main(string[] args)
        {
            Backup backup1 = new Backup("dir1");
            backup1.CreateBackup();
            backup1.Limit = new Limits.CountLimit(1);

            Backup backup2 = new Backup("dir2", true);
            backup2.CreateBackup(true, true);
            backup2.Limit = new Limits.SizeLimit(250);

            Backup backup3 = new Backup("dir1");
            backup3.CreateBackup(true);
            backup3.CreateBackup(false);
            backup3.CreateBackup(false);
            backup3.Limit = new Limits.CountLimit(1);
            backup3.CreateBackup(true);

            Backup backup4 = new Backup("dir2");
            backup4.Limit = new Limits.HybridLimit(true, new Limits.CountLimit(2), new Limits.CountLimit(3));
            backup4.CreateBackup();
            backup4.CreateBackup();
            backup4.CreateBackup();
            backup4.Limit = new Limits.HybridLimit(false, new Limits.CountLimit(2), new Limits.CountLimit(3));
        }
    }
}
