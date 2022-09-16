using System.IO;
using UnityEngine;

namespace Assets.EventServiceTest.Scripts.Files
{
    public static class FileSerializer
    {
        public const string ANALYTICS_EVENTS_FILE_NAME = "analyticsCache.txt";

        public static string Load(string fileName)
        {
            if (File.Exists(GetFilePath(fileName)) == false)
            {
                return string.Empty;
            }

            return File.ReadAllText(GetFilePath(fileName));
        }

        public static void Save(string fileName, string text)
        {
            File.WriteAllText(GetFilePath(fileName), text);
        }

        private static string GetFilePath(string fileName)
        {
            return Application.persistentDataPath + Path.DirectorySeparatorChar + fileName;
        }
    }
}