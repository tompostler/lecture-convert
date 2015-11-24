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
            if (allLectures.Count > 0 && !Utility.Directory.Exists(LectureInfo.DirectoryNameMP3Cleaned))
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
            Utility.Console.Log($"{lectures.Count} lectures to process");
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
        private void UpdateConsole(string data, int i)
        {
            // Null string indicates end of stream. Kindly let the user know
            if (String.IsNullOrEmpty(data))
            {
                _statuses[i] += "done.";
                Utility.Console.WriteLinesAndReturn(_statuses);
            }
            // If the line does not begin with 'In:', then don't print it
            else if (data.StartsWith("In:"))
            {
                _statuses[i] = $"{i + 1}:\t{data}";
                Utility.Console.WriteLinesAndReturn(_statuses);
            }
        }

        /// <summary>
        /// Set up the processes with all necessary information and place them in a list.
        /// </summary>
        /// <param name="lectures"></param>
        /// An explanation of the sox options are in order... With some descriptions copied from 
        /// the sox docs.
        /// --show-progress         Give us the progress output to show the user.
        /// --compression 64        Output mp3 file at 64kbps.
        /// compand 0.26,1.0        The attack and decay parameters (in seconds) determine the time 
        ///                         over which the instantaneous level of the input signal is 
        ///                         averaged to determine its volume; attacks refer to increases in 
        ///                         volume and decays refer to decreases. For most situations, the 
        ///                         attack time should be shorter than the decay time because the 
        ///                         human ear is more sensitive to sudden loud audio than sudden 
        ///                         soft audio.
        ///     6:-70,-60,-20       A list of points on the compander’s transfer function specified 
        ///                         in dB relative to the maximum possible signal amplitude.
        ///                         This says that very soft sounds (below −70dB) will remain 
        ///                         unchanged. This will stop the compander from boosting the 
        ///                         volume on ‘silent’ passages such as between sentences. However, 
        ///                         sounds in the range −60dB to 0dB (maximum volume) will be 
        ///                         boosted so that the 60dB dynamic range of the original audio 
        ///                         will be compressed 3-to-1 into a 20dB range, which is wide 
        ///                         enough to enjoy the audio but narrow enough to get around the 
        ///                         road noise. The ‘6:’ selects 6dB soft-knee companding.
        ///     -5                  The −5 (dB) output gain is needed to avoid clipping (the number 
        ///                         is inexact, and was derived by experimentation).
        ///     -90                 The −90 (dB) for the initial volume will work fine for a clip 
        ///                         that starts with near silence.
        ///     0.2                 The delay of 0.2 (seconds) has the effect of causing the 
        ///                         compander to react a bit more quickly to sudden volume changes.
        /// reverse                 Reverse the audio to better work with the first call of silence.
        /// silence 1 1t -50d       Trim all silence from the 'beginning' (actually end due to 
        ///                         reverse) of the audio until the audio is louder than -50dB for 
        ///                         more than 1s.
        /// reverse                 Flip the audio right way round.
        /// silence -l 1 5 -50d     Trim any silence remaining from the beginning of the audio.
        ///     -1 10t -50d         And then trim any other silences from within the audio (-l) 
        ///                         lasting more than 10s (10t)
        /// tempo -s 1.4            Increase speed by 40% without affecting pitch. '-s' is a 
        ///                         shortcut to use speech audio presets.
        private void SetUpProcesses(List<LectureInfo> lectures)
        {
            _preparations = new List<Process>(lectures.Count);

            // Set up the processes
            foreach (LectureInfo lecture in lectures)
            {
                // Set up the starting process
                ProcessStartInfo processInfo = new ProcessStartInfo();
                processInfo.Arguments = $"--show-progress {lecture.FileNameMP3} --compression 64 {lecture.FileNameMP3Cleaned} compand 0.26,1.0 6:-70,-60,-20 -5 -90 0.2 reverse silence 1 1t -50d reverse silence -l 1 5 -50d -1 10t -50d tempo -s 1.4";
                processInfo.CreateNoWindow = false;
                processInfo.FileName = "sox-14.4.2" + System.IO.Path.DirectorySeparatorChar + "sox.exe";
                processInfo.RedirectStandardOutput = true;
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
