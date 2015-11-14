[assembly: System.CLSCompliant(true)]
namespace UnlimitedInf.LectureConvert
{
    using Mono.Options;
    using System;
    using System.Collections.Generic;
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
                "The resulting filename will be gleaned from the above information, but it does not bother to check if you gave it a sensical name or not.",
                "The file name is formed as follows:",
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
                Console.WriteLine(Utility.String.Format(Messages.ErrorText, e.Message));
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
                // Open the text file
                using (StreamReader r = File.OpenText(file[0]))
                {
                    string line;
                    // While there are still lines to read
                    while (!r.EndOfStream)
                    {
                        line = r.ReadLine();
                        // But skip empty lines
                        if (String.IsNullOrEmpty(line))
                        {
                            continue;
                        }

                        // Split the line by | and make sure there are three parts
                        string[] lineData = line.Split('|');
                        if (lineData.Length != 4)
                        {
                            Console.WriteLine(Utility.String.Format(Messages.ErrorText_InvalidInputLine, line));
                            return;
                        }

                        // Fill a lecture info
                        LectureInfo info = new LectureInfo();
                        info.Url = new Uri(lineData[0]);
                        info.AlbumName = lineData[1];
                        info.Title = lineData[2];
                        info.Track = Utility.String.ParseInt(lineData[3]);
                        opts.Lectures.Add(info);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(Utility.String.Format(Messages.ErrorText, e.Message));
                return;
            }

            App app = new App(opts);
            app.Run();
        }
    }
}
