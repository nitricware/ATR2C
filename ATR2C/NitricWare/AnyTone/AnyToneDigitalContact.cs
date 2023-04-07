using CsvHelper.Configuration.Attributes;

namespace ATCSVCreator.NitricWare.AnyTone; 

public class AnyToneDigitalContact {
    [Name("No.")]
    public string Id { get; set; }
    [Name("Radio ID")]
    public string DmrId { get; set; }
    public string Callsign { get; set; }
    public string Name { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string Remarks { get; set; }
    [Name("Call Type")]
    public string CallType { get; set; } = "Private Call";
    [Name("Call Alert")]
    public string CallAlert { get; set; } = "None";
}