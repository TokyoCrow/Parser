using InternTask1.Properties;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace InternTask1
{
    public static class Configuration
    {
        private static readonly Regex EmailRegex = new Regex(@"(@)(.+)$");
        public static byte NestingDegree
        {
            get
            {
                return Settings.Default.NestingDegree;
            }
            set
            {
                if (value > 0)
                {
                    Settings.Default.NestingDegree = value;
                    Settings.Default.Save();
                }
                else
                    Console.WriteLine("The degree must be greater then 0.");
            }
        }
        public static string CsvFileName
        {
            get
            {
                return Settings.Default.CsvFileName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    Settings.Default.CsvFileName = value; 
                    Settings.Default.Save();
                }
                else
                    Console.WriteLine("Name cannot be empty.");
            }
        }

        public static string CsvFilePath
        {
            get
            {
                return Settings.Default.CsvFilePath;
            }
            set
            {
                if (Directory.Exists(value))
                {
                    Settings.Default.CsvFilePath = value.EndsWith(@"\") ? value : $"{value}\\";
                    Settings.Default.Save();
                }
                else
                    Console.WriteLine("That directory not exist.");
            }
        }
        public static string ExpEmail
        {
            get
            {
                return Settings.Default.ExpEmail;
            }
            set
            {
                if (EmailRegex.IsMatch(value))
                {
                    Settings.Default.ExpEmail = value; 
                    Settings.Default.Save();
                }
                else
                    Console.WriteLine("Enter correct Email.");
            }
        }

        public static string GetInfo() =>
                    $"Nessing degree: { Settings.Default.NestingDegree }"
                  + $"\nReport file name: { Settings.Default.CsvFileName }"
                  + $"\nReport file path: { Settings.Default.CsvFilePath }"
                  + $"\nEmail for exceptions: { Settings.Default.ExpEmail }";

    }
}
