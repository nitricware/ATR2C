using CsvHelper.Configuration;

namespace ATCSVCreator.NitricWare; 

public class AnyToneAnalogContact {
    public string ID { get; set; }
    public string Number { get; set; }
    public string Name { get; set; }
}

public sealed class AnyToneAnalogContactClassMap : ClassMap<AnyToneAnalogContact>
{
    public AnyToneAnalogContactClassMap()
    {
        Map(m => m.ID).Name("No.");
        Map(m => m.Number);
        Map(m => m.Name);
    }
}