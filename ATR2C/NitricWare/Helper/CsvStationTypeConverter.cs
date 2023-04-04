using ATCSVCreator.NitricWare.ENUM;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace ATCSVCreator.NitricWare.Helper; 

public class CsvStationTypeConverter : DefaultTypeConverter {
    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData) {
        switch (text) {
            case "repeater_voice":
                return StationType.RepeaterVoice;
            case "repear_voice":
                return StationType.RepeaterVoice;
            case "digipeater":
                return StationType.Digipeater;
            default:
                return StationType.Other;
        }
    }

    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData) {
        switch (value) {
            case StationType.RepeaterVoice:
                return "repeater_voice";
            case StationType.Digipeater:
                return "digipeater";
            default:
                return "other";
        }
    }
}