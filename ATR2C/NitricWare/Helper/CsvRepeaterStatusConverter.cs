using ATCSVCreator.NitricWare.CPSObjects;
using ATCSVCreator.NitricWare.ENUM;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace ATCSVCreator.NitricWare.Helper; 

public class CsvRepeaterStatusConverter : DefaultTypeConverter {
    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData) {
        switch (text) {
            case "active":
                return RepeaterStatus.Active;
            case "historic":
                return RepeaterStatus.Historic;
            default:
                return RepeaterStatus.Other;
        }
    }

    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData) {
        switch (value) {
            case RepeaterStatus.Active:
                return "active";
            case RepeaterStatus.Historic:
                return "historic";
            default:
                return "other";
        }
    }
}