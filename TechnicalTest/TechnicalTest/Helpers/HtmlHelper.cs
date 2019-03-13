using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using TechnicalTest.Enums;
using TechnicalTest.Inventory;
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

            var htmlTable2 = currentTable != null ? RefreshTableData(currentDocument, items) : BuildLogTableData(currentDocument, items);

            if (divContainer == null)
            {
                return;
            }
            divContainer.AppendChild(htmlTable2);
            if (currentDocument.Body != null)
            {
                currentDocument.Body.AppendChild(divContainer);
                currentDocument.InvokeScript("Pagenation");
            }

           
        }

        public void BuildItemTable(HtmlDocument currentDocument, List<Item> items)
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

            var htmlTable2 = currentTable != null ? RefreshItemTableData(currentDocument, items) : BuildItemTableData(currentDocument, items);

            if (divContainer == null)
            {
                return;
            }

            if (htmlTable2 != null)
            {
                divContainer.AppendChild(htmlTable2);
                if (currentDocument.Body != null)
                {
                    currentDocument.Body.AppendChild(divContainer);
                    currentDocument.InvokeScript("Pagenation");
                }

                
            }
        }

        private HtmlElement BuildItemTableData(HtmlDocument currentDocument, List<Item> items)
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

            var columns = _dbHelper.GetDataFromDatabase<DbTableInformation>("sp_columns Items");


            foreach (var column in columns)
            {
                if (column.Column_Name != "FileId" && column.Column_Name != "Id")
                {
                    var th = currentDocument.CreateElement("th");
                    if (th == null)
                    {
                        continue;
                    }

                    th.InnerText = column.Column_Name;
                    th.SetAttribute("class", "th-sm");
                    th.SetAttribute("scope", "col");
                    if (headerTr != null)
                    {
                        headerTr.AppendChild(th);
                    }
                }

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

            

            tbody = BuildItemFileTableBody(currentDocument, items, tbody);
            tbody.Id = "body";
            resultsTable.AppendChild(tbody);

            return resultsTable;

        }
        private HtmlElement BuildLogTableData(HtmlDocument currentDocument, List<LogItem> items)
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

            
            foreach (var column in columns)
            {
                var th = currentDocument.CreateElement("th");
                if (th == null)
                {
                    continue;
                }

                th.InnerText = column.Column_Name;
                th.SetAttribute("class", "th-sm");
                th.SetAttribute("scope", "col");
                if (headerTr != null)
                {
                    headerTr.AppendChild(th);
                }
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

        private static HtmlElement BuildItemFileTableBody(HtmlDocument currentDocument, List<Item> items, HtmlElement tbody)
        {
            

            foreach (var row in items)
            {
                HtmlElement partId = currentDocument.CreateElement(ElementType.Td.ToString());
                HtmlElement partName = currentDocument.CreateElement(ElementType.Td.ToString());
                HtmlElement partType = currentDocument.CreateElement(ElementType.Td.ToString());
                HtmlElement partLength = currentDocument.CreateElement(ElementType.Td.ToString());
                HtmlElement quantity = currentDocument.CreateElement(ElementType.Td.ToString());
                HtmlElement dateAdded = currentDocument.CreateElement(ElementType.Td.ToString());

                var tr = currentDocument.CreateElement("tr");
                if (tr == null)
                {
                    continue;
                }
                if (partId != null)
                {
                    partId.InnerText = row.PartId.ToString();
                    tr.AppendChild(partId);
                }

                if (partName != null)
                {
                    partName.InnerText = row.PartName;
                    tr.AppendChild(partName);
                }

                if (partType != null)
                {
                    partType.InnerText = row.PartType.ToString();
                    tr.AppendChild(partType);
                }

                if (partLength != null)
                {
                    partLength.InnerText = row.PartLength.ToString();
                    tr.AppendChild(partLength);
                }

                if (quantity != null)
                {
                    quantity.InnerText = row.Quantity.ToString();
                    tr.AppendChild(quantity);
                }

                if (dateAdded != null)
                {
                    dateAdded.InnerText = row.DateAdded.ToString();
                    tr.AppendChild(dateAdded);
                }


                tbody.AppendChild(tr);
            }

            return tbody;
        }

        private static void BuildLogFileTableBody(HtmlDocument currentDocument, List<LogItem> items, HtmlElement tbody)
        {
            
            foreach (var row in items)
            {
                HtmlElement logId = currentDocument.CreateElement(ElementType.Td.ToString());
                HtmlElement errorMessage = currentDocument.CreateElement(ElementType.Td.ToString());
                HtmlElement fileName = currentDocument.CreateElement(ElementType.Td.ToString());
                HtmlElement rowNumber = currentDocument.CreateElement(ElementType.Td.ToString());
                HtmlElement loggedDate = currentDocument.CreateElement(ElementType.Td.ToString());

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
        private static HtmlElement RefreshItemTableData(HtmlDocument currentDocument, List<Item> items)
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
            
            tbody = BuildItemFileTableBody(currentDocument, items, tbody);
            tbody.Id = "body";
            if (resultsTable != null)
            {
                resultsTable.AppendChild(tbody);

                return resultsTable;
            }

            return null;
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
