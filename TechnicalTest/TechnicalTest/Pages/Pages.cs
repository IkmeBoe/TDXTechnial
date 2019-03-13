using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using Dapper;
using TechnicalTest.FileLoad;
using TechnicalTest.Helpers;
using TechnicalTest.Inventory;
using TechnicalTest.Log;

namespace TechnicalTest.Pages
{
    internal class Pages
    {
        readonly DbHelper _dbHelper = new DbHelper();
        readonly FileHelper _fileHelper = new FileHelper();
        private readonly HtmlHelper _htmlHelper;
        readonly string _connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private readonly FileValidation _fileValidation = new FileValidation();

        public Pages()
        {
            _htmlHelper  = new HtmlHelper(_dbHelper);
        }

        internal void SetupClickEventsForPage(string pageTitle, HtmlElement activeElement, HtmlDocument currentDocument)
        {
            switch (pageTitle)
            {
                case "default":
                    break;
                case "items":
                    break;
                case "uploadfile":
                    if (activeElement.Id != null && activeElement.Id.Equals("uploaded_file"))
                    {
                        HtmlElement filePath = currentDocument.GetElementById("filepath");

                        if (filePath != null && filePath.InnerText != null)
                        {
                            // File Ext and file name
                            var file = _fileHelper.GetFile(filePath.InnerText);
                            List<Item> validItems = new List<Item>();
                            if (_fileValidation.ValidateFile(file, _fileHelper, validItems))
                            {
                                var fileName = file.Filename.Split('\\').Last();
                                 _dbHelper.AddFileToDatabase(_connectionString, $"{_fileHelper.GetFileName(filePath.InnerText)}", file.FileBytes);
                                filePath.InnerText = "";

                                MessageBox.Show("File Successfully Uploaded");

                                // GetFileId
                                foreach (var item in validItems)
                                {
                                    _dbHelper.AddItemToDatabase(item, fileName);
                                }
                                
                                DisplayLoadedItems(currentDocument);
                            }
                            else
                            {
                                MessageBox.Show("Invalid File, Please see logs");
                                filePath.InnerText = null;
                            }
                          
                        }
                        
                    }
                    break;
                case "logfile":
                    if (activeElement.Id != null && activeElement.Id.Equals("fetchdata"))
                    {
                        GetLogFiles(currentDocument);
                    }

                    break;
            }
        }

        private void DisplayLoadedItems(HtmlDocument currentDocument)
        {
            List<Item> items = _dbHelper.GetDataFromDatabase<Item>("SELECT * FROM Items");

            _htmlHelper.BuildItemTable(currentDocument, items);
        }


        private void GetUploadedItems(HtmlDocument currentDocument, string filename)
        {
            List<Item> items = _dbHelper.GetDataFromDatabase<Item>(
                $"SELECT * FROM Items Where FileId = (SELECT top 1 FileId From FileStore WHERE Filename = '{filename}')");

        }
        private void GetLogFiles(HtmlDocument currentDocument)
        {
            List<LogItem> logItems = _dbHelper.GetDataFromDatabase<LogItem>("SELECT * FROM LogItems");

            _htmlHelper.BuildLogTable(currentDocument, logItems);
        }
    }
}
