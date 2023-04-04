using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace ATCSVCreator.NitricWare.Helper; 

public class CsvBoolConverter : DefaultTypeConverter {
    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData) {
        return text == "1";
    }

    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData) {
        return value.ToString() ?? "false";
    }
}