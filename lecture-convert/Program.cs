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
                "Usage: lectureconvert [OPTIONS] INPUTDIR",
                "Download recorded .mp4 lecture files and convert them into sped up and normalized .mp3s with proper id3 tags for easier on-the-go consumption.",
                "",
                "Each line in the input.txt text file for the downloaded recordings should be set up as follows for each line:",
                "   URL/to/file|TrackTitle",
                "",
                "The resulting filename will be gleaned from the above information, and the filename gleaned from TrackTitle will be normalized so there are no offending characters or spaces for the current filesystem. Additionally, track numbering will simply be performed sequentially within the file starting at 1.",
                "The file name is formed as follows:",
                "   AlbumName_TrackTitle.mp[3|4]",
                "",
                "Options:",
                {
                    "help|h|?",
                    "Print this help text and die.",
                    (val) => opts.Die = true
                },
                "",
                "INPUTDIR:",
                "\tThe input file to parse for URLs, file naming, and id3 tag information.",
                "",
                "Version: 1.0.0"
            };

            // Parse the args
            List<string> dirs;
            try
            {
                dirs = p.Parse(args);
            }
            catch (OptionException e)
            {
                Utility.Console.Error(e.Message);
                p.WriteOptionDescriptions(Console.Out);
                return;
            }

            // Check if we're asking for help
            if (opts.Die)
            {
                p.WriteOptionDescriptions(Console.Out);
                return;
            }

            // Make sure we have a dir
            if (dirs.Count != 1)
            {
                Utility.Console.Error("Must have one INPUTDIR");
                p.WriteOptionDescriptions(Console.Out);
                return;
            }

            // Grab the input file from dir
            string inputFile = "";
            if (Utility.Directory.Exists(dirs[0]))
            {
                inputFile = dirs[0] + Path.DirectorySeparatorChar + "input.txt";
                LectureInfo.Directory = dirs[0];
                if (!Utility.File.Exists(inputFile))
                {
                    inputFile = "";
                }
            }

            // Make sure we have our input file
            if (String.IsNullOrEmpty(inputFile))
            {
                Utility.Console.Error($"Must have an input.txt file in {dirs[0]}");
                p.WriteOptionDescriptions(Console.Out);
                return;
            }

            // Parse the input file
            try
            {
                // Open the text file
                using (StreamReader r = File.OpenText(inputFile))
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
                        if (lineData.Length != 3)
                        {
                            Utility.Console.Error($"Invalid line in input.txt: {line}");
                            p.WriteOptionDescriptions(Console.Out);
                            return;
                        }

                        // Fill a lecture info
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
                Utility.Console.Error(e.Message);
                p.WriteOptionDescriptions(Console.Out);
                return;
            }

            App app = new App(opts);
            app.Run();
        }
    }
}
