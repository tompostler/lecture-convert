﻿namespace UnlimitedInf.LectureConvert
{
    using System;
    using System.IO;

    /// <summary>
    /// The lecture info gathered from the file.
    /// </summary>
    public sealed class LectureInfo
    {
        public Uri Url { get; set; }
        public string AlbumName { get; set; }
        public string Title { get; set; }
        public int Track { get; set; }

        /// <summary>
        /// Strip everything that's not [a-zA-Z0-9_ -] and replace spaces with -
        /// </summary>
        private string _fileNameTitle
        {
            get
            {
                char[] arr = Title.ToCharArray();
                arr = Array.FindAll<char>(arr, (c => char.IsLetterOrDigit(c)
                                                    || char.IsWhiteSpace(c)
                                                    || c == '-'
                                                    || c == '_'));
                string result = new string(arr);
                return result.Replace(' ', '-');
            }
        }

        private string _fileNameMP3         => AlbumName + '_' + _fileNameTitle + ".mp3";
        private string _fileNameMP4         => AlbumName + '_' + _fileNameTitle + ".mp4";
        public string FileNameMP3           => DirectoryNameMP3 + Path.DirectorySeparatorChar + _fileNameMP3;
        public string FileNameMP3Cleaned    => DirectoryNameMP3Cleaned + Path.DirectorySeparatorChar + _fileNameMP3;
        public string FileNameMP4           => DirectoryNameMP4 + Path.DirectorySeparatorChar + _fileNameMP4;

        private static string _directory = "";
        /// <summary>
        /// Assumes that you provided a valid directory since the program should have died before actually using this.
        /// </summary>
        public static string Directory
        {
            get
            {
                return _directory;
            }
            set
            {
                _directory = value + Path.DirectorySeparatorChar;
            }
        }
        public static string DirectoryNameMP4           => Directory + "mp4s";
        public static string DirectoryNameMP3           => Directory + "mp3s";
        public static string DirectoryNameMP3Cleaned    => Directory + "cleaned";
    }
}
