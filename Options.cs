namespace UnlimitedInf.LectureConvert
{
    using System.Collections.Generic;

    public sealed class Options
    {
        public ICollection<LectureInfo> Lectures { get; }

        public Options()
        {
            Lectures = new List<LectureInfo>();
        }
    }
}
