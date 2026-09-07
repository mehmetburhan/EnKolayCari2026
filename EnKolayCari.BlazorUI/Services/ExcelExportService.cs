using ClosedXML.Excel;

namespace EnKolayCari.BlazorUI.Services;

/// <summary>Builds .xlsx workbooks for the data grid export feature.</summary>
public sealed class ExcelExportService
{
    public byte[] Export(string sheetName, IReadOnlyList<string> headers, IEnumerable<IReadOnlyList<string?>> rows)
    {
        using var workbook = new XLWorkbook();
        var sheet = workbook.Worksheets.Add(SanitizeSheetName(sheetName));

        for (var col = 0; col < headers.Count; col++)
        {
            var cell = sheet.Cell(1, col + 1);
            cell.Value = headers[col];
            cell.Style.Font.Bold = true;
            cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#EFF6FF");
        }

        var rowIndex = 2;
        foreach (var row in rows)
        {
            for (var col = 0; col < row.Count; col++)
            {
                sheet.Cell(rowIndex, col + 1).Value = row[col] ?? string.Empty;
            }

            rowIndex++;
        }

        if (rowIndex > 2)
        {
            sheet.RangeUsed()?.SetAutoFilter();
        }

        sheet.SheetView.FreezeRows(1);
        sheet.Columns().AdjustToContents();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    private static string SanitizeSheetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return "Sayfa1";
        }

        var invalid = new[] { '\\', '/', '?', '*', '[', ']', ':' };
        var sanitized = new string(name.Where(c => !invalid.Contains(c)).ToArray());
        return sanitized.Length > 31 ? sanitized[..31] : sanitized;
    }
}
