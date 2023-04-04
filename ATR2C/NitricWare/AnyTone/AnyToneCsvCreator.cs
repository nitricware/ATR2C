using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

namespace ATCSVCreator.NitricWare.AnyTone; 

public class AnyToneCsvCreator {
    public List<AnyToneZone>? Zones;
    public List<AnyToneChannel>? Channels;
    public List<AnyToneTalkGroup>? TalkGroups;
    public List<AnyToneAnalogContact>? AnalogAddressBook;
    public List<AnyToneScanList>? ScanLists;

    public string? DefaultsDir;
    public string? ExportDir;
    
    private readonly CsvConfiguration _config = new(CultureInfo.InvariantCulture)
    {
        ShouldQuote = _ => true
    };

    public void CreateAllFiles() {
        CreateZonesFile();
        CreateChannelsFile();
        CreateAnalogAddressBookFile();
        CreateTalkGroupsFile();
        CreateScanListFile();
        
        // TODO: implement bonus CHIRP export of channels only as CHIRP does not support zones
        
        MergeDefaults();
    }

    private void CreateZonesFile() {
        using var writer = new StreamWriter(
            Path.Combine(
                ExportDir ?? Path.Combine(Directory.GetCurrentDirectory(),"export"),
                "Zone.csv"
            ));
        using var csv = new CsvWriter(writer, _config);
        csv.WriteRecords(Zones);
    }

    private void CreateChannelsFile() {
        using var writer = new StreamWriter(
            Path.Combine(
                ExportDir ?? Path.Combine(Directory.GetCurrentDirectory(),"export"),
                "Channel.csv"
            ));
        using var csv = new CsvWriter(writer, _config);
        csv.WriteRecords(Channels);
    }

    private void CreateTalkGroupsFile() {
        using var writer = new StreamWriter(
            Path.Combine(
                ExportDir ?? Path.Combine(Directory.GetCurrentDirectory(),"export"),
                "TalkGroups.csv"
            ));
        using var csv = new CsvWriter(writer, _config);
        csv.WriteRecords(TalkGroups);
    }
    
    private void CreateAnalogAddressBookFile() {
        using var writer = new StreamWriter(
            Path.Combine(
                ExportDir ?? Path.Combine(Directory.GetCurrentDirectory(),"export"),
                "AnalogAddressBook.csv"
            ));
        using var csv = new CsvWriter(writer, _config);
        csv.WriteRecords(AnalogAddressBook);
    }

    private void CreateScanListFile() {
        using var writer = new StreamWriter(
            Path.Combine(
                ExportDir ?? Path.Combine(Directory.GetCurrentDirectory(),"export"),
                "ScanList.csv"
            ));
        using var csv = new CsvWriter(writer, _config);
        csv.WriteRecords(ScanLists);
    }

    private void MergeDefaults() {
        MergeFile("Channel.csv");
        MergeFile("Zone.csv");
        MergeFile("TalkGroups.csv");
        MergeFile("ScanList.csv");
    }

    private void MergeFile(string file) {
        if (!File.Exists(
                Path.Combine(
                    DefaultsDir ?? Path.Combine(Directory.GetCurrentDirectory(),"defaults"),file)
                )
            ) {
            return;
        }
        
        var generatedFile = File.Open(
            Path.Combine(
                ExportDir ?? Path.Combine(Directory.GetCurrentDirectory(),"export"),
                file
                ), 
            FileMode.Append ,FileAccess.Write);
        var defaultsFile = File.OpenRead(
            Path.Combine(
                DefaultsDir ?? Path.Combine(Directory.GetCurrentDirectory(),"defaults"),
                file)
            );
        
        defaultsFile.CopyTo(generatedFile);
        generatedFile.Close();
        defaultsFile.Close();
    }
}