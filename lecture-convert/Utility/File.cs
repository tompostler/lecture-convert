﻿namespace UnlimitedInf.LectureConvert.Utility
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
            catch (Exception ex) when (ex is System.Security.SecurityException
                                        || ex is ArgumentException || ex is UnauthorizedAccessException
                                        || ex is PathTooLongException || ex is NotSupportedException)
            {
                return false;
            }
        }
    }
}
