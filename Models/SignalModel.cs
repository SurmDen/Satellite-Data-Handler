namespace SQLite_Parser.Models
{
	public class SignalModel
	{
        public long Id { get; set; }

        public double? FrequencyMHz { get; set; }

        public double? BandWidthMHZ { get; set; }

        public double? Level { get; set; }

        public double? SNR { get; set; }

        public string? StandartType { get; set; }

        public string? ModeType { get; set; }

        //KHz
        public double? SpeedMBod { get; set; }

        public DateTime TimeFirst { get; set; }

        public DateTime TimeLast { get; set; }

        public bool? Recorder { get; set; }

        public string? FileName { get; set; }

        public double? FileSize { get; set; }

        public int? RecognitionType { get; set; }

        public int? StateTA { get; set; }

        public long? TechnicalAnalisysResult { get; set; }

        public double? SignalStrenght { get; set; }

        public bool IsSignalCompared { get; set; } = false;
    }
}
