using System;
using System.Collections.Concurrent;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using MaterialInOut.Models;

namespace MaterialInOut.Repositories;
public class MaterialRepository : IMaterialRepository
{
    private ConcurrentDictionary<string, MaterialItem> _items = new ConcurrentDictionary<string, MaterialItem>();

    public void ImportExcelFile(byte[] bytes)
    {
        using (var ms = new MemoryStream(bytes))
        using (var spreadsheetDocument = SpreadsheetDocument.Open(ms, false))
        {
            var workbookPart = spreadsheetDocument.WorkbookPart;
            if (workbookPart == null)
            {
                throw new Exception("Did not find workbook part");
            }
            var worksheetPart = workbookPart.WorksheetParts?.FirstOrDefault();
            if (worksheetPart == null)
            {
                throw new Exception("Did not find worksheet part");
            }
            var sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault();
            if (sheetData == null)
            {
                throw new Exception("Did not find sheet data");
            }
            var sharedStringTable = workbookPart.SharedStringTablePart;
            if (sharedStringTable == null)
            {
                throw new Exception("Expected shared string table part");
            }

            var rows = sheetData.Elements<Row>();
            // skip header row
            rows = rows.Skip(1);
            foreach (Row r in rows)
            {
                ImportRow(sharedStringTable.SharedStringTable, r);
            }
        }
    }

    private void ImportRow(SharedStringTable sharedStringTable, Row r)
    {
        var cells = r.Elements<Cell>().ToList();
        if (cells == null)
        {
            return;
        }

        Cell? findCellByColumn(string columnName)
        {
            return cells.FirstOrDefault(c => c.CellReference != null &&
                c.CellReference.Value != null &&
                c.CellReference.Value.StartsWith(columnName));
        }

        var mnemonicCell = findCellByColumn("F");
        var labelCell = findCellByColumn("G");
        var eanCell = findCellByColumn("H");
        if (mnemonicCell == null || labelCell == null || eanCell == null)
        {
            return;
        }

        string? getStringContent(Cell c)
        {
            if (c.CellValue == null ||
                c.DataType == null)
            {
                return null;
            }
            if (c.DataType.Value != CellValues.SharedString)
            {
                return c.CellValue.InnerText;
            }
            return sharedStringTable.ElementAt(int.Parse(c.CellValue.InnerText)).InnerText;
        }

        string mnemonic = getStringContent(mnemonicCell) ?? String.Empty;
        string label = getStringContent(labelCell) ?? String.Empty;
        string ean = getStringContent(eanCell) ?? String.Empty;

        if (String.IsNullOrWhiteSpace(ean))
        {
            return;
        }

        _items[ean] = new MaterialItem(mnemonic, label, ean);
    }
}


