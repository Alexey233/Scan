using Scan.Service;


namespace Scan.Cmd
{
    class Program
    {
        /// <summary>
        /// Запуск нашего проекта
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Commands:\n" +
                              "1. scan_util 'directory' \n" +
                              "2. exit \n");

            var input = Console.ReadLine()?.ToLower();

            while (input != "exit")
            {
                ScannerUtils(input);

                input = Console.ReadLine()?.ToLower();
            }

        }

        /// <summary>
        /// Работа с вводом в консоль от пользователя, определение запроса
        /// </summary>
        private static void ScannerUtils(string input)
        {
            try
            {
                var attribute = input.Split(" ")[1];

                if (!string.IsNullOrEmpty(attribute) && input.Contains("scan_util"))
                {
                    CreateMalwareScanner(attribute);
                }
                else
                {
                    Console.WriteLine("Try again!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Создание запроса на сканирование 
        /// </summary>
        private static void CreateMalwareScanner(string directory)
        {
            ScanService scanService = new ScanService();
            var scanResult = scanService.StartScan(directory);
            Console.WriteLine("\n" + scanResult);
        }
    }
}
