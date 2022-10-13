using Scan.DI;

namespace Scan.BL
{
    /// <summary>
    /// Реализация интерфейса
    /// </summary>
    public class ScanFileModel : IScanFile
    {
        public int ProcessedFiles { get; set; } = 0;
        public int JSDetects { get; set; } = 0;
        public int RmRfDetects { get; set; } = 0;
        public int Rundll32Detects { get; set; } = 0;
        public int Errors { get; set; } = 0;
        public string ExectionTime { get; set; }
    }
}
