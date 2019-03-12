using TechnicalTest.Log;

namespace TechnicalTest.Interface
{
    internal interface ILog
    {
        void InsertLog(string connectionString, LogItem itemToInsert);
    }
}