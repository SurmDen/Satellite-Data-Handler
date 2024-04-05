namespace SQLite_Parser.Models
{
    public class Sattellite
    {
        //C
        public string SatteliteName { get; set; }

        //B
        public double UnderlyingPoint { get; set; }

        //A
        public string Way { get; set; }

        public List<ExcelSignalModel> Signals { get; set; }

        public override string ToString()
        {
            return $"{SatteliteName}:   П/Т={UnderlyingPoint}, П={Way}";
        }
    }
}
