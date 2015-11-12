namespace UnlimitedInf.LectureConvert
{
    using System;

    /// <summary>
    /// The lecture info gathered from the file.
    /// </summary>
    public sealed class LectureInfo
    {
        public Uri Url { get; set; }
        public string AlbumName { get; set; }
        public string Title { get; set; }
        public int Track { get; set; }

        public string FileName      => AlbumName + '_' + Title;
        public string FileNameMP3   => FileName + ".mp3";
        public string FileNameMP4   => FileName + ".mp4";
    }
}
