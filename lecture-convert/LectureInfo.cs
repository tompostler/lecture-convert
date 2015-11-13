namespace UnlimitedInf.LectureConvert
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

        private string _fileNameMP3         => AlbumName + '_' + Title + ".mp3";
        private string _fileNameMP4         => AlbumName + '_' + Title + ".mp4";
        public string FileNameMP3           => "mp3s" + Path.DirectorySeparatorChar + _fileNameMP3;
        public string FileNameMP3Cleaned    => "cleaned" + Path.DirectorySeparatorChar + _fileNameMP3;
        public string FileNameMP4           => "mp4s" + Path.DirectorySeparatorChar + _fileNameMP4;
    }
}
