namespace UnlimitedInf.LectureConvert
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// The main application.
    /// </summary>
    /// This program makes a couple of assumptions since it is to be used internally by one person 
    /// that's not trying to do anything stupid while the program is running:
    ///     - No file safety checks. Assumes a rigid file structure that won't be changing.
    ///     - Uses some default values based on a reasonable network with a recent i5 or better and
    ///       an SSD.
    public sealed class App
    {
        private Options _opts { get; }

        /// <summary>
        /// Ctor. Cannot create the main application without supplying options.
        /// </summary>
        /// <param name="opts"></param>
        public App(Options opts)
        {
            if (opts == null)
            {
                throw new ArgumentNullException(nameof(opts));
            }

            _opts = opts;
        }

        /// <summary>
        /// Runs the entire program.
        /// </summary>
        /// This method will do the following four things in order:
        /// 1)  Download the mp4s           (if not already existing)
        /// 2)  Convert the mp4s to mp3s    (if not already existing)
        /// 3)  Process the mp3s            (if not already existing)
        /// 4)  Update id3 tags on all mp3s (just update all of them)
        public void Run()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            Download download = new Download(_opts.Lectures);
            download.Run();

            using (MP4toMP3 convert = new MP4toMP3(_opts.Lectures, _opts.FFMpegProcesses))
            {
                convert.Run();
            }

            using (MP3toMP3 process = new MP3toMP3(_opts.Lectures, _opts.SoxProcesses))
            {
                process.Run();
            }

            ID3 tagging = new ID3(_opts.Lectures);
            tagging.Run();

            stopwatch.Stop();

            Utility.Console.Log($"Completed operations in {stopwatch.Elapsed}");
        }
    }
}
