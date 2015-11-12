﻿namespace UnlimitedInf.LectureConvert
{
    using System;
    using System.Globalization;

    internal static class Utility
    {
        /// <summary>
        /// Writes a string to the console and then returns the cursor to the beginning of the line.
        /// </summary>
        /// <param name="value"></param>
        public static void ConsoleWriteAndReturn(string value)
        {
            Console.Write(value);
            Console.SetCursorPosition(0, Console.CursorTop);
        }

        /// <summary>
        /// A wrapper for <see cref="String.Format(IFormatProvider, string, object[])"/> that 
        /// automatically uses the <see cref="CultureInfo.InvariantCulture"/> to format the string.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string Format(string msg, params object[] args)
        {
            return String.Format(CultureInfo.InvariantCulture, msg, args);
        }
    }
}