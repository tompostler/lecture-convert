namespace UnlimitedInf.LectureConvert
{
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

        public Options()
        {
            Lectures = new List<LectureInfo>();
        }
    }
}
