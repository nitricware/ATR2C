using System.Globalization;
using ATCSVCreator.NitricWare.ENUM;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace ATCSVCreator.NitricWare.Helper; 

public class OevsvCsvDoubleConverter : DefaultTypeConverter {
    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData) {
        return text is "" or "" ? 0.0 : Convert.ToDouble(
            text,
            CultureInfo.InvariantCulture);
    }

    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData) {
        return value.ToString() ?? "0.0";
    }
}