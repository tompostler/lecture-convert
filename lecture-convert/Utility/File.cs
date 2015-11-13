namespace UnlimitedInf.LectureConvert.Utility
{
    using System;
    using System.IO;

    internal static class File
    {
        /// <summary>
        /// Check if a file exists given a path.
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static bool Exists(string fullPath)
        {
            try
            {
                FileInfo fi = new FileInfo(fullPath);
                return fi.Exists;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
