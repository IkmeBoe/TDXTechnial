using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTest.Interface;
using TechnicalTest.Log;

namespace TechnicalTestUnitTests
{
    class FakeLog: ILog
    {
        public void InsertLog(string connectionString, LogItem itemToInsert)
        {
            Console.WriteLine("");
        }
    }
}
