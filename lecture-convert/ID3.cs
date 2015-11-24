namespace UnlimitedInf.LectureConvert
{
    using System.Collections.Generic;

    /// <summary>
    /// Update the ID3 tags for all the MP3 files.
    /// </summary>
    internal sealed class ID3
    {
        private List<LectureInfo> _lectures;

        /// <summary>
        /// Copy the lectures into this object.
        /// </summary>
        /// <param name="allLectures"></param>
        public ID3(ICollection<LectureInfo> allLectures)
        {
            _lectures = new List<LectureInfo>(allLectures.Count);
            int lectureTrack = 1;
            foreach (LectureInfo lecture in allLectures)
            {
                lecture.Track = lectureTrack++;
                _lectures.Add(lecture);
            }
            Utility.Console.Log($"{_lectures.Count * 2} to update tags for.");
        }

        /// <summary>
        /// Run the updates.
        /// </summary>
        public void Run()
        {
            int completed = 0;
            int total = _lectures.Count * 2;

            foreach (LectureInfo lecture in _lectures)
            {
                // Update the ID3 on the MP3
                UpdateTags(lecture.FileNameMP3, lecture.Title, lecture.AlbumName, lecture.Track);
                completed++;
                Utility.Console.WriteAndReturn($"{completed}/{total} lecture ID3 tags updated. . .");

                // Update the ID3 on the MP3 (cleaned)
                UpdateTags(lecture.FileNameMP3Cleaned, lecture.Title, lecture.AlbumName, lecture.Track);
                completed++;
                Utility.Console.WriteAndReturn($"{completed}/{total} lecture ID3 tags updated. . .");
            }
        }

        /// <summary>
        /// Updates the tags.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="title"></param>
        /// <param name="album"></param>
        /// <param name="track"></param>
        private static void UpdateTags(string filePath, string title, string album, int track)
        {
            TagLib.File file = TagLib.File.Create(filePath);
            file.Tag.Clear();
            file.Tag.Album = album;
            file.Tag.Title = title;
            file.Tag.Track = (uint)track;
            file.Tag.Year = (uint)System.DateTime.UtcNow.Year;
            file.Save();
        }
    }
}
