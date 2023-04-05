using System.Collections;
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
        ExportDir ??= Path.Combine(Directory.GetCurrentDirectory(), "export");
        DefaultsDir ??= Path.Combine(Directory.GetCurrentDirectory(), "defaults", "AnyTone AT-D878UVII Plus");

        ExportDir = Path.Combine(ExportDir, "AnyTone AT-D878UVII Plus");
        DefaultsDir = Path.Combine(DefaultsDir, "AnyTone AT-D878UVII Plus");
        Directory.CreateDirectory(ExportDir);
        
        CreateFile("Zone.csv", Zones);
        CreateFile("Channels.csv", Channels);
        CreateFile("AnalogAddressBook.csv", AnalogAddressBook);
        CreateFile("Talkgroups.csv", TalkGroups);
        CreateFile("Scanlist.csv", ScanLists);

        MergeDefaults();
    }

    private void CreateFile(string filename, IEnumerable data) {
        using var writer = new StreamWriter(
            Path.Combine(ExportDir, filename));
        using var csv = new CsvWriter(writer, _config);
        csv.WriteRecords(data);
    }

    private void MergeDefaults() {
        MergeFile("Channel.csv");
        MergeFile("Zone.csv");
        MergeFile("TalkGroups.csv");
        MergeFile("ScanList.csv");
    }

    private void MergeFile(string file) {
        if (!File.Exists(
                Path.Combine(DefaultsDir,file))
            ) {
            return;
        }
        
        var generatedFile = File.Open(
            Path.Combine(ExportDir, file), 
            FileMode.Append ,FileAccess.Write);
        var defaultsFile = File.OpenRead(
            Path.Combine(DefaultsDir, file)
            );
        
        defaultsFile.CopyTo(generatedFile);
        generatedFile.Close();
        defaultsFile.Close();
    }
}