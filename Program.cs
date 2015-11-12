﻿[assembly: System.CLSCompliant(true)]
namespace UnlimitedInf.LectureConvert
{
    using Mono.Options;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;

    class Program
    {
        /// <summary>
        /// Parse the options and fail or start the program accordingly.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Options opts = new Options();

            OptionSet p = new OptionSet
            {
                "",
                "Usage: lectureconvert [OPTIONS] INPUTFILE",
                "Download recorded .mp4 lecture files and convert them into sped up and normalized .mp3s with proper id3 tags for easier on-the-go consumption.",
                "",
                "Each line in the input text file for the downloaded recordings should be set up as follows for each line:",
                "   URL/to/file|AlbumName|TrackTitle|TrackNumber",
                "",
                "The resulting filename will be gleaned from the above information:",
                "   AlbumName_TrackTitle.mp[3|4]",
                //"",
                //"Options:",
                //{
                //    ""
                //}
                "",
                "INPUTFILE:",
                "\tThe input file to parse for URLs, file naming, and id3 tag information."
            };

            // Parse the args
            List<string> file;
            try
            {
                file = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.WriteLine(Format(Messages.ErrorText, e.Message));
                return;
            }

            // Make sure we have our input file
            if (file.Count != 1)
            {
                Console.WriteLine(Messages.ErrorText_MustHaveInputFile);
                return;
            }

            // Parse the input file
            try
            {
                using (StreamReader r = File.OpenText(file[0]))
                {
                    string line;
                    while (!r.EndOfStream)
                    {
                        line = r.ReadLine();
                        if (String.IsNullOrEmpty(line))
                        {
                            continue;
                        }

                        string[] lineData = line.Split('|');
                        if (lineData.Length != 3)
                        {
                            Console.WriteLine(Format(Messages.ErrorText_InvalidInputLine, line));
                            return;
                        }

                        LectureInfo info = new LectureInfo();
                        info.Url = new Uri(lineData[0]);
                        info.AlbumName = lineData[1];
                        info.Title = lineData[2];
                        opts.Lectures.Add(info);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(Format(Messages.ErrorText, e.Message));
                return;
            }

            App app = new App();
            app.Run();
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
