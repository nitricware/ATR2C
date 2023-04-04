using System.Globalization;
using ATCSVCreator.NitricWare.ENUM;
using ATCSVCreator.NitricWare.Helper;
using CsvHelper;

namespace ATCSVCreator.NitricWare.TalkGroups; 

public class TalkGroupFileHandler {
    public List<TalkGroup> TalkGroupList;

    public TalkGroupFileHandler(string? path) {
        if (path == null) {
            throw new NullReferenceException();
        }
        if (!File.Exists(path)) {
            throw new FileNotFoundException();
        }

        using var reader = new StreamReader(path);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        csv.Context.TypeConverterCache.AddConverter<DmrNetwork>(new CsvDmrNetworkConverter());
        TalkGroupList = csv.GetRecords<TalkGroup>().ToList();
    }
}