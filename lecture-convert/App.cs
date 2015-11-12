namespace UnlimitedInf.LectureConvert
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The main application.
    /// </summary>
    public sealed class App
    {
        private ICollection<LectureInfo> _lectures { get; }

        /// <summary>
        /// Ctor. Cannot create the main application without supplying options.
        /// </summary>
        /// <param name="opts"></param>
        public App(Options opts)
        {
            _lectures = opts.Lectures;
            throw new NotImplementedException();
        }

        public void Run() { }
    }
}
