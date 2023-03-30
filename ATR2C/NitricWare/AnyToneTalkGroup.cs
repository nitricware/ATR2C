using CsvHelper.Configuration;

namespace ATCSVCreator.NitricWare; 

public class AnyToneTalkGroup {
    public string ID { get; set; }
    public string RadioID { get; set; }
    public string Name { get; set; }
    public string CallType { get; set; } = "Group Call";
    public string CallAlert { get; set; } = "None";
}

public sealed class AnyToneTalkGroupClassMap : ClassMap<AnyToneTalkGroup>
{
    public AnyToneTalkGroupClassMap()
    {
        Map(m => m.ID).Name("No.");
        Map(m => m.RadioID).Name("Radio ID");
        Map(m => m.Name);
        Map(m => m.CallType).Name("Call Type");
        Map(m => m.CallAlert).Name("Call Alert");
    }
}