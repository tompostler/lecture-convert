namespace UnlimitedInf.LectureConvert
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Run the conversions from mp3 to mp3 by as many processors as available at the same time.
    /// </summary>
    internal class MP3toMP3 : IDisposable
    {
        private string[] _statuses;
        private List<Process> _preparations;
        private SemaphoreSlim _processLimit;

        /// <summary>
        /// Figure out which lectures we actually need to process.
        /// </summary>
        /// <param name="allLectures"></param>
        public MP3toMP3(ICollection<LectureInfo> allLectures)
        {
            // Check for the dir
            if (!Utility.Directory.Exists(LectureInfo.DirectoryNameMP3Cleaned))
            {
                Utility.Console.Log($"Directory not found: {LectureInfo.DirectoryNameMP3Cleaned}");
                System.IO.Directory.CreateDirectory(LectureInfo.DirectoryNameMP3Cleaned);
                Utility.Console.Log($"Created directory: {LectureInfo.DirectoryNameMP3Cleaned}");
            }

            // Only process the necessary lectures
            List<LectureInfo> lectures = new List<LectureInfo>();
            foreach (LectureInfo lecture in allLectures)
            {
                if (!Utility.File.Exists(lecture.FileNameMP3Cleaned))
                {
                    lectures.Add(lecture);
                }
            }
            Utility.Console.Log($"{lectures.Count} lectures to prepare for consumption");
            if (lectures.Count > 0)
            {
                Utility.Console.Log($"{Environment.ProcessorCount} concurrent sox procs max");
            }

            // Create the list of messages to update on and the processes to wait for
            _statuses = new string[lectures.Count];
            _processLimit = new SemaphoreSlim(Environment.ProcessorCount);
            SetUpProcesses(lectures);
        }

        /// <summary>
        /// Run the processings.
        /// </summary>
        public void Run()
        {
            // Start the processings
            Task[] conversions = new Task[_preparations.Count];
            for (int i = 0; i < _preparations.Count; i++)
            {
                int processNum = i;
                conversions[i] = Task.Run(() => RunProcess(processNum));
            }

            // Wait until all processings are done
            Task.WaitAll(conversions);
            _processLimit.Dispose();

            // Write all the lines past the statuses
            foreach (string status in _statuses)
            {
                Utility.Console.WriteLine(status);
            }
        }

        private void RunProcess(int processNum)
        {
            Process process = _preparations[processNum];
            // Start the process and set up the output
            process.ErrorDataReceived += (sender, e) => UpdateConsole(e.Data, processNum);
            _processLimit.Wait();
            process.Start();
            process.BeginErrorReadLine();
            process.WaitForExit();
            _processLimit.Release();
            process.Dispose();
        }

        /// <summary>
        /// Update the console with the current line of output.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="i"></param>
        /// TODO: Refactor this for sox output
        private void UpdateConsole(string data, int i)
        {
            // Null string indicates end of stream. Kindly let the user know
            if (String.IsNullOrEmpty(data))
            {
                _statuses[i] += "done.";
                Utility.Console.WriteLinesAndReturn(_statuses);
            }
            // If the line does not begin with 'size', then don't print it
            else if (data.StartsWith("size"))
            {
                _statuses[i] = $"{i + 1}:\t{data}";
                Utility.Console.WriteLinesAndReturn(_statuses);
            }
        }

        /// <summary>
        /// Set up the processes with all necessary information and place them in a list.
        /// </summary>
        /// <param name="lectures"></param>
        /// TODO: Refactor this for sox options
        private void SetUpProcesses(List<LectureInfo> lectures)
        {
            _preparations = new List<Process>(lectures.Count);

            // Set up the processes
            foreach (LectureInfo lecture in lectures)
            {
                // Set up the starting process
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.Arguments = $"{lecture.FileNameMP3} {lecture.FileNameMP3Cleaned} options";
                processInfo.CreateNoWindow = false;
                processInfo.FileName = "sox.exe";
                processInfo.RedirectStandardError = true;
                processInfo.UseShellExecute = false;

                // Add the process into the list
                Process process = new Process();
                process.StartInfo = processInfo;
                _preparations.Add(process);
            }
        }

        public void Dispose()
        {
            _processLimit.Dispose();
        }
    }
}
