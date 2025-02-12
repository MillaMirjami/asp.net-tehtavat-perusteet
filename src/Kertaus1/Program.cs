using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Serilog.Core;

namespace Problem
{
    internal class Program
    {   
            private const string path = @"/Users/s2401576/Documents/GitHub/bc_net24s_ryhma/src/part1_kertausta/Kertaus1/file.txt";
            private const string strExeFilePath = @"/Users/s2401576/Documents/GitHub/bc_net24s_ryhma/src/part1_kertausta/Kertaus1/bin/Debug/net8.0/Kertaus1";
        private static void Main(string [] args)
        {
            if(args.Length == 0)
            {
                Console.WriteLine("No arguments were given");
                return;
            }

            int number;

            if(!File.Exists(path) || !File.Exists(strExeFilePath))
            {
                Console.WriteLine("File path does not exist");
                return;
            }
            if(int.Parse(args[0]) >= 0)
            {
                Console.WriteLine(args[0]);
            }
            if(int.TryParse(args[0], out number))
            {

                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(number);
                }

                number += 1;

                if(number <= 10)
                {
                    string numberString = number.ToString();
                    Process.Start(strExeFilePath, numberString);
                }
                else
                {
                    Console.WriteLine("Done");
                    using (StreamWriter sw = File.AppendText(path))
                    {
                        sw.WriteLine("Done");
                    }
                }
            }
            else
            {
                Console.WriteLine("Argument must be an integer!");
                return;
            }
        }
    }
}