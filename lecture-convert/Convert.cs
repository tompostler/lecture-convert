namespace UnlimitedInf.LectureConvert
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;

    /// <summary>
    /// Run the conversions from mp4 to mp3 by as many processors as available at the same time.
    /// </summary>
    internal class Convert
    {
        private string[] _statuses;
        private int _lecturesToDo;
        private Dictionary<int, List<Process>> _processes;

        /// <summary>
        /// Figure out which lectures we actually need to convert.
        /// </summary>
        /// <param name="allLectures"></param>
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
            _statuses = new string[Environment.ProcessorCount + 1];
            SetUpProcesses(lectures);
        }

        /// <summary>
        /// Run the conversions.
        /// </summary>
        public void Run()
        {
            // Start the conversions
            Task[] conversions = new Task[Environment.ProcessorCount];
            for (int i = 0; i < _processes.Count; i++)
            {
                conversions[i] = Task.Run(() => RunListOfProcesses(i));
            }

            // Wait until all conversions are done
            Task.WaitAll(conversions);

            // Write all the lines past the statuses
            foreach (string status in _statuses)
            {
                Utility.Console.WriteLine(status);
            }
        }

        private void RunListOfProcesses(int processorAffinity)
        {
            List<Process> processes = _processes[processorAffinity];
            for (int i = 0; i < processes.Count; i++)
            {
                // Start the process and set up the output
                processes[i].ErrorDataReceived += (sender, e) => UpdateConsole(e.Data, processorAffinity, i);
                processes[i].Start();
                processes[i].ProcessorAffinity = (IntPtr)(1 << processorAffinity);
                processes[i].WaitForExit();
                processes[i].Dispose();
                _lecturesToDo--;
            }
        }

        /// <summary>
        /// Update the console with the current line of output.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="processorAffinity"></param>
        /// <param name="i"></param>
        private void UpdateConsole(string data, int processorAffinity, int i)
        {
            _statuses[0] = Utility.String.Format(Messages.FFMpegOverallStatus, _lecturesToDo);
            _statuses[processorAffinity + 1] = Utility.String.Format(Messages.FFMpegStatus, processorAffinity, i, data);
            Utility.Console.WriteLinesAndReturn(_statuses);
        }

        /// <summary>
        /// Set up the processes with all necessary information and place them in a list sorted by 
        /// the processor affinity to make running them easier.
        /// </summary>
        /// <param name="lectures"></param>
        private void SetUpProcesses(List<LectureInfo> lectures)
        {
            _processes = new Dictionary<int, List<Process>>(Environment.ProcessorCount);

            // Set up the processes
            for (int processorAffinity = 0; processorAffinity < lectures.Count; processorAffinity++)
            {
                // Create list if necessary
                if (processorAffinity < Environment.ProcessorCount)
                {
                    _processes[processorAffinity] = new List<Process>();
                }

                // Set up the starting process
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.Arguments = Utility.String.Format(Messages.FFMpegCommand, lectures[processorAffinity].FileNameMP4, lectures[processorAffinity].FileNameMP3);
                processInfo.CreateNoWindow = false;
                processInfo.FileName = "ffmpeg.exe";
                processInfo.RedirectStandardError = true;
                processInfo.UseShellExecute = false;

                // Add it into the appropriate execution bucket
                Process p = new Process();
                p.StartInfo = processInfo;
                _processes[processorAffinity % Environment.ProcessorCount].Add(p);
            }
        }
    }
}
