namespace UnlimitedInf.LectureConvert.Utility
{
    internal static class Console
    {
        private static object _consoleLock = new object();

        /// <summary>
        /// Writes a string to the console and then returns the cursor to the beginning of the line.
        /// </summary>
        /// <param name="value"></param>
        public static void WriteAndReturn(string value)
        {
            lock (_consoleLock)
            {
                System.Console.Write(value);
                System.Console.SetCursorPosition(0, System.Console.CursorTop);
            }
        }

        /// <summary>
        /// Writes an array of strings to the console (one per line) and then returns the cursor 
        /// to the place it was before calling this function.
        /// </summary>
        /// <param name="lines"></param>
        public static void WriteLinesAndReturn(string[] lines)
        {
            lock (_consoleLock)
            {
                int top = System.Console.CursorTop;
                int left = System.Console.CursorLeft;
                foreach (string line in lines)
                {
                    System.Console.WriteLine(line);
                }
                System.Console.SetCursorPosition(left, top);
            }
        }

        /// <summary>
        /// Passes the <paramref name="obj"/> to <see cref="System.Console.WriteLine(object)"/>
        /// </summary>
        /// <param name="obj"></param>
        public static void WriteLine(object obj)
        {
            lock (_consoleLock)
            {
                System.Console.WriteLine(obj);
            }
        }
    }
}