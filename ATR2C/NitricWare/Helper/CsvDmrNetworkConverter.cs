using ATCSVCreator.NitricWare.ENUM;
using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace ATCSVCreator.NitricWare.Helper; 

public class CsvDmrNetworkConverter : DefaultTypeConverter {
    public override object ConvertFromString(string? text, IReaderRow row, MemberMapData memberMapData) {
        switch (text) {
            case "I2":
                return DmrNetwork.I2;
            case "BM":
                return DmrNetwork.Bm;
            default:
                return DmrNetwork.No;
        }
    }

    public override string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData) {
        switch (value) {
            case DmrNetwork.Bm:
                return "BM";
            case DmrNetwork.I2:
                return "I2";
            default:
                return "NO";
        }
    }
}