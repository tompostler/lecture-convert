namespace UnlimitedInf.LectureConvert
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Run the conversions from mp4 to mp3 by as many processors as available at the same time.
    /// </summary>
    internal class Convert
    {
        private string[] _statuses;
        private Process[] _processes;

        public Convert(ICollection<LectureInfo> allLectures)
        {
            // Check for the dir
            if (!Utility.Directory.Exists(LectureInfo.DirectoryNameMP3))
            {
                Utility.Console.Log(Messages.DirectoryNotFound, LectureInfo.DirectoryNameMP3);
                System.IO.Directory.CreateDirectory(LectureInfo.DirectoryNameMP3);
            }

            // Only convert the necessary lectures
            List<LectureInfo> lectures = new List<LectureInfo>();
            foreach (LectureInfo lecture in allLectures)
            {
                if (!Utility.File.Exists(lecture.FileNameMP3))
                {
                    lectures.Add(lecture);
                }
            }
            Utility.Console.Log("{0} lectures to convert", lectures.Count);

            // Create the list of messages to update on and the processes to wait for
            _statuses = new string[Environment.ProcessorCount];
            SetUpProcesses(lectures);
        }

        public void Run()
        {
            throw new NotImplementedException();
        }

        private void SetUpProcesses(List<LectureInfo> lectures)
        {
            _processes = new Process[Environment.ProcessorCount];

            // Set up the processes
            int i = 0;
            int processorAffinity = 1;
            foreach (LectureInfo lecture in lectures)
            {
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.Arguments = Utility.String.Format(Messages.FFMpegCommand, lecture.FileNameMP4, lecture.FileNameMP3);
                processInfo.CreateNoWindow = false;
                processInfo.FileName = "ffmpeg.exe";
                processInfo.RedirectStandardError = true;
                processInfo.UseShellExecute = false;

                _processes[i].StartInfo = processInfo;
                _processes[i].ProcessorAffinity = (IntPtr)processorAffinity++;

                i++;
                if (processorAffinity == Environment.ProcessorCount)
                {
                    processorAffinity = 1;
                }
            }
        }
    }
}
