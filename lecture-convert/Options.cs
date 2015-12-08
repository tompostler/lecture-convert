namespace UnlimitedInf.LectureConvert
{
    using System.Collections.Generic;

    public sealed class Options
    {
        public ICollection<LectureInfo> Lectures { get; }

        public bool Die { get; set; }

        public Options()
        {
            Lectures = new List<LectureInfo>();
        }
    }
}
