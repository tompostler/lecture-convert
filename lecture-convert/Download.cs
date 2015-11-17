namespace UnlimitedInf.LectureConvert
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// Asynchronously download all the MP4 lecture files if they're not already downloaded.
    /// </summary>
    internal class Download
    {
        private List<string> _statuses;
        private List<LectureInfo> _lectures;

        /// <summary>
        /// Figure out which lectures we actually need to downoad.
        /// </summary>
        /// <param name="allLectures"></param>
        public Download(ICollection<LectureInfo> allLectures)
        {
            // Check for the dir
            if (!Utility.Directory.Exists(LectureInfo.DirectoryNameMP4))
            {
                Utility.Console.Log($"Directory not found: {LectureInfo.DirectoryNameMP4}");
                System.IO.Directory.CreateDirectory(LectureInfo.DirectoryNameMP4);
            }

            // Only download necessary lectures
            _lectures = new List<LectureInfo>();
            foreach (LectureInfo lecture in allLectures)
            {
                if (!Utility.File.Exists(lecture.FileNameMP4))
                {
                    _lectures.Add(lecture);
                }
            }
            Utility.Console.Log($"{_lectures.Count} lectures to download");

            // Create the list of messages to update on and the tasks to wait for
            _statuses = new List<string>(_lectures.Count);
            
        }

        public void Run()
        {
            // Start the downloads
            int i = 0;
            Task[] downloads = new Task[_lectures.Count];
            foreach (LectureInfo lecture in _lectures)
            {
                _statuses.Add("");
                downloads[i] = DownloadFileAsync(lecture, i++);
            }

            // Wait until all downloads are done
            Task.WaitAll(downloads);

            // Write all the lines past the progresses
            foreach (string status in _statuses)
            {
                Utility.Console.WriteLine(status);
            }
        }

        /// <summary>
        /// Use a <see cref="WebClient"/> to asynchronously download a file and update the console 
        /// every time another percent completes.
        /// </summary>
        /// <param name="lecture"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task DownloadFileAsync(LectureInfo lecture, int id)
        {
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += (sender, e) => UpdateConsole(e.ProgressPercentage, lecture, id);
                await wc.DownloadFileTaskAsync(lecture.Url, lecture.FileNameMP4);
            }
        }

        /// <summary>
        /// Update the console with the progress completed.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="lecture"></param>
        /// <param name="id"></param>
        private void UpdateConsole(int progressPercentage, LectureInfo lecture, int id)
        {
            _statuses[id] = $"{id + 1}:\t{lecture.FileNameMP4} is {progressPercentage}% complete.";
            Utility.Console.WriteLinesAndReturn(_statuses.ToArray());
        }
    }
}
