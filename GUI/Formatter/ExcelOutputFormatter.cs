using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;

namespace GUI.Formatter
{
    public class ExcelOutputFormatter : OutputFormatter
    {
        public ExcelOutputFormatter()
        {
            SupportedMediaTypes.Add(
                MediaTypeHeaderValue.Parse(MyContentTypes.XLSX));
        }

        protected override bool CanWriteType(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            
            MemoryStream excelStream = createExcelFile(context.Object as IEnumerable<dynamic>);

            HttpResponse response = context.HttpContext.Response;

            response.ContentLength = excelStream.Length;
            return response.Body.WriteAsync(excelStream.ToArray()).AsTask();
        }

        public override void WriteResponseHeaders(OutputFormatterWriteContext context)
        {
            if (context?.Object == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var fileName = (context.Object as IEnumerable<dynamic>)?.GetType().GetGenericArguments()[0].Name;

            context.HttpContext.Response.Headers["Content-Disposition"] =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName + ".xlsx"
                }.ToString();
            context.HttpContext.Response.ContentType = MyContentTypes.XLSX;
        }

        private MemoryStream createExcelFile(IEnumerable<dynamic> data)
        {
            MemoryStream memoryStream = new MemoryStream();

            using (SpreadsheetDocument spreadsheetDocument =
                SpreadsheetDocument.Create(memoryStream, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = spreadsheetDocument.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

                SheetData sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                workbookPart.Workbook.AppendChild(new Sheets());

                Sheet sheet = new Sheet
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "Arkusz0"
                };

                List<PropertyInfo> propertyList =
                    new List<PropertyInfo>(data.ToList().First().GetType().GetProperties());

                Row headerRow = _buildHeaderRow(propertyList);

                sheetData.AppendChild(headerRow);

                _fillBodyWithData(sheetData, propertyList, data);

                workbookPart.Workbook.Sheets?.AppendChild(sheet);
                workbookPart.Workbook.Save();
            }

            return memoryStream;
        }

        private Row _buildHeaderRow(List<PropertyInfo> propertyList)
        {
            Row headerRow = new Row();

            foreach (PropertyInfo _property in propertyList)
            {
                headerRow.AppendChild(
                    getCell(_property.Name)
                );
            }

            return headerRow;
        }

        private void _fillBodyWithData(SheetData sheetData, List<PropertyInfo> propertyList, IEnumerable<dynamic> data)
        {
            foreach (object _value in data)
            {
                Row row = new Row();

                foreach (PropertyInfo property in propertyList)
                {
                    string? propertyValue = property.GetValue(_value, null).ToString();
                    row.AppendChild(
                        getCell(propertyValue)
                    );
                }

                sheetData.AppendChild(row);
            }
        }

        private Cell getCell(string text)
        {
            Cell cell = new Cell
            {
                DataType = CellValues.InlineString
            };

            InlineString inlineString = new InlineString();
            inlineString.AppendChild(new Text(text));

            cell.AppendChild(inlineString);
            return cell;
        }
    }
}