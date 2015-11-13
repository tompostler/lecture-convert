namespace UnlimitedInf.LectureConvert
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The main application.
    /// </summary>
    /// This program makes a couple of assumptions since it is to be used internally by one person 
    /// that's not trying to do anything stupid while the program is running:
    ///     - No file safety checks. Assumes a rigid file structure that won't be changing.
    public sealed class App
    {
        private ICollection<LectureInfo> _lectures { get; }

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

            _lectures = opts.Lectures;
            throw new NotImplementedException();
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
