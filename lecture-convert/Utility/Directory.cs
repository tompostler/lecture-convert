namespace UnlimitedInf.LectureConvert.Utility
{
    using System;
    using System.IO;

    internal static class Directory
    {
        /// <summary>
        /// Check if a directory exists given a path.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static bool Exists(string fullPath)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(fullPath);
                return di.Exists;
            }
            catch (Exception ex) when (ex is System.Security.SecurityException
                                        || ex is ArgumentException || ex is UnauthorizedAccessException
                                        || ex is PathTooLongException || ex is NotSupportedException)
            {
                return false;
            }
        }
    }
}
