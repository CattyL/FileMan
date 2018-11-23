﻿using Azylee.Core.LogUtils.SimpleLogUtils;
using Oreo.FileMan.Services;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace Oreo.FileMan.Commons
{
    public static class R
    {
        internal static string AppName = "Oreo.FileMan";
        internal static DateTime StartTime = DateTime.Now;
        internal static string MachineName = Environment.MachineName;
        internal static Module Module = Assembly.GetExecutingAssembly().GetModules()[0];
        public static string DbConnString = "DefaultConnection";
        internal static string AesKey = "12345678901234567890123456789012";

        internal static Log Log { get; set; }


        public static class Paths
        {
            public static string App = AppDomain.CurrentDomain.BaseDirectory;
            public static string Frisbee = App + "Frisbee\\";
            //public static string Root = App + "\\" + AppName;
            //public static string Data = Root + "\\Data";//应用根目录
        }
        public static class Files
        {
            public static string App = Application.ExecutablePath;
            //public static string Settings = Paths.Root + "\\Settings.ini";//应用配置信息目录
            public static string Frisbee = Paths.App + "\\Frisbee.ini";
        }

        public static class Services
        {
            public static FileBackupService FBS = new FileBackupService();
        }

        public static class Settings
        {
            public static class FileBackup
            {
                public static string FileManBackup = @"D:\temp\FileManBackup\";//文件备份目录
                public static int BACK_UP_INTERVAL = 60 * 1000;//备份文件间隔
                public static int BACK_UP_COUNT = 10;//备份文件版本个数
            }
        }
    }
}
