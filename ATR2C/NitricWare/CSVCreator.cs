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

    public string DefaultsDir;
    public string ExportDir;
    
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
        using (var writer = new StreamWriter(Path.Combine(ExportDir,"Zone.csv")))
        using (var csv = new CsvWriter(writer, _config)) {
            csv.Context.RegisterClassMap<AnyToneZoneClassMap>();
            csv.WriteRecords(Zones);
        }
    }

    public void CreateChannelsFile() {
        using (var writer = new StreamWriter(Path.Combine(ExportDir,"Channel.csv")))
        using (var csv = new CsvWriter(writer, _config)) {
            csv.Context.RegisterClassMap<AnyToneChannelClassMap>();
            csv.WriteRecords(Channels);
        }
    }

    public void CreateTalkgroupsFile() {
        using (var writer = new StreamWriter(Path.Combine(ExportDir,"TalkGroups.csv")))
        using (var csv = new CsvWriter(writer, _config)) {
            csv.Context.RegisterClassMap<AnyToneTalkGroupClassMap>();
            csv.WriteRecords(Talkgroups);
        }
    }
    
    public void CreateAnalogAddressBookFile() {
        using (var writer = new StreamWriter(Path.Combine(ExportDir,"AnalogAddressBook.csv")))
        using (var csv = new CsvWriter(writer, _config)) {
            csv.Context.RegisterClassMap<AnyToneAnalogContactClassMap>();
            csv.WriteRecords(AnalogAddressBook);
        }
    }

    public void CreateScanListFile() {
        using (var writer = new StreamWriter(Path.Combine(ExportDir,"ScanList.csv")))
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

    private void MergeFile(string file) {
        if (!File.Exists(Path.Combine(DefaultsDir,file))) {
            return;
        }
        
        var generatedFile = File.Open(Path.Combine(ExportDir,file), FileMode.Append ,FileAccess.Write);
        var defaultsFile = File.OpenRead(Path.Combine(DefaultsDir,file));
        
        defaultsFile.CopyTo(generatedFile);
        generatedFile.Close();
        defaultsFile.Close();
    }
}