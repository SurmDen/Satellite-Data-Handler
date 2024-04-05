using SQLite_Parser.Models;

namespace SQLite_Parser.Interfaces
{
    public interface IExcelParser
    {
        public void UpdateExcelDocument(string path, Sattellite updatedSattelite);

        public List<Sattellite> GetSattelitesWithParamsFromExcel(string path);

        public void UpdateDataToSattelite(Sattellite sattellite, List<SignalModel> models, string path);
    }
}
