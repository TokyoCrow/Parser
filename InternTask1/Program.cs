using InternTask1.Classes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace InternTask1
{
    class Program
    {
        private static bool isInProgress = false;
        private const int footerLine = 100;

        static int choose = 0;
        static void Main(string[] args)
        {
            do
            {
                ShowConfig();
                Console.WriteLine("Select the action:");
                Console.WriteLine("1 - Start the programm.");
                Console.WriteLine("2 - Make changes to the configuration file.");
                Console.WriteLine("3 - Close console.");
                if (int.TryParse(Console.ReadLine(), out choose))
                    switch (choose)
                    {
                        case 1:
                            {
                                Console.Write("Please, enter website url:");
                                string url = Console.ReadLine();
                                isInProgress = true;
                                InProgress();
                                var parser = new Parser(url);
                                var csvFile = new CsvFile();
                                var inbox = new InboxMailRU();
                                csvFile.Save(parser.ShieldedUrls());
                                inbox.Send(parser.Errors);
                                isInProgress = false;
                                Console.Clear();
                                Console.WriteLine("Complete!");
                                Thread.Sleep(2000);
                                break;
                            }
                        case 2:
                            {
                                ConfigurationSetting();
                                break;
                            }
                        case 3: break;
                        default:
                            {
                                Console.WriteLine("Please, select an action.");
                                Thread.Sleep(1000);
                                Console.Clear();
                                break;
                            }
                    }
                else
                    Console.WriteLine("Enter the number, please.");
            } while (choose != 3);
        }


        static void ConfigurationSetting()
        {
            do
            {
                ShowConfig();
                Console.WriteLine("Select the action:");
                Console.WriteLine("1 - Change nessing degree.");
                Console.WriteLine("2 - Change report file name.");
                Console.WriteLine("3 - Change report file path.");
                Console.WriteLine("4 - Change email.");
                Console.WriteLine("5 - Close cofiguration file menu.");
                if (int.TryParse(Console.ReadLine(), out choose))
                    switch (choose)
                    {
                        case 1:
                            {
                                Console.WriteLine("Pealse set nessing degree. The degree must be a number greater than 0.");
                                if (byte.TryParse(Console.ReadLine(), out byte degree))
                                    Configuration.NestingDegree = degree;
                                else
                                    Console.WriteLine("The degree must be a number greater than 0.");
                                Thread.Sleep(1000);
                                break;
                            }
                        case 2:
                            {
                                Console.WriteLine("Pealse set report file name.");
                                Configuration.CsvFileName = Console.ReadLine();
                                Thread.Sleep(1000);
                                break;
                            }
                        case 3:
                            {
                                Console.WriteLine("Pealse set report file path.");
                                Configuration.CsvFilePath = Console.ReadLine();
                                Thread.Sleep(1000);
                                break;
                            }
                        case 4:
                            {
                                Console.WriteLine("Pealse set email.");
                                Configuration.ExpEmail = Console.ReadLine();
                                Thread.Sleep(1000);
                                break;
                            }
                        case 5: break;
                        default:
                            {
                                Console.WriteLine("Please, select an action.");
                                Thread.Sleep(1000);
                                Console.Clear();
                                break;
                            }
                    }
                else
                    Console.WriteLine("Enter the number, please.");
            } while (choose != 5);
        }

        static void ShowConfig()
        {
            Console.Clear();
            Console.WriteLine($"{new string('=', footerLine / 2 - 4)}Settings{new string('=', footerLine / 2 - 4)}");
            Console.WriteLine(Configuration.GetInfo());
            Console.WriteLine(new string('=', footerLine));
        }

        static async void InProgress() =>
            await Task.Run(() =>
            {
                int i = 1;
                while (isInProgress)
                {
                    Console.Clear();
                    Console.WriteLine($"Processing{new string('.', i)}");
                    i++;
                    if (i > 3)
                        i = 1;
                    Thread.Sleep(200);
                }
            });
    }
}
