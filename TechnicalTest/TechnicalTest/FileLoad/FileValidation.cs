using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using TechnicalTest.Enums;
using TechnicalTest.Helpers;
using TechnicalTest.Interface;
using TechnicalTest.Inventory;
using TechnicalTest.Log;
using TechnicalTest.Validators;

namespace TechnicalTest.FileLoad
{
    internal class FileValidation
    {
        ILog _log;
        private string _connectionString;
        DbHelper _dbHelper = new DbHelper();
        public FileValidation()
        {
            _log = new Log.Log();
            _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public FileValidation(ILog log, string connectionString)
        {
            _log = log;
            _connectionString = connectionString;
        }
        
        internal bool ValidateFile(LoadedFile file, FileHelper fileHelper, List<Item> validItems)
        {
            string[] fileLines = fileHelper.GetFileLines(file.FileBytes).ToArray();
            int maxNumberOfFieldsPerRow = typeof(Item).GetProperties().Length;
            for (int line = 1; line < fileLines.Count(); line++)
            {
                var fields = fileLines[line].Split(',');
                if (fields.Length != maxNumberOfFieldsPerRow)
                {
                    LogItem failedItem = new LogItem
                    {
                        ErrorMessage = "Incorrect number of items in row",
                        Filename = file.Filename,
                        LoggedDate = DateTime.Now,
                        LogId = Guid.NewGuid(),
                        RowNumber = line
                    };

                    _log.InsertLog(_connectionString, failedItem);
                    
                    return false;
                }

                var item = new Item();
                Guid.TryParse(fields[0], out var lineGuid);
                Enum.TryParse<PartType>(fields[2], true, out var itemPart);

                var validator = new ItemValidator();
                item.PartId = lineGuid;
                item.PartName = fields[1];
                item.PartType = itemPart;
                item.Quantity = int.Parse(fields[3]);
                item.DateAdded = DateTime.Parse(fields[4]);
                item.PartLength = double.Parse(fields[5]);
                    
                var results = validator.Validate(item);

                if (!results.IsValid)
                {
                    foreach (var failure in results.Errors)
                    {
                        LogItem failedItem = new LogItem
                        {
                            ErrorMessage = failure.ErrorMessage,
                            Filename = file.Filename,
                            LoggedDate = DateTime.Now,
                            LogId = Guid.NewGuid(),
                            RowNumber = line
                        };

                        _log.InsertLog(_connectionString, failedItem);
                    }

                    return false;
                }

                validItems.Add(item);

            }

            return true;
        }

        
    }
}