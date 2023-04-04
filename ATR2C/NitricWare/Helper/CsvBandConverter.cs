using ATCSVCreator.NitricWare.ENUM;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace ATCSVCreator.NitricWare.Helper; 

public class CsvBandConverter : DefaultTypeConverter {
    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData) {
        switch (text) {
            case "70cm":
                return RadioBand.CM70;
            case "2m":
                return RadioBand.M2;
            default:
                return RadioBand.Other;
        }
    }

    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData) {
        switch (value) {
            case RadioBand.CM70:
                return "70cm";
            case RadioBand.M2:
                return "2m";
            default:
                return "other";
        }
    }
}