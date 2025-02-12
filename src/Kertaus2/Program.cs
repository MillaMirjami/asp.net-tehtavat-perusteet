using Serilog;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace Problem
{
    internal class Program
    {   
        private const int maxNumber = 100;
        private const string path = @"/Users/s2401576/Documents/GitHub/bc_net24s_ryhma/src/part1_kertausta/Kertaus2/file.txt";
        private const string strExeFilePath = @"/Users/s2401576/Documents/GitHub/bc_net24s_ryhma/src/part1_kertausta/Kertaus2/bin/Debug/net8.0/Kertaus2";
        private static void Main(string [] args)
        {

            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var logger = new LoggerConfiguration()
            .Enrich.WithProperty("UserName", "Milla Sukki")
            .ReadFrom.Configuration(configuration)
            .CreateLogger();

            logger.Information("Program executed by {UserName}");


            if(args.Length == 0)
            {
                logger.Error("No arguments were given");
                return;
            }

            int number;

            if(!File.Exists(path) || !File.Exists(strExeFilePath))
            {
                logger.Error("File path doesn't exist");
                return;
            }
            if(int.TryParse(args[0], out number))
            {
                Console.WriteLine(args[0]);

                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(number);
                }

                number += 1;

                if(number < maxNumber)
                {
                    string numberString = number.ToString();
                    Process.Start(strExeFilePath, numberString);
                }
                else
                {
                    logger.Information("Done");
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine("Done");
                    }
                }
            }
            else
            {
                logger.Warning("Argument must be an integer!");
                return;
            }
        }
    }
}