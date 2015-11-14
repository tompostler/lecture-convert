namespace UnlimitedInf.LectureConvert
{
    using System.Collections.Generic;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>
    /// Asynchronously download all the MP4 lecture files if they're not already downloaded.
    /// </summary>
    internal static class Download
    {
        private static int _filesDownloaded = 0;

        public static void Run(ICollection<LectureInfo> lectures)
        {
            // Check for the dir
            if (!Utility.Directory.Exists(LectureInfo.DirectoryNameMP4))
            {
                Utility.Console.Log("Directory {0} does not exist. Creating.", LectureInfo.DirectoryNameMP4);
                System.IO.Directory.CreateDirectory(LectureInfo.DirectoryNameMP4);
            }
            // Only download necessary lectures
            foreach (LectureInfo lecture in lectures)
            {
                if (Utility.File.Exists(lecture.FileNameMP4))
                {
                    lectures.Remove(lecture);
                }
            }
            Utility.Console.Log("{0} lectures to download", lectures.Count);

            // Create the list of messages to update on and the tasks to wait for
            List<string> statuses = new List<string>(lectures.Count);
            Task[] downloads = new Task[lectures.Count];

            // Start the downloads
            int i = 0;
            foreach (LectureInfo lecture in lectures)
            {
                statuses.Add("");
                downloads[i] = DownloadFileAsync(lecture, statuses, i++);
            }

            // Wait until all downloads are done
            Task.WaitAll(downloads);
        }

        /// <summary>
        /// Use a <see cref="WebClient"/> to asynchronously download a file and update the console 
        /// every time another percent completes.
        /// </summary>
        /// <param name="lecture"></param>
        /// <param name="statuses"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static async Task DownloadFileAsync(LectureInfo lecture, List<string> statuses, int id)
        {
            using (WebClient wc = new WebClient())
            {
                wc.DownloadProgressChanged += (sender, e) => UpdateConsole(e.ProgressPercentage, lecture, statuses, id);
                wc.DownloadFileCompleted += (sender, e) => _filesDownloaded++;
                await wc.DownloadFileTaskAsync(lecture.Url, lecture.FileNameMP4);
            }
        }

        /// <summary>
        /// Update the console with the progress completed.
        /// </summary>
        /// <param name="e"></param>
        /// <param name="lecture"></param>
        /// <param name="statuses"></param>
        /// <param name="id"></param>
        private static void UpdateConsole(int progressPercentage, LectureInfo lecture, List<string> statuses, int id)
        {
            statuses[id] = Utility.String.Format("{0} is {1}% complete.", lecture.FileNameMP4, progressPercentage);
            Utility.Console.WriteLinesAndReturn(statuses.ToArray());
        }
    }
}
