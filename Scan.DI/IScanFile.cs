namespace Scan.DI
{

    /// <summary>
    /// Интерфейс ScanFile
    /// </summary>
    public interface IScanFile
    {
        public int ProcessedFiles { get; set; }
        public int JSDetects { get; set; }
        public int RmRfDetects { get; set; }
        public int Rundll32Detects { get; set; }
        public int Errors { get; set; }
        public string ExectionTime { get; set; }
    }
}
