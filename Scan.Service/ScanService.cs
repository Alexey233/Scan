using Scan.DI;
using Scan.Settings;
using System.Diagnostics;
using System.Text;

namespace Scan.Service
{
    public class ScanService
    {
        private static Configuration _configuration;
      
        /// <summary>
        /// Создаем связь с конфигурациями из Settings
        /// </summary>
        public ScanService()
        {
            _configuration = new Configuration();
        }


        /// <summary>
        /// Сканирует конкретный файл переданный по ссылке и заполняет файлы
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="scanFileModel"></param>
        /// <returns>Возвращает собранные от сканирования данные</returns>
        private IScanFile ScanFile(string filePath)
        {
            var scanFileModel = _configuration.Container.GetInstance<IScanFile>();

            var malwareFiles = MalwareList.GetMalwareList();


            try
            {
                StreamReader file = new StreamReader(filePath);
                string ext = Path.GetExtension(filePath);
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains(malwareFiles[0].MalwareText) && ext == ".js")
                    {
                        scanFileModel.JSDetects++;
                        break;
                    }

                    if (line.Contains(malwareFiles[1].MalwareText))
                    {
                        scanFileModel.RmRfDetects++;
                        break;
                    }

                    if (line.Contains(malwareFiles[2].MalwareText))
                    {
                        scanFileModel.Rundll32Detects++;
                        break;
                    }
                }
            }
            catch
            {
                scanFileModel.Errors++;
            }

            scanFileModel.ProcessedFiles++;
            return scanFileModel;
 
        }

        /// <summary>
        /// Запускает процессы сканирования
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns>Возвращает результирующую строку классу откуда вызвана программа</returns>
        public StringBuilder StartScan(string filePath)
        {
            var scanFileModel = _configuration.Container.GetInstance<IScanFile>();

            Stopwatch myStopwatch = new System.Diagnostics.Stopwatch();
            myStopwatch.Start();
           

            var allFiles = Directory.GetFiles(filePath, "*", SearchOption.AllDirectories);

            foreach (var file in allFiles)
            {
                scanFileModel = ScanFile(file);
            }
                

            myStopwatch.Stop();
            TimeSpan ts = myStopwatch.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
            ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            scanFileModel.ExectionTime = elapsedTime;


            StringBuilder resultString = BuildResultString();

            return resultString;
        }


        /// <summary>
        /// Создает строку из предоставленной модели
        /// </summary>
        /// <param name="scanFileModel"></param>
        /// <returns>Результаты сканирования</returns>
        public StringBuilder BuildResultString()
        {
            var scanFileModel = _configuration.Container.GetInstance<IScanFile>();

            StringBuilder result = new StringBuilder("====== Scan result ======\n");
            result.Append($"Processed files: {scanFileModel.ProcessedFiles} \n");
            result.Append($"JS detects: {scanFileModel.JSDetects} \n");
            result.Append($"rm -rf detects: {scanFileModel.RmRfDetects} \n");
            result.Append($"Rundll32 detects: {scanFileModel.Rundll32Detects} \n");
            result.Append($"Errors: {scanFileModel.Errors} \n");
            result.Append($"Exection time: {scanFileModel.ExectionTime} \n");
            result.Append("=========================\n");

            return result;
        }

    }
}
