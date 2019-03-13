using System;

namespace TechnicalTest.Log
{
    internal class LogItem
    {
        public Guid LogId { get; set; }
        public string ErrorMessage { get; set; }
        public string Filename { get; set; }
        public long? RowNumber { get; set; }
        public DateTime LoggedDate { get; set; }
        
    }
}
