using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest.Log
{
    public class LogErrors
    {
        readonly string _connectionString;
        readonly Log _log;

        public LogErrors()
        {
            _log = new Log();
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        internal void ConfigurationError(Exception e)
        {
            _log.InsertLog(_connectionString, new LogItem
            {
                ErrorMessage = e.Message,
                Filename = null,
                LoggedDate = DateTime.Now,
                LogId = Guid.NewGuid(),
                RowNumber = null
            });
        }

        internal void NoTitleTag()
        {
            _log.InsertLog(_connectionString, new LogItem
            {
                ErrorMessage = "An Error occured with the html page, no title tag was found",
                Filename = null,
                LoggedDate = DateTime.Now,
                LogId = Guid.NewGuid(),
                RowNumber = null
            });
        }
        internal void ErrorWithHtmlPageLoading()
        {
            _log.InsertLog(_connectionString, new LogItem
            {
                ErrorMessage = "An Error occured whilst loading the html page",
                Filename = null,
                LoggedDate = DateTime.Now,
                LogId = Guid.NewGuid(),
                RowNumber = null
            });
            throw new FileNotFoundException();
        }
    }
}
