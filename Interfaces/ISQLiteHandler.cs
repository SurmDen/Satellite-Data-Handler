using SQLite_Parser.Models;

namespace SQLite_Parser.Interfaces
{
    public interface ISQLiteHandler
    {
        public List<SignalModel> GetData(string dbPath);
    }
}
