using System.Globalization;
using ATCSVCreator.NitricWare.ENUM;
using ATCSVCreator.NitricWare.Helper;
using CsvHelper;

namespace ATCSVCreator.NitricWare.Oevsv;

public class OevsvRepeaterFileHandler {
    public List<OevsvRepeater> OevsvRepeaters;

    public OevsvRepeaterFileHandler(string? path) {
        if (path == null) {
            throw new NullReferenceException("Path to repeaters.csv was empty");
        }

        if (!File.Exists(path)) {
            throw new FileNotFoundException();
        }

        using var reader = new StreamReader(path);
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture)) {
            csv.Context.TypeConverterCache.RemoveConverter<bool>();
            csv.Context.TypeConverterCache.AddConverter<bool>(new CsvBoolConverter());
            csv.Context.TypeConverterCache.AddConverter<RadioBand>(new CsvBandConverter());
            csv.Context.TypeConverterCache.AddConverter<StationType>(new CsvStationTypeConverter());
            csv.Context.TypeConverterCache.AddConverter<RepeaterStatus>(new CsvRepeaterStatusConverter());
            csv.Context.TypeConverterCache.RemoveConverter<double>();
            csv.Context.TypeConverterCache.AddConverter<double>(new OevsvCsvDoubleConverter());
            OevsvRepeaters = csv.GetRecords<OevsvRepeater>().ToList();
        }
    }
}