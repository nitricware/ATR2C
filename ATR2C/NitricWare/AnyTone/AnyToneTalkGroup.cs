using CsvHelper.Configuration.Attributes;

namespace ATCSVCreator.NitricWare.AnyTone; 

public class AnyToneTalkGroup {
    [Name("No.")]
    public string Id { get; set; }
    [Name("Radio ID")]
    public string RadioId { get; set; }
    public string Name { get; set; }
    [Name("Call Type")]
    public string CallType { get; set; } = "Group Call";
    [Name("Call Alert")]
    public string CallAlert { get; set; } = "None";
}