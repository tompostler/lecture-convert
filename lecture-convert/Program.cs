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
                "   URL/to/file|AlbumName|TrackTitle",
                "",
                "The resulting filename will be gleaned from the above information, but it does not bother to check if you gave it a sensical name or not. Additionally, track numbering will simply be performed sequentially within the file starting at 1.",
                "The file name is formed as follows:",
                "   AlbumName_TrackTitle.mp[3|4]",
                "",
                "Options:",
                {
                    "help|h|?",
                    "Print this help text and die.",
                    (val) => opts.Die = true
                },
                {
                    "dir=|d=",
                    "The top level directory to use for execution. A input.txt file containing the lecture information should be within this directory instead of specifying INPUTFILE.",
                    (val) => opts.Directory = val
                },
                {
                    "ffmpeg-processes=|fp=",
                    "The maximum number of ffmpeg processes to let run at one time. FFMpeg is generally constrained by the CPU, so this defaults to Environment.ProcessorCount.",
                    (val) => opts.FFMpegProcesses = Int32.Parse(val)
                },
                {
                    "sox-processes=|sp=",
                    "The maximum number of sox processes to let run at one time. Sox is generally constrained by the disk, so this defaults to 2.",
                    (val) => opts.SoxProcesses = Int32.Parse(val)
                },
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

            // See if we're grabbing the input file from dir
            string inputFile = "";
            if (Utility.Directory.Exists(opts.Directory))
            {
                inputFile = opts.Directory + Path.DirectorySeparatorChar + "input.txt";
                LectureInfo.Directory = opts.Directory;
                if (!Utility.File.Exists(inputFile))
                {
                    inputFile = "";
                }
            }

            // Make sure we have our input file
            if (file.Count != 1 && String.IsNullOrEmpty(inputFile))
            {
                Utility.Console.Error("Must have one INPUTFILE.");
                p.WriteOptionDescriptions(Console.Out);
                return;
            }
            if (String.IsNullOrEmpty(inputFile))
            {
                inputFile = file[0];
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
                            Utility.Console.Error($"Invalid line in INPUTFILE: {line}");
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
