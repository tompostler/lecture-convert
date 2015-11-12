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
        public string FileNameMp3   => FileName + ".mp3";
        public string FileNameMp4   => FileName + ".mp4";
    }
}
