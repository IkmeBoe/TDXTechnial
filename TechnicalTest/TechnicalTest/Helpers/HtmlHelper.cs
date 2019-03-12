using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using TechnicalTest.Enums;
using TechnicalTest.Log;

namespace TechnicalTest.Helpers
{
    class HtmlHelper
    {

        private DbHelper _dbHelper;

        public HtmlHelper(DbHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public void BuildLogTable(HtmlDocument currentDocument, List<LogItem> items)
        {
            var divContainer = currentDocument.GetElementById("container");

            if (divContainer == null)
            {
                divContainer = CreateDivContainer(currentDocument);

            }
            else
            {
                divContainer.InnerText = "";
            }

            var currentTable = currentDocument.GetElementById("display");

            var htmlTable2 = currentTable != null ? RefreshTableData(currentDocument, items) : BuildTableData(currentDocument, items);

            if (divContainer == null)
            {
                return;
            }
            divContainer.AppendChild(htmlTable2);
            if (currentDocument.Body != null)
            {
                currentDocument.Body.AppendChild(divContainer);
            }

            currentDocument.InvokeScript("Pagenation");
        }

        private HtmlElement BuildTableData(HtmlDocument currentDocument, List<LogItem> items)
        {
            var resultsTable = currentDocument.CreateElement("table");
            var headerTr = currentDocument.CreateElement("tr");
            var tbody = currentDocument.CreateElement("tbody");
            if (resultsTable == null)
            {
                return null;
            }
            resultsTable.Id = "display";
            resultsTable.SetAttribute("class", "table table-bordered table-sm");
            resultsTable.SetAttribute("cellspacing", "0");
            var thead = currentDocument.CreateElement("thead");

            var columns = _dbHelper.GetDataFromDatabase<DbTableInformation>("sp_columns LogItems");

            var th = currentDocument.CreateElement("th");
            foreach (var column in columns)
            {
                if (th == null) continue;
                th.InnerText = column.Column_Name;
                th.SetAttribute("class", "th-sm");
                th.SetAttribute("scope", "col");
                if (headerTr != null) headerTr.AppendChild(th);
            }


            if (thead != null)
            {
                if (headerTr != null)
                {
                    thead.AppendChild(headerTr);
                }
                resultsTable.AppendChild(thead);
            }

            if (tbody == null)
            {
                return resultsTable;
            }

            tbody.Id = "body";

            BuildLogFileTableBody(currentDocument, items, tbody);
            resultsTable.AppendChild(tbody);

            return resultsTable;

        }

        private static void BuildLogFileTableBody(HtmlDocument currentDocument, List<LogItem> items, HtmlElement tbody)
        {
            HtmlElement logId = currentDocument.CreateElement(ElementType.Td.ToString());
            HtmlElement errorMessage = currentDocument.CreateElement(ElementType.Td.ToString());
            HtmlElement fileName = currentDocument.CreateElement(ElementType.Td.ToString());
            HtmlElement rowNumber = currentDocument.CreateElement(ElementType.Td.ToString());
            HtmlElement loggedDate = currentDocument.CreateElement(ElementType.Td.ToString());
            foreach (var row in items)
            {
                var tr = currentDocument.CreateElement("tr");
                if (tr == null)
                {
                    continue;
                }
                if (logId != null)
                {
                    logId.InnerText = row.LogId.ToString();
                    tr.AppendChild(logId);
                }

                if (errorMessage != null)
                {
                    errorMessage.InnerText = row.ErrorMessage;
                    tr.AppendChild(errorMessage);
                }

                if (fileName != null)
                {
                    fileName.InnerText = row.Filename;
                    tr.AppendChild(fileName);
                }

                if (rowNumber != null)
                {
                    rowNumber.InnerText = row.RowNumber.ToString();
                    tr.AppendChild(rowNumber);
                }

                if (loggedDate != null)
                {
                    loggedDate.InnerText = row.LoggedDate.ToString(CultureInfo.CurrentCulture);
                    tr.AppendChild(loggedDate);
                }
                
                tbody.AppendChild(tr);
            }
        }

        private static HtmlElement RefreshTableData(HtmlDocument currentDocument, List<LogItem> items)
        {
            var resultsTable = currentDocument.GetElementById("display");

            var tbody = currentDocument.GetElementById("body");
            if (tbody != null)
            {
                tbody.InnerHtml = "";
            }
            tbody = currentDocument.CreateElement("tbody");
            if (tbody == null)
            {
                return resultsTable;
            }
            tbody.Id = "body";
            BuildLogFileTableBody(currentDocument, items, tbody);
            if (resultsTable != null)
            {
                resultsTable.AppendChild(tbody);

                return resultsTable;
            }

            return null;
        }

        private static HtmlElement CreateDivContainer(HtmlDocument currentDocument)
        {
            var divContainer = currentDocument.CreateElement("div");
            if (divContainer == null)
            {
                return null;
            }

            divContainer.Id = "container";
            divContainer.SetAttribute("class", "container");
            divContainer.SetAttribute("width", "100%");
            return divContainer;

        }


        public void AddTablePagenation(HtmlDocument currentDocument, string tableName)
        {
            currentDocument.InvokeScript("pagenation", new object[] { $"{tableName}" });
        }
    }
}
