namespace UnlimitedInf.LectureConvert
{
    using System;
    using System.Collections.Generic;

    public sealed class Options
    {
        public ICollection<LectureInfo> Lectures { get; }

        /// <summary>
        /// The top level directory to use for execution. A *.txt file containing the lecture 
        /// information should be within this directory.
        /// </summary>
        public string Directory { get; set; }

        public bool Die { get; set; }

        /// <summary>
        /// Maximum number of ffmpeg processes to run at one time. FFMpeg is generally constrained 
        /// by CPU capability, so it defaults to Environment.ProcessorCount.
        /// </summary>
        public int FFMpegProcesses { get; set; }

        /// <summary>
        /// Maximum number of sox processes to run at one time. Sox is generally constrained by disk 
        /// capabilities, so it defaults to 2.
        /// </summary>
        public int SoxProcesses { get; set; }

        public static int FFMpegProcessesDefault => Environment.ProcessorCount;
        public static int SoxProcessesDefault => 2;

        public Options()
        {
            Lectures = new List<LectureInfo>();
        }
    }
}
