using System.Collections;
using System.Globalization;
using System.Text;
using ATCSVCreator.NitricWare.CHIRP;
using ATCSVCreator.NitricWare.CPSObjects;
using CsvHelper;
using CsvHelper.Configuration;

namespace ATCSVCreator.NitricWare.Generic_5_Character; 

public class Generic5CharacterCsvCreator : ICsvCreator {
    public List<Generic5CharacterListItem>? ListItems;
    
    public string? DefaultsDir;
    public string? ExportDir;
    
    private readonly CsvConfiguration _config = new(CultureInfo.InvariantCulture)
    {
        ShouldQuote = _ => true
    };
    
    public void CreateAllFiles() {
        ExportDir ??= Path.Combine(Directory.GetCurrentDirectory(), "export", "Generic 5 Character");
        DefaultsDir ??= Path.Combine(Directory.GetCurrentDirectory(), "defaults", "Generic 5 Character");

        ExportDir = Path.Combine(ExportDir, "Generic 5 Character");
        DefaultsDir = Path.Combine(DefaultsDir, "Generic 5 Character");
        Directory.CreateDirectory(ExportDir);
        
        CreateFile("Generic-5-Character.csv", ListItems);
    }
    
    private void CreateFile(string filename, IEnumerable data) {
        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        using var writer = new StreamWriter(
            new FileStream(Path.Combine(ExportDir, filename), FileMode.Create, FileAccess.ReadWrite),
            Encoding.GetEncoding(1252));
        using var csv = new CsvWriter(writer, _config);
        csv.WriteRecords(data);
    }
}