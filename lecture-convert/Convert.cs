﻿namespace UnlimitedInf.LectureConvert
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Run the conversions from mp4 to mp3 by as many processors as available at the same time.
    /// </summary>
    internal class Convert
    {
        private string[] _statuses;
        private int _lecturesToDo;
        private List<Process> _processes;
        private SemaphoreSlim _processLimit;

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
            Utility.Console.Log("{0} concurrent ffmpeg procs max", System.Environment.ProcessorCount);

            // Create the list of messages to update on and the processes to wait for
            _statuses = new string[lectures.Count + 1];
            _lecturesToDo = lectures.Count;
            _processLimit = new SemaphoreSlim(System.Environment.ProcessorCount);
            SetUpProcesses(lectures);
        }

        /// <summary>
        /// Run the conversions.
        /// </summary>
        public void Run()
        {
            // Start the conversions
            Task[] conversions = new Task[_processes.Count];
            for (int i = 0; i < _processes.Count; i++)
            {
                int processNum = i;
                conversions[i] = Task.Run(() => RunProcess(processNum));
            }

            // Wait until all conversions are done
            Task.WaitAll(conversions);

            // Write all the lines past the statuses
            foreach (string status in _statuses)
            {
                Utility.Console.WriteLine(status);
            }
        }

        private void RunProcess(int processNum)
        {
            Process process = _processes[processNum];
            // Start the process and set up the output
            process.ErrorDataReceived += (sender, e) => UpdateConsole(e.Data, processNum);
            _processLimit.Wait();
            process.Start();
            process.BeginErrorReadLine();
            process.WaitForExit();
            _processLimit.Release();
            process.Dispose();
            _lecturesToDo--;
        }

        /// <summary>
        /// Update the console with the current line of output.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="i"></param>
        private void UpdateConsole(string data, int i)
        {
            // If the line is probably going to be too long for the terminal, then don't print it
            if (data.StartsWith("size"))
            {
                _statuses[0] = Utility.String.Format(Messages.FFMpegOverallStatus, _lecturesToDo);
                _statuses[i + 1] = data;
                Utility.Console.WriteLinesAndReturn(_statuses);
            }
        }

        /// <summary>
        /// Set up the processes with all necessary information and place them in a list.
        /// </summary>
        /// <param name="lectures"></param>
        private void SetUpProcesses(List<LectureInfo> lectures)
        {
            _processes = new List<Process>(lectures.Count);

            // Set up the processes
            foreach (LectureInfo lecture in lectures)
            {
                // Set up the starting process
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.Arguments = Utility.String.Format(Messages.FFMpegCommand, lecture.FileNameMP4, lecture.FileNameMP3);
                processInfo.CreateNoWindow = false;
                processInfo.FileName = "ffmpeg.exe";
                processInfo.RedirectStandardError = true;
                processInfo.UseShellExecute = false;

                // Add the process into the list
                Process process = new Process();
                process.StartInfo = processInfo;
                _processes.Add(process);
            }
        }
    }
}
