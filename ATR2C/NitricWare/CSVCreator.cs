using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace ATCSVCreator.NitricWare; 

public class CSVCreator {
    public List<AnyToneZone> Zones;
    public List<AnyToneChannel> Channels;
    public List<AnyToneTalkGroup> Talkgroups;
    public List<AnyToneAnalogContact> AnalogAddressBook;
    public List<AnyToneScanList> ScanLists;
    
    private CsvConfiguration _config = new(CultureInfo.InvariantCulture)
    {
        ShouldQuote = _ => true
    };

    public void CreateAllFiles() {
        CreateZonesFile();
        CreateChannelsFile();
        CreateAnalogAddressBookFile();
        CreateTalkgroupsFile();
        CreateScanListFile();
        
        MergeDefaults();
    }
    public void CreateZonesFile() {
        using (var writer = new StreamWriter("./export/Zone.csv"))
        using (var csv = new CsvWriter(writer, _config)) {
            csv.Context.RegisterClassMap<AnyToneZoneClassMap>();
            csv.WriteRecords(Zones);
        }
    }

    public void CreateChannelsFile() {
        using (var writer = new StreamWriter("./export/Channel.csv"))
        using (var csv = new CsvWriter(writer, _config)) {
            csv.Context.RegisterClassMap<AnyToneChannelClassMap>();
            csv.WriteRecords(Channels);
        }
    }

    public void CreateTalkgroupsFile() {
        using (var writer = new StreamWriter("./export/TalkGroups.csv"))
        using (var csv = new CsvWriter(writer, _config)) {
            csv.Context.RegisterClassMap<AnyToneTalkGroupClassMap>();
            csv.WriteRecords(Talkgroups);
        }
    }
    
    public void CreateAnalogAddressBookFile() {
        using (var writer = new StreamWriter("./export/AnalogAddressBook.csv"))
        using (var csv = new CsvWriter(writer, _config)) {
            csv.Context.RegisterClassMap<AnyToneAnalogContactClassMap>();
            csv.WriteRecords(AnalogAddressBook);
        }
    }

    public void CreateScanListFile() {
        using (var writer = new StreamWriter("./export/ScanList.csv"))
        using (var csv = new CsvWriter(writer, _config)) {
            csv.Context.RegisterClassMap<AnyToneScanListClassMap>();
            csv.WriteRecords(ScanLists);
        }
    }

    public void MergeDefaults() {
        MergeFile("Channel.csv");
        MergeFile("Zone.csv");
        MergeFile("TalkGroups.csv");
        MergeFile("ScanList.csv");
    }

    private void MergeFile(string path) {
        if (!File.Exists("./defaults/" + path)) {
            return;
        }
        
        var generatedFile = File.Open("./export/"+path, FileMode.Append ,FileAccess.Write);
        var defaultsFile = File.OpenRead("./defaults/"+path);
        
        defaultsFile.CopyTo(generatedFile);
        generatedFile.Close();
        defaultsFile.Close();
    }
}